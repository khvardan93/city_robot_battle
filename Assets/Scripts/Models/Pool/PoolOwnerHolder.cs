using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RobotBattle
{
    public class PoolOwnerHolder : IDestructible
    {
        private readonly Dictionary<Type, PoolGroupHolder> _poolGroups = new ();
        private readonly Transform _holder;

        public PoolOwnerHolder(Transform holder)
        {
            _holder = holder;
        }
        
        public PoolGroupHolder AddGroup<T>(T prefab, int prepCount) where T : IPoolObject
        {
            var type = typeof(T);
            
            if(_poolGroups.TryGetValue(type, out var group))
            {
                return group;
            }
            
            var newHolder = new GameObject(type.Name).transform;
            newHolder.SetParent(_holder);
            group = new PoolGroupHolder(newHolder, prefab, prepCount);
            _poolGroups.Add(type, group);
            return group;
        }
        
        public void Destruct()
        {
            foreach (var group in _poolGroups)
            {
                group.Value.Destruct();
                _poolGroups.Remove(group.Key);
            }
            
            Object.Destroy(_holder.gameObject);
        }
    }
}
