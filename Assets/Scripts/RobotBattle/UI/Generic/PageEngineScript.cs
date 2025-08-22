using UnityEngine;
using RobotBattle.Generic;

namespace RobotBattle.UI.Generic
{
    public class PageEngineScript<TPageEngine, TPageBaseType> : BaseMonoBehaviourSingleton<TPageEngine> 
        where TPageEngine : BaseMonoBehaviourSingleton<TPageEngine>
        where TPageBaseType : AbstractUIScript<TPageEngine,TPageBaseType>
    {
        private TPageBaseType[] _pages;

        private TPageBaseType _lastPage;
        private TPageBaseType _currentPage;

        protected override void Awake()
        {
            base.Awake();

            _pages = GetComponentsInChildren<TPageBaseType>(true);

            foreach (var page in _pages)
            {
                page.SetPageEngine(this);
            }

            foreach (var page in _pages)
            {
                if (!page.gameObject.activeSelf) continue;
                _currentPage = page;
                break;
            }
        }

        public void Change<T>() where T : AbstractUIScript<TPageEngine, TPageBaseType>
        {
            _lastPage = _currentPage;

            foreach (var page in _pages)
            {
                if (page is T)
                {
                    _currentPage = page;
                    page.Show();
                }
                else
                {
                    page.Hide();
                }
            }
        }

        public TPageBaseType GetPage<T>() where T : AbstractUIScript<TPageEngine, TPageBaseType>
        {
            foreach (var page in _pages)
            {
                if (page is T)
                {
                    return page;
                }
            }

            return null;
        }

        public TPageBaseType Show<T>() where T : AbstractUIScript<TPageEngine, TPageBaseType>
        {
            foreach (var page in _pages)
            {
                if (page is T)
                {
                    page.Show();
                    return page;
                }
            }

            return null;
        }

        public void Hide<T>() where T : AbstractUIScript<TPageEngine, TPageBaseType>
        {
            foreach (var page in _pages)
            {
                if (page is T)
                {
                    page.Hide();
                    break;
                }
            }
        }

        public void HideAll()
        {
            foreach (var page in _pages)
            {
                page.Hide();
            }
        }

        public void GoBack()
        {
            if (_lastPage != null)
            {
                var localLastPage = _currentPage;
                foreach (var page in _pages)
                {
                    if (page == _lastPage)
                    {
                        _currentPage = page;
                        page.Show();
                    }
                    else
                    {
                        page.Hide();
                    }
                }

                _lastPage = localLastPage;
            }
        }
    }
}