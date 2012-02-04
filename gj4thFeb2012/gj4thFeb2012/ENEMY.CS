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
        float speed;
        static Random _r = new Random();

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.target = null;

            speed = _r.Next(7, 10)/10.0f*2;
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Roam:
                    roam(gameTime);
                    break;
                case State.Chase:
                    roam(gameTime);
                    simpleChase(gameTime);
                    break;
                case State.Dead:
                    break;
                default:
                    throw (new Exception("Wrong state"));
            }
        }
        private void roam(GameTime gameTime)
        {
            Position += randomRandomOffset(gameTime);
            //speed = _r.Next(9, 10) / 10.0f;
        }

        private Vector2 randomRandomOffset(GameTime gameTime)
        {
            return new Vector2(_r.Next(-1, 1), _r.Next(-1, 1)) * gameTime.ElapsedGameTime.Milliseconds / 100;
        }

        private void simpleChase(GameTime gameTime)
        {
            if (target == null) throw new Exception("Enemy chasing but no target exists");
            //Move towards the target
            this.Move(towardsTargetOffset(gameTime));
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

        public void Move(Vector2 vector)
        {
            this.Position += vector * speed;
        }

        public void die()
        {
            state = State.Dead;
        }

        public bool isDead()
        {
            return (state == State.Dead);
        }

        public override void HandleCollision(Sprite sprite)
        {
            die();
            
        }
    }
}
