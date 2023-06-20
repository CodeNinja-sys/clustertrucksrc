using System;

namespace MP
{
	// Token: 0x02000153 RID: 339
	public class AudioStreamInfo
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x00032C98 File Offset: 0x00030E98
		public AudioStreamInfo()
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00032CA0 File Offset: 0x00030EA0
		public AudioStreamInfo(AudioStreamInfo ai)
		{
			this.codecFourCC = ai.codecFourCC;
			this.audioFormat = ai.audioFormat;
			this.sampleCount = ai.sampleCount;
			this.sampleSize = ai.sampleSize;
			this.channels = ai.channels;
			this.sampleRate = ai.sampleRate;
			this.lengthBytes = ai.lengthBytes;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00032D08 File Offset: 0x00030F08
		public float lengthSeconds
		{
			get
			{
				return (float)this.sampleCount / (float)this.sampleRate;
			}
		}

		// Token: 0x040005D2 RID: 1490
		public uint codecFourCC;

		// Token: 0x040005D3 RID: 1491
		public uint audioFormat;

		// Token: 0x040005D4 RID: 1492
		public int sampleCount;

		// Token: 0x040005D5 RID: 1493
		public int sampleSize;

		// Token: 0x040005D6 RID: 1494
		public int channels;

		// Token: 0x040005D7 RID: 1495
		public int sampleRate;

		// Token: 0x040005D8 RID: 1496
		public long lengthBytes;
	}
}
