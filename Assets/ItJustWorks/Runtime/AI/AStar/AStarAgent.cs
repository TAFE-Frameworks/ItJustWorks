using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	public class AStarAgent : MonoBehaviour
	{
		public Vector3 Position
		{
			get => transform.position;
			set => transform.position = value;
		}

		public float moveSpeed;

		private AStarPath path;

		private void Update()
		{
			if(Input.GetMouseButtonDown(0) && 
			   Physics.Raycast(Camera.main!.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
			{
				AStarPathRequest request = new AStarPathRequest(Position, hit.point, (_path, _success) =>
				{
					if(_success)
						path = _path;
				});
				AStarRequestManager.RequestPath(request);
			}
			
			if(path != null)
			{
				if(!path.Progress(this))
					path = null;
			}
		}
	}
}