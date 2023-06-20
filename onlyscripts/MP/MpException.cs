using System;
using System.Runtime.Serialization;

namespace MP
{
	// Token: 0x02000161 RID: 353
	public class MpException : ApplicationException
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x000349F8 File Offset: 0x00032BF8
		public MpException()
		{
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00034A00 File Offset: 0x00032C00
		public MpException(string msg) : base(msg)
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00034A0C File Offset: 0x00032C0C
		public MpException(string msg, Exception inner) : base(msg, inner)
		{
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00034A18 File Offset: 0x00032C18
		public MpException(SerializationInfo info, StreamingContext ctx) : base(info, ctx)
		{
		}
	}
}
