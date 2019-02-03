using UnityEngine;

namespace Geekbrains
{
	public class InputController : BaseController
	{
		private KeyCode _codeFlashLight = KeyCode.F;
		public override void OnUpdate()
		{
			if (!IsActive) return;
			if (Input.GetKeyDown(_codeFlashLight))
			{
				Main.Instance.FlashLightController.Switch();
			}
		}
	}
}