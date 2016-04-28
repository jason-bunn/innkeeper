using Microsoft.Xna.Framework;

namespace InnKeeper.Shared
{
    public abstract class GamePlayState
    {
        protected GameState Parent { get; set; }

        public enum State
        {
            INSPECT,
            BUILD,
            STATS
        }

        public GamePlayState(GameState parent)
        {
            Parent = parent;
        }

        public State StateID { get; set; }
        public virtual void EnterState()
        {

        }

        public virtual void ExitState()
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
