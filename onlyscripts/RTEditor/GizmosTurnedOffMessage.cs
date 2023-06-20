using System;

namespace RTEditor
{
	// Token: 0x020001C6 RID: 454
	public class GizmosTurnedOffMessage : Message
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x00045070 File Offset: 0x00043270
		public GizmosTurnedOffMessage(GizmoType activeGizmoType) : base(MessageType.GizmosTurnedOff)
		{
			this._activeGizmoType = activeGizmoType;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00045080 File Offset: 0x00043280
		public GizmoType ActiveGizmoType
		{
			get
			{
				return this._activeGizmoType;
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00045088 File Offset: 0x00043288
		public static void SendToInterestedListeners(GizmoType activeGizmoType)
		{
			GizmosTurnedOffMessage message = new GizmosTurnedOffMessage(activeGizmoType);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007B9 RID: 1977
		private GizmoType _activeGizmoType;
	}
}
