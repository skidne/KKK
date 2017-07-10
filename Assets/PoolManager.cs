using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{

    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    static PoolManager _instance;


    public static PoolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }

    public void CreatePool(GameObject prefab, int poolSize, Transform poolParent)
    {
        int poolKey = prefab.GetInstanceID();

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());

            GameObject poolHolder = new GameObject(prefab.name + " pool");
            poolHolder.transform.parent = poolParent;
            poolHolder.transform.localScale = new Vector3(1, 1);

            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Enqueue(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }

    public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (CanPool(prefab))
        {
            if (poolDictionary.ContainsKey(poolKey))
            {
                ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
                poolDictionary[poolKey].Enqueue(objectToReuse);

                return (objectToReuse.Reuse(position, rotation));
            }
        }
        return (null);
    }

    public bool CanPool(GameObject prefab)
    {
        int poolKey = prefab.GetInstanceID();
        ObjectInstance objectToCheck;

        if (poolDictionary.ContainsKey(poolKey))
            for (int i = 0; i < poolDictionary[poolKey].Count; i++)
            {
                objectToCheck = poolDictionary[poolKey].Dequeue();
                if (objectToCheck.IsReusable())
                {
                    poolDictionary[poolKey].Enqueue(objectToCheck);
                    return (true);
                }

            }
        return (false);
    }

    public void SortByAmount(ref Pool[] _pl)
    {
        Pool tmp;

        for (int j = 0; j < _pl.Length; j++)
            for (int i = 0; i < _pl.Length - 1; i++)
                if (_pl[i].amount > _pl[i + 1].amount)
                {
                    tmp = _pl[i];
                    _pl[i] = _pl[i + 1];
                    _pl[i + 1] = tmp;
                }
    }

    public class ObjectInstance
    {

        GameObject gameObject;
        Transform transform;

        bool hasPoolObjectComponent;
        PoolObject poolObjectScript;

        public ObjectInstance(GameObject objectInstance)
        {
            
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive(false);

            if (gameObject.GetComponent<PoolObject>())
            {
                hasPoolObjectComponent = true;
                poolObjectScript = gameObject.GetComponent<PoolObject>();
            }
        }

        public GameObject Reuse(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<Collider2D>().enabled = true;
            transform.position = position;
            transform.rotation = rotation;

            if (hasPoolObjectComponent)
            {
                poolObjectScript.OnObjectReuse();
            }
            return (gameObject);
        }

        public bool IsReusable()
        {
            if (gameObject.activeInHierarchy == false)
                return (true);
            return (false);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
    }
}

[System.Serializable]
public class Pool
{
    public GameObject prefab;
    public int amount;
}