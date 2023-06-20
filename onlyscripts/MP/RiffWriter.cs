using System;
using System.Collections.Generic;
using System.IO;

namespace MP
{
	// Token: 0x02000169 RID: 361
	public class RiffWriter
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x000357A4 File Offset: 0x000339A4
		public RiffWriter(Stream stream)
		{
			this.writer = new BinaryWriter(stream);
			this.stack = new Stack<long>();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000357C4 File Offset: 0x000339C4
		public RiffWriter(BinaryWriter writer)
		{
			this.writer = writer;
			this.stack = new Stack<long>();
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000357E0 File Offset: 0x000339E0
		public void BeginRiff(uint fourCC)
		{
			this.Begin(1179011410U, fourCC);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000357F0 File Offset: 0x000339F0
		public void BeginList(uint fourCC)
		{
			this.Begin(1414744396U, fourCC);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00035800 File Offset: 0x00033A00
		public void BeginChunk(uint fourCC)
		{
			this.writer.Write(fourCC);
			this.stack.Push(this.writer.BaseStream.Position);
			this.writer.Write(0);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00035840 File Offset: 0x00033A40
		public void EndRiff()
		{
			this.End();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00035848 File Offset: 0x00033A48
		public void EndList()
		{
			this.End();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00035850 File Offset: 0x00033A50
		public void EndChunk()
		{
			this.End();
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00035858 File Offset: 0x00033A58
		public void WriteChunk(uint fourCC, byte[] data, int size = -1)
		{
			if (size < 0)
			{
				size = data.Length;
			}
			this.writer.Write(fourCC);
			this.writer.Write(size);
			this.writer.Write(data, 0, size);
			if (size % 2 != 0)
			{
				this.writer.Write(0);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x000358AC File Offset: 0x00033AAC
		public BinaryWriter binaryWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x000358B4 File Offset: 0x00033AB4
		public long currentElementSize
		{
			get
			{
				return this.writer.BaseStream.Position - this.stack.Peek();
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000358E0 File Offset: 0x00033AE0
		public void Close()
		{
			while (this.stack.Count > 0)
			{
				this.End();
			}
			this.writer.Close();
			this.writer.BaseStream.Close();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00035924 File Offset: 0x00033B24
		private void Begin(uint what, uint fourCC)
		{
			this.writer.Write(what);
			this.stack.Push(this.writer.BaseStream.Position);
			this.writer.Write(0);
			this.writer.Write(fourCC);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00035970 File Offset: 0x00033B70
		private void End()
		{
			long num = this.writer.BaseStream.Position - this.stack.Pop();
			if (num > 2147483647L)
			{
				throw new RiffWriterException("RIFF or LIST element too large for writing (" + num + " bytes)");
			}
			int num2 = (int)num;
			int num3 = num2 % 2;
			if (num3 > 0)
			{
				this.writer.Write(0);
			}
			this.writer.Seek(-num2 - num3, SeekOrigin.Current);
			this.writer.Write(num2 - 4);
			this.writer.Seek(num2 - 4 + num3, SeekOrigin.Current);
		}

		// Token: 0x0400064D RID: 1613
		public const uint RIFF4CC = 1179011410U;

		// Token: 0x0400064E RID: 1614
		public const uint LIST4CC = 1414744396U;

		// Token: 0x0400064F RID: 1615
		private BinaryWriter writer;

		// Token: 0x04000650 RID: 1616
		private Stack<long> stack;
	}
}
