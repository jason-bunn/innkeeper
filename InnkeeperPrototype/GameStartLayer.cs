using System.Collections.Generic;
using CocosSharp;
using CocosDenshion;

namespace InnkeeperPrototype
{
    public class GameStartLayer : CCLayerColor
    {
        public GameStartLayer() : base(CCColor4B.Black)
        {
            PreLoadTextures();


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
        void PreLoadTextures()
        {
            var cache = CCTextureCache.SharedTextureCache;

            cache.AddImage("Background.png");
        }
    }


}