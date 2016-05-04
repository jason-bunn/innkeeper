using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace InnKeeper.Shared
{
    public abstract class GameState
    {
        public GameController Controller { get; set; }
        protected List<Entity> entities;
        protected List<TextEntity> textEntities;

        public TouchCollection Touches
        {
            get; set;
        }

        protected bool Ready { get; set; }

        public GameState(GameController controller)
        {
            Controller = controller;
            entities = new List<Entity>();
            textEntities = new List<TextEntity>();
            Ready = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (entities.Count > 0 && Ready)
            {
                foreach (Entity entity in entities)
                {
                    entity.Update(gameTime);
                }
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (entities.Count > 0 && Ready)
            {
                foreach (Entity entity in entities)
                {
                    if(entity.IsVisible)
                    {
                        Controller.SBatch.Draw(entity.SpriteTexture, entity.Position, entity.SourceRect, entity.Tint);
                    }
                    

                }
            }
        }

        public virtual void DrawStrings(GameTime gameTime)
        {
            foreach (TextEntity entity in textEntities)
            {
                Controller.SBatch.DrawString(entity.SFont, entity.Text, entity.Position, entity.Tint);
                //entity.Draw(gameTime);
            }
        }

        public virtual void EnterState()
        {
            Ready = true;
        }

        public virtual void ExitState()
        {
            Ready = false;
        }

        public List<Entity> GetEntities()
        {
            return entities;
        }

        public void AddEntity(Entity ent)
        {
            entities.Add(ent);
        }

    }

}
