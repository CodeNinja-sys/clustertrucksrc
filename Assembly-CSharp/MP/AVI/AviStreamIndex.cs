using System;
using System.Collections.Generic;

namespace MP.AVI
{
	// Token: 0x0200014A RID: 330
	public class AviStreamIndex
	{
		// Token: 0x0400058C RID: 1420
		public uint streamId;

		// Token: 0x0400058D RID: 1421
		public List<AviStreamIndex.Entry> entries = new List<AviStreamIndex.Entry>();

		// Token: 0x0400058E RID: 1422
		public int globalOffset;

		// Token: 0x0200014B RID: 331
		public class Entry
		{
			// Token: 0x0400058F RID: 1423
			public long chunkOffset;

			// Token: 0x04000590 RID: 1424
			public int chunkLength;

			// Token: 0x04000591 RID: 1425
			public bool isKeyframe;
		}

		// Token: 0x0200014C RID: 332
		public enum Type
		{
			// Token: 0x04000593 RID: 1427
			SUPERINDEX,
			// Token: 0x04000594 RID: 1428
			CHUNKS,
			// Token: 0x04000595 RID: 1429
			DATA = 128
		}
	}
}
