﻿using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Origins_Remake.Util;
using OriginsLib.Util;
using OriginsLib.IO;
using OriginsLib.TileEngine;
using Origins_Remake.Levels;
using Origins_Remake.Entities.Interfaces;

namespace Origins_Remake.Entities.Mobs
{
    public class Player : AnimatedEntity, Origins_Remake.Entities.Interfaces.IAttackable
    {
        private bool isTalking = false;
        private int idOfExitZoneEntered = 0;
        private bool justEntered = false;

        private string previousLevel = "";
        private string currentLevel = "";

        public bool IsTalking
        {
            get { return isTalking; }
            set { isTalking = value; }
        }

        public Player()
            : base(Vector2.Zero)
        {
            this.pos = new Vector2(32, 32);

            width = 28;
            height = 30;
        }

        public override void SetAnimation()
        {
            var sheet = SheetManager.SpriteSheets["player"];

            Texture2D[] imgsUp = new Texture2D[3] { sheet.GetSubImage(0, 3), sheet.GetSubImage(1, 3), sheet.GetSubImage(2, 3) };
            Texture2D[] imgsDown = new Texture2D[3] { sheet.GetSubImage(0, 0), sheet.GetSubImage(1, 0), sheet.GetSubImage(2, 0) };
            Texture2D[] imgsLeft = new Texture2D[3] { sheet.GetSubImage(0, 1), sheet.GetSubImage(1, 1), sheet.GetSubImage(2, 1) };
            Texture2D[] imgsRight = new Texture2D[3] { sheet.GetSubImage(0, 2), sheet.GetSubImage(1, 2), sheet.GetSubImage(2, 2) };

            animUp = new Animation(imgsUp);
            animDown = new Animation(imgsDown);
            animLeft = new Animation(imgsLeft);
            animRight = new Animation(imgsRight);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 newPos = Vector2.Zero;
            HandleInput(InputHandler.GamePadConnected, ref newPos);
            Move(Position.X + newPos.X, Position.Y + newPos.Y);
            Camera.SetPosition(Position - new Vector2(Camera.View.Width / 2, Camera.View.Height / 2));

            LevelManager.CurrentLevel.ExitZones.ForEach((zone) =>
            {
                if (!justEntered)
                {
                    if (new Rectangle((int)GridPosition.X, (int)GridPosition.Y, 1, 1).Intersects(zone.Bounds) && !zone.InUse)
                    {
                        LevelManager.ChangeLevel(zone.Target);
                        LevelManager.CurrentLevel.exitZones[0] = new ExitZone(LevelManager.CurrentLevel.exitZones[0].Bounds, zone.Target, zone.Parent) { InUse = true };
                        idOfExitZoneEntered = 0;
                        justEntered = true;
                    }
                }
                else
                {
                    if (!(new Rectangle((int)GridPosition.X, (int)GridPosition.Y, 1, 1).Intersects(zone.Bounds)) && zone.InUse)
                    {
                        zone.InUse = false;
                        justEntered = false;
                    }
                }
            });

        }

        /// <summary>
        /// This method handles getting attacked by another entity
        /// </summary>
        /// <param name="hit">How many hitpoints the attacker removes</param>
        
        public void getHit(int hit)
        {
            hitpoints = hitpoints - hit;
            if (hitpoints <= 0)
            {
                LevelManager.ChangeLevel(LevelManager.LevelIds["obstacle 1 level"]);
            }
        }

        private void HandleInput(bool gamePadConnected, ref Vector2 newPos)
        {
            if (gamePadConnected)
            {
            }
            else
            {
#if DEBUG
                if (InputHandler.KeyPressed(Keys.N)) noClip = !noClip;
#endif

                if (InputHandler.KeyDown(Keys.Up))
                    newPos.Y -= movementSpeed;
                if (InputHandler.KeyDown(Keys.Down))
                    newPos.Y += movementSpeed;
                if (InputHandler.KeyDown(Keys.Left))
                    newPos.X -= movementSpeed;
                if (InputHandler.KeyDown(Keys.Right))
                    newPos.X += movementSpeed;

                if (newPos.Y < 0) dir = 0;
                if (newPos.Y > 0) dir = 1;
                if (newPos.X < 0) dir = 2;
                if (newPos.X > 0) dir = 3;

                if (newPos == Vector2.Zero) 
                    isMoving = false;
                else 
                    isMoving = true;

                var npc = GetNearbyNpc();
                var enemy = GetNearbyEnemy();

                if (npc != null)
                {
                    if (InputHandler.KeyPressed(Keys.Z))
                    {
                        if (!npc.IsBusy && !isTalking)
                        {
                            isTalking = true;
                            npc.Speak();
                        }
                    }
                }
                if (enemy != null)
                {
                    if (InputHandler.KeyPressed(Keys.C))
                    {
                        Attack(enemy);
                    }
                }
            }
        }

        private Npc GetNearbyNpc()
        {
            Npc to_return = null;
            foreach (Npc npc in EntityManager.Npcs)
            {
                if (Vector2.Distance(this.pos, npc.Position) < 15)
                    to_return = npc;
            }
            return to_return;
        }

        private Enemy GetNearbyEnemy()
        {
            Enemy to_return = null;
            foreach (Enemy enemy in EntityManager.Enemies)
            {
                if (Vector2.Distance(this.pos, enemy.Position) < 15)
                    to_return = enemy;
            }
            return to_return;
        }

        public void LoadFromData(EntityProperties props)
        {
            Name = props.Name;
            Position = props.Position;
            dir = props.Direction;
            godMode = props.GodMode;
            noClip = props.NoClip;
            superSpeed = props.SuperSpeed;
        }
    }
}
