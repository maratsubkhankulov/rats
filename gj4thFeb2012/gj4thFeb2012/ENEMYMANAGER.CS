using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gj4thFeb2012
{
    class EnemyManager : GameComponent
    {
        private readonly List<Enemy> _enemies;
        private readonly Grid _grid;
        private readonly Camera _camera;
        private readonly Texture2D _enemyTexture;
        private Timer _spawnTimer;

        public EnemyManager(Game game, Grid grid, Camera camera, Texture2D enemyTexture) : base(game)
        {
            _enemies = new List<Enemy>();
            _grid = grid;
            _camera = camera;
            _enemyTexture = enemyTexture;
            _spawnTimer = new Timer(2000, SpawnAtEdge);
        }

        public override void Update(GameTime gameTime)
        {
            int groupDistanceMin = 150;
            int groupDistanceMax = 200;
            _spawnTimer.Update(gameTime);

            float dt = gameTime.ElapsedGameTime.Milliseconds;
            //Direct the enemies around the grid
            foreach (Enemy e in _enemies)
            {
                //Grid Collision
                CollisionManager.HandleGridCollisions(_grid, e);
                
                //Circular formation
                foreach (Enemy f in _enemies)
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

        public void SpawnAtEdge()
        {
            
        }

        public void SetTargetAll(Sprite sprite)
        {
            foreach (Enemy e in _enemies)
            {
                e.SetTarget(sprite);
            }
        }

        public void Register(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}
