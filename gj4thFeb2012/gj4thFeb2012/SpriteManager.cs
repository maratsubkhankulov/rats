using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gj4thFeb2012
{
    public class SpriteManager : DrawableGameComponent
    {
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        List<Sprite> sprites;
        Camera _camera;

        public SpriteManager(Game game, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Camera camera)
            : base(game)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
            this._camera = camera;
            sprites = new List<Sprite>();
        }

        /*public SpriteManager(GameComponentCollection components, GraphicsDevice graphicsDevice, ISpriteBatch spriteBatch, IView view)
            : this(components, graphicsDevice, spriteBatch)
        {
            this.view = view;
        }*/

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Sprite sprite in sprites)
            {
                if (sprite.BoundingRectangle.Intersects(_camera.WorldBoundingRectangle))
                    this.spriteBatch.Draw(sprite.Texture, sprite.Position - _camera.Position, sprite.Color);       
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Register(Sprite sprite)
        {
            this.sprites.Add(sprite);
        }

        public void Remove(Sprite sprite)
        {
            this.sprites.Remove(sprite);
        }
    }
}
