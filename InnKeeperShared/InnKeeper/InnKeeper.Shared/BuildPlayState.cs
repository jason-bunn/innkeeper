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
        BuildGrid grid;
        List<Entity> buildEntities;
        List<Entity> uiEntities;

        public BuildPlayState(GameState parent) : base(parent)
        {
            StateID = State.BUILD;
            
        }

        public override void EnterState()
        {
            if(!isLoaded)
            {
                Entity temp;
                uiEntities = new List<Entity>();
                buildEntities = new List<Entity>();
                temp = Parent.Controller.EntFactory.CreateIcon(GameVariables.IconTypes.BED, LodgingBuildSelect);
                uiEntities.Add(temp);
                //Parent.AddEntity(temp);

                temp = Parent.Controller.EntFactory.CreateIcon(GameVariables.IconTypes.MUG, FareBuildSelect);
                uiEntities.Add(temp);
                //Parent.AddEntity(temp);

                grid = new BuildGrid(GameVariables.GRID_SIZE, Parent.Controller.ScreenWidth, Parent.Controller.ScreenHeight);

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
            if (currentEntity != null)
            {
                Parent.RemoveEntity(currentEntity);
                currentEntity = null;
            }
            base.ExitState();
        }

        public override void Update(GameTime gameTime)
        {
            TouchCollection touches = Parent.Touches;

            
            // if an entity has been spawned
            if(currentEntity != null)
            {
                // check if it overlaps
                if (Parent.CheckOverlap(currentEntity))
                {
                    currentEntity.SetColor(Color.Red);
                }
                // if not, check its current y position
                else if (currentEntity.Position.Y <= GameVariables.GROUNDHEIGHT && !Parent.CheckIfRoomBelow(currentEntity))
                {
                    if (currentEntity.Tint != Color.Red)
                    {
                        currentEntity.SetColor(Color.Red);
                    }
                }
                else // if it is on the ground and not overlapping
                {
                    // TODO check if it is on top of another room
                    if (currentEntity.Tint != Color.Green || currentEntity.Tint != Color.White)
                    {
                        currentEntity.SetColor(Color.Green);
                    }
                }

                

                if (touches.Count > 0)
                {
                    var worldPos = Parent.Controller.Camera.ScreenToWorld(touches[0].Position);

                    // if touch is in current entity bounding box
                    if (currentEntity.BoundingBox.Contains(worldPos) &&
                        currentEntity.Tint != Color.White)
                    {
                        // if touch moved
                        if(touches[0].State == TouchLocationState.Moved)
                        {

                            //currentEntity.SetPosition(new Vector2(
                            //touches[0].Position.X - currentEntity.BoundingBox.Width / 2,
                            //touches[0].Position.Y - currentEntity.BoundingBox.Height / 2));

                            currentEntity.SetPosition(grid.SnapToNearestNode(new Vector2(
                                worldPos.X - currentEntity.BoundingBox.Width / 2,
                                worldPos.Y - currentEntity.BoundingBox.Height / 2)));
                        }
                        // if touch released
                        if (touches[0].State == TouchLocationState.Released)
                        {
                            if (currentEntity.Tint == Color.Green)
                            {
                                var room = (Room)currentEntity;
                                // check to see if you can afford it
                                if (Parent.Controller.CurrentInn.TotalGold >= room.Cost)
                                {
                                    currentEntity.SetColor(Color.White);

                                    // add room to the inn 
                                    
                                    Parent.Controller.CurrentInn.AddRoom((Room)currentEntity);
                                    currentEntity = null;
                                }
                                
                            }
                        }
                    }
                }
                
            }
            if (touches.Count > 0)
            {

            
                // loop through ui entities
                for (int i = uiEntities.Count - 1; i >= 0; i--)
                {
                    if (touches[0].State == TouchLocationState.Released)
                    {
                        //var worldPos = Controller.Camera.ScreenToWorld(Touches[0].Position);
                        if (uiEntities[i].BoundingBox.Contains(touches[0].Position))
                        {
                            // if the entity is visible and a callback exists, execute it
                            if (uiEntities[i].IsVisible && uiEntities[i].Action != null)
                            {
                                uiEntities[i].Action();
                            }
                        }
                    }
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Parent.Controller.SBatch.Begin();

            // Draw ui elements here as well
            if (uiEntities.Count > 0 )
            {
                foreach (Entity entity in uiEntities)
                {
                    if (entity.IsVisible)
                    {
                        Parent.Controller.SBatch.Draw(entity.SpriteTexture, entity.Position, entity.SourceRect, entity.Tint);
                    }


                }
            }

            
            Parent.Controller.SBatch.End();
            base.Draw(gameTime);

            
            
            
        }

        void DummyCallback()
        {

        }

        void LodgingBuildSelect()
        {
            if (currentEntity != null)
            {
                Parent.RemoveEntity(currentEntity);
                currentEntity = null;
            }
            
            currentEntity = Parent.Controller.EntFactory.CreateRoom(GameVariables.RoomTypes.LOWBED);
            currentEntity.SetPosition(new Vector2(64, 64));

            // This should really only be called after the room is set
            Parent.Controller.CurrentInn.IncreaseCustomerChance(10);
            Parent.Controller.CurrentInn.UpdateInterArrivalRate();

            Parent.AddEntity(currentEntity);
        }

        void FareBuildSelect()
        {
            if (currentEntity != null)
            {
                Parent.RemoveEntity(currentEntity);
                currentEntity = null;
            }
            currentEntity = Parent.Controller.EntFactory.CreateRoom(GameVariables.RoomTypes.GREATHALL);
            currentEntity.SetPosition(new Vector2(64, 64));

            // This should really only be called after the room is set
            Parent.Controller.CurrentInn.IncreaseCustomerChance(40);
            Parent.Controller.CurrentInn.UpdateInterArrivalRate();

            Parent.AddEntity(currentEntity);
        }
    }
}
