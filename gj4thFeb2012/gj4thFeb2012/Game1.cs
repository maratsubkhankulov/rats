using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gj4thFeb2012
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Camera _camera;
        SpriteManager _spriteManager;
        Player _player;
        Grid _grid;
        EnemyManager _enemyManager;
        CollisionManager collisionManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Make mouse visible
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera(GraphicsDevice);
            _spriteManager =  new SpriteManager(this, GraphicsDevice, _spriteBatch, _camera);
            this.Components.Add(_spriteManager);
            _grid = new Grid(this.Content.Load<Texture2D>("level_test"), _spriteManager, this.Content.Load<Texture2D>("floor_block"), this.Content.Load<Texture2D>("wall_block"), this.Content.Load<Texture2D>("mine_block"), this.Content.Load<Texture2D>("mine_hint"));
            _player = new Player(this.Content.Load<Texture2D>("player"), new Vector2(100, 100), _grid);
            _spriteManager.Register(_player);
            _camera.AttachTo(_player);
            CollisionManager collisionManager = new CollisionManager(this);
            this.Components.Add(collisionManager);

            //Test enemies
            _enemyManager = new EnemyManager(this, _grid, _player, _camera, _spriteManager, this.Content.Load<Texture2D>("enemy"));
            this.Components.Add(_enemyManager);
            
            for (int i = 0; i < 10; i++)
            {
                Enemy e = new Enemy(this.Content.Load<Texture2D>("enemy"), new Vector2(Rng.Next(150, 200)));
                _enemyManager.Register(e);
                _spriteManager.Register(e);
            }
            _enemyManager.SetTargetAll(_player);
            //End of test
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here.
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _camera.Update(gameTime);
            _player.Update(gameTime);
            _player.HandleGridCollisions(_grid);

            //Enemy targeting test
            if (Mouse.GetState().LeftButton ==  ButtonState.Pressed){
                _enemyManager.SetTargetAll(new Vector2(Mouse.GetState().X, Mouse.GetState().Y) + _camera.Position);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
