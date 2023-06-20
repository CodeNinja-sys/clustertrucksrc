using UnityEngine;

namespace InControl
{
	public class UnityInputDeviceProfile : InputDeviceProfile
	{
		[SerializeField]
		protected string[] JoystickNames;
		[SerializeField]
		protected string[] JoystickRegex;
		[SerializeField]
		protected string LastResortRegex;
	}
}
