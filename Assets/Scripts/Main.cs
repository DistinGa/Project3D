using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public class Main : MonoBehaviour
	{
        public Arsenal Arsenal { get; private set; }
		public FlashLightController FlashLightController { get; private set; }
		public InputController InputController { get; private set; }
		public PlayerController PlayerController { get; private set; }
        public WeaponController WeaponController { get; private set; }

        private List<BaseController> Controllers;

		public static Main Instance { get; private set; }

		private void Awake()
		{
			Instance = this;

            Cursor.lockState = CursorLockMode.Locked;

            Arsenal = GetComponent<Arsenal>();
            foreach (var item in Arsenal.WeaponsList)
            {
                if (item != null)
                {
                    item.InitWeapon();
                    item.AddAmmo(100);
                    item.Recharge();
                }
            }

			PlayerController = new PlayerController(new UnitMotor(FindObjectOfType<CharacterController>().transform));
			PlayerController.On();
			FlashLightController = new FlashLightController();
			InputController = new InputController();
			InputController.On();


            WeaponController = new WeaponController();
            WeaponController.On(Arsenal.WeaponsList[0]);
            Controllers = new List<BaseController>();
			Controllers.Add(FlashLightController);
			Controllers.Add(InputController);
			Controllers.Add(PlayerController);
			Controllers.Add(WeaponController);
		}

		private void Update()
		{
			for (var index = 0; index < Controllers.Count; index++)
			{
				var controller = Controllers[index];
				controller.OnUpdate();
			}
		}
	}
}