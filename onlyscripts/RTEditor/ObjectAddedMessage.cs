using System;

namespace RTEditor
{
	// Token: 0x020001CE RID: 462
	public class ObjectAddedMessage : Message
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x0004523C File Offset: 0x0004343C
		public ObjectAddedMessage(levelEditorManager.Block[] Blocks) : base(MessageType.ObjectAdded)
		{
			this._blocks = Blocks;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00045250 File Offset: 0x00043450
		public levelEditorManager.Block[] Blocks
		{
			get
			{
				return this._blocks;
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00045258 File Offset: 0x00043458
		public static void SendToInterestedListeners(levelEditorManager.Block[] Blocks)
		{
			ObjectAddedMessage message = new ObjectAddedMessage(Blocks);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007C1 RID: 1985
		private levelEditorManager.Block[] _blocks;
	}
}
