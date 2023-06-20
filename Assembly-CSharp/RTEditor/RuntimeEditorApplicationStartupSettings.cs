using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001D9 RID: 473
	[Serializable]
	public class RuntimeEditorApplicationStartupSettings
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x000464D8 File Offset: 0x000446D8
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x000464E0 File Offset: 0x000446E0
		public bool AcquireVertexSnappingInfoOnStartup
		{
			get
			{
				return this._acquireVertexSnappingInfoOnStartup;
			}
			set
			{
				this._acquireVertexSnappingInfoOnStartup = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x000464EC File Offset: 0x000446EC
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x000464F4 File Offset: 0x000446F4
		public bool AttachObjectCollidersToAllSceneObjectsAtStartup
		{
			get
			{
				return this._attachObjectCollidersToAllSceneObjectsAtStartup;
			}
			set
			{
				this._attachObjectCollidersToAllSceneObjectsAtStartup = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00046500 File Offset: 0x00044700
		public ObjectColliderAttachmentSettings ObjectColliderAttachmentSettings
		{
			get
			{
				return this._objectColliderAttachmentSettings;
			}
		}

		// Token: 0x040007DF RID: 2015
		[SerializeField]
		private bool _acquireVertexSnappingInfoOnStartup = true;

		// Token: 0x040007E0 RID: 2016
		[SerializeField]
		private bool _attachObjectCollidersToAllSceneObjectsAtStartup = true;

		// Token: 0x040007E1 RID: 2017
		[SerializeField]
		private ObjectColliderAttachmentSettings _objectColliderAttachmentSettings = new ObjectColliderAttachmentSettings();
	}
}
