using System;

namespace MP
{
	// Token: 0x0200016C RID: 364
	public class VideoStreamInfo
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x00035B58 File Offset: 0x00033D58
		public VideoStreamInfo()
		{
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00035B8C File Offset: 0x00033D8C
		public VideoStreamInfo(VideoStreamInfo vi)
		{
			this.codecFourCC = vi.codecFourCC;
			this.bitsPerPixel = vi.bitsPerPixel;
			this.frameCount = vi.frameCount;
			this.width = vi.width;
			this.height = vi.height;
			this.framerate = vi.framerate;
			this.lengthBytes = vi.lengthBytes;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00035C14 File Offset: 0x00033E14
		public float lengthSeconds
		{
			get
			{
				return (float)this.frameCount / this.framerate;
			}
		}

		// Token: 0x04000651 RID: 1617
		public uint codecFourCC;

		// Token: 0x04000652 RID: 1618
		public int bitsPerPixel;

		// Token: 0x04000653 RID: 1619
		public int frameCount = 1;

		// Token: 0x04000654 RID: 1620
		public int width = 1;

		// Token: 0x04000655 RID: 1621
		public int height = 1;

		// Token: 0x04000656 RID: 1622
		public float framerate = 30f;

		// Token: 0x04000657 RID: 1623
		public long lengthBytes;
	}
}
