using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class AutoCam : PivotBasedCameraRig
	{
		[SerializeField]
		private float m_MoveSpeed;
		[SerializeField]
		private float m_TurnSpeed;
		[SerializeField]
		private float m_RollSpeed;
		[SerializeField]
		private bool m_FollowVelocity;
		[SerializeField]
		private bool m_FollowTilt;
		[SerializeField]
		private float m_SpinTurnLimit;
		[SerializeField]
		private float m_TargetVelocityLowerLimit;
		[SerializeField]
		private float m_SmoothTurnTime;
	}
}
