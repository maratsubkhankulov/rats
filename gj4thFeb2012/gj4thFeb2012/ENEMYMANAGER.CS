using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace gj4thFeb2012
{
    class EnemyManager : GameComponent
    {
        List<Enemy> enemies;
        Grid grid;

        public EnemyManager(Game game, Grid grid) : base(game)
        {
            enemies = new List<Enemy>();
            this.grid = grid;
        }

        public override void Update(GameTime gameTime)
        {
            int groupDistanceMin = 150;
            int groupDistanceMax = 200;
            float dt = gameTime.ElapsedGameTime.Milliseconds;
            //Direct the enemies around the grid
            foreach (Enemy e in enemies)
            {
                //Grid Collision
                CollisionManager.HandleGridCollisions(grid, e);
                
                //Circular formation
                foreach (Enemy f in enemies)
                {
                    if (e.isDead()) break;
                    if (e == f) break;
                    float dist = Vector2.Distance(e.Position, f.Position);
                    if (dist == 0) { e.Move(new Vector2(Game1.random.Next(5, 10) / 10, Game1.random.Next(5, 10) / 10)); break; }
                    //Get difference vector
                    Vector2 dif = e.Position - f.Position;
                    dif = (Vector2.Normalize(dif))*1.0f;
                    //Keep distance
                    if (dist < groupDistanceMin)
                    {
                        e.Move(dif);
                        f.Move(-dif);
                        //break;
                    }

                    //Group together
                    if (dist > groupDistanceMax)
                    {
                        e.Move(-dif);
                        f.Move(dif);
                        //break;
                    }
                }
                e.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void SetTargetAll(Sprite sprite)
        {
            foreach (Enemy e in enemies)
            {
                e.SetTarget(sprite);
            }
        }

        public void Register(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void Remove(Enemy enemy)
        {
            enemies.Remove(enemy);
        }
    }
}
