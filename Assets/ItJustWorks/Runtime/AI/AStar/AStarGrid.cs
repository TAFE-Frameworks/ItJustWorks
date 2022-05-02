using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	[Serializable]
	public class AStarGrid : IEnumerable<AStarNode>
	{
		public int MaxSize => gridSize.x * gridSize.y;
		public float NodeDiameter => nodeRadius * 2;
		public Vector2 WorldGridSize => worldGridSize;
		public Vector2Int GridSize => gridSize;
		public bool Ready { get; private set; }
		
		[SerializeField] private Vector2 worldGridSize;
		[SerializeField] private float nodeRadius;
		[SerializeField] private LayerMask unwalkableLayers;

		private AStarNode[,] grid;
		private Vector2Int gridSize;

		public void Prepare()
		{
			gridSize = new Vector2Int
			{
				x = Mathf.RoundToInt(worldGridSize.x / NodeDiameter),
				y = Mathf.RoundToInt(worldGridSize.y / NodeDiameter)
			};

			Ready = true;
		}

		public void CreateGrid(Transform _manager)
		{
			grid = new AStarNode[gridSize.x, gridSize.y];
			Vector3 worldBottomLeft = _manager.position - Vector3.right * worldGridSize.x / 2 - Vector3.forward * worldGridSize.y / 2;

			for(int x = 0; x < gridSize.x; x++)
			{
				for(int y = 0; y < gridSize.y; y++)
				{
					Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter + nodeRadius) + Vector3.forward * (y * NodeDiameter + nodeRadius);

					bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableLayers);
					grid[x, y] = new AStarNode(walkable, worldPoint, new Vector2Int(x, y));
				}
			}
		}

		public List<AStarNode> GetNeighbours(AStarNode _node)
		{
			List<AStarNode> neighbours = new List<AStarNode>();

			for(int x = -1; x <= 1; x++)
			{
				for(int y = -1; y <= 1; y++)
				{
					if(x == 0 && y == 0)
						continue;

					int checkX = _node.gridPos.x + x;
					int checkY = _node.gridPos.y + y;
					AStarNode neighbour = GetNode(checkX, checkY);
					if(neighbour != null)
						neighbours.Add(neighbour);
				}
			}
			
			return neighbours;
		}

		public AStarNode GetNode(int _x, int _y)
		{
			if(_x >= 0 && _x < gridSize.x && _y >= 0 && _y < gridSize.y)
				return grid[_x, _y];

			return null;
		}

		public AStarNode NodeFromWorldPoint(Vector3 _worldPos)
		{
			float percentX = Mathf.Clamp01((_worldPos.x + worldGridSize.x / 2) / worldGridSize.x);
			float percentY = Mathf.Clamp01((_worldPos.z + worldGridSize.y / 2) / worldGridSize.y);

			int x = Mathf.RoundToInt((gridSize.x - 1) * percentX);
			int y = Mathf.RoundToInt((gridSize.y - 1) * percentY);

			return GetNode(x, y);
		}

		public IEnumerator<AStarNode> GetEnumerator() => new AStarGridEnumerator(this);
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
	
	public class AStarGridEnumerator : IEnumerator<AStarNode>
	{
		object IEnumerator.Current => Current;
		public AStarNode Current { get; private set; }

		private readonly AStarGrid grid;
		private int index = -1;
		
		public AStarGridEnumerator(AStarGrid _grid)
		{
			grid = _grid;
			Current = grid.GetNode(0, 0);
		}

		public bool MoveNext()
		{
			index++;
			int x = index % grid.GridSize.x;
			int y = index / grid.GridSize.x;
			Current = grid.GetNode(x, y);
			return Current != null;
		}

		public void Reset()
		{
			index = -1;
			Current = null;
		}

		public void Dispose() => GC.SuppressFinalize(this);
	}
}