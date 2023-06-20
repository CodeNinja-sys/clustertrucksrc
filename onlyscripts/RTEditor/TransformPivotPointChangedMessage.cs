using System;

namespace RTEditor
{
	// Token: 0x020001C7 RID: 455
	public class TransformPivotPointChangedMessage : Message
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x000450A8 File Offset: 0x000432A8
		public TransformPivotPointChangedMessage(TransformPivotPoint oldPivotPoint, TransformPivotPoint newPivotPoint) : base(MessageType.TransformPivotPointChanged)
		{
			this._oldPivotPoint = oldPivotPoint;
			this._newPivotPoint = newPivotPoint;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000450C0 File Offset: 0x000432C0
		public TransformPivotPoint OldPivotPoint
		{
			get
			{
				return this._oldPivotPoint;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000450C8 File Offset: 0x000432C8
		public TransformPivotPoint NewPivotPoint
		{
			get
			{
				return this._newPivotPoint;
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000450D0 File Offset: 0x000432D0
		public static void SendToInterestedListeners(TransformPivotPoint oldPivotPoint, TransformPivotPoint newPivotPoint)
		{
			TransformPivotPointChangedMessage message = new TransformPivotPointChangedMessage(oldPivotPoint, newPivotPoint);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007BA RID: 1978
		private TransformPivotPoint _oldPivotPoint;

		// Token: 0x040007BB RID: 1979
		private TransformPivotPoint _newPivotPoint;
	}
}
