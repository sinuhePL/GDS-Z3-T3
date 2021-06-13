using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "newState", menuName = "Scriptable Objects/AI/State")]
    public class State : ScriptableObject
    {
        public Action _enterAction;
        public Action[] _actions;
        public Transition[] _transitions;
        public Color _stateGizmoColor = Color.grey;

        private void CheckTransitions(CharacterBrain brain)
        {
            foreach(Transition transition in _transitions)
            {
                bool conditionFulfilled = transition._decision.Decide(brain);

                if (conditionFulfilled && transition._changeStateWhenResult == BoolEnum.True || !conditionFulfilled && transition._changeStateWhenResult == BoolEnum.False)
                {
                    brain.TransitionToState(transition._targetState);
                }
            }
        }

        public void UpdateState(CharacterBrain brain)
        {
            foreach(Action action in _actions)
            {
                action.Act(brain);
            }
            CheckTransitions(brain);
        }

        public void FixedUpdateState(CharacterBrain brain)
        {
            foreach (Action action in _actions)
            {
                action.ActFixed(brain);
            }
        }

        public void OnEnterState(CharacterBrain brain)
        {
            if (_enterAction != null)
            {
                _enterAction.Act(brain);
                _enterAction.ActFixed(brain);
            }
        }
    }
}
