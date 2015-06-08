using System;
using GameHelperLibrary;
using GameHelperLibrary.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Origins_Remake.Entities;
using Origins_Remake.Levels;
using OriginsLib.Util;
using Origins_Remake.GUI;
using Origins_Remake.Util;
using GameHelperLibrary.Shapes;
using OriginsLib.IO;
using OriginsLib.TileEngine;
using Origins_Remake.Quests;
using Origins_Remake.Entities.Mobs;

namespace Origins_Remake.States
{
    public class GameplayState : BaseGameState
    {
        public enum State
        {
            Playing,
            Frozen,
            Paused
        }

        public enum State_Paused
        {
            Normal,
            Save,
            Quit
        }

        public State currentState = State.Playing;

        private LevelManager levelManager;
        private EntityManager entityManager;
        private GaussianBlur blurOverlay;

        private ControlManager pauseControls_Normal;
        private ControlManager pauseControls_Save;
        private ControlManager pauseControls_Quit;
        private State_Paused pauseState = State_Paused.Normal;

        private RenderTarget2D mainTarget;
        private RenderTarget2D target1;
        private RenderTarget2D target2;
        private int renderTargetWidth;
        private int renderTargetHeight;

        private Texture2D background;

        private QuestManager quests;

        public static HUD hud;

        private bool loadedGame = false;
        private EntityProperties loadedProps;
        public GameplayState(Game game, GameStateManager manager)
            : base(game, manager) 
        {
            hud = new HUD(this);
        }

