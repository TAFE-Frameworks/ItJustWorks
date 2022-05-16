using ItJustWorks.AI.AStar;

using UnityEngine;

namespace ItJustWorks.Samples.AI.Pathfinding
{
	public class AStarSample : MonoBehaviour
	{
		[SerializeField] private LineRenderer pathRenderer;

		private Vector3 initialClick = Vector3.positiveInfinity;

		private void Update()
		{
			if(Input.GetMouseButtonDown(0))
			{
				if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
				{
					if(initialClick == Vector3.positiveInfinity)
					{
						initialClick = hit.point;
					}
					else
					{
						//AStarManager.Instance.FindPath(initialClick, hit.point).Visualise(pathRenderer);
						initialClick = Vector3.positiveInfinity;
					}
				}
			}
		}
	}
}