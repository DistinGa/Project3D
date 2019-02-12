using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
    /// <summary>
    /// Патрулирование территории.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class BotPatrolling : BaseBotRoutine
    {
        [SerializeField] private List<Transform> Path;

        private NavMeshAgent _agent;
        private bool _randomPatrol = true;
        private int _pathNode = 0;
        private Vector3 _targetPosition;

        private int PathNode
        {
            get { return _pathNode; }

            set
            {
                _pathNode = value;
                if (_pathNode == Path.Count)
                    _pathNode = 0;
            }
        }

        public override void Init()
        {
            Init(Path);
        }

        public void Init(List<Transform> Path)
        {
            _agent = GetComponent<NavMeshAgent>();

            if (Path != null && Path.Count > 0)
                SetPath(Path);

            SetNewDestination();
        }

        public override bool Act()
        {
            if (_restDelayTime > 0)
            {
                _restDelayTime -= Time.deltaTime;
                return false;
            }

            if ((_agent.pathEndPosition - transform.position).magnitude < _agent.stoppingDistance || (transform.position - _targetPosition).magnitude < _agent.stoppingDistance)
            {
                return SetNewDestination();
            }

            if(_agent.pathStatus == NavMeshPathStatus.PathInvalid)
                return SetNewDestination();

            return true;
        }

        public void SetPath(List<Transform> newPath)
        {
            _randomPatrol = false;
            Path = newPath;
            PathNode = 0;
        }

        private bool SetNewDestination()
        {
            if (_randomPatrol)
            {
                _targetPosition = Random.insideUnitSphere * 20f;
                NavMeshHit _hit;
                float _maxDistance = 5f;
                if (!NavMesh.SamplePosition(_targetPosition, out _hit, _maxDistance, NavMesh.AllAreas))
                {
                    // Если с первого раза не нашли допустимую точку, делаем паузу для осмотра.
                    _restDelayTime = 1f;
                    return false;
                }
                _targetPosition = _hit.position;
            }
            else
            {
                _targetPosition = Path[PathNode++].position;
            }

            return _agent.SetDestination(_targetPosition);
        }
    }
}
