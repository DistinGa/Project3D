using UnityEngine;

namespace Geekbrains
{
    public class WeaponController : BaseController
    {
        private Weapons _weapon;
        private WeaponUi _weaponUI;
        private int _weaponID;

        public WeaponController()
        {
            _weaponUI = MonoBehaviour.FindObjectOfType<WeaponUi>();
        }

        public override void OnUpdate()
        {
            if (!IsActive) return;

            if (Input.GetAxisRaw("Fire1") == 1)
            {
                if (_weapon == null) return;

                _weapon.Fire();
                ShowAmmoInfo();
            }

            if (Input.GetAxisRaw("Reload") == 1)
            {
                if (_weapon == null) return;

                Recharge();
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
            {
                ChangeWeapon((int)Input.GetAxisRaw("Mouse ScrollWheel"));
            }
        }

        public override void On(BaseObjectScene weapon)
        {
            if (IsActive) return;

            base.On(weapon);

            _weapon = weapon as Weapons;
            if (_weapon == null) return;
            _weapon.InitWeapon();
            _weapon.gameObject.SetActive(true);
            _weaponUI.SetActive(true);
            ShowAmmoInfo();
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();

            if (_weapon == null) return;
            _weapon.gameObject.SetActive(false);
            _weaponUI.SetActive(false);
        }

        public void Recharge()
        {
            if (_weapon == null) return;
            _weapon.Recharge();
            ShowAmmoInfo();
        }

        public void ChangeWeapon(int dir)
        {
            if (!IsActive) return;

            Weapons newWeapon = null;
            _weaponID += dir;
            _weaponID = Mathf.Clamp(_weaponID, 0, Main.Instance.Arsenal.WeaponsList.Count-1);
            newWeapon = Main.Instance.Arsenal.WeaponsList[_weaponID];

            Off();
            On(newWeapon);
        }

        public void AddAmmo(int amount)
        {
            _weapon.AddAmmo(amount);
            ShowAmmoInfo();
        }

        private void ShowAmmoInfo()
        {
            _weaponUI.UpdateUI($"{_weapon.AmmoInClip} / {_weapon.AmmoCount}");
        }
    }
}
