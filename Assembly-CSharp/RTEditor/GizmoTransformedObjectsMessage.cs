using System;

namespace RTEditor
{
	// Token: 0x020001C3 RID: 451
	public class GizmoTransformedObjectsMessage : Message
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x00044FC4 File Offset: 0x000431C4
		public GizmoTransformedObjectsMessage(Gizmo gizmo) : base(MessageType.GizmoTransformedObjects)
		{
			this._gizmo = gizmo;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00044FD4 File Offset: 0x000431D4
		public Gizmo Gizmo
		{
			get
			{
				return this._gizmo;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00044FDC File Offset: 0x000431DC
		public static void SendToInterestedListeners(Gizmo gizmo)
		{
			GizmoTransformedObjectsMessage message = new GizmoTransformedObjectsMessage(gizmo);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007B6 RID: 1974
		private Gizmo _gizmo;
	}
}
