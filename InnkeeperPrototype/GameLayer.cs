
using System.Collections.Generic;
using CocosSharp;

namespace InnkeeperPrototype
{
    public class GameLayer : CCLayerColor
    {

        // Define a label variable
        CCLabel label;
        CCSprite background;
        public GameLayer() : base(CCColor4B.Black)
        {
           
            // create and initialize a Label
            //label = new CCLabel("InnKeeper", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);

            // add the label as a child to this Layer
            //AddChild(label);

            

            background = new CCSprite("Background.png");
            background.PositionX = background.ContentSize.Width / 2;
            background.PositionY = background.ContentSize.Height / 2;


            AddChild(background);

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;

            // position the label on the center of the screen
            //label.Position = bounds.Center;
           
            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            AddEventListener(touchListener, this);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                // Perform touch handling here
            }
        }

        
    }
}
