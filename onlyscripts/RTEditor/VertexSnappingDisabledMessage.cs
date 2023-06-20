using System;

namespace RTEditor
{
	// Token: 0x020001CC RID: 460
	public class VertexSnappingDisabledMessage : Message
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x000451D4 File Offset: 0x000433D4
		public VertexSnappingDisabledMessage() : base(MessageType.VertexSnappingDisabled)
		{
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000451E0 File Offset: 0x000433E0
		public static void SendToInterestedListeners()
		{
			VertexSnappingDisabledMessage message = new VertexSnappingDisabledMessage();
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}
	}
}
