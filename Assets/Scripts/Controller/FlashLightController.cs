using UnityEngine;

namespace Geekbrains
{
	public class FlashLightController : BaseController
	{
		private FlashLightModel _flashLight;
		private FlashLightUi _flashLightUi;

		public FlashLightController()
		{
			_flashLight = MonoBehaviour.FindObjectOfType<FlashLightModel>();
			_flashLightUi = MonoBehaviour.FindObjectOfType<FlashLightUi>();
            
			Off();
            _flashLight.Switch(false);
            _flashLightUi.SetActive(false);
        }

        public override void OnUpdate()
		{
            if (_flashLight == null) return;

            if (!IsActive)
            {
                if (_flashLight.EditBatteryCharge(1))
                {
                    _flashLightUi.Value = _flashLight.BatteryPercent;
                }
            }
            else
            {
                _flashLight.Rotation();
                if (_flashLight.EditBatteryCharge(-1))
                {
                    _flashLightUi.Value = _flashLight.BatteryPercent;
                }
                else
                {
                    Off();
                }
            }
        }

        public override void On()
		{
			//if (IsActive)return;
			base.On();
			_flashLight.Switch(true);
			_flashLightUi.SetActive(true);
		}

		public sealed override void Off()
		{
            //if (!IsActive) return;
            base.Off();
			_flashLight.Switch(false);
			_flashLightUi.SetActive(false);
		}
	}
}