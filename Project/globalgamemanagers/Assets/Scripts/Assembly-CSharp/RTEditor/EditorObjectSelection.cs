using System;
using UnityEngine;

namespace RTEditor
{
	[Serializable]
	public class EditorObjectSelection : MonoSingletonBase<EditorObjectSelection>
	{
		[SerializeField]
		private ObjectSelectionBoxDrawSettings _objectSelectionBoxDrawSettings;
		[SerializeField]
		private ObjectSelectionRectangle _objectSelectionRectangle;
	}
}
