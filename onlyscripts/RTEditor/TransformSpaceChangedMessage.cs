using System;

namespace RTEditor
{
	// Token: 0x020001C5 RID: 453
	public class TransformSpaceChangedMessage : Message
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x00045028 File Offset: 0x00043228
		public TransformSpaceChangedMessage(TransformSpace oldTransformSpace, TransformSpace newTransformSpace) : base(MessageType.TransformSpaceChanged)
		{
			this._oldTransformSpace = oldTransformSpace;
			this._newTransformSpace = newTransformSpace;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00045040 File Offset: 0x00043240
		public TransformSpace OldTransformSpace
		{
			get
			{
				return this._oldTransformSpace;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00045048 File Offset: 0x00043248
		public TransformSpace NewTransformSpace
		{
			get
			{
				return this._newTransformSpace;
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00045050 File Offset: 0x00043250
		public static void SendToInterestedListeners(TransformSpace oldTransformSpace, TransformSpace newTransformSpace)
		{
			TransformSpaceChangedMessage message = new TransformSpaceChangedMessage(oldTransformSpace, newTransformSpace);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007B7 RID: 1975
		private TransformSpace _oldTransformSpace;

		// Token: 0x040007B8 RID: 1976
		private TransformSpace _newTransformSpace;
	}
}
