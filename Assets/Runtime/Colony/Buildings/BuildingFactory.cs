using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Runtime.Colony.Buildings
{
    public sealed class BuildingFactory
    {
        private readonly Dictionary<string, Func<int, Vector2, BuildingDescription, BuildingModel>> _constructors 
            = new();

        public void Register<T>(string type, 
            Func<int, Vector2, BuildingDescription, T> ctor) 
            where T : BuildingModel
        {
            _constructors[type] = ctor;
        }

        public BuildingModel Create(string type, int id, Vector2 pos, BuildingDescription desc)
        {
            return _constructors[type](id, pos, desc);
        }
    }
}