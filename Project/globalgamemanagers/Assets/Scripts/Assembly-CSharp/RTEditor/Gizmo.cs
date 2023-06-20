using UnityEngine;

namespace RTEditor
{
	public class Gizmo : MonoBehaviour
	{
		[SerializeField]
		protected Color[] _axesColors;
		[SerializeField]
		protected Color _selectedAxisColor;
		[SerializeField]
		protected float _gizmoBaseScale;
		[SerializeField]
		protected bool _preserveGizmoScreenSize;
	}
}
