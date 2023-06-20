using UnityEngine;

namespace RTEditor
{
	public class TranslationGizmo : Gizmo
	{
		[SerializeField]
		private float _axisLength;
		[SerializeField]
		private float _arrowConeRadius;
		[SerializeField]
		private float _arrowConeLength;
		[SerializeField]
		private float _multiAxisSquareSize;
		[SerializeField]
		private bool _adjustMultiAxisForBetterVisibility;
		[SerializeField]
		private Color[] _multiAxisSquareColors;
		[SerializeField]
		private Color[] _multiAxisSquareLineColors;
		[SerializeField]
		private Color _selectedMultiAxisSquareColor;
		[SerializeField]
		private Color _selectedMultiAxisSquareLineColor;
		[SerializeField]
		private bool _areArrowConesLit;
		[SerializeField]
		private bool _translateAlongCameraRightAndUpAxes;
		[SerializeField]
		private float _screenSizeOfCameraAxesTranslationSquare;
		[SerializeField]
		private Color _colorOfCameraAxesTranslationSquareLines;
		[SerializeField]
		private Color _colorOfCameraAxesTranslationSquareLinesWhenSelected;
		[SerializeField]
		private float _screenSizeOfVertexSnappingSquare;
		[SerializeField]
		private Color _colorOfVertexSnappingSquareLines;
		[SerializeField]
		private Color _colorOfVertexSnappingSquareLinesWhenSelected;
	}
}
