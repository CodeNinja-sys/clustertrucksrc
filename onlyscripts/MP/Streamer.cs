using System;
using System.IO;
using MP.Net;

namespace MP
{
	// Token: 0x0200016A RID: 362
	public abstract class Streamer : Demux
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x00035A14 File Offset: 0x00033C14
		public static Streamer forUrl(string url)
		{
			if (url.StartsWith("http"))
			{
				return new HttpMjpegStreamer();
			}
			throw new MpException("Can't detect suitable Streamer for given url: " + url);
		}

		// Token: 0x06000819 RID: 2073
		public abstract void Connect(string url, LoadOptions loadOptions = null);

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600081A RID: 2074
		public abstract bool IsConnected { get; }

		// Token: 0x0600081B RID: 2075 RVA: 0x00035A48 File Offset: 0x00033C48
		public override void Init(Stream stream, LoadOptions loadOptions = null)
		{
			throw new MpException("Streamer requires you to call Connect() instead of Init()");
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00035A54 File Offset: 0x00033C54
		public override int ReadVideoFrame(out byte[] targetBuf)
		{
			throw new NotSupportedException("Can't read arbitrary frame from a stream");
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00035A60 File Offset: 0x00033C60
		public override int ReadAudioSamples(out byte[] targetBuf, int sampleCount)
		{
			throw new NotSupportedException("Can't read arbitrary audio from a stream");
		}
	}
}
