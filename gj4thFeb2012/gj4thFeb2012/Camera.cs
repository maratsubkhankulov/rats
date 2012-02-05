using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gj4thFeb2012
{
    public class Camera
    {
        private Sprite _attachment;
        private GraphicsDevice _graphicsDevice;
        
        private Vector2 _position;
        private const float CameraSpeed = Player.MoveSpeed;
        private const int BorderSize = 100;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private Rectangle StaticZoneRectangle
        {
            get { return new Rectangle((int)_position.X + BorderSize, (int)_position.Y + BorderSize, _graphicsDevice.Viewport.Width - 2 * BorderSize, _graphicsDevice.Viewport.Height - 2 * BorderSize); }
        }

        public Rectangle WorldBoundingRectangle
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height); }
        }

        public Rectangle SpawnRectangle
        {
            get { return new Rectangle((int)_position.X - BorderSize, (int)_position.Y - BorderSize, _graphicsDevice.Viewport.Width + 2 * BorderSize, _graphicsDevice.Viewport.Height + 2 * BorderSize); }
        }

        public void Update(GameTime gameTime)
        {
            float dt = gameTime.ElapsedGameTime.Milliseconds;

            if (_attachment != null)
            {
                Vector2 velocity = default(Vector2);
                if (_attachment.BoundingRectangle.Right > StaticZoneRectangle.Right)
                    velocity += new Vector2(CameraSpeed * dt, 0);
                if (_attachment.BoundingRectangle.Left < StaticZoneRectangle.Left)
                    velocity += new Vector2(-CameraSpeed * dt, 0);
                if (_attachment.BoundingRectangle.Bottom > StaticZoneRectangle.Bottom)
                    velocity += new Vector2(0, CameraSpeed * dt);
                if (_attachment.BoundingRectangle.Top < StaticZoneRectangle.Top)
                    velocity += new Vector2(0, -CameraSpeed * dt);

                _position += velocity;
            }
        }

        public Camera(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        internal void AttachTo(Player player)
        {
            _attachment = player;
        }
    }
}
