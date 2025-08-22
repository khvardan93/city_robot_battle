using UnityEngine;

namespace RobotBattle
{
    public class BasePoolObject : MonoBehaviour, IPoolObject
    {
        private PoolGroup _poolGroup;
        
        IPoolObject IPoolObject.Clone(Transform parent)
        {
            return Instantiate(this, parent);
        }

        void IPoolObject.SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        void IPoolObject.RegisterPoolGroup(PoolGroup poolGroup)
        {
            _poolGroup = poolGroup;
        }

        void IPoolObject.SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        void IPoolObject.Destroy()
        {
            Destroy(gameObject);
        }

        public virtual void Release()
        {
            gameObject.SetActive(false);
            _poolGroup.Add(this);
        }
    }
}
