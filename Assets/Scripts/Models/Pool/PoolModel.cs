using System.Collections.Generic;
using UnityEngine;

namespace RobotBattle
{
    public enum PoolOwner
    {
        Generic = 0,
        Player = 1,
        OrangeSpider = 2, 
    }

    public class PoolModel : BaseModel, IDestructible
    {
        private PoolHolder _poolHolder;
        private readonly Dictionary<PoolOwner, PoolOwnerHolder> _poolOwners = new ();
        
        public PoolModel(System system) : base(system)
        {
        }

        public void AddHolder(PoolHolder poolHolder)
        {
            _poolHolder = poolHolder;
        }

        public PoolGroupHolder AddGroup<T>(T prefab, PoolOwner owner, int prepCount) where T : IPoolObject
        {
            var ownerHolder = GetOwner(owner);
            return ownerHolder.AddGroup(prefab, prepCount);
        }

        private PoolOwnerHolder GetOwner(PoolOwner owner)
        {
            if (_poolOwners.TryGetValue(owner, out var holder))
            {
                return holder;
            }
            
            var holderTransform = new GameObject(owner.ToString()).transform;
            holder = new(holderTransform);
            _poolOwners.Add(owner, holder);
            return holder;
        }

        void IDestructible.Destruct()
        {
            foreach (var group in _poolOwners)
            {
                group.Value.Destruct();
                _poolOwners.Remove(group.Key);
            }
        }
    }
}