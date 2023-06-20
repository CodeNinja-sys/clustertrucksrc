using System;

namespace MP.AVI
{
	// Token: 0x0200014D RID: 333
	public class AVIFile
	{
		// Token: 0x04000596 RID: 1430
		public AVIMainHeader avih;

		// Token: 0x04000597 RID: 1431
		public AVIStreamHeader strhVideo;

		// Token: 0x04000598 RID: 1432
		public BitmapInfoHeader strfVideo;

		// Token: 0x04000599 RID: 1433
		public AVIStreamHeader strhAudio;

		// Token: 0x0400059A RID: 1434
		public WaveFormatEx strfAudio;

		// Token: 0x0400059B RID: 1435
		public ODMLHeader odml;

		// Token: 0x0400059C RID: 1436
		public AviStreamIndex videoIndex;

		// Token: 0x0400059D RID: 1437
		public AviStreamIndex audioIndex;
	}
}
