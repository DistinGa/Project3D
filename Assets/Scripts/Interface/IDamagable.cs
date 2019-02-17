using UnityEngine;

namespace Geekbrains
{
    interface IDamagable
    {
        void ApplyDamage(float damage, Vector3 point, Vector3 force);
        float HP { get; set; }
    }
}
