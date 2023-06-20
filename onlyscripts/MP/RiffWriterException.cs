using System;
using System.Runtime.Serialization;

namespace MP
{
	// Token: 0x02000168 RID: 360
	public class RiffWriterException : ApplicationException
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x00035778 File Offset: 0x00033978
		public RiffWriterException()
		{
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00035780 File Offset: 0x00033980
		public RiffWriterException(string msg) : base(msg)
		{
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0003578C File Offset: 0x0003398C
		public RiffWriterException(string msg, Exception inner) : base(msg, inner)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00035798 File Offset: 0x00033998
		public RiffWriterException(SerializationInfo info, StreamingContext ctx) : base(info, ctx)
		{
		}
	}
}
