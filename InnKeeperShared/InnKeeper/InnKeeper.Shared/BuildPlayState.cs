using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace InnKeeper.Shared
{
    public class BuildPlayState : GamePlayState
    {
        bool isLoaded = false;

        public BuildPlayState(GameState parent) : base(parent)
        {
            StateID = State.BUILD;
        }

        public override void EnterState()
        {
            if(!isLoaded)
            {
                Parent.AddEntity(Parent.Controller.EntFactory.CreateIcon(GameVariables.IconTypes.BED));
                Parent.AddEntity(Parent.Controller.EntFactory.CreateIcon(GameVariables.IconTypes.MUG));
                isLoaded = true;
            }

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
