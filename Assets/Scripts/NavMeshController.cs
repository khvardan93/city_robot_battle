using UnityEngine;
using UnityEngine.AI;

namespace RobotBattle
{
    public class NavMeshController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        private NavMeshSettings _settings;

        public void Init(NavMeshSettings settings)
        {
            _settings = settings;

            _agent.updatePosition = settings.UpdatePosition;
            _agent.updateRotation = settings.UpdateRotation;
        }

        public void GoToTarget(Vector3 target)
        {
            _agent.isStopped = false;
            _agent.SetDestination(target);
        }

        public void SetStopped()
        {
            _agent.isStopped = true;
        }

        public void Move(Vector3 offset)
        {
            _agent.Move(offset);

        }
    }
}
