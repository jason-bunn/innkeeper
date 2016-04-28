using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public class SplashScreenState : GameState
    {
        
        float timeToShow = 2;
        float timeRemaining;

        public SplashScreenState(GameController gameController) : base(gameController)
        {
            Controller = gameController;
            Ready = false;
        }

        public override void Update(GameTime gameTime)
        {
            timeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeRemaining <= 0)
            {
                
                Controller.StateStack.ChangeState("MainMenu");
            }

            base.Update(gameTime);

            
        }

        public override void EnterState()
        {
            timeRemaining = timeToShow;
       
            entities.Add(new ImageEntity(Controller.TexManager.GetTexture("Splash"), 
                new Vector2((Controller.ScreenWidth - Controller.TexManager.GetTexture("Splash").Width)/2, 0), 
                Color.White));
            Ready = true;
            base.EnterState();
        }

        public override void ExitState()
        {
            
            Ready = false;
            entities.Clear();
            base.ExitState();

           
        }
    }
}
