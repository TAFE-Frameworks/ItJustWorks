using ItJustWorks.Heaping;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	public class AStarManager : Singleton<AStarManager>
	{
		[SerializeField] private AStarGrid grid = new AStarGrid();

		#region FUNCTIONALITY

		public AStarPath FindPath(Vector3 _startPos, Vector3 _endPos)
		{
			AStarNode startNode = grid.NodeFromWorldPoint(_startPos);
			AStarNode endNode = grid.NodeFromWorldPoint(_endPos);

			Heap<AStarNode> openSet = new Heap<AStarNode>(grid.MaxSize);
			HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
			openSet.Add(startNode);

			while(openSet.Count > 0)
			{
				AStarNode current = openSet.RemoveFirst();
				closedSet.Add(current);

				// We have found a valid path to the end node
				if(current == endNode)
					return RetracePath(startNode, endNode);

				// Visit all neighbours to this node
				foreach(AStarNode neighbour in grid.GetNeighbours(current))
				{
					// Check if we have already visited this neighbour, or we can't walk on it
					if(!neighbour.walkable || closedSet.Contains(neighbour))
						continue;

					int newMovementCost = current.gCost + GetDistance(current, neighbour);
					if(newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
					{
						neighbour.gCost = newMovementCost;
						neighbour.hCost = GetDistance(neighbour, endNode);
						neighbour.parent = current;
						
						if(!openSet.Contains(neighbour))
							openSet.Add(neighbour);
					}
				}
			}

			// Failed to find a valid path, so throw an error and return null
			Debug.LogException(new InvalidOperationException($"Unable to find path to {_endPos}"));
			return null;
		}

		private int GetDistance(AStarNode _start, AStarNode _end)
		{
			int dstX = Mathf.Abs(_start.gridPos.x - _end.gridPos.x);
			int dstY = Mathf.Abs(_start.gridPos.y - _end.gridPos.y);

			if(dstX > dstY)
				return 14 * dstY + 10 * (dstX - dstY);

			return 14 * dstX + 10 * (dstY - dstX);
		}

		private AStarPath RetracePath(AStarNode _start, AStarNode _end)
		{
			AStarPath path = new AStarPath();

			AStarNode current = _end;
			while(current != _start)
			{
				path.Add(current);
				current = current.parent;
			}
			
			path.Reverse();
			return path;
		}

		#endregion
		
		#region UNITY MESSAGES

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
					Gizmos.DrawCube(node.position, Vector3.one * (grid.NodeDiameter / 4));
				}
			}
		}

		#endregion
	}
}