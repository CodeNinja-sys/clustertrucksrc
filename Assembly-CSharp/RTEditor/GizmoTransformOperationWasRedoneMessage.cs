using System;

namespace RTEditor
{
	// Token: 0x020001CA RID: 458
	public class GizmoTransformOperationWasRedoneMessage : Message
	{
		// Token: 0x06000AFA RID: 2810 RVA: 0x00045170 File Offset: 0x00043370
		public GizmoTransformOperationWasRedoneMessage(Gizmo gizmoInvolvedInTransformOperation) : base(MessageType.GizmoTransformOperationWasRedone)
		{
			this._gizmoInvolvedInTransformOperation = gizmoInvolvedInTransformOperation;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00045180 File Offset: 0x00043380
		public Gizmo GizmoInvolvedInTransformOperation
		{
			get
			{
				return this._gizmoInvolvedInTransformOperation;
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00045188 File Offset: 0x00043388
		public static void SendToInterestedListeners(Gizmo gizmoInvolvedInTransformOperation)
		{
			GizmoTransformOperationWasRedoneMessage message = new GizmoTransformOperationWasRedoneMessage(gizmoInvolvedInTransformOperation);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007BF RID: 1983
		private Gizmo _gizmoInvolvedInTransformOperation;
	}
}
