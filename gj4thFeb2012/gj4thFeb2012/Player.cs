using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gj4thFeb2012
{
    class Player : Sprite
    {
        private const float MoveSpeed = 0.1F;
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

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (grid.Tiles[y, x] == Grid.Tile.Wall)
                    {
                        Vector2 displacement = grid.TileBoundingRectangle(x, y).IntersectionDepth(BoundingRectangle);
                        Position += displacement;
                    }
                }
            }
        }
    }
}
