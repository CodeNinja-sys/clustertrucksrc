using System;
using System.IO;
using System.Text;

namespace MP
{
	// Token: 0x02000151 RID: 337
	public class AtomicBinaryReader
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x0003261C File Offset: 0x0003081C
		public AtomicBinaryReader(Stream stream) : this(stream, Encoding.UTF8)
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0003262C File Offset: 0x0003082C
		public AtomicBinaryReader(Stream stream, Encoding encoding)
		{
			this.reader = new BinaryReader(stream, encoding);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0003264C File Offset: 0x0003084C
		public void Close()
		{
			if (this.reader != null)
			{
				this.reader.Close();
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00032664 File Offset: 0x00030864
		public long StreamLength
		{
			get
			{
				return this.reader.BaseStream.Length;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00032678 File Offset: 0x00030878
		public long BytesLeft(long offset)
		{
			return this.reader.BaseStream.Length - offset;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0003268C File Offset: 0x0003088C
		public int Read(ref long offset, byte[] buffer, int index, int count)
		{
			object obj = this.locker;
			int result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				int num = this.reader.Read(buffer, index, count);
				offset += (long)num;
				result = num;
			}
			return result;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00032704 File Offset: 0x00030904
		public int Read(ref long offset, uint[] buffer, int index, int count)
		{
			object obj = this.locker;
			int result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				int i = 0;
				try
				{
					while (i < count)
					{
						buffer[index + i] = this.reader.ReadUInt32();
						i++;
					}
				}
				catch (EndOfStreamException)
				{
				}
				result = i;
			}
			return result;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000327A8 File Offset: 0x000309A8
		public byte ReadByte(ref long offset)
		{
			object obj = this.locker;
			byte result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				byte b = this.reader.ReadByte();
				offset = this.reader.BaseStream.Position;
				result = b;
			}
			return result;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00032824 File Offset: 0x00030A24
		public sbyte ReadSByte(ref long offset)
		{
			object obj = this.locker;
			sbyte result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				sbyte b = this.reader.ReadSByte();
				offset = this.reader.BaseStream.Position;
				result = b;
			}
			return result;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000328A0 File Offset: 0x00030AA0
		public short ReadInt16(ref long offset)
		{
			object obj = this.locker;
			short result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				short num = this.reader.ReadInt16();
				offset = this.reader.BaseStream.Position;
				result = num;
			}
			return result;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0003291C File Offset: 0x00030B1C
		public ushort ReadUInt16(ref long offset)
		{
			object obj = this.locker;
			ushort result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				ushort num = this.reader.ReadUInt16();
				offset = this.reader.BaseStream.Position;
				result = num;
			}
			return result;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00032998 File Offset: 0x00030B98
		public int ReadInt32(ref long offset)
		{
			object obj = this.locker;
			int result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				int num = this.reader.ReadInt32();
				offset = this.reader.BaseStream.Position;
				result = num;
			}
			return result;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00032A14 File Offset: 0x00030C14
		public uint ReadUInt32(ref long offset)
		{
			object obj = this.locker;
			uint result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				uint num = this.reader.ReadUInt32();
				offset = this.reader.BaseStream.Position;
				result = num;
			}
			return result;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00032A90 File Offset: 0x00030C90
		public long ReadInt64(ref long offset)
		{
			object obj = this.locker;
			long result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				long num = this.reader.ReadInt64();
				offset = this.reader.BaseStream.Position;
				result = num;
			}
			return result;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00032B0C File Offset: 0x00030D0C
		public ulong ReadUInt64(ref long offset)
		{
			object obj = this.locker;
			ulong result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				ulong num = this.reader.ReadUInt64();
				offset = this.reader.BaseStream.Position;
				result = num;
			}
			return result;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00032B88 File Offset: 0x00030D88
		public byte[] ReadBytes(ref long offset, int count)
		{
			object obj = this.locker;
			byte[] result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
				byte[] array = this.reader.ReadBytes(count);
				offset = this.reader.BaseStream.Position;
				result = array;
			}
			return result;
		}

		// Token: 0x040005D0 RID: 1488
		private readonly object locker = new object();

		// Token: 0x040005D1 RID: 1489
		private BinaryReader reader;
	}
}
