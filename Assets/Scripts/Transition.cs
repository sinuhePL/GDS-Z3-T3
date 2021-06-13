using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public enum BoolEnum {True, False }

    [System.Serializable]
    public class Transition
    {
        public Decision _decision;
        public State _targetState;
        public BoolEnum _changeStateWhenResult;
    }
}
