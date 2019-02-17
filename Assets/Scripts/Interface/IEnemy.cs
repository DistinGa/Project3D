using System;

namespace Geekbrains
{
    public interface IEnemy
    {
        void Init(Action OnDeath, float maxHP);
        void OnUpdate();
    }
}
