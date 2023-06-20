using System;
using System.IO;
using UnityEngine;

namespace MP.AVI
{
	// Token: 0x0200014F RID: 335
	public class AviRemux : Remux
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00031908 File Offset: 0x0002FB08
		public AviRemux(int maxSuperindexEntries = 32, int maxRiffElementSize = 2000000000)
		{
			this.maxSuperindexEntries = maxSuperindexEntries;
			this.maxRiffElementSize = maxRiffElementSize;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00031920 File Offset: 0x0002FB20
		private bool hasAudioStream
		{
			get
			{
				return base.audioStreamInfo != null;
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00031930 File Offset: 0x0002FB30
		public override void Init(Stream dstStream, VideoStreamInfo videoStreamInfo, AudioStreamInfo audioStreamInfo)
		{
			if (dstStream == null || videoStreamInfo == null)
			{
				throw new ArgumentException("At least destination stream and video stream info is needed");
			}
			base.Init(dstStream, videoStreamInfo, audioStreamInfo);
			this.usingMultipleRiffs = false;
			this.totalFramesOld = 0;
			this.totalFrames = 0;
			this.totalSamples = 0;
			this.writer = new RiffWriter(dstStream);
			this.writer.BeginRiff(541677121U);
			this.writer.BeginList(1819436136U);
			this.offsets.avih = AviRemux.WriteMainHeader(this.writer, videoStreamInfo, this.hasAudioStream);
			this.writer.BeginList(1819440243U);
			this.offsets.videoStrh = AviRemux.WriteVideoStreamHeader(this.writer, videoStreamInfo);
			AviRemux.WriteVideoFormatHeader(this.writer, videoStreamInfo);
			this.offsets.videoIndx = AviRemux.WriteDummySuperIndex(this.writer, 1667510320U, this.maxSuperindexEntries);
			this.videoSuperIndexEntryCount = 0;
			this.writer.EndList();
			this.videoIndex = new AviStreamIndex();
			this.videoIndex.streamId = 1667510320U;
			if (this.hasAudioStream)
			{
				this.writer.BeginList(1819440243U);
				this.offsets.audioStrh = AviRemux.WriteAudioStreamHeader(this.writer, audioStreamInfo);
				AviRemux.WriteAudioFormatHeader(this.writer, audioStreamInfo);
				this.offsets.audioIndx = AviRemux.WriteDummySuperIndex(this.writer, 1651978544U, this.maxSuperindexEntries);
				this.audioSuperIndexEntryCount = 0;
				this.writer.EndList();
				this.audioIndex = new AviStreamIndex();
				this.audioIndex.streamId = 1651978544U;
			}
			this.writer.BeginList(1819108463U);
			this.offsets.dmlh = AviRemux.WriteDmlhHeader(this.writer, videoStreamInfo.frameCount);
			this.writer.EndList();
			this.writer.EndList();
			this.writer.BeginList(1769369453U);
			this.offsets.indexBase = this.writer.binaryWriter.Seek(0, SeekOrigin.Current);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00031B40 File Offset: 0x0002FD40
		public override void WriteNextVideoFrame(byte[] frameBytes, int size = -1)
		{
			if (this.writer.currentElementSize > (long)this.maxRiffElementSize)
			{
				this.StartNewRiff();
			}
			if (size < 0)
			{
				size = frameBytes.Length;
			}
			AviStreamIndex.Entry entry = new AviStreamIndex.Entry();
			entry.chunkOffset = this.writer.binaryWriter.Seek(0, SeekOrigin.Current) + 8L;
			entry.chunkLength = size;
			this.videoIndex.entries.Add(entry);
			this.writer.WriteChunk(1667510320U, frameBytes, size);
			this.totalFrames++;
			if (!this.usingMultipleRiffs)
			{
				this.totalFramesOld++;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00031BE8 File Offset: 0x0002FDE8
		public override void WriteVideoFrame(int frameOffset, byte[] frameBytes, int size = -1)
		{
			throw new NotSupportedException("Only adding frames at the end is supported");
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		public bool WriteLookbackVideoFrame(int frame)
		{
			if (frame < 0)
			{
				frame = this.totalFrames - frame;
			}
			int num = frame - this.videoIndex.globalOffset;
			if (num < 0 || num >= this.videoIndex.entries.Count)
			{
				return false;
			}
			AviStreamIndex.Entry entry = this.videoIndex.entries[num];
			AviStreamIndex.Entry entry2 = new AviStreamIndex.Entry();
			entry2.chunkOffset = entry.chunkOffset;
			entry2.chunkLength = entry.chunkLength;
			this.videoIndex.entries.Add(entry2);
			this.totalFrames++;
			if (!this.usingMultipleRiffs)
			{
				this.totalFramesOld++;
			}
			return true;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00031CA8 File Offset: 0x0002FEA8
		public override void WriteNextAudioSamples(byte[] sampleBytes, int size = -1)
		{
			if (this.writer.currentElementSize > (long)this.maxRiffElementSize)
			{
				this.StartNewRiff();
			}
			if (size < 0)
			{
				size = sampleBytes.Length;
			}
			AviStreamIndex.Entry entry = new AviStreamIndex.Entry();
			entry.chunkOffset = this.writer.binaryWriter.Seek(0, SeekOrigin.Current) + 8L;
			entry.chunkLength = size;
			this.audioIndex.entries.Add(entry);
			this.writer.WriteChunk(1651978544U, sampleBytes, -1);
			this.totalSamples += size / base.audioStreamInfo.sampleSize;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00031D44 File Offset: 0x0002FF44
		public override void WriteAudioSamples(int sampleOffset, byte[] frameBytes, int size = -1)
		{
			throw new NotSupportedException("Only adding samples at the end is supported");
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00031D50 File Offset: 0x0002FF50
		public override void Shutdown()
		{
			BinaryWriter binaryWriter = this.writer.binaryWriter;
			long offset = binaryWriter.Seek(0, SeekOrigin.Current);
			binaryWriter.Seek(this.offsets.avih + 16, SeekOrigin.Begin);
			binaryWriter.Write(this.totalFramesOld);
			binaryWriter.Seek(this.offsets.videoStrh + 32, SeekOrigin.Begin);
			binaryWriter.Write(this.totalFrames);
			binaryWriter.Seek(this.offsets.dmlh, SeekOrigin.Begin);
			binaryWriter.Write(this.totalFrames);
			if (this.hasAudioStream)
			{
				binaryWriter.Seek(this.offsets.audioStrh + 32, SeekOrigin.Begin);
				binaryWriter.Write(this.totalSamples);
			}
			binaryWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
			if (this.videoIndex.entries.Count > 0)
			{
				AviRemux.WriteChunkIndex(this.writer, this.videoIndex, this.offsets.videoIndx, ref this.videoSuperIndexEntryCount, this.offsets.indexBase, this.maxSuperindexEntries);
			}
			if (this.hasAudioStream && this.audioIndex.entries.Count > 0)
			{
				AviRemux.WriteChunkIndex(this.writer, this.audioIndex, this.offsets.audioIndx, ref this.audioSuperIndexEntryCount, this.offsets.indexBase, this.maxSuperindexEntries);
			}
			this.writer.EndList();
			this.writer.EndRiff();
			this.writer.Close();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00031ED0 File Offset: 0x000300D0
		private void StartNewRiff()
		{
			if (this.videoIndex.entries.Count > 0)
			{
				AviRemux.WriteChunkIndex(this.writer, this.videoIndex, this.offsets.videoIndx, ref this.videoSuperIndexEntryCount, this.offsets.indexBase, this.maxSuperindexEntries);
			}
			if (this.hasAudioStream && this.audioIndex.entries.Count > 0)
			{
				AviRemux.WriteChunkIndex(this.writer, this.audioIndex, this.offsets.audioIndx, ref this.audioSuperIndexEntryCount, this.offsets.indexBase, this.maxSuperindexEntries);
			}
			this.writer.EndList();
			this.writer.EndRiff();
			this.writer.BeginRiff(1481201217U);
			this.writer.BeginList(1769369453U);
			this.offsets.indexBase = this.writer.binaryWriter.Seek(0, SeekOrigin.Current);
			this.usingMultipleRiffs = true;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00031FD4 File Offset: 0x000301D4
		private static int WriteMainHeader(RiffWriter rw, VideoStreamInfo vsi, bool hasAudioStream)
		{
			rw.BeginChunk(1751742049U);
			int result = (int)rw.binaryWriter.Seek(0, SeekOrigin.Current);
			BinaryWriter binaryWriter = rw.binaryWriter;
			binaryWriter.Write(Mathf.RoundToInt(1000000f / vsi.framerate));
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(48);
			binaryWriter.Write(vsi.frameCount);
			binaryWriter.Write(0);
			binaryWriter.Write((!hasAudioStream) ? 1 : 2);
			binaryWriter.Write(0);
			binaryWriter.Write(vsi.width);
			binaryWriter.Write(vsi.height);
			binaryWriter.Write(0L);
			binaryWriter.Write(0L);
			rw.EndChunk();
			return result;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0003208C File Offset: 0x0003028C
		private static int WriteVideoStreamHeader(RiffWriter rw, VideoStreamInfo vsi)
		{
			rw.BeginChunk(1752331379U);
			int result = (int)rw.binaryWriter.Seek(0, SeekOrigin.Current);
			BinaryWriter binaryWriter = rw.binaryWriter;
			binaryWriter.Write(1935960438U);
			binaryWriter.Write(vsi.codecFourCC);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			int value;
			int value2;
			AviRemux.FindScaleAndRate(out value, out value2, vsi.framerate);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			binaryWriter.Write(0);
			binaryWriter.Write(vsi.frameCount);
			binaryWriter.Write(0);
			binaryWriter.Write(-1);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write((short)vsi.width);
			binaryWriter.Write((short)vsi.height);
			rw.EndChunk();
			return result;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00032164 File Offset: 0x00030364
		private static int WriteAudioStreamHeader(RiffWriter rw, AudioStreamInfo asi)
		{
			rw.BeginChunk(1752331379U);
			int result = (int)rw.binaryWriter.Seek(0, SeekOrigin.Current);
			BinaryWriter binaryWriter = rw.binaryWriter;
			binaryWriter.Write(1935963489U);
			binaryWriter.Write(asi.codecFourCC);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(1);
			binaryWriter.Write(asi.sampleRate);
			binaryWriter.Write(0);
			binaryWriter.Write(asi.sampleCount);
			binaryWriter.Write(0);
			binaryWriter.Write(-1);
			binaryWriter.Write(asi.sampleSize);
			binaryWriter.Write(0L);
			rw.EndChunk();
			return result;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00032214 File Offset: 0x00030414
		private static void FindScaleAndRate(out int scale, out int rate, float framerate)
		{
			rate = Mathf.FloorToInt(framerate);
			scale = 1;
			while ((double)rate < 100000.0)
			{
				float num = (float)rate / (float)scale - framerate;
				if ((double)Mathf.Abs(num) < 1E-05)
				{
					break;
				}
				if (num > 0f)
				{
					scale++;
				}
				else
				{
					rate++;
				}
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00032284 File Offset: 0x00030484
		private static void WriteVideoFormatHeader(RiffWriter rw, VideoStreamInfo vsi)
		{
			rw.BeginChunk(1718776947U);
			BinaryWriter binaryWriter = rw.binaryWriter;
			binaryWriter.Write(40);
			binaryWriter.Write(vsi.width);
			binaryWriter.Write(vsi.height);
			binaryWriter.Write(1);
			binaryWriter.Write((short)vsi.bitsPerPixel);
			binaryWriter.Write(vsi.codecFourCC);
			binaryWriter.Write(vsi.width * vsi.height * vsi.bitsPerPixel / 8);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			rw.EndChunk();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00032324 File Offset: 0x00030524
		private static void WriteAudioFormatHeader(RiffWriter rw, AudioStreamInfo asi)
		{
			rw.BeginChunk(1718776947U);
			BinaryWriter binaryWriter = rw.binaryWriter;
			binaryWriter.Write((ushort)asi.audioFormat);
			binaryWriter.Write((short)asi.channels);
			binaryWriter.Write(asi.sampleRate);
			binaryWriter.Write(asi.sampleRate * asi.sampleSize * asi.channels);
			binaryWriter.Write((short)asi.sampleSize);
			binaryWriter.Write((short)(8 * asi.sampleSize / asi.channels));
			binaryWriter.Write(0);
			rw.EndChunk();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000323B4 File Offset: 0x000305B4
		private static int WriteDmlhHeader(RiffWriter rw, int totalFrames)
		{
			rw.BeginChunk(1751936356U);
			int result = (int)rw.binaryWriter.Seek(0, SeekOrigin.Current);
			rw.binaryWriter.Write(totalFrames);
			rw.EndChunk();
			return result;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000323F0 File Offset: 0x000305F0
		private static int WriteDummySuperIndex(RiffWriter rw, uint streamId, int entriesToReserve)
		{
			rw.BeginChunk(2019847785U);
			int result = (int)rw.binaryWriter.Seek(0, SeekOrigin.Current);
			BinaryWriter binaryWriter = rw.binaryWriter;
			binaryWriter.Write(4);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(streamId);
			binaryWriter.Write(new byte[12 + entriesToReserve * 16]);
			rw.EndChunk();
			return result;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0003245C File Offset: 0x0003065C
		private static void WriteChunkIndex(RiffWriter rw, AviStreamIndex index, int superIndexChunkOffset, ref int superIndexEntryCount, long indexBaseOffset, int maxSuperindexEntries)
		{
			BinaryWriter binaryWriter = rw.binaryWriter;
			long num = binaryWriter.Seek(0, SeekOrigin.Current);
			superIndexEntryCount++;
			if (superIndexEntryCount > maxSuperindexEntries)
			{
				throw new MpException("Not enough space was reserved for superindex. Please increase maxSuperindexEntries");
			}
			binaryWriter.Seek(superIndexChunkOffset + 4, SeekOrigin.Begin);
			binaryWriter.Write(superIndexEntryCount);
			binaryWriter.Seek(superIndexChunkOffset + 24 + (superIndexEntryCount - 1) * 16, SeekOrigin.Begin);
			binaryWriter.Write(num);
			binaryWriter.Write(32 + 8 * index.entries.Count);
			binaryWriter.Write(index.entries.Count);
			binaryWriter.BaseStream.Seek(num, SeekOrigin.Begin);
			rw.BeginChunk((RiffParser.ToFourCC("ix__") & 65535U) | (index.streamId << 16 & 4294901760U));
			binaryWriter.Write(2);
			binaryWriter.Write(0);
			binaryWriter.Write(1);
			binaryWriter.Write(index.entries.Count);
			binaryWriter.Write(index.streamId);
			binaryWriter.Write(indexBaseOffset);
			binaryWriter.Write(0);
			foreach (AviStreamIndex.Entry entry in index.entries)
			{
				long num2 = entry.chunkOffset - indexBaseOffset;
				if (num2 > 2147483647L)
				{
					throw new MpException("Internal error. Can't write index, because chunk offset won't fit into 31 bits: " + num2);
				}
				binaryWriter.Write((uint)num2);
				binaryWriter.Write(entry.chunkLength);
			}
			rw.EndChunk();
			index.globalOffset += index.entries.Count;
			index.entries.Clear();
		}

		// Token: 0x040005BD RID: 1469
		private int maxSuperindexEntries;

		// Token: 0x040005BE RID: 1470
		private int maxRiffElementSize;

		// Token: 0x040005BF RID: 1471
		private RiffWriter writer;

		// Token: 0x040005C0 RID: 1472
		private AviStreamIndex videoIndex;

		// Token: 0x040005C1 RID: 1473
		private int videoSuperIndexEntryCount;

		// Token: 0x040005C2 RID: 1474
		private AviStreamIndex audioIndex;

		// Token: 0x040005C3 RID: 1475
		private int audioSuperIndexEntryCount;

		// Token: 0x040005C4 RID: 1476
		private bool usingMultipleRiffs;

		// Token: 0x040005C5 RID: 1477
		private int totalFramesOld;

		// Token: 0x040005C6 RID: 1478
		private int totalFrames;

		// Token: 0x040005C7 RID: 1479
		private int totalSamples;

		// Token: 0x040005C8 RID: 1480
		private AviRemux.ByteOffsets offsets;

		// Token: 0x02000150 RID: 336
		private struct ByteOffsets
		{
			// Token: 0x040005C9 RID: 1481
			public long indexBase;

			// Token: 0x040005CA RID: 1482
			public int avih;

			// Token: 0x040005CB RID: 1483
			public int videoStrh;

			// Token: 0x040005CC RID: 1484
			public int videoIndx;

			// Token: 0x040005CD RID: 1485
			public int audioStrh;

			// Token: 0x040005CE RID: 1486
			public int audioIndx;

			// Token: 0x040005CF RID: 1487
			public int dmlh;
		}
	}
}
