using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;

namespace InnKeeper.Shared
{
    public class GameController
    {
        public SpriteBatch SBatch { get; private set; }
        public ContentManager CManager { get; private set; }
        public GraphicsDeviceManager GDevice { get; private set; }
        public TextureManager TexManager { get; private set; }
        public GameStateStack StateStack { get; private set; }
        public EntityFactory EntFactory { get; private set; }
        public Inn CurrentInn { get; private set; }

        public Camera2D Camera { get; private set; }
        public Vector2 WorldPosition { get; set; }

        // preferred camera width and height
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        // width and height of the actual graphics device of the display
        int displayWidth;
        int displayHeight;

        Vector2 screenCenter;

        public GameController()
        {
            CManager = null;
            screenCenter = Vector2.Zero;
        }
        
        public void SetSpriteBatch(SpriteBatch sbatch)
        {
            this.SBatch = sbatch;
        }

        public void SetStateStack(GameStateStack gsStack)
        {
            StateStack = gsStack;
        }

        public void SetContentManager(ContentManager cMan)
        {
            CManager = cMan;
        }

        public void SetScreenDimension(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            screenCenter.X = width / 2;
            screenCenter.Y = height / 2;
        }

        public Vector2 GetScreenCenter()
        {
            return screenCenter;
        }

        public void SetGraphicsDevice(GraphicsDeviceManager device)
        {
            this.GDevice = device;

            displayWidth = device.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            displayHeight = device.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
        }

        public void SetTextureManager(TextureManager manager)
        {
            if(TexManager == null)
            {
                TexManager = manager;
            }
        }

        public void SetEntityFactory(EntityFactory factory)
        {
            this.EntFactory = factory;
        }

        public void SetCurrentInn(Inn inn)
        {
            CurrentInn = inn;
        }

        public void SetCamera(Camera2D camera)
        {
            this.Camera = camera;
        }

        public Vector2 GetDeviceDimensions()
        {
            return new Vector2(displayWidth, displayHeight);
        }
    }
}
