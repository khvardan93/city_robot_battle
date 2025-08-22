using System.Collections;
using RobotBattle.Generic;
using UnityEngine;

namespace RobotBattle.UI.Generic
{
    public abstract class AbstractUIScript<TPageEngine, TPageBaseType> : MonoBehaviour
        where TPageBaseType : AbstractUIScript<TPageEngine, TPageBaseType>
        where TPageEngine : BaseMonoBehaviourSingleton<TPageEngine>
    {
        protected PageEngineScript<TPageEngine, TPageBaseType> PageEngine;

        public virtual bool IsVisible => gameObject.activeSelf;

        public virtual void Show()
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            if (gameObject.activeSelf) gameObject.SetActive(false);
        }

        public void SetPageEngine(PageEngineScript<TPageEngine, TPageBaseType> pageEngineScript)
        {
            PageEngine = pageEngineScript;
        }

        public virtual bool IsPageVisible<T>() where T : AbstractUIScript<TPageEngine, TPageBaseType>
        {
            return PageEngine.GetPage<T>().IsVisible;
        }

        public virtual void ShowPage<T>() where T : AbstractUIScript<TPageEngine, TPageBaseType>
        {
            PageEngine.Show<T>();
        }

        public virtual void HidePage<T>() where T : AbstractUIScript<TPageEngine, TPageBaseType>
        {
            PageEngine.Hide<T>();
        }

        public virtual void ChangePage<T>() where T : AbstractUIScript<TPageEngine, TPageBaseType>
        {
            PageEngine.Change<T>();
        }

        protected virtual void GoBack()
        {
            PageEngine.GoBack();
        }
    }
}
