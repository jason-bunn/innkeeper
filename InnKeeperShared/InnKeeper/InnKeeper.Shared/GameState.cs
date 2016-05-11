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

        public void RemoveEntity(Entity ent)
        {
            entities.Remove(ent);
        }

        public bool CheckOverlap(Entity ent)
        {
            bool overlap = false;

            foreach (Entity other in entities)
            {
                // don't check against itself
                if(ent != other)
                {
                    if (ent.BoundingBox.Intersects(other.BoundingBox))
                    {
                        if (ent.Layer == other.Layer)
                            overlap = true;
                        break;
                    }
                }
                
            }

            return overlap;
        }

        public bool CheckIfRoomBelow(Entity ent)
        {
            bool support = false;

            foreach (Entity other in entities)
            {
                if(ent != other)
                {
                    // check left side
                    //var left = new Vector2(ent.Position.X, ent.Position.Y + ent.BoundingBox.Height + 1);
                    //if(other.BoundingBox.Contains(left.X, left.Y))
                    //{
                    //    support = true;
                    //    break;
                    //}
                    // check middle
                    var middle = new Vector2(ent.Position.X + (ent.BoundingBox.Width / 2), ent.Position.Y + ent.BoundingBox.Height + 1);
                    if(other.BoundingBox.Contains(middle.X, middle.Y))
                    {
                        support = true;
                        break;
                    }
                    // check right
                }
            }

            return support;
        }

    }

}
