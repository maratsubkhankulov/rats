using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace gj4thFeb2012
{
    class CollisionManager : GameComponent
    {
        List<Sprite> entities;

        public CollisionManager(Game game) : base(game)
        {

        }

        public bool isCollision(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            Rectangle a = new Rectangle((int)a1.X, (int)a1.Y, (int)a2.X, (int)a2.Y);
            Rectangle b = new Rectangle((int)b1.X, (int)b1.Y, (int)b2.X, (int)b2.Y);

            return a.Intersects(b);
        }

        /*public Vector2 getIntersectionDepth(Rectangle a, Rectangle b)
        {
            if (a.Right < b.Left || b.Right < a.Left)
            {
                return Vector2.Zero;
            } else
            {
                Vector2 dif1 = a.Right - b.Left;
                Vector2 dif2 = 
        }*/

        public void Update(GameTime gameTime)
        {
            foreach (Sprite a in entities)
            {
                foreach (Sprite b in entities)
                {

                    
                    //if (getIntersectionDepth(a.BoundingRectangle, b.BoundingRectangle) > Vector2.Zero))
                    if (a.BoundingRectangle.Intersects(b.BoundingRectangle))
                    {
                        a.HandleCollision(b);
                        //b.HandleCollision(a, -intersectionDepth);
                    }
                }
            }
        }
    }
}

