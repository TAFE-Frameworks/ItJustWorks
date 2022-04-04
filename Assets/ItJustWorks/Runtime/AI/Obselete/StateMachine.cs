using System;
using System.Collections.Generic;

using UnityEngine;

namespace ItJustWorks.AI.Obselete
{
	public delegate bool StateDelegate(StateMachine _machine);

	public class StateMachine : MonoBehaviour
	{
		// This is a dictionary of functions that match the delegate format
		private Dictionary<string, StateDelegate> states = new Dictionary<string, StateDelegate>();
		private string state;

		private WanderClass wanderer;

		public void RegisterState(string _id, StateDelegate _function)
		{
			states.Add(_id, _function);
		}

		private void Start()
		{
			states.Add("Target", TargetState);
			states.Add("Flee", FleeState);
			wanderer = new WanderClass(this);

			state = "Wander";
		}

		private void Update()
		{
			print(states[state].Invoke(this));

			if(Input.GetKeyDown(KeyCode.T))
				state = "Target";

			if(Input.GetKeyDown(KeyCode.F))
				state = "Flee";

			if(Input.GetKeyDown(KeyCode.W))
				state = "Wander";
		}

		private bool TargetState(StateMachine _state)
		{
			Debug.Log("Target State");
			return true;
		}

		private bool FleeState(StateMachine _state)
		{
			Debug.Log("Flee State");
			return true;
		}

		private bool WanderState(StateMachine _state)
		{
			Debug.Log("Wander State");
			return true;
		}
	}
}