using System;

namespace MP
{
	// Token: 0x02000167 RID: 359
	public class RiffParser
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x00035478 File Offset: 0x00033678
		public RiffParser(AtomicBinaryReader reader)
		{
			this.reader = reader;
			this.nextElementOffset = 0L;
			long num = 0L;
			this.streamRiff = reader.ReadUInt32(ref num);
			if (this.streamRiff != 1179011410U && this.streamRiff != 1481001298U)
			{
				throw new RiffParserException("Error. Not a valid RIFF stream");
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000354D8 File Offset: 0x000336D8
		public bool ReadNext(RiffParser.ProcessChunkElement chunkCallback, RiffParser.ProcessListElement listCallback = null, RiffParser.ProcessRiffElement riffCallback = null)
		{
			if (this.reader.BytesLeft(this.nextElementOffset) < 8L)
			{
				return false;
			}
			uint num = this.reader.ReadUInt32(ref this.nextElementOffset);
			int num2 = this.reader.ReadInt32(ref this.nextElementOffset);
			if (this.reader.BytesLeft(this.nextElementOffset) < (long)num2)
			{
				this.nextElementOffset = (long)num2;
				throw new RiffParserException("Element size mismatch for element " + RiffParser.FromFourCC(num) + " need " + num2.ToString());
			}
			if (num == 1179011410U || num == 1481001298U)
			{
				num = this.reader.ReadUInt32(ref this.nextElementOffset);
				if (this.reader.StreamLength < this.nextElementOffset + (long)num2 - 4L)
				{
					throw new RiffParserException("Error. Truncated stream");
				}
				if (riffCallback != null && !riffCallback(this, num, num2 - 4))
				{
					this.nextElementOffset += (long)(num2 - 4);
				}
			}
			else if (num == 1414744396U)
			{
				num = this.reader.ReadUInt32(ref this.nextElementOffset);
				if (listCallback != null && !listCallback(this, num, num2 - 4))
				{
					this.nextElementOffset += (long)(num2 - 4);
				}
			}
			else
			{
				int num3 = num2;
				if ((num2 & 1) != 0)
				{
					num3++;
				}
				if (chunkCallback != null)
				{
					chunkCallback(this, num, num2, num3);
				}
				this.nextElementOffset += (long)num3;
			}
			return true;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00035660 File Offset: 0x00033860
		public void Rewind()
		{
			this.nextElementOffset = 0L;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0003566C File Offset: 0x0003386C
		public uint StreamRIFF
		{
			get
			{
				return this.streamRiff;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00035674 File Offset: 0x00033874
		public long Position
		{
			get
			{
				return this.nextElementOffset;
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0003567C File Offset: 0x0003387C
		public static string FromFourCC(uint fourCC)
		{
			return new string(new char[]
			{
				(char)(fourCC & 255U),
				(char)(fourCC >> 8 & 255U),
				(char)(fourCC >> 16 & 255U),
				(char)(fourCC >> 24 & 255U)
			});
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000356CC File Offset: 0x000338CC
		public static uint ToFourCC(string fourCC)
		{
			if (fourCC.Length != 4)
			{
				throw new RiffParserException("FourCC strings must be 4 characters long " + fourCC);
			}
			return (uint)((uint)fourCC[3] << 24 | (uint)fourCC[2] << 16 | (uint)fourCC[1] << 8 | fourCC[0]);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00035720 File Offset: 0x00033920
		public static uint ToFourCC(char[] fourCC)
		{
			if (fourCC.Length != 4)
			{
				throw new RiffParserException("FourCC char arrays must be 4 characters long " + new string(fourCC));
			}
			return (uint)((uint)fourCC[3] << 24 | (uint)fourCC[2] << 16 | (uint)fourCC[1] << 8 | fourCC[0]);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00035764 File Offset: 0x00033964
		public static uint ToFourCC(char c0, char c1, char c2, char c3)
		{
			return (uint)((uint)c3 << 24 | (uint)c2 << 16 | (uint)c1 << 8 | c0);
		}

		// Token: 0x04000647 RID: 1607
		public const uint RIFF4CC = 1179011410U;

		// Token: 0x04000648 RID: 1608
		public const uint RIFX4CC = 1481001298U;

		// Token: 0x04000649 RID: 1609
		public const uint LIST4CC = 1414744396U;

		// Token: 0x0400064A RID: 1610
		private long nextElementOffset;

		// Token: 0x0400064B RID: 1611
		public readonly AtomicBinaryReader reader;

		// Token: 0x0400064C RID: 1612
		private uint streamRiff;

		// Token: 0x02000316 RID: 790
		// (Invoke) Token: 0x06001266 RID: 4710
		public delegate bool ProcessRiffElement(RiffParser rp, uint fourCC, int length);

		// Token: 0x02000317 RID: 791
		// (Invoke) Token: 0x0600126A RID: 4714
		public delegate bool ProcessListElement(RiffParser rp, uint fourCC, int length);

		// Token: 0x02000318 RID: 792
		// (Invoke) Token: 0x0600126E RID: 4718
		public delegate void ProcessChunkElement(RiffParser rp, uint fourCC, int unpaddedLength, int paddedLength);
	}
}
