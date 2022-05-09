using System.Collections.Generic;

using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	public class AStarPath
	{
		private readonly List<Vector3> path;

		private int index;
		private float progress;

		public AStarPath()
		{
			path = new List<Vector3>();
			index = 0;
			progress = 0;
		}

		public void Add(Vector3 _node) => path.Add(_node);

		public void Reverse() => path.Reverse();

		private void ProgressToNextNode()
		{
			index++;
			progress = 0;
		}

		public void Visualise(LineRenderer _pathRenderer)
		{
			_pathRenderer.positionCount = path.Count;

			for(int i = 0; i < path.Count; i++)
			{
				_pathRenderer.SetPosition(i, path[i]);
			}
		}
	}
}