using System;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public abstract class Building
    {
        public int Id;
        public Vector2 Position;
    }
}