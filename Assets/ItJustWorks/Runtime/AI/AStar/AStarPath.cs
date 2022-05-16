using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	public class AStarPath
	{
		private readonly Vector3[] path;

		private int index;

		public AStarPath(Vector3[] _path)
		{
			path = _path;
			index = 0;
		}

		public bool Progress(AStarAgent _agent)
		{
			// TODO: Add your logic here to follow the path and if it reaches the end of the path return false
			if(index >= path.Length)
				return false;
			
			Vector3 currentNode = path[index];
			Vector3 position = Vector3.MoveTowards(_agent.Position, currentNode, _agent.moveSpeed * Time.deltaTime);
			
			if(_agent.Position == currentNode)
				ProgressToNextNode();

			_agent.Position = position;
			
			return true;
		}

		private void ProgressToNextNode()
		{
			index++;
		}

		public void Visualise(LineRenderer _pathRenderer)
		{
			_pathRenderer.positionCount = path.Length;

			for(int i = 0; i < path.Length; i++)
			{
				_pathRenderer.SetPosition(i, path[i]);
			}
		}
	}
}