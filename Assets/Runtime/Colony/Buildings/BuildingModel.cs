using System;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public abstract class BuildingModel
    {
        public int Id { get; }
        public Vector2 Position { get; }

        protected BuildingModel(int id, Vector2 position)
        {
            Id = id;    
            Position = position;
        }
    }
}