        public GameplayState(Game game, GameStateManager manager, EntityProperties properties)
            : base(game, manager)
        {
            loadedGame = true;
            loadedProps = properties == null ? new EntityProperties() : properties;
            hud = new HUD(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void LargeLoadContent(object sender)
        {
            var Content = gameRef.Content;

            background = new DrawableRectangle(gameRef.GraphicsDevice, new Vector2(10, 10), Color.Black, true).Texture;

            levelManager = new LevelManager(gameRef);
            Camera.Initialize(Vector2.Zero, new Rectangle(0, 0, MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT),
                new Vector2(LevelManager.CurrentLevel.WidthInPixels, LevelManager.CurrentLevel.HeightInPixels));
            Camera.MaxClamp -= new Vector2(Camera.View.Width / 2, Camera.View.Height / 2);

            entityManager = new EntityManager(gameRef);


            if (!loadedGame)
            {
                EntityManager.Player.Name = Config.currentlyPlaying;

                if (LevelManager.CurrentLevel.PlayerSpawnPoint.X > -1 &&
                    LevelManager.CurrentLevel.PlayerSpawnPoint.Y > -1)
                    EntityManager.Player.Position = new Vector2(LevelManager.CurrentLevel.PlayerSpawnPoint.X, LevelManager.CurrentLevel.PlayerSpawnPoint.Y) * Engine.TileWidth ;
            }
            else
            {
                Config.SetLastLogin(loadedProps.Name);
                Config.currentlyPlaying = loadedProps.Name;
                EntityManager.Player.LoadFromData(loadedProps);
            }

            new Pathfinder(LevelManager.CurrentLevel);

            #region Pause_Normal
            pauseControls_Normal = new ControlManager(gameRef, gameRef.Content.Load<SpriteFont>("Fonts\\default"));
            {
                Label lblPauseDisplay = new Label()
                {
                    Name = "lblPauseDisplay",
                    Text = "P A U S E D",
                    Color = Color.White
                };
                lblPauseDisplay.Position = new Vector2(MainGame.GAME_WIDTH / 2 - lblPauseDisplay.Width / 2,
                    MainGame.GAME_HEIGHT / 1.8f - lblPauseDisplay.Height - 10);

                var back = new LinkLabel(1) { Name = "lnklblBack", Text = "Resume" };
                var save = new LinkLabel(2) { Name = "lnklblSave", Text = "Save Game" };
                var quit = new LinkLabel(3) { Name = "lnklblQuit", Text = "Quit to Menu" };

                pauseControls_Normal.Add(lblPauseDisplay);
                pauseControls_Normal.Add(back);
                pauseControls_Normal.Add(save);
                pauseControls_Normal.Add(quit);

                Vector2 startPos = new Vector2(MainGame.GAME_WIDTH / 2, MainGame.GAME_HEIGHT / 1.8f);
                foreach (Control c in pauseControls_Normal)
                {
                    if (c is LinkLabel)
                    {
                        var l = (LinkLabel)c;
                        var offset = new Vector2(c.Width / 2, 0);
                        c.Position = startPos - offset;
                        c.Selected += new EventHandler(LinkLabel_Selected);
                        c.Effect = ControlEffect.PULSE;
                        c.Color = Color.Black;
                        l.OnMouseIn += LinkLabel_OnMouseIn;

                        startPos.Y += c.Height + 10f;
                    }
                }
            }
            #endregion

            #region Pause_Save
            pauseControls_Save = new ControlManager(gameRef, gameRef.Content.Load<SpriteFont>("Fonts\\default"));

            Label status = new Label();
            status.AutoSize = true;
            status.Text = "Save game success!";
            status.Position = new Vector2(MainGame.GAME_WIDTH / 2 - status.Width / 2, MainGame.GAME_HEIGHT / 2 - status.Height);
            pauseControls_Save.Add(status);

            LinkLabel goBack_Save = new LinkLabel(0);
            goBack_Save.AutoSize = true;
            goBack_Save.Text = "Go Back";
            goBack_Save.Position = new Vector2(MainGame.GAME_WIDTH / 2 - goBack_Save.Width / 2, status.Position.Y + status.Height + 10);
            goBack_Save.Selected += (o, e) => { pauseState = State_Paused.Normal; };
            goBack_Save.OnMouseIn += LinkLabel_OnMouseIn;
            pauseControls_Save.Add(goBack_Save);
            #endregion

            #region Pause_Quit
            pauseControls_Quit = new ControlManager(gameRef, gameRef.Content.Load<SpriteFont>("Fonts\\default"));

            Label areYouSure1 = new Label();
            areYouSure1.Name = "lblAreYouSure?";
            areYouSure1.Text = "Are you sure you want to quit?";
            areYouSure1.Position = new Vector2(MainGame.GAME_WIDTH / 2 - areYouSure1.SpriteFont.MeasureString(
                areYouSure1.Text).X / 2, 140);
            pauseControls_Quit.Add(areYouSure1);

            LinkLabel yes = new LinkLabel(1) { Name = "lnklblYes", Text = "Yes" };
            yes.Position = new Vector2(areYouSure1.Position.X + 40, areYouSure1.Position.Y + areYouSure1.Height + 50);
            yes.OnMouseIn += LinkLabel_OnMouseIn;
            yes.Selected += (o, e) => { SwitchStateWithFade(new MainMenuState(gameRef, StateManager)); };
            yes.Effect = ControlEffect.PULSE;

            LinkLabel no = new LinkLabel(2) { Name = "lnklblNo", Text = "No" };
            no.Position = new Vector2(areYouSure1.Position.X + areYouSure1.Width - no.Width - 40, yes.Position.Y);
            no.OnMouseIn += LinkLabel_OnMouseIn;
            no.Selected += (o, e) => { pauseState = State_Paused.Normal; };
            no.Effect = ControlEffect.PULSE;

            pauseControls_Quit.Add(yes);
            pauseControls_Quit.Add(no);
            #endregion

            blurOverlay = new GaussianBlur(gameRef);
            renderTargetWidth = MainGame.GAME_WIDTH;
            renderTargetHeight = MainGame.GAME_HEIGHT;
            blurOverlay.ComputeKernel(7, 2.0f);

            mainTarget = new RenderTarget2D(gameRef.GraphicsDevice, MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT);
            target1 = new RenderTarget2D(gameRef.GraphicsDevice, renderTargetWidth, renderTargetHeight, false,
                gameRef.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None);
            target2 = new RenderTarget2D(gameRef.GraphicsDevice, renderTargetWidth, renderTargetHeight, false,
                gameRef.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None);

            blurOverlay.ComputeOffsets(renderTargetWidth, renderTargetHeight);

            quests = new QuestManager();

            Quest q = new Quest(0, (QuestGiver)EntityManager.Npcs[0], EntityManager.Player);
            q.CheckSuccess += (delegate()
            {
                return EntityManager.Player.NoClip == true;
            });
            quests.AddQuest(q);

            ((QuestGiver)EntityManager.Npcs[0]).Quest = q;
            ((QuestGiver)EntityManager.Npcs[0]).OnQuestCompleted += (delegate(object sender2, EventArgs args)
            {
                var questGiverSender = (QuestGiver)sender2;
                questGiverSender.Dialogues[0] = "Good job!";
            });

            base.LargeLoadContent(sender);
        }

        protected override void UnloadContent()
        {
            mainTarget.Dispose();
            target1.Dispose();
            target2.Dispose();

            Config.SetLastLogin(Config.currentlyPlaying);

            base.UnloadContent();
        }

        private void LinkLabel_Selected(object sender, EventArgs e)
        {
            LinkLabel lbl = (LinkLabel)sender;

            if (lbl.Name == "lnklblBack")
            {
                currentState = State.Playing;
                pauseState = State_Paused.Normal;
            }
            else if (lbl.Name == "lnklblSave")
            {
                pauseState = State_Paused.Save;
                pauseControls_Save[0].Text = IOManager.SavePlayerData(
                    new EntityProperties()
                    {
                        Name = EntityManager.Player.Name,
                        Direction = EntityManager.Player.Direction,
                        Position = EntityManager.Player.Position,
                        GodMode = EntityManager.Player.GodMode,
                        NoClip = EntityManager.Player.NoClip,
                        SuperSpeed = EntityManager.Player.SuperSpeed,
                        TimeOfSave = DateTime.Now
                    }
                    ) ? "Save game success!" : "Save game failure!";
            }
            else if (lbl.Name == "lnklblQuit")
            {
                pauseState = State_Paused.Quit;
            }
        }

        private void LinkLabel_OnMouseIn(object sender, EventArgs e)
        {
            var lbl = (LinkLabel)sender;

            switch (currentState)
            {
                case State.Playing:
                    break;
                case State.Paused:
                    switch (pauseState)
                    {
                        case State_Paused.Normal:
                            pauseControls_Normal.SelectControl(lbl.Index);
                            break;
                        case State_Paused.Save:
                            pauseControls_Save.SelectControl(lbl.Index);
                            break;
                        case State_Paused.Quit:
                            pauseControls_Quit.SelectControl(lbl.Index);
                            break;
                    }
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (currentState != State.Frozen)
            {
                if (currentState == State.Playing)
                {
#if DEBUG
                    if (InputHandler.KeyDown(Keys.OemPlus)) Camera.Zoom += .01f;
                    if (InputHandler.KeyDown(Keys.OemMinus)) Camera.Zoom -= .01f;
#endif
                    LevelManager.Update(gameTime);
                    EntityManager.UpdateAll(gameTime);
                    quests.Update(gameTime);

                    if (InputHandler.KeyPressed(Keys.Escape))
                    {
                        if (pauseState == State_Paused.Normal)
                            currentState = State.Paused;
                        else
                            pauseState = State_Paused.Normal;
                    }
                }
                else
                {
                    switch (pauseState)
                    {
                        case State_Paused.Normal:
                            pauseControls_Normal.Update(gameTime, playerIndexInControl);
                            break;
                        case State_Paused.Save:
                            pauseControls_Save.Update(gameTime, playerIndexInControl);
                            break;
                        case State_Paused.Quit:
                            pauseControls_Quit.Update(gameTime, playerIndexInControl);
                            break;
                    }

                    if (InputHandler.KeyPressed(Keys.Escape))
                    {
                        currentState = State.Playing;
                    }
                }
            }


            hud.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (currentState == State.Playing || currentState == State.Frozen)
            {
                gameRef.GraphicsDevice.SetRenderTarget(mainTarget);
                gameRef.GraphicsDevice.Clear(Color.Transparent);
                var spriteBatch = gameRef.spriteBatch;

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransform());

                LevelManager.Draw(spriteBatch, gameTime);
                EntityManager.Draw(spriteBatch, gameTime);

                spriteBatch.End();
                gameRef.GraphicsDevice.SetRenderTarget(null);

                spriteBatch.Begin();
                spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
                hud.Draw(gameTime, spriteBatch);

                spriteBatch.End();
            }
            else
            {
                var spriteBatch = gameRef.spriteBatch;
                Texture2D result = blurOverlay.PerformGaussianBlur((Texture2D)mainTarget,
                    target1, target2, spriteBatch);

                spriteBatch.Begin();
                spriteBatch.Draw(result, Vector2.Zero, Color.White);
                spriteBatch.Draw(background, new Rectangle(0, 0, MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT), Color.Black * .6f);
                switch (pauseState)
                {
                    case State_Paused.Normal:
                        pauseControls_Normal.Draw(spriteBatch, gameTime);
                        break;
                    case State_Paused.Save:
                        pauseControls_Save.Draw(spriteBatch, gameTime);
                        break;
                    case State_Paused.Quit:
                        pauseControls_Quit.Draw(spriteBatch, gameTime);
                        break;
                    default:
                        break;
                }
                                
                spriteBatch.End();
            }

            gameRef.spriteBatch.Begin();
            FadeOutRect.Draw(gameRef.spriteBatch, Vector2.Zero, FadeOutColor);
            gameRef.spriteBatch.End();
        }
    }
}
