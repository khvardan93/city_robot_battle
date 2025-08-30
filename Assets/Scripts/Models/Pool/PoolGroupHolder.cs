using System.Collections.Generic;
using UnityEngine;

namespace RobotBattle
{
    public class PoolGroupHolder : IDestructible
    {
        private readonly Transform _holder;
        private readonly Queue<IPoolObject> _pool = new();
        private readonly int _prepCount;
        private readonly IPoolObject _prefab;
        
        public PoolGroupHolder(Transform holder, IPoolObject prefab, int prepCount)
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
}
