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
        List<Entity> uiEntities;

        float innUpdateTimeRemaining;

        public bool DrawGrid { get; set; }

        int numCols = 64;
        int numRows = 64;
        int gridSize = 64;

        

        public PlayState(GameController gameController) : base(gameController)
        {
            Controller = gameController;
            DrawGrid = false;
            uiEntities = new List<Entity>();

        }

        public override void EnterState()
        {
            // Initialize Inn container
            currentInn = new Inn("Prancing Pony");
            Controller.SetCurrentInn(currentInn);

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
            //Controller.Camera.Position = new Vector2(GameVariables.WORLD_WIDTH / 2, GameVariables.WORLD_HEIGHT / 2);
            parallaxEntities.Add(new ImageEntity(Controller.TexManager.GetTexture("Background"),
               new Vector2((Controller.ScreenWidth - Controller.TexManager.GetTexture("Background").Width) / 2, 0),
               Color.White));

            // Create a texture for the grid lines
            texture1px = new Texture2D(Controller.GDevice.GraphicsDevice, 1, 1);
            texture1px.SetData(new Color[] { Color.White });

            hammerIcon = Controller.EntFactory.CreateIcon(GameVariables.IconTypes.BUILD, EnterBuildState);
            

            uiEntities.Add(hammerIcon);

            innUpdateTimeRemaining = GameVariables.UpdateInnTimeInSeconds;

            Ready = true;
            base.EnterState();
        }

        public override void ExitState()
        {
            entities.Clear();
            parallaxEntities.Clear();
            textEntities.Clear();
            base.ExitState();
        }

        public override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime);
            //base.Draw(gameTime);
            var cameraTransformMatrix = Controller.Camera.GetViewMatrix(Vector2.Zero);
            Controller.SBatch.Begin(transformMatrix: cameraTransformMatrix);

            if (entities.Count > 0 && Ready)
            {
                foreach (Entity entity in entities)
                {
                    if (entity.IsVisible)
                    {
                        Controller.SBatch.Draw(entity.SpriteTexture, entity.Position, entity.SourceRect, entity.Tint);
                    }


                }
            }
            
            //currentState.Draw(gameTime);
            
            // Draw grid if in the build state
            if (currentState.StateID == GamePlayState.State.BUILD)
            {
                for (float x = -numCols; x < numCols; x++)
                {
                    //Rectangle rectangle = new Rectangle(
                    //    (int)(Controller.GetScreenCenter().X + x * gridSize), 0, 1,
                    //    Controller.ScreenHeight);
                    //Controller.SBatch.Draw(texture1px, rectangle, Color.Red);
                    Rectangle rectangle = new Rectangle(
                        (int)x * gridSize, 0, 1,
                        Controller.ScreenHeight);
                    Controller.SBatch.Draw(texture1px, rectangle, Color.Red);
                }
                for (float y = -numRows; y < numRows; y++)
                {
                    Rectangle rectangle = new Rectangle(0, (int)y * gridSize,
                        Controller.ScreenWidth, 1);
                    Controller.SBatch.Draw(texture1px, rectangle, Color.Red);
                }
            }
            
            //playStateUI.Draw(gameTime);
            //playStateUI.DrawStrings(gameTime);
            Controller.SBatch.End();
        }

        public override void DrawStrings(GameTime gameTime)
        {
            Controller.SBatch.Begin();

            // Draw ui elements here as well
            if (uiEntities.Count > 0 && Ready)
            {
                foreach (Entity entity in uiEntities)
                {
                    if (entity.IsVisible)
                    {
                        Controller.SBatch.Draw(entity.SpriteTexture, entity.Position, entity.SourceRect, entity.Tint);
                    }


                }
            }

            playStateUI.Draw(gameTime);
            playStateUI.DrawStrings(gameTime);
            Controller.SBatch.End();
            //base.DrawStrings(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // store current touch state
            Touches = TouchPanel.GetState();


            ProcessGestures(deltaTime);

            if (Touches.Count > 0 && Ready)
            {
                // Loop through entities and check whether or not they have been touched
                    for(int i = entities.Count -1; i >= 0; i--)
                    {
                        if (Touches[0].State == TouchLocationState.Released)
                        {
                            var worldPos = Controller.Camera.ScreenToWorld(Touches[0].Position);
                            if (entities[i].BoundingBox.Contains(worldPos))
                            {
                                // if the entity is visible and a callback exists, execute it
                                if (entities[i].IsVisible && entities[i].Action != null)
                                {
                                    entities[i].Action();
                                }
                            }
                        }
                    }
                    // loop through ui entities
                    for (int i = uiEntities.Count - 1; i >= 0; i--)
                    {
                        if (Touches[0].State == TouchLocationState.Released)
                        {
                            //var worldPos = Controller.Camera.ScreenToWorld(Touches[0].Position);
                            if (uiEntities[i].BoundingBox.Contains(Touches[0].Position))
                            {
                                // if the entity is visible and a callback exists, execute it
                                if (uiEntities[i].IsVisible && uiEntities[i].Action != null)
                                {
                                    uiEntities[i].Action();
                                }
                            }
                        }
                    }


            }// end Touches.Count > 0 && Ready

            

            innUpdateTimeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (innUpdateTimeRemaining <= 0)
            {
                UpdateInn();
            }
            currentInn.CustomerUpdate(gameTime);
            playStateUI.Update(gameTime);
            currentState.Update(gameTime);
            base.Update(gameTime);
        }

        void EnterBuildState()
        {
            if(currentState != gamePlayStates["Build"])
            {
                currentState = gamePlayStates["Build"];
                currentState.EnterState();
            }
            else
            {
                currentState.ExitState();
                currentState = gamePlayStates["Inspect"];
                currentState.EnterState();
            }
            
        }

        void EnterInspectState()
        {

        }

        void UpdateInn()
        {
            innUpdateTimeRemaining = GameVariables.UpdateInnTimeInSeconds;
            currentInn.ProcessUpdate();
        }

        void ProcessGestures(float deltaTime)
        {
       

            var gesture = default(GestureSample);
            while (TouchPanel.IsGestureAvailable)
            {
                gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.Pinch)
                {
                    Vector2 a = gesture.Position;
                    Vector2 b = gesture.Position2;
                    float dist = Vector2.Distance(a, b);

                    Vector2 olda = gesture.Position - gesture.Delta;
                    Vector2 oldb = gesture.Position2 - gesture.Delta2;
                    float oldDist = Vector2.Distance(olda, oldb);

                    if(oldDist > dist)
                    {
                        
                        Controller.Camera.ZoomOut(GameVariables.CAMERA_ZOOM_SPEED * deltaTime);
                    }
                    else
                    {
                        
                        Controller.Camera.ZoomIn(GameVariables.CAMERA_ZOOM_SPEED * deltaTime);
                    }

                    
                }
                //if the gesture is a drag, move camera
                if(gesture.GestureType == GestureType.FreeDrag)
                {
                    
                    Vector2 cameraPos = Controller.Camera.Position;
                    Vector2 direction = gesture.Delta;

                    Vector2 worldPos = Controller.Camera.ScreenToWorld(direction);
                    //if (cameraPos.X >= 0 &&
                    //    cameraPos.Y >= 0 &&
                    //    cameraPos.X <= GameVariables.WORLD_WIDTH &&
                    //    cameraPos.Y <= GameVariables.WORLD_HEIGHT)
                    //{
                    //    Controller.Camera.Move(-direction * (GameVariables.CAMERA_MOVE_SPEED * deltaTime));
                    //}

                    Controller.Camera.Move(-direction * (GameVariables.CAMERA_MOVE_SPEED * deltaTime));
                }
            }
        }
    }
}
