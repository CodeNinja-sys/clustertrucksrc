using System;

namespace MP.AVI
{
	// Token: 0x02000146 RID: 326
	public class AVIStreamHeader
	{
		// Token: 0x04000566 RID: 1382
		public const uint AVISF_DISABLED = 1U;

		// Token: 0x04000567 RID: 1383
		public const uint AVISF_VIDEO_PALCHANGES = 65536U;

		// Token: 0x04000568 RID: 1384
		public uint fccType;

		// Token: 0x04000569 RID: 1385
		public uint fccHandler;

		// Token: 0x0400056A RID: 1386
		public uint dwFlags;

		// Token: 0x0400056B RID: 1387
		public ushort wPriority;

		// Token: 0x0400056C RID: 1388
		public ushort wLanguage;

		// Token: 0x0400056D RID: 1389
		public uint dwInitialFrames;

		// Token: 0x0400056E RID: 1390
		public uint dwScale;

		// Token: 0x0400056F RID: 1391
		public uint dwRate;

		// Token: 0x04000570 RID: 1392
		public uint dwStart;

		// Token: 0x04000571 RID: 1393
		public uint dwLength;

		// Token: 0x04000572 RID: 1394
		public uint dwSuggestedBufferSize;

		// Token: 0x04000573 RID: 1395
		public uint dwQuality;

		// Token: 0x04000574 RID: 1396
		public uint dwSampleSize;

		// Token: 0x04000575 RID: 1397
		public short rcFrameLeft;

		// Token: 0x04000576 RID: 1398
		public short rcFrameTop;

		// Token: 0x04000577 RID: 1399
		public short rcFrameRight;

		// Token: 0x04000578 RID: 1400
		public short rcFrameBottom;
	}
}
