using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace InnKeeper.Shared
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static GameController controller = new GameController();
        GameStateStack stateStack = new GameStateStack(controller);
        
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 768;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            controller.SetStateStack(stateStack);
            controller.SetGraphicsDevice(graphics);
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

            // Set references to content manager and screen dimension in GameController
            controller.SetContentManager(this.Content);
            controller.SetScreenDimension(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            controller.SetTextureManager(new TextureManager());
            controller.SetEntityFactory(new EntityFactory(controller.TexManager));

            stateStack.RegisterState("Splash", new SplashScreenState(controller));
            stateStack.RegisterState("MainMenu", new MainMenuState(controller));
            stateStack.RegisterState("PlayState", new PlayState(controller));

            stateStack.Push("Splash");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Set the gamecontroller reference for the spritebatch
            controller.SetSpriteBatch(spriteBatch);
            // TODO: use this.Content to load your game content here

            // Load splash texture
            controller.TexManager.AddTexture("Splash", Content.Load<Texture2D>("Images/Ld/Wizardry"));
            // Load remaining textures
            controller.TexManager.AddTexture("BlackBox", Content.Load<Texture2D>("Images/Ld/blackbox"));
            controller.TexManager.AddTexture("Background", Content.Load<Texture2D>("Images/Ld/Background3"));
            controller.TexManager.AddTexture("Icons", Content.Load<Texture2D>("Images/Ld/Icons48"));
            controller.TexManager.AddTexture("Rooms", Content.Load<Texture2D>("Images/Ld/Tileset"));

            // Launch splash screen state
            stateStack.GetState("Splash").EnterState();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            controller.TexManager.Clear();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // Update state stack

            stateStack.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

            // Draw top state from stack
            
            stateStack.Draw(gameTime);
            stateStack.DrawStrings(gameTime);

            spriteBatch.End();
            base.Draw(gameTime);


        }
    }
}
