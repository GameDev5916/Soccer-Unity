using UnityEngine;
using System.Collections.Generic;

namespace Pooling
{
	public static class SimplePool
	{
		private const int DEFAULT_POOL_SIZE = 3;

		private static Dictionary<GameObject, Pool> pools;

		private static void Initialize(GameObject prefab = null, int qty = DEFAULT_POOL_SIZE)
		{
			if (pools == null)
			{
				pools = new Dictionary<GameObject, Pool>();
			}

			if (prefab != null && !pools.ContainsKey(prefab))
			{
				pools[prefab] = new Pool(prefab, qty);
			}
		}

		public static void Preload(GameObject prefab, int qty = 1)
		{
			Initialize(prefab, qty);

			GameObject[] obs = new GameObject[qty];
			for (int i = 0; i < qty; i++)
			{
				obs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
			}

			for (int i = 0; i < qty; i++)
			{
				Despawn(obs[i]);
			}
		}

		public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			Initialize(prefab);

			return pools[prefab].Spawn(pos, rot);
		}

		public static void Despawn(GameObject obj)
		{
			PoolMember pm = obj.GetComponent<PoolMember>();
			if (pm == null)
			{
				GameObject.Destroy(obj);
			}
			else
			{
				pm.myPool.Despawn(obj);
			}
		}

	}
}

