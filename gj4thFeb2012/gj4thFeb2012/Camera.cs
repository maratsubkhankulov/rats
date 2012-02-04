using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gj4thFeb2012
{
    class Camera
    {
        Vector2 position;
        Vector2 dimension;

        public Camera()
        {
            position = new Vector2(0, 0);
            dimension = new Vector2(800, 600);
        }
    }
}
