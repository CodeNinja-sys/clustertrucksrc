using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MP.RAW
{
	// Token: 0x02000163 RID: 355
	public class RawMjpegDemux : Demux
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x00035024 File Offset: 0x00033224
		public override void Init(Stream sourceStream, LoadOptions loadOptions = null)
		{
			if (loadOptions != null && loadOptions.skipVideo)
			{
				return;
			}
			if (sourceStream == null)
			{
				throw new ArgumentException("sourceStream is required");
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			this.reader = new AtomicBinaryReader(sourceStream);
			int num = 0;
			this.frameStartIndex.Clear();
			this.frameSize.Clear();
			long num2 = 0L;
			long num3 = -1L;
			bool flag = false;
			long num4 = 0L;
			byte[] array = new byte[8096];
			long num5 = 0L;
			int num6;
			do
			{
				num6 = this.reader.Read(ref num5, array, 0, 8096);
				for (int i = 0; i < num6; i++)
				{
					byte b = array[i];
					if (b == 255)
					{
						flag = true;
					}
					else if (flag)
					{
						byte b2 = b;
						if (b2 != 216)
						{
							if (b2 == 217)
							{
								this.frameStartIndex.Add(num3);
								int num7 = (int)(num4 + (long)i - num3 + 1L);
								if (num7 > num)
								{
									num = num7;
								}
								this.frameSize.Add(num7);
							}
						}
						else
						{
							num3 = num4 + (long)i - 1L;
						}
						flag = false;
						num2 += 1L;
					}
				}
				num4 += (long)num6;
			}
			while (num6 >= 8096);
			this.rawJpgBuffer = new byte[num];
			stopwatch.Stop();
			if (loadOptions != null && loadOptions.videoStreamInfo != null)
			{
				base.videoStreamInfo = loadOptions.videoStreamInfo;
			}
			else
			{
				base.videoStreamInfo = new VideoStreamInfo();
				base.videoStreamInfo.codecFourCC = 1196444237U;
			}
			base.videoStreamInfo.frameCount = this.frameSize.Count;
			base.videoStreamInfo.lengthBytes = this.reader.StreamLength;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000351F4 File Offset: 0x000333F4
		public override void Shutdown(bool force = false)
		{
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x000351F8 File Offset: 0x000333F8
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00035200 File Offset: 0x00033400
		public override int VideoPosition
		{
			get
			{
				return this.nextVideoFrame;
			}
			set
			{
				this.nextVideoFrame = value;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0003520C File Offset: 0x0003340C
		public override int ReadVideoFrame(out byte[] targetBuf)
		{
			targetBuf = this.rawJpgBuffer;
			if (this.nextVideoFrame < 0 || this.nextVideoFrame >= base.videoStreamInfo.frameCount)
			{
				return 0;
			}
			this.nextVideoFrame++;
			long num = this.frameStartIndex[this.nextVideoFrame - 1];
			return this.reader.Read(ref num, this.rawJpgBuffer, 0, this.frameSize[this.nextVideoFrame - 1]);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00035290 File Offset: 0x00033490
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0003529C File Offset: 0x0003349C
		public override int AudioPosition
		{
			get
			{
				throw new NotSupportedException("There's no hidden audio in raw MJPEG stream");
			}
			set
			{
				throw new NotSupportedException("There's no hidden audio in raw MJPEG stream");
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000352A8 File Offset: 0x000334A8
		public override int ReadAudioSamples(out byte[] targetBuf, int sampleCount)
		{
			throw new NotSupportedException("There's no hidden audio in raw MJPEG stream");
		}

		// Token: 0x0400063B RID: 1595
		private const int FILE_READ_BUFFER_SIZE = 8096;

		// Token: 0x0400063C RID: 1596
		private AtomicBinaryReader reader;

		// Token: 0x0400063D RID: 1597
		private List<long> frameStartIndex = new List<long>();

		// Token: 0x0400063E RID: 1598
		private List<int> frameSize = new List<int>();

		// Token: 0x0400063F RID: 1599
		private byte[] rawJpgBuffer;

		// Token: 0x04000640 RID: 1600
		private int nextVideoFrame;
	}
}
