using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gj4thFeb2012
{
    public class Enemy : Sprite
    {
        enum State {Roam, Chase, Dead};
        State state;
        Sprite target;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.target = null;
        }

        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Roam:
                    roam(gameTime);
                    break;
                case State.Chase:
                    simpleChase(gameTime);
                    break;
                default:
                    throw (new Exception("Wrong state"));
            }
        }

        static Random _r = new Random();
        private void roam(GameTime gameTime)
        {
            Position += randomRandomOffset(gameTime);
        }

        private Vector2 randomRandomOffset(GameTime gameTime)
        {
            return new Vector2(_r.Next(-1, 1), _r.Next(-1, 1)) * gameTime.ElapsedGameTime.Milliseconds / 10;
        }

        private void simpleChase(GameTime gameTime)
        {
            if (target == null) throw new Exception("Enemy chasing but no target exists");
            //Move towards the target
            Position += towardsTargetOffset(gameTime);
        }

        private Vector2 towardsTargetOffset(GameTime gameTime)
        {
            Vector2 v = (target.Position - this.Position);
            v.Normalize();
            return v;
        }

        public void SetTarget(Sprite sprite)
        {
            target = sprite;
            state = State.Chase;
        }
    }
}
