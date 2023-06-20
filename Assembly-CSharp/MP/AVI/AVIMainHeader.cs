using System;

namespace MP.AVI
{
	// Token: 0x02000145 RID: 325
	public class AVIMainHeader
	{
		// Token: 0x04000552 RID: 1362
		public const uint AVIF_COPYRIGHTED = 131072U;

		// Token: 0x04000553 RID: 1363
		public const uint AVIF_HASINDEX = 16U;

		// Token: 0x04000554 RID: 1364
		public const uint AVIF_ISINTERLEAVED = 256U;

		// Token: 0x04000555 RID: 1365
		public const uint AVIF_MUSTUSEINDEX = 32U;

		// Token: 0x04000556 RID: 1366
		public const uint AVIF_TRUSTCKTYPE = 2048U;

		// Token: 0x04000557 RID: 1367
		public const uint AVIF_WASCAPTUREFILE = 65536U;

		// Token: 0x04000558 RID: 1368
		public uint dwMicroSecPerFrame;

		// Token: 0x04000559 RID: 1369
		public uint dwMaxBytesPerSec;

		// Token: 0x0400055A RID: 1370
		public uint dwPaddingGranularity;

		// Token: 0x0400055B RID: 1371
		public uint dwFlags;

		// Token: 0x0400055C RID: 1372
		public uint dwTotalFrames;

		// Token: 0x0400055D RID: 1373
		public uint dwInitialFrames;

		// Token: 0x0400055E RID: 1374
		public uint dwStreams;

		// Token: 0x0400055F RID: 1375
		public uint dwSuggestedBufferSize;

		// Token: 0x04000560 RID: 1376
		public uint dwWidth;

		// Token: 0x04000561 RID: 1377
		public uint dwHeight;

		// Token: 0x04000562 RID: 1378
		public uint dwReserved0;

		// Token: 0x04000563 RID: 1379
		public uint dwReserved1;

		// Token: 0x04000564 RID: 1380
		public uint dwReserved2;

		// Token: 0x04000565 RID: 1381
		public uint dwReserved3;
	}
}
