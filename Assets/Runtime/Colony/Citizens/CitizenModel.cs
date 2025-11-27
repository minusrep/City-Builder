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

        public Dictionary<string, ResourceModel> Needs;

        public Vector2 Position;

        public float MoveSpeed;

        private CitizenStateMachine _stateMachine;

        public CitizenModel(Dictionary<string, CitizenStateDescription> stateDescriptions)
        {
            _stateMachine = new CitizenStateMachine(stateDescriptions);
        }
    }
}