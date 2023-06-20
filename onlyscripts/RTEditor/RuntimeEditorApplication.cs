using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001D8 RID: 472
	[Serializable]
	public class RuntimeEditorApplication : MonoSingletonBase<RuntimeEditorApplication>
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00046450 File Offset: 0x00044650
		public RuntimeEditorApplicationStartupSettings StartupSettings
		{
			get
			{
				return this._editorApplicationStartupSettings;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00046458 File Offset: 0x00044658
		public void OnGUI()
		{
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00046468 File Offset: 0x00044668
		private void Start()
		{
			if (this._editorApplicationStartupSettings.AcquireVertexSnappingInfoOnStartup)
			{
				SingletonBase<MeshVertexGroupMappings>.Instance.CreateMappingsForAllSceneMeshObjects();
			}
			if (this._editorApplicationStartupSettings.AttachObjectCollidersToAllSceneObjectsAtStartup)
			{
				SingletonBase<ObjectColliderAttachment>.Instance.AttachCollidersToAllSceneObjects(this._editorApplicationStartupSettings.ObjectColliderAttachmentSettings);
			}
		}

		// Token: 0x040007DE RID: 2014
		[SerializeField]
		private RuntimeEditorApplicationStartupSettings _editorApplicationStartupSettings = new RuntimeEditorApplicationStartupSettings();
	}
}
