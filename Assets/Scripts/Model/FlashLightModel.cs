using UnityEngine;

namespace Geekbrains
{
	public sealed class FlashLightModel : BaseObjectScene
	{
		private Light _light;
		private Transform _goFollow;
		private Vector3 _vecOffset;
		public float BatteryChargeCurrent { get; private set; }
		[SerializeField] private float _speed = 10;
		[SerializeField] private float _chargeSpeed = 10;
        [SerializeField] private float _batteryChargeMax;

        public float BatteryPercent
        {
            get { return BatteryChargeCurrent / _batteryChargeMax; }
        }

        protected override void Awake()
		{
			base.Awake();
			_light = GetComponent<Light>();
			_goFollow = Camera.main.transform;
			_vecOffset = transform.position - _goFollow.position;
			BatteryChargeCurrent = _batteryChargeMax;
		}

		public void Switch(bool value)
		{
			_light.enabled = value;
			if (!value) return;
			transform.position = _goFollow.position + _vecOffset;
			transform.rotation = _goFollow.rotation;
		}

		public void Rotation()
		{
			if (!_light) return;
			transform.position = _goFollow.position + _vecOffset;
			transform.rotation = Quaternion.Lerp(transform.rotation,
				_goFollow.rotation, _speed * Time.deltaTime);
		}

		public bool EditBatteryCharge(int dir)
		{
			BatteryChargeCurrent = Mathf.Clamp(BatteryChargeCurrent + Time.deltaTime * _chargeSpeed * dir, 0, _batteryChargeMax);

			return (BatteryChargeCurrent > 0);
		}
	}
}