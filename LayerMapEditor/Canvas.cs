using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary.Shapes;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Input;
using OriginsLib.Util;
using OriginsLib.TileEngine;

namespace LayerMapEditor
{
    public class Canvas
    {
        public const int CANVAS_WIDTH = 1100;
        public const int CANVAS_HEIGHT = 768;

        private enum Mode { Navigate, Edit }

        private MainGame gameRef;
        private Vector2 startPos = new Vector2(1366 - CANVAS_WIDTH, 0);
        private Vector2 startPanPos;
        private Vector2 transformedPos;
        private Mode mode = Mode.Edit;
        private Rectangle bounds;
        private Texture2D background;
        private DrawableRectangle border;
        private Texture2D plainWhite;

        private SpriteFont font;

        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }

        public Canvas(Game game)
        {
            gameRef = (MainGame)game;
            bounds = new Rectangle((int)startPos.X, (int)startPos.Y, CANVAS_WIDTH, CANVAS_HEIGHT);
        }

        public void Initialize()
        {
            Camera.Initialize(startPos, new Rectangle(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT), 
                new Vector2(gameRef.level.WidthInPixels - CANVAS_WIDTH, gameRef.level.HeightInPixels - CANVAS_HEIGHT)); 
        }

        public void LoadContent()
        {
            background = new DrawableRectangle(gameRef.GraphicsDevice, 
                new Vector2(32, 32), Color.White, true).Texture;

            border = new DrawableRectangle(gameRef.GraphicsDevice,
                new Vector2(CANVAS_WIDTH, CANVAS_HEIGHT), Color.White,
                false);

            font = gameRef.Content.Load<SpriteFont>(@"Fonts\main_font");
        }

        List<Vector2> positions = new List<Vector2>();

        public void Update(GameTime gameTime)
        {
            var mousePos = InputHandler.MousePos;
            transformedPos = (new Vector2(mousePos.X, mousePos.Y) + Camera.Position) / Camera.Zoom - startPos;
            var transformedTile = Engine.VectorToCell(transformedPos);

            mode = InputHandler.KeyDown(Keys.Space) ? Mode.Navigate : Mode.Edit;

            if (mode == Mode.Edit)
            {
                Camera.Zoom = 1.0f;
                if (InputHandler.MouseButtonDown(MouseButton.LeftButton))
                {
                    if (gameRef.level.CurrentLayerIndex == -1)
                    {
                        gameRef.level.SetCollision((int)transformedTile.X, (int)transformedTile.Y,
                            true);
                    }
                    else
                    {
                        if (gameRef.SelectedTool == Tool.Pencil)
                        {
                            gameRef.level.SetTile((int)transformedTile.X, (int)transformedTile.Y,
                                new Tile(gameRef.SelectedTileIndex, gameRef.SelectedTileSheetIndex));
                        }
                        else
                        {
                            Floodfill(new Point((int)transformedTile.X, (int)transformedTile.Y), gameRef.level.CurrentLayer.GetTile((int)transformedTile.X, (int)transformedTile.Y),
                                new Tile(gameRef.SelectedTileIndex, gameRef.SelectedTileSheetIndex));
                        }
                    }
                }
                else if (InputHandler.MouseButtonDown(MouseButton.RightButton))
                {
                    if (gameRef.level.CurrentLayerIndex == -1)
                    {
                        gameRef.level.SetCollision((int)transformedTile.X, (int)transformedTile.Y,
                            false);
                    }
                    else
                    {
                        if (gameRef.SelectedTool == Tool.Pencil)
                        {
                            gameRef.level.SetTile((int)transformedTile.X, (int)transformedTile.Y,
                                new Tile(-1, -1));
                        } else
                        {
                            Floodfill(new Point((int)transformedTile.X, (int)transformedTile.Y),
                                gameRef.level.CurrentLayer.GetTile((int)transformedTile.X, (int)transformedTile.Y),
                                new Tile(-1, -1));
                        }
                    }
                }

                float speedMult = 2.0f;

                if (InputHandler.KeyDown(Keys.Up))
                    Camera.SetPosition(Camera.Position + new Vector2(0, -3 * speedMult));
                if (InputHandler.KeyDown(Keys.Down))
                    Camera.SetPosition(Camera.Position + new Vector2(0, 3 * speedMult));
                if (InputHandler.KeyDown(Keys.Left))
                    Camera.SetPosition(Camera.Position + new Vector2(-3 * speedMult, 0));
                if (InputHandler.KeyDown(Keys.Right))
                    Camera.SetPosition(Camera.Position + new Vector2(3 * speedMult, 0));
            }
            else
            {
                if (InputHandler.MouseButtonPressed(MouseButton.LeftButton))
                    startPanPos = Camera.Position + new Vector2(InputHandler.MousePos.X, InputHandler.MousePos.Y);

                if (InputHandler.MouseButtonDown(MouseButton.LeftButton))
                {
                    var newPos = startPanPos - (Camera.Position + new Vector2(InputHandler.MousePos.X, InputHandler.MousePos.Y));

                    if (!(double.IsNaN(newPos.X)) && !(double.IsNaN(newPos.Y)))
                        Camera.SetPosition(Camera.Position + newPos);
                }

                if (InputHandler.LastMouseState.ScrollWheelValue < InputHandler.MouseState.ScrollWheelValue)
                {
                    Camera.Zoom = Camera.Zoom + .05f;
                }
                else if (InputHandler.LastMouseState.ScrollWheelValue > InputHandler.MouseState.ScrollWheelValue)
                {
                    Camera.Zoom = Camera.Zoom - .05f;
                }
            }
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Begin();
            batch.Draw(background,
                new Rectangle((int)startPos.X, (int)startPos.Y, CANVAS_WIDTH, CANVAS_HEIGHT),
                Color.CornflowerBlue);
            batch.End();

            batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransform());
            gameRef.level.Draw(batch, gameTime);
            if (gameRef.ShowCollisionMap)
            {
                int startX = (int)((Camera.Position.X / Engine.TileWidth)) - 2;
                int startY = (int)((Camera.Position.Y / Engine.TileHeight)) - 2;
                int endX = startX + (int)((Camera.View.Width / Engine.TileWidth)) + 4;
                int endY = startY + (int)((Camera.View.Height / Engine.TileHeight)) + 4;

                for (int x = startX; x < endX; x++)
                {
                    if (x < 0 || x >= gameRef.level.WidthInTiles)
                        continue;
                    for (int y = startY; y < endY; y++)
                    {
                        if (y < 0 || y >= gameRef.level.HeightInTiles)
                            continue;

                        if (gameRef.level.GetIsCollision(x, y))
                            batch.Draw(background, new Rectangle(x * Engine.TileWidth,
                                y * Engine.TileHeight, Engine.TileWidth, Engine.TileHeight),
                                Color.Red * .4f);
                    }
                }
            }
            batch.End();

