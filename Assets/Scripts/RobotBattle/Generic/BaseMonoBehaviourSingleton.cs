using UnityEngine;

namespace RobotBattle.Generic
{
    public class BaseMonoBehaviourSingleton<T> : MonoBehaviour where T : BaseMonoBehaviourSingleton<T>
    {
        public static T Instance { private set; get; }

        protected virtual void Awake()
        {
            Instance = (T)this;
        }
    }
}