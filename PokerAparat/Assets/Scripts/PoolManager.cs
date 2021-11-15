using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolManager : MonoBehaviour
{
	private static PoolManager _instance;
	[Header("Prefabs")]
	public GameObject CardItem;
	private Transform _transform;
	private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
	private Dictionary<GameObject, PoolObject> _poolObjects = new Dictionary<GameObject, PoolObject>();

	public static PoolManager Instance
	{
		get
		{
			return _instance;
		}
	}

	public class Pool
	{
		public Transform Parent;
		public Queue<GameObject> Objects;
		public Type Type;

		public Pool(Transform parent)
		{
			Parent = parent;
			Objects = new Queue<GameObject>();
		}
	}

	public class PoolObject
	{
		public Transform Transform;
		public Component Component;

		public PoolObject(Transform t, Component c)
		{
			Transform = t;
			Component = c;
		}
	}

	private void Awake()
	{
		_transform = transform;
		_instance = this;

		CreatePool(CardItem, 5, null, _transform);
	}

	public void CreatePool(GameObject prefab, int poolSize, Type type = null, Transform poolHolder = null)
	{
		if (prefab == null)
			return;

		string poolKey = prefab.name;

		if (!_pools.ContainsKey(poolKey))
		{
			if (poolHolder == null)
			{
				poolHolder = new GameObject(prefab.name + " Pool").transform;
				poolHolder.SetParent(transform);
			}

			Pool pool = new Pool(poolHolder);
			pool.Type = type;

			for (int i = 0; i < poolSize; i++)
			{
				GameObject newObject = GameObject.Instantiate(prefab);

				newObject.SetActive(false);

				PoolObject poolObject;
				if (type == null)
				{
					poolObject = new PoolObject(newObject.transform, null);
				}
				else
				{
					poolObject = new PoolObject(newObject.transform, newObject.GetComponent(type));
				}
				_poolObjects.Add(newObject, poolObject);

				newObject.name = prefab.name;
				pool.Objects.Enqueue(newObject);
				_poolObjects[newObject].Transform.SetParent(poolHolder.transform, false);
			}

			_pools.Add(poolKey, pool);
		}
		else
		{
			Debug.LogWarning("Pool " + poolKey + " already exists!", gameObject);
		}
	}

	public Transform PoolOutTransform(GameObject prefab, bool active = true)
	{
		return _poolObjects[PoolOut(prefab, active)].Transform;
	}

	public T PoolOut<T>(GameObject prefab, bool active = true) where T : Component
	{
		return _poolObjects[PoolOut(prefab, active)].Component as T;
	}

	public GameObject PoolOut(GameObject prefab, bool active = true)
	{
		string poolKey = prefab.name;

		if (_pools.ContainsKey(poolKey))
		{
			GameObject objectToReuse;

			if (PoolSize(prefab) == 0)
			{
				objectToReuse = GameObject.Instantiate(prefab);

				objectToReuse.SetActive(active);

				PoolObject poolObject;
				if (_pools[poolKey].Type == null)
				{
					poolObject = new PoolObject(objectToReuse.transform, null);
				}
				else
				{
					poolObject = new PoolObject(objectToReuse.transform, objectToReuse.GetComponent(_pools[poolKey].Type));
				}
				_poolObjects.Add(objectToReuse, poolObject);

				objectToReuse.name = prefab.name;
				_poolObjects[objectToReuse].Transform.SetParent(_pools[poolKey].Parent, false);
			}
			else
			{
				objectToReuse = _pools[poolKey].Objects.Dequeue();
				objectToReuse.SetActive(active);
			}

			return objectToReuse;
		}

		Debug.LogError("Object not registered for pooling! " + prefab.name, gameObject);
		return null;
	}

	public void ExtendPool(GameObject prefab, int count)
	{
		string poolKey = prefab.name;

		if (_pools.ContainsKey(poolKey))
		{
			Transform poolParent = _pools[poolKey].Parent;
			poolParent.SetParent(transform);

			for (int i = 0; i < count; i++)
			{
				GameObject newObject = GameObject.Instantiate(prefab);

				newObject.SetActive(false);

				PoolObject poolObject;
				if (_pools[poolKey].Type == null)
				{
					poolObject = new PoolObject(newObject.transform, null);
				}
				else
				{
					poolObject = new PoolObject(newObject.transform, newObject.GetComponent(_pools[poolKey].Type));
				}
				_poolObjects.Add(newObject, poolObject);
				newObject.name = prefab.name;

				_pools[poolKey].Objects.Enqueue(newObject);
				_poolObjects[newObject].Transform.SetParent(poolParent, false);
			}
		}
		else
		{
			Debug.LogWarning("Pool " + poolKey + " does not exists!", gameObject);
		}
	}

	public void PoolIn(GameObject obj)
	{
		if (_pools[obj.name].Objects.Contains(obj))
		{
			Debug.LogError("Duplicate pool in!", obj);
			return;
		}

		obj.SetActive(false);
		_pools[obj.name].Objects.Enqueue(obj);
		_poolObjects[obj].Transform.SetParent(_pools[obj.name].Parent);
	}

	public bool PoolExists(GameObject prefab)
	{
		return _pools.ContainsKey(prefab.name);
	}

	public int PoolSize(GameObject prefab)
	{
		string poolKey = prefab.name;

		if (_pools.ContainsKey(poolKey))
		{
			return _pools[poolKey].Objects.Count;
		}
		return 0;
	}

	public int PoolSizeActive(GameObject prefab)
	{
		string poolKey = prefab.name;

		int count = 0;

		if (_pools.ContainsKey(poolKey))
		{
			GameObject[] array = _pools[poolKey].Objects.ToArray();

			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].activeSelf)
				{
					count++;
				}
			}
		}

		return count;
	}
}