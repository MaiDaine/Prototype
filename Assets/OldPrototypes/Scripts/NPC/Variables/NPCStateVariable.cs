using System;
using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu]
    public class NPCStateVariable : ScriptableObject
    {
        [Serializable]
        public struct State
        {
            public int npcType;
        }

        public State currentState;

        public void SetNPCType(int value)
        {
            currentState.npcType = value;
        }

        public void SetValue(NPCStateVariable value)
        {
            currentState = value.currentState;
        }
    }
}