using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class FreeLookCam : PivotBasedCameraRig
	{
		[SerializeField]
		private float m_MoveSpeed;
		[SerializeField]
		private float m_TurnSpeed;
		[SerializeField]
		private float m_TurnSmoothing;
		[SerializeField]
		private float m_TiltMax;
		[SerializeField]
		private float m_TiltMin;
		[SerializeField]
		private bool m_LockCursor;
		[SerializeField]
		private bool m_VerticalAutoReturn;
	}
}
