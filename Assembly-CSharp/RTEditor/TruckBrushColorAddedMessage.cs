using System;

namespace RTEditor
{
	// Token: 0x020001CF RID: 463
	public class TruckBrushColorAddedMessage : Message
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00045278 File Offset: 0x00043478
		public TruckBrushColorAddedMessage(levelEditorManager.Block Block) : base(MessageType.TruckBrushColorAdded)
		{
			this._block = Block;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0004528C File Offset: 0x0004348C
		public levelEditorManager.Block Block
		{
			get
			{
				return this._block;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00045294 File Offset: 0x00043494
		public static void SendToInterestedListeners(levelEditorManager.Block Block)
		{
			TruckBrushColorAddedMessage message = new TruckBrushColorAddedMessage(Block);
			SingletonBase<MessageListenerDatabase>.Instance.SendMessageToInterestedListeners(message);
		}

		// Token: 0x040007C2 RID: 1986
		private levelEditorManager.Block _block;
	}
}
