using System;
using UnityEngine;

namespace Geekbrains
{
    public enum AmmoTypes
    {
        SciFiGunLight,
        Bazooka
    }

    /// <summary>
    ///  Класс для всех типов огнестрельного оружия.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Weapons : BaseObjectScene, IWeapon
    {
        #region Serialize Variable
        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _fireDelay = 0.2f;
        [SerializeField] protected AmmoTypes _ammoType;
        [SerializeField] protected bool _autoRechargable;
        [SerializeField] protected AudioClip _shootSound, _dryShootSound;
        #endregion
        // Количество боеприпасов в обойме.
        public int AmmoInClip { get; private set; }
        // Общее количество боеприпасов.
        public int AmmoCount { get; private set; }
        // Флаг, разрешающий стрелять.
        private bool _canFire = true;
        private Ammunition Ammo;
        private AudioSource _audioSource;

        public void InitWeapon()
        {
            _audioSource = GetComponent<AudioSource>();
            Ammo = Main.Instance.Arsenal.GetAmmoByType(_ammoType);

            if (Ammo == null)
            {
                throw new Exception($"There is no ammo of type {_ammoType.ToString()}.");
            }
        }

        public void Fire()
        {
            if (_canFire)
            {
                _canFire = false;
                _audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);

                if (AmmoInClip > 0)
                {
                    if(_shootSound != null)
                        _audioSource.PlayOneShot(_shootSound);

                    var bullet = Instantiate(Ammo, _barrel.position, _barrel.rotation);

                    AmmoInClip--;
                    Invoke("DoFireDelay", _fireDelay);

                    if (AmmoInClip == 0 && _autoRechargable)
                    {
                        Recharge();
                    }
                }
                else
                {
                    // Осечка.
                    Invoke("DoFireDelay", 0.5f);
                    if (_dryShootSound != null)
                        _audioSource.PlayOneShot(_dryShootSound, 0.5f);
                }
            }
        }

        public void Recharge()
        {
            // Сначала вернём в общий пул остаток патронов в обойме (полупустые обоймы не выбрасываем)
            AmmoCount += AmmoInClip;
            AmmoInClip = Math.Min(AmmoCount, Ammo.ClipCap);
            AmmoCount -= AmmoInClip;
        }

        public void AddAmmo(int amount)
        {
            AmmoCount += amount;
        }

        /// <summary>
        /// Выполняется через _fireDelay секунд после выстрела.
        /// </summary>
        private void DoFireDelay()
        {
            _canFire = true;
        }
    }
}

