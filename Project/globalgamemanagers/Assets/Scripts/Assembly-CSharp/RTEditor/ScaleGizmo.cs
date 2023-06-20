using UnityEngine;

namespace RTEditor
{
	public class ScaleGizmo : Gizmo
	{
		[SerializeField]
		private float _axisLength;
		[SerializeField]
		private float _scaleBoxWidth;
		[SerializeField]
		private float _scaleBoxHeight;
		[SerializeField]
		private float _scaleBoxDepth;
		[SerializeField]
		private bool _scaleAlongAllAxes;
		[SerializeField]
		private float _screenSizeOfAllAxesSquare;
		[SerializeField]
		private Color _colorOfAllAxesSquareLines;
		[SerializeField]
		private Color _colorOfAllAxesSquareLinesWhenSelected;
		[SerializeField]
		private bool _adjustAllAxesScaleSquareWhileScalingObjects;
		[SerializeField]
		private bool _areScaleBoxesLit;
		[SerializeField]
		private bool _adjustAxisLengthWhileScalingObjects;
		[SerializeField]
		private Color[] _multiAxisTriangleColors;
		[SerializeField]
		private Color[] _multiAxisTriangleLineColors;
		[SerializeField]
		private Color _selectedMultiAxisTriangleColor;
		[SerializeField]
		private Color _selectedMultiAxisTriangleLineColor;
		[SerializeField]
		private float _multiAxisTriangleSideLength;
		[SerializeField]
		private bool _adjustMultiAxisForBetterVisibility;
		[SerializeField]
		private bool _adjustMultiAxisTrianglesWhileScalingObjects;
		[SerializeField]
		private bool _drawObjectsLocalAxesWhileScaling;
		[SerializeField]
		private float _objectsLocalAxesLength;
		[SerializeField]
		private bool _preserveObjectLocalAxesScreenSize;
		[SerializeField]
		private bool _adjustObjectLocalAxesWhileScalingObjects;
		[SerializeField]
		private ScaleGizmoSnapSettings _snapSettings;
	}
}
