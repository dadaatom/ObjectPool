using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject PooledObject;
    public int initialCount;
    
    public LinkedList<GameObject> ActiveList { get; private set; }
    private Stack<GameObject> _reserveList;

    void Awake()
    {
        PooledObject = gameObject;
        
        ActiveList = new LinkedList<GameObject>();
        _reserveList = new Stack<GameObject>();

        for (int i = 0; i < initialCount; i++)
        {
            _reserveList.Push(GenerateObject());
        }
    }

    /// <summary>
    /// Gets an object from the object pool.
    /// </summary>
    /// <returns>Pooled or new object</returns>
    public GameObject Get()
    {
        GameObject obj;
        if (_reserveList.Count > 0)
        {
            obj = _reserveList.Pop();
        }
        else
        {
            obj = GenerateObject();
        }

        ActiveList.AddLast(obj);
        return obj;
    }

    /// <summary>
    /// Returns object to the reserve list.
    /// </summary>
    /// <param name="obj">Object to be returned</param>
    public void Return(GameObject obj)
    {
        gameObject.SetActive(false);
        ActiveList.Remove(obj);
        _reserveList.Push(obj);
    }

    /// <summary>
    /// Adds a number of objects to the reserve list.
    /// </summary>
    /// <param name="count">Number of items to generate</param>
    public void Generate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _reserveList.Push(GenerateObject());
        }
    }

    /// <summary>
    /// Instantiates a single object.
    /// </summary>
    /// <returns>Generated object</returns>
    private GameObject GenerateObject()
    {
        GameObject obj = Instantiate(PooledObject, transform, true);
        obj.SetActive(false);
        return obj;
    }
}
