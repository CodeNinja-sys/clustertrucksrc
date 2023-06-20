using System;

namespace MP.AVI
{
	// Token: 0x02000148 RID: 328
	public class BitmapInfoHeader
	{
		// Token: 0x0400057A RID: 1402
		public uint biSize;

		// Token: 0x0400057B RID: 1403
		public int biWidth;

		// Token: 0x0400057C RID: 1404
		public int biHeight;

		// Token: 0x0400057D RID: 1405
		public ushort biPlanes;

		// Token: 0x0400057E RID: 1406
		public ushort biBitCount;

		// Token: 0x0400057F RID: 1407
		public uint biCompression;

		// Token: 0x04000580 RID: 1408
		public uint biSizeImage;

		// Token: 0x04000581 RID: 1409
		public int biXPelsPerMeter;

		// Token: 0x04000582 RID: 1410
		public int biYPelsPerMeter;

		// Token: 0x04000583 RID: 1411
		public uint biClrUsed;

		// Token: 0x04000584 RID: 1412
		public uint biClrImportant;
	}
}
