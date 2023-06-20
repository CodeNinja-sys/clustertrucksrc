using System;
using System.IO;

namespace MP.RAW
{
	// Token: 0x02000164 RID: 356
	public class RawPcmDemux : Demux
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x000352BC File Offset: 0x000334BC
		public override void Init(Stream sourceStream, LoadOptions loadOptions = null)
		{
			if (sourceStream == null || loadOptions == null || loadOptions.audioStreamInfo == null)
			{
				throw new ArgumentException("sourceStream and loadOptions.audioStreamInfo are required");
			}
			this.reader = new AtomicBinaryReader(sourceStream);
			base.audioStreamInfo = loadOptions.audioStreamInfo;
			base.audioStreamInfo.lengthBytes = this.reader.StreamLength;
			this.nextAudioSample = 0;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00035320 File Offset: 0x00033520
		public override void Shutdown(bool force = false)
		{
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00035324 File Offset: 0x00033524
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x00035330 File Offset: 0x00033530
		public override int VideoPosition
		{
			get
			{
				throw new NotSupportedException("There's no hidden video in raw PCM audio");
			}
			set
			{
				throw new NotSupportedException("There's no hidden video in raw PCM audio");
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0003533C File Offset: 0x0003353C
		public override int ReadVideoFrame(out byte[] targetBuf)
		{
			throw new NotSupportedException("There's no hidden video in raw PCM audio");
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00035348 File Offset: 0x00033548
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x00035350 File Offset: 0x00033550
		public override int AudioPosition
		{
			get
			{
				return this.nextAudioSample;
			}
			set
			{
				this.nextAudioSample = value;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0003535C File Offset: 0x0003355C
		public override int ReadAudioSamples(out byte[] targetBuf, int sampleCount)
		{
			if (this.nextAudioSample + sampleCount > base.audioStreamInfo.sampleCount)
			{
				sampleCount = base.audioStreamInfo.sampleCount - this.nextAudioSample;
			}
			int num = sampleCount * base.audioStreamInfo.sampleSize;
			if (this.rawAudioBuf == null || this.rawAudioBuf.Length < num)
			{
				this.rawAudioBuf = new byte[num];
			}
			targetBuf = this.rawAudioBuf;
			if (num <= 0)
			{
				return 0;
			}
			long num2 = (long)(this.nextAudioSample * base.audioStreamInfo.sampleSize);
			this.nextAudioSample += sampleCount;
			return this.reader.Read(ref num2, this.rawAudioBuf, 0, num) / base.audioStreamInfo.sampleSize;
		}

		// Token: 0x04000641 RID: 1601
		private AtomicBinaryReader reader;

		// Token: 0x04000642 RID: 1602
		private byte[] rawAudioBuf;

		// Token: 0x04000643 RID: 1603
		private int nextAudioSample;
	}
}
