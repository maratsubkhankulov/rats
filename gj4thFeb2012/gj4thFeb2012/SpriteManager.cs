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
        Camera camera;

        public SpriteManager(Game game, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : base(game)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;

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

        Sprite workingSprite;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Sprite i in sprites)
            {
                this.spriteBatch.Draw(i.Texture, i.Position, i.Color);       
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
