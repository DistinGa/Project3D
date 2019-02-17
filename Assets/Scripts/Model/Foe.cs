using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Geekbrains
{
    public class Foe : BaseObjectScene, IDamagable, IEnemy
    {
        public float _HP = 100f;

        private event Action OnDeath = () => { };
        private BaseBotRoutine[] _botRoutines;
        private BotChasingTarget _chasingRoutine;
        private float _maxHP;

        public float HP
        {
            get { return _HP; }

            set
            {
                _HP = value;

                if (_HP <= 0)
                {
                    Death();
                }

                if (_HP > _maxHP)
                {
                    _HP = _maxHP;
                }

                if(_chasingRoutine != null)
                    _chasingRoutine.IsHeeling = (HP < _maxHP * 0.5f);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            SetPhysics(false);

            _botRoutines = GetComponents<BaseBotRoutine>();
            _botRoutines = _botRoutines.OrderBy(a => a.Priority).ToArray();
            _chasingRoutine = GetComponent<BotChasingTarget>();
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

        public void Init(Action OnDeathMethod, float maxHP = 100f)
        {
            OnDeath = OnDeathMethod;
            _maxHP = maxHP;
            HP = _maxHP;
        }

        public void ApplyDamage(float damage, Vector3 point, Vector3 force)
        {
            if (HP <= 0)
                return;

            HP -= damage;
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
