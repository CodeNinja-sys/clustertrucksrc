using System;
using UnityEngine;

namespace RTEditor
{
	[Serializable]
	public class ObjectSelectionBoxDrawSettings
	{
		[SerializeField]
		private ObjectSelectionBoxStyle _selectionBoxStyle;
		[SerializeField]
		private float _selectionBoxCornerLineLength;
		[SerializeField]
		private Color _selectionBoxLineColor;
		[SerializeField]
		private float _selectionBoxScaleFactor;
	}
}
