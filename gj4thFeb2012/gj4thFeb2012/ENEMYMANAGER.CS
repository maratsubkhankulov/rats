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
        private readonly SpriteManager _spriteManager;
        private readonly Texture2D _enemyTexture;
        private Timer _spawnTimer;
        Player _player;


        private int enemySpawnNumber = 10;
        private int enemySpawnDelay = 1000; // ms

        public EnemyManager(Game game, Grid grid, Player player, Camera camera, SpriteManager spriteManager, Texture2D enemyTexture) : base(game)
        {
            _enemies = new List<Enemy>();
            _grid = grid;
            _camera = camera;
            _spriteManager = spriteManager;
            _enemyTexture = enemyTexture;
            _player = player;

            _spawnTimer = new Timer(enemySpawnDelay, SpawnAtEdge);
            _spawnTimer.Start();
        }

        public override void Update(GameTime gameTime)
        {
            int groupDistanceMin = 150;
            int groupDistanceMax = 200;
            int distanceOfVision = 300;
            _spawnTimer.Update(gameTime);

            float dt = gameTime.ElapsedGameTime.Milliseconds;
            //Direct the enemies around the grid
            foreach (Enemy e in _enemies)
            {
                //Grid Collision
                if (CollisionManager.HandleGridCollisions(grid, e)) e.SetRandomTarget();
                
                //Enemy and player behavior
                //If an enemy sees the player - follow it
                if (Vector2.Distance(e.Position, _player.Position) < distanceOfVision)
                {
                   // e.SetTarget(_player.Position);
                }
                else
                {
                    e.SetRoam();
                }

                //Circular formation
                foreach (Enemy f in _enemies)
                {
                    if (e.isDead()) break;
                    if (e == f) break;
                    
                    /*float dist = Vector2.Distance(e.Position, f.Position);
                    if (dist == 0) { e.Move(new Vector2(Game1.random.Next(5, 10) / 10, Rng.Next(5, 10) / 10)); break; }
                    
                    //Get difference vector
                    Vector2 dif = e.Position - f.Position;
                    dif = (Vector2.Normalize(dif))*0.1f;
                    //Keep distance
                    if (dist < groupDistanceMin)
                    {
                        e.Move(dif);
                        f.Move(-dif);
                        //break;
                    }*/

                    //Group together
                    /*if (dist > groupDistanceMax)
                    {
                        e.Move(-dif);
                        f.Move(dif);
                        //break;
                    }*/
                }
                e.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void SpawnAtEdge()
        {
            SpawnAtEdge(enemySpawnNumber);
        }

        public void SpawnAtEdge(int numberOfEnemies)
        {
            int xCoord, yCoord, xIndex, yIndex;
            bool randomX, topLeftSide;
            Rectangle spawnRectangle = _camera.SpawnRectangle;

            for (int i = 0; i < numberOfEnemies - 1; i++)
            {
                randomX = Rng.Next(2) == 1;
                topLeftSide = Rng.Next(2) == 1;

                if (randomX)
                {
                    xCoord = Rng.Next(spawnRectangle.Left, spawnRectangle.Right);
                    if (topLeftSide)
                        yCoord = spawnRectangle.Top;
                    else
                        yCoord = spawnRectangle.Bottom;
                }
                else
                {
                    yCoord = Rng.Next(spawnRectangle.Top, spawnRectangle.Bottom);
                    if (topLeftSide)
                        xCoord = spawnRectangle.Left;
                    else
                        xCoord = spawnRectangle.Right;
                }

                _grid.IndicesAtCoordinate(xCoord, yCoord, out xIndex, out yIndex);
                
                if (_grid.GetTile(xIndex, yIndex) == Grid.Tile.Floor)
                {
                    Enemy enemySprite = new Enemy(_enemyTexture, _grid.PositionAtIndices(xIndex, yIndex));
                    Register(enemySprite);
                    _spriteManager.Register(enemySprite);
                }
                    
            }
        }

        public void SetTargetAll(Sprite sprite)
        {
            foreach (Enemy e in _enemies)
            {
                e.SetTarget(sprite);
            }
        }

        public void SetTargetAll(Vector2 v)
        {
            foreach (Enemy e in enemies)
            {
                e.SetTarget(v);
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
