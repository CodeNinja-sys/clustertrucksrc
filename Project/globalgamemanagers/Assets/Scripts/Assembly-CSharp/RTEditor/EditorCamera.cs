using UnityEngine;

namespace RTEditor
{
	public class EditorCamera : MonoSingletonBase<EditorCamera>
	{
		[SerializeField]
		private float _orthographicZoomSpeed;
		[SerializeField]
		private float _perspectiveZoomSpeed;
		[SerializeField]
		private float _panSpeed;
		[SerializeField]
		private float _rotationSpeedInDegrees;
	}
}
