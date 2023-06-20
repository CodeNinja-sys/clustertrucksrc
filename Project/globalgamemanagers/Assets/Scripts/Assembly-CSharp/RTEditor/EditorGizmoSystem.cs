using UnityEngine;

namespace RTEditor
{
	public class EditorGizmoSystem : MonoSingletonBase<EditorGizmoSystem>
	{
		[SerializeField]
		private TranslationGizmo _translationGizmo;
		[SerializeField]
		private RotationGizmo _rotationGizmo;
		[SerializeField]
		private ScaleGizmo _scaleGizmo;
		[SerializeField]
		private GameObject _gizmoDirectionalLight;
		[SerializeField]
		private int _gizmoLayer;
		[SerializeField]
		private TransformSpace _transformSpace;
		[SerializeField]
		private GizmoType _activeGizmoType;
		[SerializeField]
		private TransformPivotPoint _transformPivotPoint;
	}
}
