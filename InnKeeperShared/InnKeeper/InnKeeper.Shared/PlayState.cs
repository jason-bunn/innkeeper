using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace InnKeeper.Shared
{
    public class PlayState : GameState
    {
        Inn currentInn;
        Dictionary<string, GamePlayState> gamePlayStates;
        GamePlayState currentState;
        PlayStateUI playStateUI;
       
        Texture2D texture1px;

        ImageEntity hammerIcon;

        public bool DrawGrid { get; set; }

        int numCols = 10;
        int numRows = 10;
        int gridSize = 64;

        TouchCollection touchCollection;

        public PlayState(GameController gameController) : base(gameController)
        {
            Controller = gameController;
            DrawGrid = false;
            
        }

        public override void EnterState()
        {
            // Initialize Inn container
            currentInn = new Inn("Prancing Pony");
            

            // Initialize gameplay states
            gamePlayStates = new Dictionary<string, GamePlayState>();
            gamePlayStates.Add("Inspect", new InspectPlayState(this));
            gamePlayStates.Add("Build", new BuildPlayState(this));
            // Initialize UI
            playStateUI = new PlayStateUI(this);
            playStateUI.SetCurrentInn(currentInn);
            playStateUI.InitializeUIText();

            currentState = gamePlayStates["Inspect"];

            // Set Background and UI entities
            
            entities.Add(new ImageEntity(Controller.TexManager.GetTexture("Background"),
               new Vector2((Controller.ScreenWidth - Controller.TexManager.GetTexture("Background").Width) / 2, 0),
               Color.White));

            // Create a texture for the grid lines
            texture1px = new Texture2D(Controller.GDevice.GraphicsDevice, 1, 1);
            texture1px.SetData(new Color[] { Color.White });

            hammerIcon = Controller.EntFactory.CreateIcon(GameVariables.IconTypes.BUILD);

            entities.Add(hammerIcon);
           
            Ready = true;
            base.EnterState();
        }

        public override void ExitState()
        {
            entities.Clear();
            base.ExitState();
        }

        public override void Draw(GameTime gameTime)
        {
            
            base.Draw(gameTime);
            currentState.Draw(gameTime);
            
            // Draw grid if in the build state
            if (currentState.StateID == GamePlayState.State.BUILD)
            {
                for (float x = -numCols; x < numCols; x++)
                {
                    Rectangle rectangle = new Rectangle(
                        (int)(Controller.GetScreenCenter().X + x * gridSize), 0, 1,
                        Controller.ScreenHeight);
                    Controller.SBatch.Draw(texture1px, rectangle, Color.Red);
                }
                for (float y = -numRows; y < numRows; y++)
                {
                    Rectangle rectangle = new Rectangle(0, (int)(Controller.GetScreenCenter().Y + y * gridSize),
                        Controller.ScreenWidth, 1);
                    Controller.SBatch.Draw(texture1px, rectangle, Color.Red);
                }
            }
            
            playStateUI.Draw(gameTime);
            playStateUI.DrawStrings(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            touchCollection = TouchPanel.GetState();

            if(touchCollection.Count > 0)
            {
                if(hammerIcon.BoundingBox.Contains(touchCollection[0].Position))
                {
                    if(currentState.StateID == GamePlayState.State.INSPECT)
                    {
                        currentState = gamePlayStates["Build"];
                        currentState.EnterState();
                    }
                    else
                    {
                        //currentState = gamePlayStates["Inspect"];
                    }
                    entities.Add(Controller.EntFactory.CreateRoom(GameVariables.RoomTypes.GREATHALL));
                    
                }
            }


            currentState.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
