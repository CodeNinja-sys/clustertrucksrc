using System;
using System.Collections.Generic;
using System.IO;

namespace InControl
{
	// Token: 0x02000051 RID: 81
	public struct KeyCombo
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000A53C File Offset: 0x0000873C
		public KeyCombo(params Key[] keys)
		{
			this.data = 0UL;
			this.size = 0;
			for (int i = 0; i < keys.Length; i++)
			{
				this.Add(keys[i]);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A584 File Offset: 0x00008784
		private void AddInt(int key)
		{
			if (this.size == 8)
			{
				return;
			}
			this.data |= (ulong)((ulong)((long)key & 255L) << this.size * 8);
			this.size++;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A5C4 File Offset: 0x000087C4
		private int GetInt(int index)
		{
			return (int)(this.data >> index * 8 & 255UL);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A5DC File Offset: 0x000087DC
		public void Add(Key key)
		{
			this.AddInt((int)key);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public Key Get(int index)
		{
			if (index < 0 || index >= this.size)
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"Index ",
					index,
					" is out of the range 0..",
					this.size
				}));
			}
			return (Key)this.GetInt(index);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A648 File Offset: 0x00008848
		public void Clear()
		{
			this.data = 0UL;
			this.size = 0;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000A65C File Offset: 0x0000885C
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000A664 File Offset: 0x00008864
		public bool IsPressed
		{
			get
			{
				if (this.size == 0)
				{
					return false;
				}
				bool flag = true;
				for (int i = 0; i < this.size; i++)
				{
					int @int = this.GetInt(i);
					flag = (flag && KeyInfo.KeyList[@int].IsPressed);
				}
				return flag;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A6BC File Offset: 0x000088BC
		public static KeyCombo Detect(bool modifiersAsKeys)
		{
			KeyCombo result = default(KeyCombo);
			if (modifiersAsKeys)
			{
				for (int i = 1; i < 5; i++)
				{
					if (KeyInfo.KeyList[i].IsPressed)
					{
						result.AddInt(i);
					}
				}
			}
			else
			{
				for (int j = 5; j < 13; j++)
				{
					if (KeyInfo.KeyList[j].IsPressed)
					{
						result.AddInt(j);
						return result;
					}
				}
			}
			for (int k = 13; k < KeyInfo.KeyList.Length; k++)
			{
				if (KeyInfo.KeyList[k].IsPressed)
				{
					result.AddInt(k);
					return result;
				}
			}
			result.Clear();
			return result;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A780 File Offset: 0x00008980
		public override string ToString()
		{
			string text;
			if (!KeyCombo.cachedStrings.TryGetValue(this.data, out text))
			{
				text = string.Empty;
				for (int i = 0; i < this.size; i++)
				{
					if (i != 0)
					{
						text += " ";
					}
					int @int = this.GetInt(i);
					text += KeyInfo.KeyList[@int].Name;
				}
			}
			return text;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A7F4 File Offset: 0x000089F4
		public override bool Equals(object other)
		{
			if (other is KeyCombo)
			{
				KeyCombo keyCombo = (KeyCombo)other;
				return this.data == keyCombo.data;
			}
			return false;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A824 File Offset: 0x00008A24
		public override int GetHashCode()
		{
			return this.data.GetHashCode();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A834 File Offset: 0x00008A34
		internal void Load(BinaryReader reader)
		{
			this.size = reader.ReadInt32();
			this.data = reader.ReadUInt64();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A850 File Offset: 0x00008A50
		internal void Save(BinaryWriter writer)
		{
			writer.Write(this.size);
			writer.Write(this.data);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A86C File Offset: 0x00008A6C
		public static bool operator ==(KeyCombo a, KeyCombo b)
		{
			return a.data == b.data;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A880 File Offset: 0x00008A80
		public static bool operator !=(KeyCombo a, KeyCombo b)
		{
			return a.data != b.data;
		}

		// Token: 0x0400019B RID: 411
		private int size;

		// Token: 0x0400019C RID: 412
		private ulong data;

		// Token: 0x0400019D RID: 413
		private static Dictionary<ulong, string> cachedStrings = new Dictionary<ulong, string>();
	}
}
