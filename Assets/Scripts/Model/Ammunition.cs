using UnityEngine;

namespace Geekbrains
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Ammunition : BaseObjectScene
    {
        public AmmoTypes AmmoType;
        // Вместимость обоймы
        [SerializeField] protected int _clipCap;
        // Время жизни пули
        [SerializeField] protected float _timeToDestruct = 10;
        // Урон пули
        [SerializeField] protected float _damage = 20;
        // Масса пули
        [SerializeField] protected float _mass = 0.01f;
        // Начальная скорость
        [SerializeField] protected float _initVelocity = 500f;
        protected float _currentDamage { get; private set; }

        public int ClipCap
        {
            get { return _clipCap; }
        }

        protected override void Awake()
        {
            base.Awake();
            // Если пуля не встретит ничего, то через заданное время пуля самоуничтожится
            Destroy(gameObject, _timeToDestruct);
            _currentDamage = _damage;
            Rigidbody.mass = _mass;
            Rigidbody.velocity = Transform.forward * _initVelocity;
        }
    }


}
