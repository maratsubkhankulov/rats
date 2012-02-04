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
            
        }

        public Camera()
        {
            _position = new Vector2(-5, 5);
            _dimension = new Vector2(800, 600);
        }
    }
}
