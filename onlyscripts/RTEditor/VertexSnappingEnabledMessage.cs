using System;

namespace RTEditor
{
	// Token: 0x020001CB RID: 459
	public class VertexSnappingEnabledMessage : Message
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x000451A8 File Offset: 0x000433A8
		public VertexSnappingEnabledMessage() : base(MessageType.VertexSnappingEnabled)
		{
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000451B4 File Offset: 0x000433B4
		public static void SendToInterestedListeners()
		{
			VertexSnappingEnabledMessage message = new VertexSnappingEnabledMessage();
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}
	}
}
