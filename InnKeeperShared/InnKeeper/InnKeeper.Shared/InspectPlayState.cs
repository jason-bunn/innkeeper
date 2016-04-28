using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

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
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
