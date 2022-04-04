using UnityEngine;

namespace ItJustWorks.AI.Obselete
{
	public class WanderClass
	{
		public WanderClass(StateMachine _state)
		{
			_state.RegisterState("Wander", Run);
		}
		
		private bool Run(StateMachine _stateMachine)
		{
			//Debug.Log("Wander");
			return true;
		}
	}
}