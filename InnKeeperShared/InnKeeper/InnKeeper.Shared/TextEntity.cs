using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public class TextEntity : Entity
    {
        public SpriteFont SFont
        {
            get; private set;
        }
        
        public string Text { get; set; }

        public TextEntity() : base()
        {
            
        }

        public TextEntity(string text) :base()
        {
            this.Text = text;
        }

        public void SetSpriteFont(SpriteFont sFont)
        {
            this.SFont = sFont;
        }
    }
}
