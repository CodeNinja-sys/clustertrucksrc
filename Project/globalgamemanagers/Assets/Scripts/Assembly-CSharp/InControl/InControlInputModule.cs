using UnityEngine.EventSystems;

namespace InControl
{
	public class InControlInputModule : StandaloneInputModule
	{
		public enum Button
		{
			Action1 = 15,
			Action2 = 16,
			Action3 = 17,
			Action4 = 18,
		}

		public new Button submitButton;
		public new Button cancelButton;
		public float analogMoveThreshold;
		public float moveRepeatFirstDuration;
		public float moveRepeatDelayDuration;
		public bool allowMobileDevice;
		public bool allowMouseInput;
		public bool focusOnMouseHover;
	}
}
