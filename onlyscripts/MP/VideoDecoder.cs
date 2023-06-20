using System;
using MP.Decoder;
using UnityEngine;

namespace MP
{
	// Token: 0x0200016B RID: 363
	public abstract class VideoDecoder
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x00035A74 File Offset: 0x00033C74
		public static VideoDecoder CreateFor(VideoStreamInfo streamInfo)
		{
			if (streamInfo == null)
			{
				throw new ArgumentException("Can't choose VideoDecoder without streamInfo (with at least codecFourCC)");
			}
			uint codecFourCC = streamInfo.codecFourCC;
			if (codecFourCC == 0U || codecFourCC == 541215044U)
			{
				return new VideoDecoderRGB(streamInfo);
			}
			if (codecFourCC == 1196314701U)
			{
				return new VideoDecoderMPNG(streamInfo);
			}
			if (codecFourCC != 1196444227U && codecFourCC != 1196444237U && codecFourCC != 1734701162U && codecFourCC != 1935959654U)
			{
				throw new MpException(string.Concat(new string[]
				{
					"No decoder for video fourCC 0x",
					streamInfo.codecFourCC.ToString("X"),
					" (",
					RiffParser.FromFourCC(streamInfo.codecFourCC),
					")"
				}));
			}
			return new VideoDecoderMJPEG(streamInfo);
		}

		// Token: 0x06000820 RID: 2080
		public abstract void Init(out Texture2D framebuffer, Demux demux, LoadOptions loadOptions = null);

		// Token: 0x06000821 RID: 2081
		public abstract void Shutdown();

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000822 RID: 2082
		// (set) Token: 0x06000823 RID: 2083
		public abstract int Position { get; set; }

		// Token: 0x06000824 RID: 2084
		public abstract void DecodeNext();

		// Token: 0x06000825 RID: 2085 RVA: 0x00035B48 File Offset: 0x00033D48
		public void Decode(int frame)
		{
			this.Position = frame;
			this.DecodeNext();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000826 RID: 2086
		public abstract float lastFrameDecodeTime { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000827 RID: 2087
		public abstract int lastFrameSizeBytes { get; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000828 RID: 2088
		public abstract float totalDecodeTime { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000829 RID: 2089
		public abstract long totalSizeBytes { get; }
	}
}
