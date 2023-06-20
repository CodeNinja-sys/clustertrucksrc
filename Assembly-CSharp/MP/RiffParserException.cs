using System;
using System.Runtime.Serialization;

namespace MP
{
	// Token: 0x02000166 RID: 358
	public class RiffParserException : ApplicationException
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x0003544C File Offset: 0x0003364C
		public RiffParserException()
		{
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00035454 File Offset: 0x00033654
		public RiffParserException(string msg) : base(msg)
		{
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00035460 File Offset: 0x00033660
		public RiffParserException(string msg, Exception inner) : base(msg, inner)
		{
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0003546C File Offset: 0x0003366C
		public RiffParserException(SerializationInfo info, StreamingContext ctx) : base(info, ctx)
		{
		}
	}
}
