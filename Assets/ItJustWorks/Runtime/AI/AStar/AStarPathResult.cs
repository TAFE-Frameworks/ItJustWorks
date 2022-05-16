using System;

namespace ItJustWorks.AI.AStar
{
	public struct AStarPathResult
	{
		public readonly AStarPath path;
		public readonly bool success;
		public readonly Action<AStarPath, bool> callback;

		public AStarPathResult(AStarPath _path, bool _success, Action<AStarPath, bool> _callback)
		{
			path = _path;
			success = _success;
			callback = _callback;
		}
	}
}