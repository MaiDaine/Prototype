using System;
using UnityEngine;

namespace Prototype
{
    [Serializable]
    public class ObjectPoolItem
    {
        public GameObject objectToPool;
        public int amountToPool;
    }
}