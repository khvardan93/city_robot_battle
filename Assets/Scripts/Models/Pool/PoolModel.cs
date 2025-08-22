using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RobotBattle
{
    public class PoolGroup : IDestructible
    {
        private readonly Transform _holder;
        private readonly Queue<IPoolObject> _pool = new();
        private readonly int _prepCount;
        private readonly IPoolObject _prefab;
        
        public PoolGroup(Transform holder, IPoolObject prefab, int prepCount)
        {
            _prepCount = prepCount;
            _holder = holder;
            _prefab = prefab;
            
            Prepare(holder, prefab, prepCount);
        }

        private void Prepare(Transform holder, IPoolObject prefab, int prepCount)
        {
            for (var i = 0; i < prepCount; i++)
            {
                var newItem = prefab.Clone(holder);
                newItem.RegisterPoolGroup(this);
                newItem.SetActive(false);
                _pool.Enqueue(newItem);
            }
        }
        
        public void Add(IPoolObject item)
        {
            item.SetActive(false);
            item.SetParent(_holder);
            _pool.Enqueue(item);
        }

        public T Get<T>() where T : IPoolObject 
        {
            if(_pool.Count == 0)
                Prepare(_holder, _prefab, _prepCount);
            
            return (T)_pool.Dequeue();
        }

        public void Destruct()
        {
            while (_pool.Count > 0)
            {
                _pool.Dequeue().Destroy();
            }
            
            Object.Destroy(_holder.gameObject);
        }
    }

    public class PoolModel : BaseModel, IDestructible
    {
        private PoolHolder _poolHolder;
        private readonly Dictionary<Type, PoolGroup> _poolGroups = new ();
        
        public PoolModel(System system) : base(system)
        {
        }

        public void AddHolder(PoolHolder poolHolder)
        {
            _poolHolder = poolHolder;
        }

        public PoolGroup AddGroup<T>(T prefab, int prepCount) where T : IPoolObject
        {
            var type = typeof(T);
            
            if(_poolGroups.TryGetValue(type, out var group))
            {
                return group;
            }
            
            var newHolder = new GameObject(type.Name).transform;
            newHolder.SetParent(_poolHolder.transform);
            group = new PoolGroup(newHolder, prefab, prepCount);
            _poolGroups.Add(type, group);
            return group;
        }

        void IDestructible.Destruct()
        {
            foreach (var group in _poolGroups)
            {
                group.Value.Destruct();
                _poolGroups.Remove(group.Key);
            }
        }
    }
}