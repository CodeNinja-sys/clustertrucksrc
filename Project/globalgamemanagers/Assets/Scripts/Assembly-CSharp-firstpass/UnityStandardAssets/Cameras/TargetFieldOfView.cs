using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class TargetFieldOfView : AbstractTargetFollower
	{
		[SerializeField]
		private float m_FovAdjustTime;
		[SerializeField]
		private float m_ZoomAmountMultiplier;
		[SerializeField]
		private bool m_IncludeEffectsInSize;
	}
}
