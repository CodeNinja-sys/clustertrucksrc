using System;
using MP.Decoder;
using UnityEngine;

namespace MP
{
	// Token: 0x02000152 RID: 338
	public abstract class AudioDecoder
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00032C10 File Offset: 0x00030E10
		public static AudioDecoder CreateFor(AudioStreamInfo streamInfo)
		{
			if (streamInfo == null)
			{
				throw new ArgumentException("Can't choose AudioDecoder without streamInfo (with at least codecFourCC)");
			}
			uint codecFourCC = streamInfo.codecFourCC;
			if (codecFourCC != 0U && codecFourCC != 1U)
			{
				throw new MpException(string.Concat(new string[]
				{
					"No decoder for audio fourCC 0x",
					streamInfo.codecFourCC.ToString("X"),
					" (",
					RiffParser.FromFourCC(streamInfo.codecFourCC),
					")"
				}));
			}
			return new AudioDecoderPCM(streamInfo);
		}

		// Token: 0x0600076C RID: 1900
		public abstract void Init(out AudioClip audioClip, Demux demux, LoadOptions loadOptions = null);

		// Token: 0x0600076D RID: 1901
		public abstract void Shutdown();

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600076E RID: 1902
		// (set) Token: 0x0600076F RID: 1903
		public abstract int Position { get; set; }

		// Token: 0x06000770 RID: 1904
		public abstract void DecodeNext(float[] data, int sampleCount);

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000771 RID: 1905
		public abstract float totalDecodeTime { get; }
	}
}
