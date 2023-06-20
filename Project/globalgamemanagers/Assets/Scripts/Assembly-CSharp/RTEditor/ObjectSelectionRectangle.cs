using System;
using UnityEngine;

namespace RTEditor
{
	[Serializable]
	public class ObjectSelectionRectangle : ObjectSelectionShape
	{
		[SerializeField]
		private ObjectSelectionRectangleDrawSettings _drawSettings;
	}
}
