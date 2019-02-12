using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Geekbrains
{
    public class Foe : BaseObjectScene, IDamagable, IEnemy
    {
        public float HP = 100f;

        private event Action OnDeath = () => { };
        private BaseBotRoutine[] _botRoutines;

        protected override void Awake()
        {
            base.Awake();
            SetPhysics(false);

            _botRoutines = GetComponents<BaseBotRoutine>();
            _botRoutines = _botRoutines.OrderBy(a => a.Priority).ToArray();
        }

        private void Start()
        {
            foreach (var item in _botRoutines)
            {
                item.Init();
            }
        }

        public void OnUpdate()
        {
            if (HP <= 0)
                return;

            foreach (var item in _botRoutines)
            {
                if (item.Act())
                    break;
            }
        }

        public void Init(Action OnDeathMethod)
        {
            OnDeath = OnDeathMethod;
        }

        public void ApplyDamage(float damage, Vector3 point, Vector3 force)
        {
            if (HP <= 0)
                return;

            HP -= damage;

            if (HP <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            SetPhysics(true);
            OnDeath?.Invoke();
            OnDeath = null;
            Destroy(gameObject, 10f);
        }

    }
}
