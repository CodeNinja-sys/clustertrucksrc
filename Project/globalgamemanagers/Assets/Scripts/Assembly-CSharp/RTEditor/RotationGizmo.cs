using UnityEngine;

namespace RTEditor
{
	public class RotationGizmo : Gizmo
	{
		[SerializeField]
		private float _rotationSphereRadius;
		[SerializeField]
		private Color _rotationSphereColor;
		[SerializeField]
		private bool _isRotationSphereLit;
		[SerializeField]
		private bool _showRotationGuide;
		[SerializeField]
		private Color _rotationGuieLineColor;
		[SerializeField]
		private Color _rotationGuideDiscColor;
		[SerializeField]
		private bool _showRotationSphereBoundary;
		[SerializeField]
		private Color _rotationSphereBoundaryLineColor;
		[SerializeField]
		private bool _showCameraLookRotationCircle;
		[SerializeField]
		private float _cameraLookRotationCircleRadiusScale;
		[SerializeField]
		private Color _cameraLookRotationCircleLineColor;
		[SerializeField]
		private Color _cameraLookRotationCircleColorWhenSelected;
		[SerializeField]
		private RotationGizmoSnapSettings _snapSettings;
	}
}
