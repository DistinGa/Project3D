using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class HealthPack : BaseObjectScene
    {
        [SerializeField] private float hp;
        private event Action<HealthPack> OnTriggered;

        public void Init(float hpAmount, Action<HealthPack> OnTriggeredMethod)
        {
            hp = hpAmount;
            this.OnTriggered += OnTriggeredMethod;
        }

        private void OnTriggerEnter(Collider col)
        {
            IDamagable target = col.GetComponentInParent<IDamagable>();
            if (target != null)
            {
                target.HP += hp;
                OnTriggered?.Invoke(this);
            }
        }

        private void OnDestroy()
        {
            OnTriggered = null;
        }
    }
}
