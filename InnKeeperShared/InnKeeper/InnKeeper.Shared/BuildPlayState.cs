using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace InnKeeper.Shared
{
    public class BuildPlayState : GamePlayState
    {
        bool isLoaded = false;
        Entity currentEntity;

        List<Entity> buildEntities;

        public BuildPlayState(GameState parent) : base(parent)
        {
            StateID = State.BUILD;
        }

        public override void EnterState()
        {
            if(!isLoaded)
            {
                Entity temp;
                buildEntities = new List<Entity>();
                temp = Parent.Controller.EntFactory.CreateIcon(GameVariables.IconTypes.BED, LodgingBuildSelect);
                buildEntities.Add(temp);
                Parent.AddEntity(temp);

                temp = Parent.Controller.EntFactory.CreateIcon(GameVariables.IconTypes.MUG, FareBuildSelect);
                buildEntities.Add(temp);
                Parent.AddEntity(temp);

                isLoaded = true;
                
            }
            else
            {
                foreach (Entity ent in buildEntities)
                {
                    ent.IsVisible = true;
                }
            }
            currentEntity = null;
            base.EnterState();
        }

        public override void ExitState()
        {
            foreach (Entity ent in buildEntities)
            {
                ent.IsVisible = false;
            }
            base.ExitState();
        }

        public override void Update(GameTime gameTime)
        {
            TouchCollection touches = Parent.Touches;

            

            if(currentEntity != null)
            {
                if (currentEntity.Position.Y <= GameVariables.GROUNDHEIGHT)
                {
                    if (currentEntity.Tint != Color.Red)
                    {
                        currentEntity.SetColor(Color.Red);
                    }
                }
                else
                {
                    if (currentEntity.Tint != Color.Green || currentEntity.Tint != Color.White)
                    {
                        currentEntity.SetColor(Color.Green);
                    }
                }

                if (touches.Count > 0)
                {
                    // if touch is in current entity bounding box
                    if (currentEntity.BoundingBox.Contains(touches[0].Position) &&
                        currentEntity.Tint != Color.White)
                    {
                        // if touch moved
                        if(touches[0].State == TouchLocationState.Moved)
                        {
                            currentEntity.SetPosition(new Vector2(
                            touches[0].Position.X - currentEntity.BoundingBox.Width / 2,
                            touches[0].Position.Y - currentEntity.BoundingBox.Height / 2));
                        }
                        // if touch released
                        if (touches[0].State == TouchLocationState.Released)
                        {
                            if (currentEntity.Tint == Color.Green)
                            {
                                currentEntity.SetColor(Color.White);

                                // add room to the inn 
                                // TODO: subtract room build cost from total gold
                                Parent.Controller.CurrentInn.AddRoom((Room)currentEntity);
                                currentEntity = null;
                            }
                        }
                    }
                }
                
            }

            
           

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            
            
            
        }

        void DummyCallback()
        {

        }

        void LodgingBuildSelect()
        {

        }

        void FareBuildSelect()
        {
            currentEntity = Parent.Controller.EntFactory.CreateRoom(GameVariables.RoomTypes.GREATHALL);
            currentEntity.SetPosition(new Vector2(50, 50));

            Parent.AddEntity(currentEntity);
        }
    }
}
