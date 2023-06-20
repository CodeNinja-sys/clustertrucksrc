using System;
using System.IO;
using UnityEngine;

namespace MP
{
	// Token: 0x0200015F RID: 351
	public class Movie
	{
		// Token: 0x04000629 RID: 1577
		public Stream sourceStream;

		// Token: 0x0400062A RID: 1578
		public Demux demux;

		// Token: 0x0400062B RID: 1579
		public VideoDecoder videoDecoder;

		// Token: 0x0400062C RID: 1580
		public AudioDecoder audioDecoder;

		// Token: 0x0400062D RID: 1581
		public Rect[] frameUV;
	}
}
