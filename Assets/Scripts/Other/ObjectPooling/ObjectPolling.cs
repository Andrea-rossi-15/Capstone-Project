using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPolling : MonoBehaviour
{
    public static ObjectPolling _sharedIstance;
    public List<GameObject> _pooledObject;
    public List<GameObject> _objectToPool;
    public List<int> _amountToPool;


    void Awake()
    {
        _sharedIstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _pooledObject = new List<GameObject>();
        GameObject _tmp;
        for (int i = 0; i < _objectToPool.Count; i++)
        {
            GameObject prefab = _objectToPool[i];
            int amount = _amountToPool[i];
            for (int j = 0; j < amount; j++)
            {
                _tmp = Instantiate(prefab);
                _tmp.SetActive(false);
                _pooledObject.Add(_tmp);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObject.Count; i++)
        {
            if (!_pooledObject[i].activeInHierarchy)
            {
                return _pooledObject[i];
            }
        }
        return null;
    }
}
