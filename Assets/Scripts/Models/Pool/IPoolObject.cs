using UnityEngine;

namespace RobotBattle
{
    public interface IPoolObject
    {
        public IPoolObject Clone(Transform parent);
        public void SetActive(bool active);
        public void RegisterPoolGroup(PoolGroup poolGroup);
        public void Release();
        public void SetParent(Transform parent);
        public void Destroy();
    }
}