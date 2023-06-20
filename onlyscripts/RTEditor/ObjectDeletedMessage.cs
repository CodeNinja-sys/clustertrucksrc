using System;

namespace RTEditor
{
	// Token: 0x020001CD RID: 461
	public class ObjectDeletedMessage : Message
	{
		// Token: 0x06000B01 RID: 2817 RVA: 0x00045200 File Offset: 0x00043400
		public ObjectDeletedMessage(levelEditorManager.Block[] Block) : base(MessageType.ObjectDeleted)
		{
			this._blocks = Block;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00045214 File Offset: 0x00043414
		public levelEditorManager.Block[] Blocks
		{
			get
			{
				return this._blocks;
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0004521C File Offset: 0x0004341C
		public static void SendToInterestedListeners(levelEditorManager.Block[] Blocks)
		{
			ObjectDeletedMessage message = new ObjectDeletedMessage(Blocks);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007C0 RID: 1984
		private levelEditorManager.Block[] _blocks;
	}
}
