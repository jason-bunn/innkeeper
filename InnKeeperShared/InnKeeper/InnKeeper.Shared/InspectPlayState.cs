using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace InnKeeper.Shared
{
    public class InspectPlayState : GamePlayState
    {
        public InspectPlayState(GameState parent) : base(parent)
        {
            StateID = State.INSPECT;
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void Update(GameTime gameTime)
        {
            TouchCollection touches = Parent.Touches;

            if (touches.Count > 0)
            {

            }

            //var gesture = default(GestureSample);

            //while (TouchPanel.IsGestureAvailable)
            //{
            //    gesture = TouchPanel.ReadGesture();

            //    if(gesture.GestureType == GestureType.Pinch)
            //    {
            //        Parent.Controller.Camera.ZoomOut((float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f);
            //    }
            //}
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
