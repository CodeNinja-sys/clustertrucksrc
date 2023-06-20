using System;

namespace RTEditor
{
	// Token: 0x020001C9 RID: 457
	public class GizmoTransformOperationWasUndoneMessage : Message
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x00045138 File Offset: 0x00043338
		public GizmoTransformOperationWasUndoneMessage(Gizmo gizmoInvolvedInTransformOperation) : base(MessageType.GizmoTransformOperationWasUndone)
		{
			this._gizmoInvolvedInTransformOperation = gizmoInvolvedInTransformOperation;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00045148 File Offset: 0x00043348
		public Gizmo GizmoInvolvedInTransformOperation
		{
			get
			{
				return this._gizmoInvolvedInTransformOperation;
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00045150 File Offset: 0x00043350
		public static void SendToInterestedListeners(Gizmo gizmoInvolvedInTransformOperation)
		{
			GizmoTransformOperationWasUndoneMessage message = new GizmoTransformOperationWasUndoneMessage(gizmoInvolvedInTransformOperation);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007BE RID: 1982
		private Gizmo _gizmoInvolvedInTransformOperation;
	}
}
