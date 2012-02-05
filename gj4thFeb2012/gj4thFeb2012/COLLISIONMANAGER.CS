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
            entities = new List<Sprite>();
        }

        static public void HandleGridCollisions(Grid grid, Sprite sprite)
        {
            Rectangle BoundingRectangle = sprite.BoundingRectangle;

            int minX, minY, maxX, maxY;
            grid.IndicesAtCoordinate(BoundingRectangle.Left, BoundingRectangle.Top, out minX, out minY);
            grid.IndicesAtCoordinate(BoundingRectangle.Right, BoundingRectangle.Bottom, out maxX, out maxY);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (grid.GetTile(x, y) == Grid.Tile.Wall)
                    {
                        sprite.Position += IntersectionDepth(grid.TileBoundingRectangle(x, y), BoundingRectangle);
                    }

                    if (grid.GetTile(x, y) == Grid.Tile.Mine)
                    {
                         sprite.HandleCollision(null);
                         grid.RemoveMine(x, y);
                    }
                }
            }
        }

        static private Vector2 IntersectionDepth(Rectangle a, Rectangle b)
        {
            int xOverlap, yOverlap;

            if (!a.Intersects(b))
                return Vector2.Zero;

            int diff1 = a.Right - b.Left;
            int diff2 = b.Right - a.Left;

            if (Math.Pow(diff1, 2) < Math.Pow(diff2, 2))
                xOverlap = diff1;
            else
                // the rectangles overlap on the other side
                // invert the vector so that it will push out of the collision
                xOverlap = diff2 * -1;

            diff1 = a.Bottom - b.Top;
            diff2 = b.Bottom - a.Top;

            if (Math.Pow(diff1, 2) < Math.Pow(diff2, 2))
                yOverlap = diff1;
            else
                // the rectangles overlap on the other side
                // invert the vector so that it will push out of the collision
                yOverlap = diff2 * -1;

            if (Math.Pow(yOverlap, 2) < Math.Pow(xOverlap, 2))
                return new Vector2(0, yOverlap);
            else
                return new Vector2(xOverlap, 0);
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

        public override void Update(GameTime gameTime)
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

