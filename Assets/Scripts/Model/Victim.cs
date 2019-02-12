using System;
using System.Collections;
using UnityEngine;

namespace Geekbrains
{
    class Victim : BaseObjectScene, IDamagable, IEnemy
    {
        public float HP = 100f;

        private event Action OnDeath = () => { };

        public void Init(Action OnDeathMethod)
        {
            OnDeath = OnDeathMethod;
        }

        public void ApplyDamage(float damage, Vector3 point, Vector3 force)
        {
            if (HP <= 0)
                return;

            HP -= damage;

            Rigidbody.AddForceAtPosition(force, point);

            if (HP <= 0)
            {
                Death();
            }
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
