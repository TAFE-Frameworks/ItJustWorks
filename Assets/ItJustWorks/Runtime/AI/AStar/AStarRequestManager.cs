using System.Collections.Generic;
using System.Threading;

using UnityEngine;

namespace ItJustWorks.AI.AStar
{
	[RequireComponent(typeof(AStarPathfinding))]
	public class AStarRequestManager : Singleton<AStarRequestManager>
	{
		private readonly Queue<AStarPathResult> results = new Queue<AStarPathResult>();

		private AStarPathfinding aStar;

		public static void RequestPath(AStarPathRequest _request)
		{
			ThreadStart start = () => Instance.aStar.FindPath(_request, Instance.OnPathFinishedProcessing);
			start.Invoke();
		}
		
		private void Awake()
		{
			// This is just to initialise the singleton, it won't be used
			// ReSharper disable once UnusedVariable
			AStarRequestManager instance = Instance;

			aStar = gameObject.GetComponent<AStarPathfinding>();
		}

		private void Update()
		{
			if(results.Count > 0)
			{
				int itemsInQueue = results.Count;
				lock(results)
				{
					for(int i = 0; i < itemsInQueue; i++)
					{
						AStarPathResult result = results.Dequeue();
						result.callback(result.path, result.success);
					}
				}
			}
		}

		private void OnPathFinishedProcessing(AStarPathResult _result)
		{
			lock(results)
			{
				results.Enqueue(_result);
			}
		}
	}
}