            batch.Begin();
            batch.DrawString(font,
                Engine.VectorToCell(transformedPos).ToString(),
                startPos,
                Color.White);
            batch.DrawString(font,
                "Layer " + (gameRef.level.CurrentLayerIndex + 1).ToString(),
                startPos + new Vector2(font.MeasureString(Engine.VectorToCell(transformedPos).ToString()).X + 10, 0),
                Color.White);
            batch.DrawString(font,
                "| Tool: " + gameRef.SelectedTool.ToString(),
                new Vector2(
                    font.MeasureString(
                    Engine.VectorToCell(transformedPos).ToString() + 10 + "Layer " + (gameRef.level.CurrentLayerIndex + 1).ToString() + 10).X, 
                    0),
                Color.White);
            batch.End();
        }

        private void Floodfill(Point q, Tile target, Tile replacement)
        {
            int w = gameRef.level.WidthInTiles;
            int h = gameRef.level.HeightInTiles;

            if (q.X < 0 || q.X > w - 1 || q.Y < 0 || q.Y > h - 1)
                return;

            Stack<Point> stack = new Stack<Point>();
            stack.Push(q);

            while (stack.Count > 0)
            {
                Point p = stack.Pop();
                int x = p.X;
                int y = p.Y;
                if (y < 0 || y > h - 1 || x < 0 || x > w - 1)
                    continue;

                var val = gameRef.level.CurrentLayer.GetTile(x, y);

                if (target != null)
                {
                    if (val.TileSet == replacement.TileSet && val.TileIndex == replacement.TileIndex)
                        continue;

                    if (val.TileSet == target.TileSet && val.TileIndex == target.TileIndex)
                    {
                        gameRef.level.SetTile(x, y,
                            new Tile(replacement.TileIndex, replacement.TileSet));

                        stack.Push(new Point(x + 1, y));
                        stack.Push(new Point(x - 1, y));
                        stack.Push(new Point(x, y + 1));
                        stack.Push(new Point(x, y - 1));
                    }
                }
                else
                {
                    if (val == null)
                    {
                        gameRef.level.CurrentLayer.SetTile(x, y,
                            new Tile(replacement.TileIndex, replacement.TileSet));
                        stack.Push(new Point(x + 1, y));
                        stack.Push(new Point(x - 1, y));
                        stack.Push(new Point(x, y + 1));
                        stack.Push(new Point(x, y - 1));
                    }
                }
            }
        }
    }

    class PaintNode
    {
        public bool Visited { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
