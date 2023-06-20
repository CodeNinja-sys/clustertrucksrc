using UnityEngine;

namespace InControl
{
	public class TouchControl : MonoBehaviour
	{
		public enum ButtonTarget
		{
			None = 0,
			Action1 = 15,
			Action2 = 16,
			Action3 = 17,
			Action4 = 18,
			LeftTrigger = 19,
			RightTrigger = 20,
			LeftBumper = 21,
			RightBumper = 22,
			DPadDown = 12,
			DPadLeft = 13,
			DPadRight = 14,
			DPadUp = 11,
			Menu = 30,
			Button0 = 62,
			Button1 = 63,
			Button2 = 64,
			Button3 = 65,
			Button4 = 66,
			Button5 = 67,
			Button6 = 68,
			Button7 = 69,
			Button8 = 70,
			Button9 = 71,
			Button10 = 72,
			Button11 = 73,
			Button12 = 74,
			Button13 = 75,
			Button14 = 76,
			Button15 = 77,
			Button16 = 78,
			Button17 = 79,
			Button18 = 80,
			Button19 = 81,
		}

		public enum AnalogTarget
		{
			None = 0,
			LeftStick = 1,
			RightStick = 2,
			Both = 3,
		}

		public enum SnapAngles
		{
			None = 0,
			Four = 4,
			Eight = 8,
			Sixteen = 16,
		}

	}
}
