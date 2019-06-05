using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class ObjectPool : MonoBehaviour
    {
        public ObjectPoolItem item;
        private List<GameObject> pooledObjects;

        void Start()//TODO LOAD
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
                if (!pooledObjects[i].activeInHierarchy)
                    return pooledObjects[i];
            GameObject obj = (GameObject)Instantiate(item.objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
    }
}