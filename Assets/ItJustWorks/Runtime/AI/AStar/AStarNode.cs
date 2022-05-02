using ItJustWorks.Heaping;

using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	public class AStarNode : IHeapItem<AStarNode>
	{
		public int HeapIndex { get; set; }
		public int FCost => gCost + hCost;

		public readonly bool walkable;
		public readonly Vector3 position;
		public Vector2Int gridPos;

		public int gCost;
		public int hCost;
		public AStarNode parent;

		public AStarNode(bool _walkable, Vector3 _position, Vector2Int _gridPos)
		{
			walkable = _walkable;
			position = _position;
			gridPos = _gridPos;
		}

		public int CompareTo(AStarNode _other)
		{
			int comparison = FCost.CompareTo(_other.FCost);
			if(comparison == 0)
				comparison = hCost.CompareTo(_other.hCost);

			return -comparison;
		}

		public static implicit operator Vector3(AStarNode _node) => _node.position;
	}
}