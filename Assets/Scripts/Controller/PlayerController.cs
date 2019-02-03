namespace Geekbrains
{
	public class PlayerController : BaseController
	{
		private IMotor _motor;

		public PlayerController(IMotor motor)
		{
			_motor = motor;
		}

		public override void OnUpdate()
		{
			if (!IsActive) return;
			_motor.Move();
		}
	}
}