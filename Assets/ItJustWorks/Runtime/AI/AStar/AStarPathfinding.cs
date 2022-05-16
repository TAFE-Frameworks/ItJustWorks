using ItJustWorks.Heaping;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	public class AStarPathfinding : MonoBehaviour
	{
		[SerializeField] private AStarGrid grid = new AStarGrid();

		#region FUNCTIONALITY

		public void FindPath(AStarPathRequest _request, Action<AStarPathResult> _callback)
		{
			Vector3[] nodes = Array.Empty<Vector3>();
			bool pathFound = false;

			AStarNode startNode = grid.NodeFromWorldPoint(_request.start);
			AStarNode endNode = grid.NodeFromWorldPoint(_request.end);

			if(startNode.walkable && endNode.walkable)
			{
				Heap<AStarNode> openSet = new Heap<AStarNode>(grid.MaxSize);
				HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
				openSet.Add(startNode);

				while(openSet.Count > 0)
				{
					AStarNode current = openSet.RemoveFirst();
					closedSet.Add(current);

					// We have found a valid path to the end node
					if(current == endNode)
					{
						pathFound = true;
						break;
					}

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
			}

			if(pathFound)
			{
				nodes = RetracePath(startNode, endNode);
				pathFound = nodes.Length > 0;
			}

			_callback(new AStarPathResult(new AStarPath(nodes), pathFound, _request.callback));
		}

		private int GetDistance(AStarNode _start, AStarNode _end)
		{
			int dstX = Mathf.Abs(_start.gridPos.x - _end.gridPos.x);
			int dstY = Mathf.Abs(_start.gridPos.y - _end.gridPos.y);

			if(dstX > dstY)
				return 14 * dstY + 10 * (dstX - dstY);

			return 14 * dstX + 10 * (dstY - dstX);
		}

		private Vector3[] RetracePath(AStarNode _start, AStarNode _end)
		{
			List<AStarNode> path = new List<AStarNode>();

			AStarNode current = _end;
			while(current != _start)
			{
				path.Add(current);
				current = current.parent;
			}

			Vector3[] nodes = SimplifyPath(path);
			Array.Reverse(nodes);
			
			return nodes;
		}

		private Vector3[] SimplifyPath(List<AStarNode> _nodes)
		{
			List<Vector3> newPath = new List<Vector3>();
			Vector2 directionOld = Vector2.zero;

			for(int i = 1; i < _nodes.Count; i++)
			{
				Vector2 directionNew = new Vector2
				{
					x = _nodes[i - 1].gridPos.x - _nodes[i].gridPos.x,
					y = _nodes[i - 1].gridPos.y - _nodes[i].gridPos.y
				};
				
				if(directionNew != directionOld)
					newPath.Add(_nodes[i]);

				directionOld = directionNew;
			}

			return newPath.ToArray();
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