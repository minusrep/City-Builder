using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    [Serializable]
    public class CitizenModel
    {
        public int Id;

        public string Name;

        public Vector2 Position;

        public Dictionary<string, ResourceModel> Needs;

        public float MoveSpeed;

        private CitizenStateMachine _stateMachine;

        public CitizenModel(Dictionary<string, CitizenStateDescription> stateDescriptions)
        {
            _stateMachine = new CitizenStateMachine(stateDescriptions);
        }
    }
}