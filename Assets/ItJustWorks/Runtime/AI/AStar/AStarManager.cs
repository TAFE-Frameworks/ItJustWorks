using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	public class AStarManager : Singleton<AStarManager>
	{
		[SerializeField] private AStarGrid grid = new AStarGrid();
		
		private void Awake()
		{
			grid.Prepare();
			grid.CreateGrid(transform);
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(grid.WorldGridSize.x, 1, grid.WorldGridSize.y));

			if(grid.Ready)
			{
				foreach(AStarNode node in grid)
				{
					Gizmos.color = node.walkable ? Color.green : Color.red;
					Gizmos.DrawCube(node.position, Vector3.one * (grid.NodeDiameter - .1f));
				}
			}
		}
	}
}