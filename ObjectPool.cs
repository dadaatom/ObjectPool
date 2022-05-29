using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public GameObject ToPool;
	public int ItemsInPool;

	private List<GameObject> pooledObjects;
	private List<GameObject> objectsInUse;
	// Use this for initialization
	void Awake()
	{
		pooledObjects = new List<GameObject>();
		objectsInUse = new List<GameObject>();
		for (int i = 0; i < ItemsInPool; i++)
		{
			GameObject obj = Instantiate(ToPool);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetObject()
	{
		if (pooledObjects.Count > 0)
		{
			GameObject obj = pooledObjects[0];
			objectsInUse.Add(obj);
			pooledObjects.Remove(obj);
			return obj;
		}
		else
		{
			GameObject obj = Instantiate(ToPool);
			obj.SetActive(false);
			objectsInUse.Add(obj);
			return obj;
		}
	}

	public void RemoveObject(GameObject obj)
	{
		obj.SetActive(false);
		pooledObjects.Add(obj);
		objectsInUse.Remove(obj);
	}

	public GameObject[] GetPooledObjects()
	{
		return pooledObjects.ToArray();
	}

	public void AddToPool(int numberOfClones)
	{
		for (int i = 0; i < numberOfClones; i++)
		{
			GameObject obj = Instantiate(ToPool);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}
}
