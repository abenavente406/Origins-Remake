using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Origins_Remake.Levels;
using Origins_Remake.Util;
using Microsoft.Xna.Framework.Graphics;

namespace Origins_Remake.Entities.Mobs
{
    public enum AiState
    {
        ROAMING,
        TARGETTING,
        ATTACKING,
        SEARCHING,
        FLEEING
    }

    public abstract class Enemy : AnimatedEntity
    {
        protected float detectRange = 250f;

        public AiState aiState = AiState.ROAMING;

        protected int movementTimer = 0;
        protected int movementTimerMax = 300;
        protected int movementDir = 1;

        protected bool canMove = true;

        Random rand = new Random();

        Vector2 newPos;
        int dirX = 0;
        int dirY = 0;
        Point currentTargetPoint;

        public Enemy(Vector2 pos)
            : base(pos)
        {
            
        }

        public abstract override void SetAnimation();

        public override void Update(GameTime gameTime)
        {
            newPos = Position;
            dirX = 0;
            dirY = 0;

            Player player = this.ScanForPlayer();

            if (!(player == null))
            {
                aiState = AiState.TARGETTING;
                if (Vector2.Distance(Position, player.Position) < detectRange / 4)
                {
                    aiState = AiState.ATTACKING;
                }

                var angle = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - pos.X);
                if (angle > MathHelper.PiOver4 && angle < MathHelper.PiOver4 * 3) dir = 1;
                else if (angle < -MathHelper.PiOver4 && angle > -MathHelper.PiOver4 * 3) dir = 0;
                else if (angle < MathHelper.PiOver4 && angle > -MathHelper.PiOver4) dir = 3;
                else if (angle > MathHelper.PiOver4 * 3 && angle < MathHelper.PiOver4 * 5) dir = 2;

            }
            else
            {
                aiState = AiState.ROAMING;
            }

            switch (aiState)
            {
                case AiState.ROAMING:   // If the player has not been found, move randomly
                    {
                        Roam();
                        break;
                    }
                case AiState.TARGETTING:    // If the player HAS been found, move torwards it and sprint
                    {
                        isMoving = true;

                        Vector2 direction = determineMoveDirection(player);

                        var newDir = direction - Position;
                        newDir.Normalize();

                        newPos = Position + newDir * 2;
                        break;
                    }
                case AiState.ATTACKING:     // Attack the player. Attacking prevents moving
                    {
                        isMoving = false;
                        return;
                    }
            }

            Move(newPos.X, newPos.Y);
        }

        public void DrawPathToPlayer(SpriteBatch batch, Player player)
        {
            List<Vector2> path = Pathfinder.FindPath(this.GridPosition, player.GridPosition);
            foreach (Vector2 v in path)
                batch.Draw(texDown, v, Color.Red * .2f);
        }

        public Player ScanForPlayer()
        {
            Player result = null;
            if (Vector2.Distance(EntityManager.Player.Position, Position) < detectRange)
            {
                result = EntityManager.Player;
            }
            return result;
        }

        private Vector2 determineMoveDirection(Player player)
        {
            List<Vector2> path = Pathfinder.FindPath(LevelManager.Vector2Tile(Origin), LevelManager.Vector2Tile(player.Origin));

            if (path.Count < 1)
                return Vector2.Zero;

            return (path.Count > 2 ? path[0] : path[0]) + new Vector2(2);
        }

        private void Roam()
        {
            if (canMove)
            {
                movementTimer = rand.Next(250) + 1;
                movementDir = rand.Next(4);
                canMove = false;
            }

            dir = movementDir;

            if (movementTimer < movementTimerMax)
            {
                switch (movementDir)
                {
                    case 0:
                        dirY--;
                        break;
                    case 1:
                        dirY++;
                        break;
                    case 2:
                        dirX--;
                        break;
                    case 3:
                        dirX++;
                        break;
                }

                movementTimer++;
                isMoving = true;
            }
            else
            {
                isMoving = false;
                if (rand.NextDouble() > 0.9)
                    canMove = true;
            }

            newPos = Position + new Vector2(dirX * 2 * 1,
                 dirY * 2 * 1);
        }
    }
}
