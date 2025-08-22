using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RobotBattle.Inputs
{
    public enum InputPhase
    {
        Started,
        Performed,
        Canceled
    }

    public class InputActionHandler<T> where T : struct
    {
        public event Action<InputPhase, T> OnAction; 
        
        private InputAction _action;
        
        public InputActionHandler(string actionName)
        {
            var action = InputSystem.actions.FindAction(actionName);

            action.started += HandleStarted;
            action.performed += HandlePerformed;
            action.canceled += HandleCanceled;
            
            _action = action;
        }
        
        private void HandleStarted(InputAction.CallbackContext context)
        {
            OnAction?.Invoke(InputPhase.Started, context.ReadValue<T>());
        }
        
        private void HandlePerformed(InputAction.CallbackContext context)
        {
            OnAction?.Invoke(InputPhase.Performed, context.ReadValue<T>());
        }
        
        private void HandleCanceled(InputAction.CallbackContext context)
        {
            OnAction?.Invoke(InputPhase.Canceled, context.ReadValue<T>());
        }
        
        public void Destruct()
        {
            _action.started -= HandleStarted;
            _action.performed -= HandlePerformed;
            _action.canceled -= HandleCanceled;
        }
    }
    
    public class InputModel : BaseModel, IDestructible
    {
        private readonly InputActionHandler<Vector2> _moveInputs;
        private readonly InputActionHandler<float> _attackInputs;
        private readonly InputActionHandler<float> _shotInputs;
        private readonly InputActionHandler<float> _fireInputs;
        private readonly InputActionHandler<float> _rocketInputs;
        
        public InputActionHandler<Vector2> MoveInputs => _moveInputs;
        public InputActionHandler<float> AttackInputs => _attackInputs;
        public InputActionHandler<float> ShotInputs => _shotInputs;
        public InputActionHandler<float> FireInputs => _fireInputs;
        public InputActionHandler<float> RocketInputs => _rocketInputs;

        public InputModel(System system) : base(system)
        {
            const string moveActionName = "Move";
            _moveInputs = new InputActionHandler<Vector2>(moveActionName);

            const string attackActionName = "Attack";
            _attackInputs = new InputActionHandler<float>(attackActionName);
            
            const string shotActionName = "Shot";
            _shotInputs = new InputActionHandler<float>(shotActionName);
            
            const string fireActionName = "Fire";
            _fireInputs = new InputActionHandler<float>(fireActionName);
            
            const string rocketActionName = "Rocket";
            _rocketInputs = new InputActionHandler<float>(rocketActionName);
        }

        void IDestructible.Destruct()
        {
            _moveInputs.Destruct();
            _attackInputs.Destruct();
        }
    }
}