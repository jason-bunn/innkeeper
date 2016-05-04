﻿using System.Collections.Generic;
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
            TextEntity currentGold = new TextEntity("Gold: " + currentInn.TotalGold);
            TextEntity netGold = new TextEntity("0");
            

            temp.SetColor(Color.White);
            currentGold.SetColor(Color.White);
            netGold.SetColor(Color.White);

            temp.SetPosition(new Vector2(parent.Controller.GetScreenCenter().X, 2));
            currentGold.SetPosition(new Vector2(2, 2));
            netGold.SetPosition(new Vector2(parent.Controller.ScreenWidth / 4, 2));

            temp.SetSpriteFont(uiFont);
            currentGold.SetSpriteFont(uiFont);
            netGold.SetSpriteFont(uiFont);

            uiText.Add(temp);
            uiText.Add(currentGold);
            uiText.Add(netGold);
        }

        public void Update(GameTime gameTime)
        {
            int netIncome = currentInn.GetNetGold();
            if(netIncome < 0)
            {
                uiText[2].SetColor(Color.Red);
            }
            else if (netIncome > 0)
            {
                uiText[2].SetColor(Color.Green);
            }
            else
            {
                uiText[2].SetColor(Color.White);
            }
            

            uiText[1].Text = "Gold: " + currentInn.TotalGold;
            uiText[2].Text = currentInn.GetNetGold().ToString();

            
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
