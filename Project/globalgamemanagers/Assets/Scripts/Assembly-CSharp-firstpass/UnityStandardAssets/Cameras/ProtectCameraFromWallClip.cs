using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class ProtectCameraFromWallClip : MonoBehaviour
	{
		public float clipMoveTime;
		public float returnTime;
		public float sphereCastRadius;
		public bool visualiseInEditor;
		public float closestDistance;
		public string dontClipTag;
	}
}
