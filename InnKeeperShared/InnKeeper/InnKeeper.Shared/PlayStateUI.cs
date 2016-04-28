using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public class PlayStateUI
    {
        GameState parent;
        SpriteFont uiFont;
        List<TextEntity> uiText;
        Texture2D topBorder;
        Inn currentInn;

        public PlayStateUI(GameState parent)
        {
            this.parent = parent;
            uiText = new List<TextEntity>();
            topBorder = parent.Controller.TexManager.GetTexture("BlackBox");
            uiFont = parent.Controller.CManager.Load<SpriteFont>("Fonts/HarringtonUI");

            

        }

        public void InitializeUIText()
        {
            TextEntity temp = new TextEntity(currentInn.Name);
            TextEntity currentGold = new TextEntity("Gold: ");

            temp.SetColor(Color.White);
            currentGold.SetColor(Color.White);

            temp.SetPosition(new Vector2(parent.Controller.GetScreenCenter().X, 2));
            currentGold.SetPosition(new Vector2(2, 2));

            temp.SetSpriteFont(uiFont);
            currentGold.SetSpriteFont(uiFont);

            uiText.Add(temp);
            uiText.Add(currentGold);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            parent.Controller.SBatch.Draw(topBorder, new Rectangle(0,0,1280,50), Color.Black);
        }
        public void DrawStrings(GameTime gameTime)
        {
            foreach(TextEntity entity in uiText)
            {
                parent.Controller.SBatch.DrawString(entity.SFont, entity.Text, entity.Position, entity.Tint);
            }

        }

        public void SetCurrentInn(Inn inn)
        {
            this.currentInn = inn;
        }
    }
}
