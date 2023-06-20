using System;

namespace InControl
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public class InControlException : Exception
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000E79C File Offset: 0x0000C99C
		public InControlException()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		public InControlException(string message) : base(message)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		public InControlException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
