using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class AbstractTargetFollower : MonoBehaviour
	{
		public enum UpdateType
		{
			FixedUpdate = 0,
			LateUpdate = 1,
			ManualUpdate = 2,
		}

		[SerializeField]
		protected Transform m_Target;
		[SerializeField]
		private bool m_AutoTargetPlayer;
		[SerializeField]
		private UpdateType m_UpdateType;
	}
}
