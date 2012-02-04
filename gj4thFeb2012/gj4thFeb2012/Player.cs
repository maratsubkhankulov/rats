using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gj4thFeb2012
{
    public class Player : Sprite
    {
        public const float MoveSpeed = 0.1F;
        public Player(Texture2D texture, Vector2 position):base(texture, position)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            float dt = gameTime.ElapsedGameTime.Milliseconds;

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                this.Position += (new Vector2(-MoveSpeed, 0) * dt);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                this.Position += (new Vector2(MoveSpeed, 0) * dt);
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.Position += (new Vector2(0, -MoveSpeed) * dt);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                this.Position += (new Vector2(0, MoveSpeed) * dt);
            }
        }

        public void HandleGridCollisions(Grid grid)
        {
            int minX = (int)Math.Floor((float)BoundingRectangle.Left / Grid.TileWidth);
            int maxX = (int)Math.Ceiling((float)BoundingRectangle.Right / Grid.TileWidth);
            int minY = (int)Math.Floor((float)BoundingRectangle.Top / Grid.TileWidth);
            int maxY = (int)Math.Ceiling((float)BoundingRectangle.Bottom / Grid.TileWidth);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (grid.Tiles[y, x] == Grid.Tile.Wall)
                    {
                        Position += IntersectionDepth(grid.TileBoundingRectangle(x,y), BoundingRectangle);
                    }
                }
            }
        }

        private Vector2 IntersectionDepth(Rectangle a, Rectangle b)
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
            diff2 = b.Bottom- a.Top;

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
    }
}
