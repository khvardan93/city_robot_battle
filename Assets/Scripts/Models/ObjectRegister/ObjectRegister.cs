using System.Collections.Generic;
using System;
using UnityEngine;

namespace RobotBattle
{
    public class ObjectRegister
    {
        private readonly Dictionary<Type, ISingleObject> _singleObjects = new();
        private readonly Dictionary<Type, Action<ISingleObject>> _registerActions = new();

        public void Register<T>(ISingleObject singleObject) where T : ISingleObject
        {
            if (_singleObjects.TryAdd(typeof(T), singleObject))
            {
                if (_registerActions.TryGetValue(typeof(T), out var registerAction))
                {
                    registerAction?.Invoke(singleObject);
                }
            }
            else
            {
                Debug.LogError($"Object already registered: {typeof(T)}");
            }
        }

        public void Get<T>(Action<ISingleObject> callbeck) where T : ISingleObject
        {
            if (_singleObjects.TryGetValue(typeof(T), out var singleObject))
            {
                callbeck?.Invoke(singleObject);
                return;
            }

            if (_registerActions.ContainsKey(typeof(T)))
            {
                _registerActions[typeof(T)] += callbeck;
                return;
            }
            
            _registerActions.Add(typeof(T), callbeck);
        }
        
        public void Unregister<T>() where T : ISingleObject
        {
            if(_singleObjects.ContainsKey(typeof(T)))
            {
                _singleObjects.Remove(typeof(T));
            }
        }
    }
}
