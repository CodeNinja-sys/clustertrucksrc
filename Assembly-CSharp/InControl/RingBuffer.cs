using System;

namespace InControl
{
	// Token: 0x020000F7 RID: 247
	internal class RingBuffer<T>
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00027960 File Offset: 0x00025B60
		public RingBuffer(int size)
		{
			if (size <= 0)
			{
				throw new ArgumentException("RingBuffer size must be 1 or greater.");
			}
			this.size = size + 1;
			this.data = new !0[this.size];
			this.head = 0;
			this.tail = 0;
			this.sync = new object();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000279B8 File Offset: 0x00025BB8
		public void Enqueue(T value)
		{
			object obj = this.sync;
			lock (obj)
			{
				if (this.size > 1)
				{
					this.head = (this.head + 1) % this.size;
					if (this.head == this.tail)
					{
						this.tail = (this.tail + 1) % this.size;
					}
				}
				this.data[this.head] = value;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00027A54 File Offset: 0x00025C54
		public T Dequeue()
		{
			object obj = this.sync;
			T result;
			lock (obj)
			{
				if (this.size > 1 && this.tail != this.head)
				{
					this.tail = (this.tail + 1) % this.size;
				}
				result = this.data[this.tail];
			}
			return result;
		}

		// Token: 0x040003ED RID: 1005
		private int size;

		// Token: 0x040003EE RID: 1006
		private T[] data;

		// Token: 0x040003EF RID: 1007
		private int head;

		// Token: 0x040003F0 RID: 1008
		private int tail;

		// Token: 0x040003F1 RID: 1009
		private object sync;
	}
}
