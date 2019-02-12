using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
    /// <summary>
    /// Поиск и преследование цели.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class BotChasingTarget : BaseBotRoutine
    {
        [SerializeField] private float VisionAngle = 30f;
        [SerializeField] private float CloseDistance = 5f;
        [SerializeField] private float FarDistance = 50f;
        private NavMeshAgent _agent;
        private Transform _playerTransform;

        public override void Init()
        {
            _agent = GetComponent<NavMeshAgent>();

            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public void Init(float VisionAngle, float CloseDistance, float FarDistance)
        {
            this.VisionAngle = VisionAngle;
            this.CloseDistance = CloseDistance;
            this.FarDistance = FarDistance;

            Init();
        }

        public override bool Act()
        {
            if (_restDelayTime > 0)
            {
                _restDelayTime -= Time.deltaTime;
                return false;
            }

            float _distanceToTarget = (_playerTransform.position - transform.position).magnitude;

            if (_distanceToTarget <= CloseDistance)
                return TargetLock(_playerTransform.position);

            if (_distanceToTarget <= FarDistance)
            {
                if (Vector3.Angle(_playerTransform.position - transform.position, transform.forward) < VisionAngle * 0.5f)
                {
                    RaycastHit hit;
                    if (Physics.Linecast(transform.position, _playerTransform.position, out hit) && hit.collider.tag != "Player")
                        return false;
                    else
                    {
                        // Если цель близко к конечной точке расчитанного маршрута, маршрут не пересчитываем.
                        if ((_agent.pathEndPosition - _playerTransform.position).magnitude < CloseDistance)
                        {
                            return true;
                        }
                        else
                        {
 print("Вижу цель");
                            return TargetLock(_playerTransform.position);
                        }
                    }
                }
            }

            return false;
        }


        private bool TargetLock(Vector3 targetPosition)
        {
            return _agent.SetDestination(targetPosition);
        }
    }
}
