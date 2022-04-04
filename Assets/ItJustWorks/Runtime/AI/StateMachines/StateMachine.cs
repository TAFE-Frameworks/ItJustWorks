using System;
using System.Collections.Generic;

using UnityEngine;

namespace ItJustWorks.AI.StateMachines
{
	public class StateMachine
	{
		public delegate void StateDelegate(StateMachine _machine);

		public static string GetStateID(Type _type)
		{
			return _type.Name.Replace("State", "");
		}
		
		public StateMachineAgent Agent { get; private set; }

		private readonly Dictionary<string, StateDelegate> states = new Dictionary<string, StateDelegate>();
		private string currentState;

		public void Init(StateMachineAgent _agent)
		{
			Agent = _agent;

			// Any other initialisation you need to do
		}

		public void Process()
		{
			if(states.TryGetValue(currentState, out StateDelegate state))
			{
				state.Invoke(this);
			}
			else
			{
				Debug.LogWarning($"State: <{currentState}> is not valid.");
			}
		}

		public bool ChangeState(string _newState)
		{
			if(states.ContainsKey(_newState))
			{
				currentState = _newState;
				return true;
			}

			Debug.LogWarning($"No state matching: <{_newState}>");
			return false;
		}

		public void RegisterState(string _id, StateDelegate _processor)
		{
			if(states.ContainsKey(_id))
			{
				states[_id] += _processor;
			}
			else
			{
				states.Add(_id, _processor);
			}
		}
	}
}