using ItJustWorks.AI.StateMachines.States;

using UnityEngine;

namespace ItJustWorks.AI.StateMachines
{
	public class StateMachineAgent : MonoBehaviour
	{
		public readonly StateMachine stateMachine = new StateMachine();

		private WanderState wander;
		
		private void Awake()
		{
			stateMachine.Init(this);

			wander = new WanderState(stateMachine);
			
			// Set the initial state to the wander state
			stateMachine.ChangeState(StateMachine.GetStateID(typeof(WanderState)));
		}

		private void Update()
		{
			stateMachine.Process();
		}
	}
}