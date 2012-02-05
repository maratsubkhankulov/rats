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
        enum Facing
        {
            Up,
            Down,
            Left,
            Right
        }

        public const float MoveSpeed = 0.1F;
        private Facing currentOrientation = default(Facing);
        private Grid _grid;

        public Player(Texture2D texture, Vector2 position, Grid grid):base(texture, position)
        {
            _grid = grid;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = gameTime.ElapsedGameTime.Milliseconds;

            MineHint();

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                this.Position += (new Vector2(-MoveSpeed, 0) * dt);
                currentOrientation = Facing.Left;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                this.Position += (new Vector2(MoveSpeed, 0) * dt);
                currentOrientation = Facing.Right;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.Position += (new Vector2(0, -MoveSpeed) * dt);
                currentOrientation = Facing.Up;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                this.Position += (new Vector2(0, MoveSpeed) * dt);
                currentOrientation = Facing.Down;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                PlaceMine();
            }
        }

        private void MineHint()
        {
            int squareX, squareY; 
            CalculateMineSquares(out squareX, out squareY);
            _grid.SetMineHint(squareX, squareY);
        }

        private void PlaceMine()
        {
            int squareX, squareY; 
            CalculateMineSquares(out squareX, out squareY);
            _grid.SetMine(squareX, squareY);
        }

        private void CalculateMineSquares(out int squareX, out int squareY)
        {
            int dx, dy;
            switch (currentOrientation)
            {
                case Facing.Up:
                    // use float for accuracy in division then round down
                    dx = 0; dy = -1;
                    break;
                case Facing.Down:
                    dx = 0; dy = 1;
                    break;
                case Facing.Left:
                    dx = -1; dy = 0;
                    break;
                case Facing.Right:
                    dx = 1; dy = 0;
                    break;
                default:
                    throw new Exception("NO");
            }
            int xBase, yBase;
            if (dy == 0)
                yBase = BoundingRectangle.Center.Y;
            else if (dy == 1)
                yBase = BoundingRectangle.Bottom;
            else
                yBase = BoundingRectangle.Top;

            if (dx == 0)
                xBase = BoundingRectangle.Center.X;
            else if (dx == 1)
                xBase = BoundingRectangle.Right;
            else
                xBase = BoundingRectangle.Left;

             _grid.IndicesAtCoordinate(xBase, yBase, out squareX, out squareY);
            squareX += dx;
            squareY += dy;
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
