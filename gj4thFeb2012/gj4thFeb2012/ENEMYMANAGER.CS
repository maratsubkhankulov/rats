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

        public EnemyManager(Game game, Grid grid) : base(game)
        {
            enemies = new List<Enemy>();
        }

        public override void Update(GameTime gameTime)
        {
            //Direct the enemies around the grid
            foreach (Enemy e in enemies)
            {
                //Find path
                //Move
                e.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void SetTargetAll(Sprite sprite)
        {

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
