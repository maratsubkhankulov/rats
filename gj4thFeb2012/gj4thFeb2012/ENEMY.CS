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
        Vector2 staticTarget;
        float speed;
        Vector2 headingVector;
        int targetVisionDistance = 50;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.target = null;
            this.staticTarget = new Vector2();
            this.headingVector = new Vector2(0, 0);

            speed = Rng.Next(7, 10)/10.0f*2;
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

            //Smoother motion using a heading vector
            Position += headingVector * gameTime.ElapsedGameTime.Milliseconds;
        }
        private void roam(GameTime gameTime)
        {
            //Roam by selecting a random target in close proximity when an old target has been met
            if (Vector2.Distance(this.Position, staticTarget) < targetVisionDistance)
            {
                SetRandomTarget();
            }
            else
            {
                //Move towards the random target
                towardsStaticTargetOffset(gameTime);
            }                
            //Random shake
            Position += randomRandomOffset(gameTime);
            //speed = Rng.Next(9, 10) / 10.0f;
        }

        private Vector2 randomRandomOffset(GameTime gameTime)
        {
            return new Vector2(Rng.Next(-1, 1), Rng.Next(-1, 1)) * gameTime.ElapsedGameTime.Milliseconds / 100;
        }

        private void simpleChase(GameTime gameTime)
        {
            if (target != null)
            {
                //Move towards the target
                this.Move(towardsTargetOffset(gameTime));
            }
            else
            {
                //Move towards the target
                this.Move(towardsStaticTargetOffset(gameTime));
            }
        }

        private Vector2 towardsTargetOffset(GameTime gameTime)
        {
            Vector2 v = (target.Position - this.Position);
            v.Normalize();
            return v;
        }

        private Vector2 towardsStaticTargetOffset(GameTime gameTime)
        {
            Vector2 headingVector = (staticTarget - this.Position);
            if (Vector2.Distance(staticTarget, this.Position) != 0) headingVector.Normalize();
            return headingVector;
        }

        public void SetTarget(Sprite sprite)
        {
            target = sprite;
            state = State.Chase;
        }

        public void SetTarget(Vector2 v)
        {
            staticTarget = v;
            target = null;
            state = State.Chase;
        }

        public void Move(Vector2 vector)
        {
            this.Position += vector * speed;
        }

        private void Die()
        {
            state = State.Dead;
        }

        public bool isDead()
        {
            return (state == State.Dead);
        }

        public override void HandleCollision(Sprite sprite)
        {
            Die();
        }

        public void SetRoam()
        {
            state = State.Roam;
            SetRandomTarget();
        }

        public void SetRandomTarget()
        {
            staticTarget = this.Position + new Vector2(Game1.random.Next(-100, 100), Game1.random.Next(-100, 100));
            //Set a random target
        }
    }
}
