using System.Collections.Generic;

using UnityEngine;

namespace ItJustWorks.Threading.Physics
{
	public static class ThreadedPhysics
	{
		private static readonly Dictionary<string, OBB> sceneBounds = new Dictionary<string, OBB>();

		public static void RegisterBounds(BoxCollider _collider)
		{
			if(!sceneBounds.TryGetValue(_collider.gameObject.name, out OBB _))
			{
				OBB threadedBounds = new OBB(_collider);
				sceneBounds.Add(_collider.gameObject.name, threadedBounds);
			}
		}

		public static void UpdateBounds(BoxCollider _collider)
		{
			if(sceneBounds.TryGetValue(_collider.gameObject.name, out OBB threaded))
			{
				threaded.Update(_collider);
			}
		}

		public static void DeregisterBounds(string _id)
		{
			if(sceneBounds.ContainsKey(_id))
				sceneBounds.Remove(_id);
		}

		public static bool OverlapsAny(Vector3 _point, float _radius)
		{
			foreach(OBB bound in sceneBounds.Values)
			{
				if(bound.Overlaps(_point, _radius))
					return true;
			}

			return false;
		}
	}
}