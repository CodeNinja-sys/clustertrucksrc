using System;

namespace RTEditor
{
	// Token: 0x020001C8 RID: 456
	public class ActiveGizmoTypeChangedMessage : Message
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x000450F0 File Offset: 0x000432F0
		public ActiveGizmoTypeChangedMessage(GizmoType oldActiveGizmoType, GizmoType newActiveGizmoType) : base(MessageType.ActiveGizmoTypeChanged)
		{
			this._oldActiveGizmoType = oldActiveGizmoType;
			this._newActiveGizmoType = newActiveGizmoType;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00045108 File Offset: 0x00043308
		public GizmoType OldActiveGizmoType
		{
			get
			{
				return this._oldActiveGizmoType;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00045110 File Offset: 0x00043310
		public GizmoType NewActiveGizmoType
		{
			get
			{
				return this._newActiveGizmoType;
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00045118 File Offset: 0x00043318
		public static void SendToInterestedListeners(GizmoType oldActiveGizmoType, GizmoType newActiveGizmoType)
		{
			ActiveGizmoTypeChangedMessage message = new ActiveGizmoTypeChangedMessage(oldActiveGizmoType, newActiveGizmoType);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007BC RID: 1980
		private GizmoType _oldActiveGizmoType;

		// Token: 0x040007BD RID: 1981
		private GizmoType _newActiveGizmoType;
	}
}
