using System;

namespace RTEditor
{
	// Token: 0x020001D1 RID: 465
	public abstract class Message
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x000452B4 File Offset: 0x000434B4
		public Message(MessageType type)
		{
			this._type = type;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000452C4 File Offset: 0x000434C4
		public MessageType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040007C3 RID: 1987
		private MessageType _type;
	}
}
