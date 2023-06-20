using System;
using System.IO;

namespace MP
{
	// Token: 0x02000165 RID: 357
	public abstract class Remux
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x00035424 File Offset: 0x00033624
		public virtual void Init(Stream dstStream, VideoStreamInfo videoStreamInfo, AudioStreamInfo audioStreamInfo)
		{
			this.dstStream = dstStream;
			this._videoStreamInfo = videoStreamInfo;
			this._audioStreamInfo = audioStreamInfo;
		}

		// Token: 0x060007F1 RID: 2033
		public abstract void Shutdown();

		// Token: 0x060007F2 RID: 2034
		public abstract void WriteNextVideoFrame(byte[] frameBytes, int size = -1);

		// Token: 0x060007F3 RID: 2035
		public abstract void WriteVideoFrame(int frameOffset, byte[] frameBytes, int size = -1);

		// Token: 0x060007F4 RID: 2036
		public abstract void WriteNextAudioSamples(byte[] sampleBytes, int size = -1);

		// Token: 0x060007F5 RID: 2037
		public abstract void WriteAudioSamples(int sampleOffset, byte[] sampleBytes, int size = -1);

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0003543C File Offset: 0x0003363C
		public VideoStreamInfo videoStreamInfo
		{
			get
			{
				return this._videoStreamInfo;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00035444 File Offset: 0x00033644
		public AudioStreamInfo audioStreamInfo
		{
			get
			{
				return this._audioStreamInfo;
			}
		}

		// Token: 0x04000644 RID: 1604
		protected Stream dstStream;

		// Token: 0x04000645 RID: 1605
		private VideoStreamInfo _videoStreamInfo;

		// Token: 0x04000646 RID: 1606
		private AudioStreamInfo _audioStreamInfo;
	}
}
