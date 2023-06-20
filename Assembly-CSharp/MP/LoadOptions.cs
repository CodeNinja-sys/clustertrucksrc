using System;

namespace MP
{
	// Token: 0x0200015D RID: 349
	[Serializable]
	public class LoadOptions
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000344B0 File Offset: 0x000326B0
		public static LoadOptions Default
		{
			get
			{
				return new LoadOptions();
			}
		}

		// Token: 0x0400061D RID: 1565
		public bool _3DSound;

		// Token: 0x0400061E RID: 1566
		public bool preloadAudio;

		// Token: 0x0400061F RID: 1567
		public bool preloadVideo;

		// Token: 0x04000620 RID: 1568
		public bool skipVideo;

		// Token: 0x04000621 RID: 1569
		public bool skipAudio;

		// Token: 0x04000622 RID: 1570
		public AudioStreamInfo audioStreamInfo;

		// Token: 0x04000623 RID: 1571
		public VideoStreamInfo videoStreamInfo;

		// Token: 0x04000624 RID: 1572
		public float connectTimeout = 10f;

		// Token: 0x04000625 RID: 1573
		public Demux demuxOverride;

		// Token: 0x04000626 RID: 1574
		public bool enableExceptionThrow;
	}
}
