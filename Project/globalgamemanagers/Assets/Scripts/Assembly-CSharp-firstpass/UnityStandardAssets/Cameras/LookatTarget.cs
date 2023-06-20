using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class LookatTarget : AbstractTargetFollower
	{
		[SerializeField]
		private Vector2 m_RotationRange;
		[SerializeField]
		private float m_FollowSpeed;
	}
}
