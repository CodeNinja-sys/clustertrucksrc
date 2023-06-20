using System;

namespace RTEditor
{
	// Token: 0x020001C4 RID: 452
	public class ObjectSelectionChangedMessage : Message
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x00044FFC File Offset: 0x000431FC
		public ObjectSelectionChangedMessage() : base(MessageType.ObjectSelectionChanged)
		{
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00045008 File Offset: 0x00043208
		public static void SendToInterestedListeners()
		{
			ObjectSelectionChangedMessage message = new ObjectSelectionChangedMessage();
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}
	}
}
