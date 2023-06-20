using UnityEngine;

namespace RTEditor
{
	public class EditorUndoRedoSystem : MonoSingletonBase<EditorUndoRedoSystem>
	{
		[SerializeField]
		private int _actionLimit;
	}
}
