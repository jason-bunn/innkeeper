using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace InnKeeper.Shared
{
    public class MainMenuState : GameState
    {
        SpriteFont testFont;
        SpriteFont titleFont;

        TextEntity title;
        TextEntity menuItem1;

        

        public MainMenuState(GameController controller) : base(controller)
        {
            Controller = controller;
        }

        public override void EnterState()
        {
            // set font for menu
            testFont = Controller.CManager.Load<SpriteFont>("Fonts/Harrington");
            titleFont = Controller.CManager.Load<SpriteFont>("Fonts/HarringtonTitle");
            // initialize title text
            title = new TextEntity("Inn Keeper");
            title.SetColor(Color.White);
            title.SetPosition(new Vector2(Controller.GetScreenCenter().X - (titleFont.MeasureString(title.Text).X/2), Controller.GetScreenCenter().Y - 200));
            
            title.SetSpriteFont(titleFont);

            // initialize new menu items
            menuItem1 = new TextEntity("New Game");
            menuItem1.SetColor(Color.White);
            
            menuItem1.SetPosition(new Vector2(Controller.GetScreenCenter().X - (testFont.MeasureString(menuItem1.Text).X/2), Controller.GetScreenCenter().Y));
            menuItem1.SetBoundingBox(new Rectangle((int)menuItem1.Position.X, (int)menuItem1.Position.Y, (int)testFont.MeasureString(menuItem1.Text).X, (int)testFont.MeasureString(menuItem1.Text).Y));
            menuItem1.SetSpriteFont(testFont);

            // Add menut items to text entities
            textEntities.Add(title);
            textEntities.Add(menuItem1);


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
        }

        public override void DrawStrings(GameTime gameTime)
        {
            var cameraTransformMatrix = Controller.Camera.GetViewMatrix(Vector2.Zero);
            Controller.SBatch.Begin(transformMatrix: cameraTransformMatrix);

            if (textEntities.Count > 0 && Ready)
            {
                foreach (TextEntity entity in textEntities)
                {
                    Controller.SBatch.DrawString(entity.SFont, entity.Text, entity.Position, entity.Tint);
                   
                }
            }
            Controller.SBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            if(touchCollection.Count > 0)
            {
                var worldPos = Controller.Camera.ScreenToWorld(touchCollection[0].Position);
                if (menuItem1.BoundingBox.Contains(worldPos))
                {
                    Controller.StateStack.ChangeState("PlayState");
                }
            }
           

            base.Update(gameTime);
        }
        
    }

    
}