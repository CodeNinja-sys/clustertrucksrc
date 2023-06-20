using System;
using System.IO;
using MP.AVI;
using MP.RAW;

namespace MP
{
	// Token: 0x02000159 RID: 345
	public abstract class Demux
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x00033D98 File Offset: 0x00031F98
		public static Demux forSource(Stream sourceStream)
		{
			byte[] array = new byte[4];
			sourceStream.Seek(0L, SeekOrigin.Begin);
			if (sourceStream.Read(array, 0, 4) < 4)
			{
				throw new MpException("Stream too small");
			}
			if (array[0] == 82 && array[1] == 73 && array[2] == 70 && (array[3] == 70 || array[3] == 88))
			{
				return new AviDemux();
			}
			if (array[0] == 255 && array[1] == 216)
			{
				sourceStream.Seek(-2L, SeekOrigin.End);
				sourceStream.Read(array, 0, 2);
				if (array[0] == 255 && array[1] == 217)
				{
					return new RawMjpegDemux();
				}
			}
			throw new MpException("Can't detect suitable DEMUX for given stream");
		}

		// Token: 0x0600079C RID: 1948
		public abstract void Init(Stream sourceStream, LoadOptions loadOptions = null);

		// Token: 0x0600079D RID: 1949
		public abstract void Shutdown(bool force = false);

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00033E64 File Offset: 0x00032064
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00033E6C File Offset: 0x0003206C
		public VideoStreamInfo videoStreamInfo { get; protected set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00033E78 File Offset: 0x00032078
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00033E80 File Offset: 0x00032080
		public AudioStreamInfo audioStreamInfo { get; protected set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00033E8C File Offset: 0x0003208C
		public bool hasVideo
		{
			get
			{
				return this.videoStreamInfo != null;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00033E9C File Offset: 0x0003209C
		public bool hasAudio
		{
			get
			{
				return this.audioStreamInfo != null;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007A4 RID: 1956
		// (set) Token: 0x060007A5 RID: 1957
		public abstract int VideoPosition { get; set; }

		// Token: 0x060007A6 RID: 1958
		public abstract int ReadVideoFrame(out byte[] targetBuf);

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007A7 RID: 1959
		// (set) Token: 0x060007A8 RID: 1960
		public abstract int AudioPosition { get; set; }

		// Token: 0x060007A9 RID: 1961
		public abstract int ReadAudioSamples(out byte[] targetBuf, int sampleCount);
	}
}
