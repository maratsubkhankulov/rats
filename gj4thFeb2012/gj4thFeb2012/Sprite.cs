using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gj4thFeb2012
{
    public class Sprite
    {
        public Vector2 Position;
        private readonly Texture2D _texture;
        private Color _color;
        public readonly float Depth;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
        } 


        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Sprite(Texture2D texture, Vector2 position = default(Vector2), float depth = 0)
        {
            _texture = texture;
            Position = position;
            _color = Color.White;
            Depth = depth;
        }

        public virtual void Update(GameTime gameTime) { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public virtual void HandleCollision(Sprite entity)
        {
            //Do something
        }
    }
}