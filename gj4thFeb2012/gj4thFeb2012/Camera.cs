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
        Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        Vector2 _dimension;

        public void Update(GameTime gameTime)
        {
            float dt = gameTime.ElapsedGameTime.Milliseconds;

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                this._position += (new Vector2(-1,0)* dt);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                this._position += (new Vector2(1, 0) * dt);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                this._position += (new Vector2(0, -1) * dt);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                this._position += (new Vector2(0, 1) * dt);
            }
        }

        public Camera()
        {
            _position = new Vector2(-5, 5);
            _dimension = new Vector2(800, 600);
        }
    }
}
