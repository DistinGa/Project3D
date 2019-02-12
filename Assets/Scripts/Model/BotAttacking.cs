using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    /// <summary>
    /// Атака игрока, если он на линии огня.
    /// </summary>
    public class BotAttacking : BaseBotRoutine
    {
        [SerializeField] private float FireAttack = 40f;
        [SerializeField] private Weapons _weapon;

        public override bool Act()
        {
            RaycastHit hit;
            if (Physics.Raycast(_weapon.Transform.position, transform.forward, out hit, FireAttack) && hit.collider.tag == "Player")
            {
                _weapon.Fire();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Init()
        {
            if(_weapon == null)
                _weapon = GetComponentInChildren<Weapons>();

            Init(FireAttack, 200);
        }

        public void Init(float FireAttack, int AmmoAmount)
        {
            this.FireAttack = FireAttack;

            _weapon.InitWeapon();
            _weapon.AddAmmo(AmmoAmount);
            _weapon.Recharge();
        }
    }
}
