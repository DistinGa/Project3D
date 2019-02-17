using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
    /// <summary>
    /// Бот оглядывается, когда не может построить маршрут.
    /// </summary>
    public class BotLookingOut : BaseBotRoutine
    {
        private NavMeshAgent _agent;
        private float _dir;

        public override bool Act()
        {
            transform.Rotate(new Vector3(0, _dir * Time.deltaTime * _agent.angularSpeed, 0));
            _restDelayTime -= Time.deltaTime;
            if (_restDelayTime < 0)
                NewLoop();

            return true;
        }

        public override void Init()
        {
            _agent = GetComponent<NavMeshAgent>();
            NewLoop();
        }

        private void NewLoop()
        {
            _restDelayTime = Random.Range(Delay * 0.5f, Delay);
            _dir = Random.Range(0, 100) > 50 ? -1 : 1;
        }
    }
}
