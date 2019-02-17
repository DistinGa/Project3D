using System;
using System.Collections;
using UnityEngine;

namespace Geekbrains
{
    class Victim : BaseObjectScene, IDamagable, IEnemy
    {
        public float _HP = 100f;
        private float _maxHP;

        private event Action OnDeath = () => { };

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
            }
        }

        public void Init(Action OnDeathMethod, float maxHP = 100)
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

            Rigidbody.AddForceAtPosition(force, point);
        }

        private void Death()
        {
            OnDeath?.Invoke();
            OnDeath = null;
            StartCoroutine(AnimatedDeath(2f));
        }

        private IEnumerator AnimatedDeath(float period)
        {
            Material _mat = GetComponent<MeshRenderer>().material;
            Color _color = _mat.color;
            float _rest = period;

            while (_rest > 0)
            {
                _color.r = _rest / period;
                _mat.color = _color;
                yield return null;
                _rest -= Time.deltaTime;
           }

            Destroy(gameObject);
        }

        public void OnUpdate()
        {
            // Кубики просто лежат для красоты :)
        }
    }
}
