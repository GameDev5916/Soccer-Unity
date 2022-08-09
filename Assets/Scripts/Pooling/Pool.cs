using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class Pool
    {
		int nextId = 1;

		private Stack<GameObject> inactive;

		private GameObject prefab;

		public Pool(GameObject prefab, int initialQty)
		{
			this.prefab = prefab;

			inactive = new Stack<GameObject>(initialQty);
		}

		public GameObject Spawn(Vector3 pos, Quaternion rot)
		{
			GameObject obj;
			if (inactive.Count == 0)
			{
				obj = GameObject.Instantiate(prefab, pos, rot);
				obj.name = prefab.name + " (" + (nextId++) + ")";

				obj.AddComponent<PoolMember>().myPool = this;
			}
			else
			{
				obj = inactive.Pop();

				if (obj == null)
				{
					return Spawn(pos, rot);
				}
			}

			obj.transform.position = pos;
			obj.transform.rotation = rot;
			obj.SetActive(true);

			return obj;
		}

		public void Despawn(GameObject obj)
		{
			obj.SetActive(false);

			inactive.Push(obj);
		}
	}
}


