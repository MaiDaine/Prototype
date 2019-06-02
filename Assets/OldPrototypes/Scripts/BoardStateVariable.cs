using System;
using UnityEngine;

namespace Prototype
{
    public class BoardStateVariable : ScriptableObject
    {
        [Serializable]
        public struct State
        {
            public Transform playerSpawnPosition;
            public Transform npcSpawnPosition;
        }

        public State currentState;

        public void SetValue(BoardStateVariable value)
        {
            currentState = value.currentState;
        }
    }
}