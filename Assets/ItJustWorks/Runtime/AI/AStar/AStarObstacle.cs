using ItJustWorks.Threading.Physics;

using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	[RequireComponent(typeof(BoxCollider))]
	public sealed class AStarObstacle : MonoBehaviour
	{
		private new BoxCollider collider;
		
		private void Awake()
		{
			collider = gameObject.AddComponent<BoxCollider>();
			
			ThreadedPhysics.RegisterBounds(collider);
		}

		private void Update()
		{
			ThreadedPhysics.UpdateBounds(collider);
		}

		private void OnDestroy()
		{
			ThreadedPhysics.DeregisterBounds(gameObject.name);
		}
	}
}