using UnityEngine;

namespace RobotBattle
{
    public class BasePoolObject : MonoBehaviour, IPoolObject
    {
        private PoolGroupHolder _poolGroupHolder;
        
        IPoolObject IPoolObject.Clone(Transform parent)
        {
            return Instantiate(this, parent);
        }

        void IPoolObject.SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        void IPoolObject.RegisterPoolGroup(PoolGroupHolder poolGroupHolder)
        {
            _poolGroupHolder = poolGroupHolder;
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
            _poolGroupHolder.Add(this);
        }
    }
}
