using UnityEngine;

namespace InControl
{
	public class TouchTrackControl : TouchControl
	{
		[SerializeField]
		private TouchUnitType areaUnitType;
		[SerializeField]
		private Rect activeArea;
		public TouchControl.AnalogTarget target;
		public float scale;
	}
}
