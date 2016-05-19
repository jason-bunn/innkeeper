using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    public abstract class GameState
    {
        public GameController Controller { get; set; }
        protected List<Entity> parallaxEntities;
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
            parallaxEntities = new List<Entity>();
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

        public virtual void DrawBackground(GameTime gameTime)
        {
            var parallaxFactor = Vector2.One * GameVariables.PARALLAX_AMOUNT;
            var cameraTransformMatrix = Controller.Camera.GetViewMatrix(parallaxFactor);
            Controller.SBatch.Begin(transformMatrix: cameraTransformMatrix);

            if (parallaxEntities.Count > 0 && Ready)
            {
                foreach (Entity entity in parallaxEntities)
                {
                    if (entity.IsVisible)
                    {
                        Controller.SBatch.Draw(entity.SpriteTexture, entity.Position, entity.SourceRect, entity.Tint);
                    }


                }
            }
            Controller.SBatch.End();
        }
        public virtual void Draw(GameTime gameTime)
        {
            var cameraTransformMatrix = Controller.Camera.GetViewMatrix(Vector2.Zero);
            Controller.SBatch.Begin(transformMatrix: cameraTransformMatrix);

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
            Controller.SBatch.End();
        }

        public virtual void DrawStrings(GameTime gameTime)
        {
            Controller.SBatch.Begin(blendState: BlendState.AlphaBlend);
            foreach (TextEntity entity in textEntities)
            {
                Controller.SBatch.DrawString(entity.SFont, entity.Text, entity.Position, entity.Tint);
                //entity.Draw(gameTime);
            }
            Controller.SBatch.End();
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
