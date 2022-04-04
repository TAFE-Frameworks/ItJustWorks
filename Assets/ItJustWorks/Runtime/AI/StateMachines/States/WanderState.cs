using UnityEngine;

namespace ItJustWorks.AI.StateMachines.States
{
	public class WanderState
	{
		public WanderState(StateMachine _machine)
		{
			// _machine.RegisterState("Wander", Run)
			_machine.RegisterState(StateMachine.GetStateID(typeof(WanderState)), Run);
		}

		private void Run(StateMachine _machine)
		{
			// Do the wandering functionality
			_machine.Agent.transform.position = Random.insideUnitSphere * 5;
		}
	}
}