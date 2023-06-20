using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MP.AVI
{
	// Token: 0x0200014E RID: 334
	public class AviDemux : Demux
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00030870 File Offset: 0x0002EA70
		public override void Init(Stream sourceStream, LoadOptions loadOptions = null)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			this.reader = new AtomicBinaryReader(sourceStream);
			RiffParser riffParser = new RiffParser(this.reader);
			this.avi = new AVIFile();
			this.idx1EntryOffset = -1L;
			this.idx1Offset = -1L;
			this.currentStrh4CC = 0U;
			while (riffParser.ReadNext(new RiffParser.ProcessChunkElement(this.ProcessAviChunk), new RiffParser.ProcessListElement(this.ProcessAviList), new RiffParser.ProcessRiffElement(this.ProcessAviRiff)))
			{
			}
			if (this.avi.strhVideo != null)
			{
				base.videoStreamInfo = new VideoStreamInfo();
				base.videoStreamInfo.codecFourCC = this.avi.strhVideo.fccHandler;
				base.videoStreamInfo.bitsPerPixel = (int)this.avi.strfVideo.biBitCount;
				base.videoStreamInfo.frameCount = (int)((this.avi.odml == null) ? this.avi.avih.dwTotalFrames : this.avi.odml.dwTotalFrames);
				if (base.videoStreamInfo.frameCount == 0)
				{
					base.videoStreamInfo.frameCount = (int)this.avi.strhVideo.dwLength;
				}
				base.videoStreamInfo.width = (int)this.avi.avih.dwWidth;
				base.videoStreamInfo.height = (int)this.avi.avih.dwHeight;
				base.videoStreamInfo.framerate = this.avi.strhVideo.dwRate / this.avi.strhVideo.dwScale;
			}
			else
			{
				base.videoStreamInfo = null;
			}
			if (this.avi.strhAudio != null)
			{
				base.audioStreamInfo = new AudioStreamInfo();
				base.audioStreamInfo.codecFourCC = this.avi.strhAudio.fccHandler;
				base.audioStreamInfo.audioFormat = (uint)this.avi.strfAudio.wFormatTag;
				base.audioStreamInfo.sampleCount = (int)this.avi.strhAudio.dwLength;
				base.audioStreamInfo.sampleSize = (int)this.avi.strhAudio.dwSampleSize;
				base.audioStreamInfo.channels = (int)this.avi.strfAudio.nChannels;
				base.audioStreamInfo.sampleRate = (int)this.avi.strfAudio.nSamplesPerSec;
			}
			else
			{
				base.audioStreamInfo = null;
			}
			if (base.hasVideo)
			{
				if (this.avi.videoIndex == null)
				{
					this.avi.videoIndex = AviDemux.ParseOldIndex(this.idx1Offset, riffParser.reader, this.idx1Size, 1667510320U, this.idx1EntryOffset);
				}
				if (this.avi.videoIndex == null)
				{
					throw new MpException("No video index found (required for playback and seeking)");
				}
				this.PrepareVideoStream();
			}
			if (base.hasAudio)
			{
				if (this.avi.audioIndex == null)
				{
					this.avi.audioIndex = AviDemux.ParseOldIndex(this.idx1Offset, riffParser.reader, this.idx1Size, 1651978544U, this.idx1EntryOffset);
				}
				if (this.avi.audioIndex == null)
				{
					throw new MpException("No audio index found (required for playback and seeking)");
				}
				this.PrepareAudioStream();
			}
			if (base.videoStreamInfo != null && this.avi.videoIndex != null && base.videoStreamInfo.frameCount > this.avi.videoIndex.entries.Count)
			{
				base.videoStreamInfo.frameCount = this.avi.videoIndex.entries.Count;
			}
			stopwatch.Stop();
			this.nextVideoFrame = 0;
			this.nextAudioSample = 0;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00030C34 File Offset: 0x0002EE34
		public override void Shutdown(bool force = false)
		{
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00030C38 File Offset: 0x0002EE38
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00030C40 File Offset: 0x0002EE40
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

		// Token: 0x06000736 RID: 1846 RVA: 0x00030C4C File Offset: 0x0002EE4C
		public override int ReadVideoFrame(out byte[] targetBuf)
		{
			targetBuf = this.rawVideoBuf;
			if (this.nextVideoFrame < 0 || this.nextVideoFrame >= base.videoStreamInfo.frameCount)
			{
				return 0;
			}
			AviStreamIndex.Entry entry = this.avi.videoIndex.entries[this.nextVideoFrame++];
			long chunkOffset = entry.chunkOffset;
			return this.reader.Read(ref chunkOffset, this.rawVideoBuf, 0, entry.chunkLength);
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00030CD0 File Offset: 0x0002EED0
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00030CD8 File Offset: 0x0002EED8
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

		// Token: 0x06000739 RID: 1849 RVA: 0x00030CE4 File Offset: 0x0002EEE4
		public override int ReadAudioSamples(out byte[] targetBuf, int sampleCount)
		{
			int sampleSize = base.audioStreamInfo.sampleSize;
			int num = sampleCount * sampleSize;
			if (this.rawAudioBuf == null || this.rawAudioBuf.Length < num)
			{
				this.rawAudioBuf = new byte[num];
			}
			long num2 = (long)(this.nextAudioSample * sampleSize);
			this.nextAudioSample += sampleCount;
			if (this.rawAudioBuf.Length < num)
			{
				throw new ArgumentException("array.Length < count");
			}
			int num3 = Array.BinarySearch<long>(this.audioByteIndex, num2);
			if (num3 == -1)
			{
				throw new MpException("audioByteIndex is corrupted");
			}
			if (num3 < 0)
			{
				num3 = -num3 - 2;
			}
			int num4 = 0;
			long num5 = num2;
			int num6 = num;
			int num7 = 0;
			int num9;
			int num11;
			do
			{
				AviStreamIndex.Entry entry = this.avi.audioIndex.entries[num3];
				int num8 = (int)(num5 - this.audioByteIndex[num3]);
				num9 = entry.chunkLength - num8;
				if (num9 > num6)
				{
					num9 = num6;
				}
				long num10 = entry.chunkOffset + (long)num8;
				num11 = this.reader.Read(ref num10, this.rawAudioBuf, num4, num9);
				num7 += num11;
				num6 -= num11;
				num8 += num11;
				num4 += num11;
				num5 += (long)num11;
				if (num8 >= entry.chunkLength)
				{
					num3++;
				}
			}
			while (num6 > 0 && num11 == num9 && num3 < this.audioByteIndex.Length);
			targetBuf = this.rawAudioBuf;
			return num7 / sampleSize;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00030E54 File Offset: 0x0002F054
		private static AVIMainHeader ParseMainHeader(AtomicBinaryReader br, long p)
		{
			return new AVIMainHeader
			{
				dwMicroSecPerFrame = br.ReadUInt32(ref p),
				dwMaxBytesPerSec = br.ReadUInt32(ref p),
				dwPaddingGranularity = br.ReadUInt32(ref p),
				dwFlags = br.ReadUInt32(ref p),
				dwTotalFrames = br.ReadUInt32(ref p),
				dwInitialFrames = br.ReadUInt32(ref p),
				dwStreams = br.ReadUInt32(ref p),
				dwSuggestedBufferSize = br.ReadUInt32(ref p),
				dwWidth = br.ReadUInt32(ref p),
				dwHeight = br.ReadUInt32(ref p),
				dwReserved0 = br.ReadUInt32(ref p),
				dwReserved1 = br.ReadUInt32(ref p),
				dwReserved2 = br.ReadUInt32(ref p),
				dwReserved3 = br.ReadUInt32(ref p)
			};
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00030F2C File Offset: 0x0002F12C
		private static AVIStreamHeader ParseStreamHeader(AtomicBinaryReader br, long p)
		{
			return new AVIStreamHeader
			{
				fccType = br.ReadUInt32(ref p),
				fccHandler = br.ReadUInt32(ref p),
				dwFlags = br.ReadUInt32(ref p),
				wPriority = br.ReadUInt16(ref p),
				wLanguage = br.ReadUInt16(ref p),
				dwInitialFrames = br.ReadUInt32(ref p),
				dwScale = br.ReadUInt32(ref p),
				dwRate = br.ReadUInt32(ref p),
				dwStart = br.ReadUInt32(ref p),
				dwLength = br.ReadUInt32(ref p),
				dwSuggestedBufferSize = br.ReadUInt32(ref p),
				dwQuality = br.ReadUInt32(ref p),
				dwSampleSize = br.ReadUInt32(ref p),
				rcFrameLeft = br.ReadInt16(ref p),
				rcFrameTop = br.ReadInt16(ref p),
				rcFrameRight = br.ReadInt16(ref p),
				rcFrameBottom = br.ReadInt16(ref p)
			};
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00031030 File Offset: 0x0002F230
		private static BitmapInfoHeader ParseVideoFormatHeader(AtomicBinaryReader br, long p)
		{
			return new BitmapInfoHeader
			{
				biSize = br.ReadUInt32(ref p),
				biWidth = br.ReadInt32(ref p),
				biHeight = br.ReadInt32(ref p),
				biPlanes = br.ReadUInt16(ref p),
				biBitCount = br.ReadUInt16(ref p),
				biCompression = br.ReadUInt32(ref p),
				biSizeImage = br.ReadUInt32(ref p),
				biXPelsPerMeter = br.ReadInt32(ref p),
				biYPelsPerMeter = br.ReadInt32(ref p),
				biClrUsed = br.ReadUInt32(ref p),
				biClrImportant = br.ReadUInt32(ref p)
			};
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000310E0 File Offset: 0x0002F2E0
		private static WaveFormatEx ParseAudioFormatHeader(AtomicBinaryReader br, long p)
		{
			return new WaveFormatEx
			{
				wFormatTag = br.ReadUInt16(ref p),
				nChannels = br.ReadUInt16(ref p),
				nSamplesPerSec = br.ReadUInt32(ref p),
				nAvgBytesPerSec = br.ReadUInt32(ref p),
				nBlockAlign = br.ReadUInt16(ref p),
				wBitsPerSample = br.ReadUInt16(ref p),
				cbSize = br.ReadUInt16(ref p)
			};
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00031158 File Offset: 0x0002F358
		private static ODMLHeader ParseOdmlHeader(AtomicBinaryReader br, long p)
		{
			return new ODMLHeader
			{
				dwTotalFrames = br.ReadUInt32(ref p)
			};
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0003117C File Offset: 0x0002F37C
		private static AviStreamIndex ParseOldIndex(long idx1Offset, AtomicBinaryReader abr, int size, uint streamId, long idx1EntryOffset)
		{
			int num = size / 16;
			AviStreamIndex aviStreamIndex = new AviStreamIndex();
			aviStreamIndex.streamId = streamId;
			aviStreamIndex.entries.Capacity = num;
			long num2 = idx1Offset;
			uint[] array = new uint[num * 4];
			abr.Read(ref num2, array, 0, num * 4);
			for (int i = 0; i < num; i++)
			{
				uint num3 = array[i * 4];
				if (num3 == streamId || (num3 == 1650733104U && streamId == 1667510320U))
				{
					AviStreamIndex.Entry entry = new AviStreamIndex.Entry();
					entry.isKeyframe = ((array[i * 4 + 1] & 16U) != 0U);
					entry.chunkOffset = idx1EntryOffset + (long)((ulong)array[i * 4 + 2]);
					entry.chunkLength = (int)array[i * 4 + 3];
					aviStreamIndex.entries.Add(entry);
				}
			}
			return aviStreamIndex;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0003124C File Offset: 0x0002F44C
		private static AviStreamIndex ParseOdmlIndex(AtomicBinaryReader reader, long p, out uint streamId)
		{
			ushort num = reader.ReadUInt16(ref p);
			byte b = reader.ReadByte(ref p);
			byte b2 = reader.ReadByte(ref p);
			uint num2 = reader.ReadUInt32(ref p);
			streamId = reader.ReadUInt32(ref p);
			AviStreamIndex aviStreamIndex = new AviStreamIndex();
			aviStreamIndex.streamId = streamId;
			if (b2 == 0)
			{
				p += 12L;
				if (b != 0 || num != 4)
				{
				}
				for (uint num3 = 0U; num3 < num2; num3 += 1U)
				{
					long num4 = reader.ReadInt64(ref p);
					int num5 = reader.ReadInt32(ref p);
					reader.ReadInt32(ref p);
					if (num4 != 0L)
					{
						long num6 = p;
						p = num4;
						aviStreamIndex.entries.Capacity += num5 / 8;
						AviDemux.ParseChunkIndex(reader, p, ref aviStreamIndex);
						p = num6;
					}
				}
			}
			else
			{
				if (b2 != 1)
				{
					throw new MpException(string.Concat(new object[]
					{
						"Unsupported index type ",
						b2,
						" encountered for stream ",
						RiffParser.FromFourCC(streamId)
					}));
				}
				AviDemux.ParseChunkIndex(reader, p - 20L, ref aviStreamIndex);
			}
			aviStreamIndex.entries.TrimExcess();
			return aviStreamIndex;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00031378 File Offset: 0x0002F578
		private static void ParseChunkIndex(AtomicBinaryReader reader, long p, ref AviStreamIndex index)
		{
			uint num = reader.ReadUInt32(ref p);
			uint num2 = (num & 65535U) | 538968064U;
			if (num2 != RiffParser.ToFourCC("ix  ") && num != RiffParser.ToFourCC("indx"))
			{
				throw new MpException("Unexpected chunk id for index " + RiffParser.FromFourCC(num) + " for stream " + RiffParser.FromFourCC(index.streamId));
			}
			uint num3 = reader.ReadUInt32(ref p);
			ushort num4 = reader.ReadUInt16(ref p);
			byte b = reader.ReadByte(ref p);
			byte b2 = reader.ReadByte(ref p);
			uint num5 = reader.ReadUInt32(ref p);
			uint num6 = reader.ReadUInt32(ref p);
			if (b2 != 1 || b != 0 || num6 != index.streamId || num4 != 2 || (ulong)num3 < (ulong)((long)(4 * num4) * (long)((ulong)num5) + 24L))
			{
				throw new MpException(string.Concat(new object[]
				{
					"Broken or unsupported index for stream ",
					RiffParser.FromFourCC(num6),
					". ",
					num6,
					"!=",
					index.streamId,
					", wLongsPerEntry=",
					num4,
					", bIndexType=",
					b2,
					", bSubIndexType=",
					b
				}));
			}
			long num7 = reader.ReadInt64(ref p);
			p += 4L;
			uint[] array = new uint[num5 * 2U];
			reader.Read(ref p, array, 0, (int)(num5 * 2U));
			int num8 = 0;
			while ((long)num8 < (long)((ulong)num5))
			{
				AviStreamIndex.Entry entry = new AviStreamIndex.Entry();
				entry.chunkOffset = num7 + (long)((ulong)array[2 * num8]);
				uint num9 = array[2 * num8 + 1];
				entry.chunkLength = (int)(num9 & 2147483647U);
				if ((num9 & 2147483648U) == 0U)
				{
					entry.isKeyframe = true;
				}
				index.entries.Add(entry);
				num8++;
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00031574 File Offset: 0x0002F774
		private bool ProcessAviRiff(RiffParser rp, uint fourCC, int length)
		{
			if (fourCC != 541677121U && fourCC != 1481201217U)
			{
				throw new MpException("Not an AVI");
			}
			return true;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000315A4 File Offset: 0x0002F7A4
		private bool ProcessAviList(RiffParser rp, uint fourCC, int length)
		{
			if (fourCC == 1769369453U && this.idx1EntryOffset < 0L)
			{
				this.idx1EntryOffset = rp.Position + 4L;
			}
			return fourCC == 1819436136U || fourCC == 1819440243U || fourCC == 1819108463U;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000315FC File Offset: 0x0002F7FC
		private void ProcessAviChunk(RiffParser rp, uint fourCC, int unpaddedLength, int paddedLength)
		{
			if (fourCC != 829973609U)
			{
				if (fourCC != 1718776947U)
				{
					if (fourCC != 1751742049U)
					{
						if (fourCC != 1751936356U)
						{
							if (fourCC != 1752331379U)
							{
								if (fourCC == 2019847785U)
								{
									uint num;
									AviStreamIndex aviStreamIndex = AviDemux.ParseOdmlIndex(rp.reader, rp.Position, out num);
									if (num == 1667510320U || num == 1650733104U || num == 1667510576U || num == 1650733360U)
									{
										this.avi.videoIndex = aviStreamIndex;
									}
									else if (num == 1651978544U || num == 1651978288U)
									{
										this.avi.audioIndex = aviStreamIndex;
									}
								}
							}
							else
							{
								AVIStreamHeader avistreamHeader = AviDemux.ParseStreamHeader(rp.reader, rp.Position);
								this.currentStrh4CC = avistreamHeader.fccType;
								if (this.currentStrh4CC == 1935960438U)
								{
									this.avi.strhVideo = avistreamHeader;
								}
								else if (this.currentStrh4CC == 1935963489U)
								{
									this.avi.strhAudio = avistreamHeader;
								}
							}
						}
						else
						{
							this.avi.odml = AviDemux.ParseOdmlHeader(rp.reader, rp.Position);
						}
					}
					else
					{
						this.avi.avih = AviDemux.ParseMainHeader(rp.reader, rp.Position);
					}
				}
				else if (this.currentStrh4CC == 1935960438U)
				{
					this.avi.strfVideo = AviDemux.ParseVideoFormatHeader(rp.reader, rp.Position);
				}
				else if (this.currentStrh4CC == 1935963489U)
				{
					this.avi.strfAudio = AviDemux.ParseAudioFormatHeader(rp.reader, rp.Position);
				}
			}
			else
			{
				this.idx1Offset = rp.Position;
				this.idx1Size = unpaddedLength;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000317EC File Offset: 0x0002F9EC
		private void PrepareAudioStream()
		{
			long num = 0L;
			int num2 = 0;
			List<AviStreamIndex.Entry> entries = this.avi.audioIndex.entries;
			this.audioByteIndex = new long[entries.Count];
			for (int i = 0; i < entries.Count; i++)
			{
				AviStreamIndex.Entry entry = entries[i];
				this.audioByteIndex[i] = num;
				num += (long)entry.chunkLength;
				if (entry.chunkLength > num2)
				{
					num2 = entry.chunkLength;
				}
			}
			this.rawAudioBuf = new byte[num2];
			base.audioStreamInfo.lengthBytes = num;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00031884 File Offset: 0x0002FA84
		private void PrepareVideoStream()
		{
			long num = 0L;
			int num2 = 0;
			List<AviStreamIndex.Entry> entries = this.avi.videoIndex.entries;
			for (int i = 0; i < entries.Count; i++)
			{
				AviStreamIndex.Entry entry = entries[i];
				num += (long)entry.chunkLength;
				if (entry.chunkLength > num2)
				{
					num2 = entry.chunkLength;
				}
			}
			this.rawVideoBuf = new byte[num2 + 420];
			base.videoStreamInfo.lengthBytes = num;
		}

		// Token: 0x0400059E RID: 1438
		public const uint ID_AVI_ = 541677121U;

		// Token: 0x0400059F RID: 1439
		public const uint ID_AVIX = 1481201217U;

		// Token: 0x040005A0 RID: 1440
		public const uint ID_hdrl = 1819436136U;

		// Token: 0x040005A1 RID: 1441
		public const uint ID_avih = 1751742049U;

		// Token: 0x040005A2 RID: 1442
		public const uint ID_strl = 1819440243U;

		// Token: 0x040005A3 RID: 1443
		public const uint ID_strh = 1752331379U;

		// Token: 0x040005A4 RID: 1444
		public const uint ID_strf = 1718776947U;

		// Token: 0x040005A5 RID: 1445
		public const uint ID_odml = 1819108463U;

		// Token: 0x040005A6 RID: 1446
		public const uint ID_dmlh = 1751936356U;

		// Token: 0x040005A7 RID: 1447
		public const uint ID_movi = 1769369453U;

		// Token: 0x040005A8 RID: 1448
		public const uint ID_00dc = 1667510320U;

		// Token: 0x040005A9 RID: 1449
		public const uint ID_00db = 1650733104U;

		// Token: 0x040005AA RID: 1450
		public const uint ID_01wb = 1651978544U;

		// Token: 0x040005AB RID: 1451
		public const uint ID_00wb = 1651978288U;

		// Token: 0x040005AC RID: 1452
		public const uint ID_01dc = 1667510576U;

		// Token: 0x040005AD RID: 1453
		public const uint ID_01db = 1650733360U;

		// Token: 0x040005AE RID: 1454
		public const uint ID_idx1 = 829973609U;

		// Token: 0x040005AF RID: 1455
		public const uint ID_indx = 2019847785U;

		// Token: 0x040005B0 RID: 1456
		public const uint FCC_vids = 1935960438U;

		// Token: 0x040005B1 RID: 1457
		public const uint FCC_auds = 1935963489U;

		// Token: 0x040005B2 RID: 1458
		public AVIFile avi;

		// Token: 0x040005B3 RID: 1459
		private AtomicBinaryReader reader;

		// Token: 0x040005B4 RID: 1460
		private uint currentStrh4CC;

		// Token: 0x040005B5 RID: 1461
		private long idx1EntryOffset;

		// Token: 0x040005B6 RID: 1462
		private long idx1Offset;

		// Token: 0x040005B7 RID: 1463
		private int idx1Size;

		// Token: 0x040005B8 RID: 1464
		private byte[] rawVideoBuf;

		// Token: 0x040005B9 RID: 1465
		private byte[] rawAudioBuf;

		// Token: 0x040005BA RID: 1466
		private long[] audioByteIndex;

		// Token: 0x040005BB RID: 1467
		private int nextVideoFrame;

		// Token: 0x040005BC RID: 1468
		private int nextAudioSample;
	}
}
