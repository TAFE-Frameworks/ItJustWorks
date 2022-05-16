using System;

using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	public struct AStarPathRequest
	{
		public readonly Vector3 start;
		public readonly Vector3 end;
		public readonly Action<AStarPath, bool> callback;

		public AStarPathRequest(Vector3 _start, Vector3 _end, Action<AStarPath, bool> _callback)
		{
			start = _start;
			end = _end;
			callback = _callback;
		}
	}
}