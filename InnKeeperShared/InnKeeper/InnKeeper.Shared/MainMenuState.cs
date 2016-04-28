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
            title.SetPosition(new Vector2(Controller.GetScreenCenter().X - (title.Text.Length + 96), Controller.GetScreenCenter().Y - 200));
            
            title.SetSpriteFont(titleFont);

            // initialize new menu items
            menuItem1 = new TextEntity("New Game");
            menuItem1.SetColor(Color.White);
            menuItem1.SetPosition(new Vector2(Controller.GetScreenCenter().X - (menuItem1.Text.Length + 32), Controller.GetScreenCenter().Y));
            menuItem1.SetBoundingBox(new Rectangle((int)menuItem1.Position.X, (int)menuItem1.Position.Y, 100, 40));
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
            //Controller.SBatch.DrawString(testFont, "This is a test font", new Vector2(100, 100), Color.Black);

            //Controller.SBatch.DrawString(menuItem1.SFont, menuItem1.Text, menuItem1.Position, menuItem1.Tint);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            if(touchCollection.Count > 0)
            {
                if (menuItem1.BoundingBox.Contains(touchCollection[0].Position))
                {
                    Controller.StateStack.ChangeState("PlayState");
                }
            }
           

            base.Update(gameTime);
        }
    }
}