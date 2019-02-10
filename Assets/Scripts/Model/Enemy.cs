using System;
using System.Collections;
using UnityEngine;

namespace Geekbrains
{
    class Enemy : BaseObjectScene, IDamagable
    {
        public float HP = 100f;

        private event Action OnDeath = () => { };

        protected override void Awake()
        {
            base.Awake();
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
            GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().enabled = false;
            GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            Rigidbody.freezeRotation = false;
            Rigidbody.drag = 1f;
            Rigidbody.angularDrag = 1f;

            OnDeath?.Invoke();
            OnDeath = null;
        }

    }
}
