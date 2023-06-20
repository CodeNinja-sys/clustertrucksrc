using System;

namespace InControl
{
	// Token: 0x0200005F RID: 95
	public interface IInputControl
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600025C RID: 604
		bool HasChanged { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600025D RID: 605
		bool IsPressed { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600025E RID: 606
		bool WasPressed { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600025F RID: 607
		bool WasReleased { get; }

		// Token: 0x06000260 RID: 608
		void ClearInputState();
	}
}
