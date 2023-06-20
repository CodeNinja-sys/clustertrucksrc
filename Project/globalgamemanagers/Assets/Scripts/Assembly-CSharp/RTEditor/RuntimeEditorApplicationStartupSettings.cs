using System;
using UnityEngine;

namespace RTEditor
{
	[Serializable]
	public class RuntimeEditorApplicationStartupSettings
	{
		[SerializeField]
		private bool _acquireVertexSnappingInfoOnStartup;
		[SerializeField]
		private bool _attachObjectCollidersToAllSceneObjectsAtStartup;
		[SerializeField]
		private ObjectColliderAttachmentSettings _objectColliderAttachmentSettings;
	}
}
