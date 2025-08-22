using UnityEngine;

namespace RobotBattle
{
    public class SingleObjectBase<T> : MonoBehaviour, ISingleObject where T : ISingleObject
    {
        protected virtual void Start()
        {
            Register();
        }

        protected virtual void OnDestroy()
        {
            Unregister();
        }

        private void Register()
        {
            System.Instance.ObjectRegister.Register<T>(this);
        }

        private void Unregister()
        {
            System.Instance.ObjectRegister.Unregister<T>();
        }
    }
}
