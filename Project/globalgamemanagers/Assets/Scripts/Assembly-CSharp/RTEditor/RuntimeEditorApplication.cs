using System;
using UnityEngine;

namespace RTEditor
{
	[Serializable]
	public class RuntimeEditorApplication : MonoSingletonBase<RuntimeEditorApplication>
	{
		[SerializeField]
		private RuntimeEditorApplicationStartupSettings _editorApplicationStartupSettings;
	}
}
