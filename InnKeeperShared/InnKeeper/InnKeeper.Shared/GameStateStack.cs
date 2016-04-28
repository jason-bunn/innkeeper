using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace InnKeeper.Shared
{
    public class GameStateStack
    {
        GameController Controller { get; set; }
        Stack<GameState> gameStateStack;
        Dictionary<string, GameState> registeredStates;

        public GameStateStack(GameController gameController)
        {
            Controller = gameController;
            gameStateStack = new Stack<GameState>();
            registeredStates = new Dictionary<string, GameState>();
        }

        public void Update(GameTime gameTime)
        {
            if(gameStateStack.Count > 0)
            {
                gameStateStack.Peek().Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (gameStateStack.Count > 0)
            {
                gameStateStack.Peek().Draw(gameTime);
            }
        }

        public void DrawStrings(GameTime gameTime)
        {
            if (gameStateStack.Count > 0)
            {
                gameStateStack.Peek().DrawStrings(gameTime);
            }
        }

        public void RegisterState(string name, GameState gameState)
        {
            if(!registeredStates.ContainsKey(name))
            {
                registeredStates.Add(name, gameState);
            }
        }

        public void Pop()
        {
            gameStateStack.Pop();
        }

        public void Push(string stateName)
        {
            gameStateStack.Push(registeredStates[stateName]);
        }

        public void Clear()
        {
            gameStateStack.Clear();
        }

        public GameState GetState(string name)
        {
            return registeredStates[name];
        }

        public void ChangeState(string name)
        {
            if(gameStateStack.Peek() != null)
            {
                gameStateStack.Peek().ExitState();
                gameStateStack.Pop();
            }
            Push(name);
            registeredStates[name].EnterState();
        }

    }
}
