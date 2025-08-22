using System.Collections.Generic;
using RobotBattle.Inputs;
using UnityEngine;

namespace RobotBattle
{
    public class System : MonoBehaviour
    {
        [SerializeField] private PoolHolder _poolHolder;
        
        private readonly List<BaseModel> models = new();
        private readonly ObjectRegister _objectRegister = new();
        
        public ObjectRegister ObjectRegister => _objectRegister;
        
        public static System Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
            
            models.Add(new PoolModel(this));

            models.Add(new InputModel(this));
            GetModel<PoolModel>().AddHolder(_poolHolder);
            
            models.Add(new PlayerModel(this));
            
            
            //DontDestroyOnLoad(this);
        }

        public T GetModel<T>() where T : BaseModel
        {
            return models.Find(model => model.GetType() == typeof(T)) as T;
        }
    }
}