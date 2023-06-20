using System;
using UnityEngine;

namespace MP
{
	// Token: 0x0200015A RID: 346
	public class DuplicateFrameFinder
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x00033EAC File Offset: 0x000320AC
		public DuplicateFrameFinder(VideoDecoder videoDecoder, Texture2D framebuffer, int frameOffset, int frameCount, DuplicateFrameFinder.Options options = null)
		{
			if (options == null)
			{
				options = DuplicateFrameFinder.Options.Default;
			}
			this.options = options;
			this.videoDecoder = videoDecoder;
			this.framebuffer = framebuffer;
			this.frameOffset = frameOffset;
			this.frameCount = frameCount;
			this.Reset();
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00033EF8 File Offset: 0x000320F8
		public int framesProcessed
		{
			get
			{
				return this.currentFrame;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00033F00 File Offset: 0x00032100
		public int[] duplicates
		{
			get
			{
				return this.duplicateOf;
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00033F08 File Offset: 0x00032108
		public void Reset()
		{
			this.Reset(this.frameOffset, this.frameCount, this.options);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00033F24 File Offset: 0x00032124
		public void Reset(int frameOffset, int frameCount, DuplicateFrameFinder.Options options = null)
		{
			this.frameOffset = frameOffset;
			this.frameCount = frameCount;
			this.frameTones = new Color32[frameCount];
			this.duplicateOf = new int[frameCount];
			for (int i = 0; i < frameCount; i++)
			{
				this.duplicateOf[i] = -1;
			}
			if (options.pixelCacheSize > 0)
			{
				this.pixelCache = new Color32[options.pixelCacheSize][];
			}
			else
			{
				this.pixelCache = null;
			}
			this.currentFrame = 0;
			this.stats = default(DuplicateFrameFinder.Stats);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00033FB4 File Offset: 0x000321B4
		public bool Progress()
		{
			if (this.currentFrame >= this.frameCount)
			{
				return false;
			}
			this.videoDecoder.Decode(this.currentFrame + this.frameOffset);
			Color32[] array;
			if (this.pixelCache != null)
			{
				array = (this.pixelCache[this.currentFrame % this.pixelCache.Length] = this.framebuffer.GetPixels32());
			}
			else
			{
				array = this.framebuffer.GetPixels32();
			}
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			foreach (Color32 color in array)
			{
				num += (uint)color.r;
				num2 += (uint)color.g;
				num3 += (uint)color.b;
				num4 += (uint)color.a;
			}
			this.frameTones[this.currentFrame] = new Color32((byte)(num / (uint)array.Length), (byte)(num2 / (uint)array.Length), (byte)(num3 / (uint)array.Length), (byte)(num4 / (uint)array.Length));
			int num5 = this.currentFrame - 1;
			int num6 = 0;
			while (num6 < this.options.maxLookbackFrames && num5 >= 0)
			{
				if (this.duplicateOf[num5] < 0)
				{
					this.stats.totalFramesCompared = this.stats.totalFramesCompared + 1L;
					if (Mathf.Abs(DuplicateFrameFinder.SqrPixelDiff(this.frameTones[this.currentFrame], this.frameTones[num5])) <= this.options.toneCompareDistrust * this.options.toneCompareDistrust)
					{
						this.stats.pixelCacheQueries = this.stats.pixelCacheQueries + 1L;
						Color32[] b;
						if (this.pixelCache != null && this.currentFrame - num5 < this.pixelCache.Length)
						{
							this.stats.pixelCacheHits = this.stats.pixelCacheHits + 1L;
							b = this.pixelCache[num5 % this.pixelCache.Length];
						}
						else
						{
							this.videoDecoder.Decode(num5 + this.frameOffset);
							b = this.framebuffer.GetPixels32();
						}
						this.stats.framesPartiallyCompared = this.stats.framesPartiallyCompared + 1L;
						float num7;
						int num8;
						DuplicateFrameFinder.ImageDiff(out num7, out num8, array, b, true, 53);
						if (num7 <= this.options.maxImageDiff && num8 <= this.options.maxPixelDiff)
						{
							this.stats.framesFullyCompared = this.stats.framesFullyCompared + 1L;
							DuplicateFrameFinder.ImageDiff(out num7, out num8, array, b, false, 1);
							if (num7 <= this.options.maxImageDiff && num8 <= this.options.maxPixelDiff)
							{
								this.duplicateOf[this.currentFrame] = num5;
								this.stats.duplicateCount = this.stats.duplicateCount + 1;
								break;
							}
						}
					}
					if (!this.options.otherStreamsAvailable)
					{
						num6++;
					}
				}
				if (this.options.otherStreamsAvailable)
				{
					num6++;
				}
				num5--;
			}
			this.currentFrame++;
			return true;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000342D8 File Offset: 0x000324D8
		private static int SqrPixelDiff(Color32 c1, Color32 c2)
		{
			int num = (int)(c1.r - c2.r);
			int num2 = (int)(c1.g - c2.g);
			int num3 = (int)(c1.b - c2.b);
			int num4 = (int)(c1.a - c2.a);
			return num * num + num2 * num2 + num3 * num3 + num4 * num4;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00034334 File Offset: 0x00032534
		private static void ImageDiff(out float sqrImageDiff, out int maxPixelDiff, Color32[] a, Color32[] b, bool fasterPixelCompare = false, int considerEveryNthPixel = 1)
		{
			long num = 0L;
			maxPixelDiff = 0;
			if (considerEveryNthPixel < 1)
			{
				considerEveryNthPixel = 1;
			}
			int num2 = a.Length;
			if (fasterPixelCompare)
			{
				for (int i = 0; i < num2; i += considerEveryNthPixel)
				{
					int num3 = (int)(a[i].g - b[i].g);
					if (num3 > maxPixelDiff)
					{
						maxPixelDiff = num3;
					}
					num += (long)(num3 * num3);
				}
				num *= 4L;
			}
			else
			{
				for (int j = 0; j < num2; j += considerEveryNthPixel)
				{
					int num4 = DuplicateFrameFinder.SqrPixelDiff(a[j], b[j]);
					num += (long)num4;
					if (num4 > maxPixelDiff)
					{
						maxPixelDiff = num4;
					}
				}
				maxPixelDiff = Mathf.RoundToInt(Mathf.Sqrt((float)maxPixelDiff));
			}
			sqrImageDiff = (float)((double)num / (double)a.Length);
		}

		// Token: 0x04000607 RID: 1543
		public DuplicateFrameFinder.Stats stats;

		// Token: 0x04000608 RID: 1544
		private int[] duplicateOf;

		// Token: 0x04000609 RID: 1545
		private Color32[] frameTones;

		// Token: 0x0400060A RID: 1546
		private Color32[][] pixelCache;

		// Token: 0x0400060B RID: 1547
		private int currentFrame;

		// Token: 0x0400060C RID: 1548
		private VideoDecoder videoDecoder;

		// Token: 0x0400060D RID: 1549
		private Texture2D framebuffer;

		// Token: 0x0400060E RID: 1550
		private int frameOffset;

		// Token: 0x0400060F RID: 1551
		private int frameCount;

		// Token: 0x04000610 RID: 1552
		private DuplicateFrameFinder.Options options;

		// Token: 0x0200015B RID: 347
		public class Options
		{
			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0003444C File Offset: 0x0003264C
			public static DuplicateFrameFinder.Options Default
			{
				get
				{
					return new DuplicateFrameFinder.Options();
				}
			}

			// Token: 0x04000611 RID: 1553
			public float maxImageDiff = 5f;

			// Token: 0x04000612 RID: 1554
			public int maxPixelDiff = 50;

			// Token: 0x04000613 RID: 1555
			public int maxLookbackFrames = 100;

			// Token: 0x04000614 RID: 1556
			public int toneCompareDistrust = 2;

			// Token: 0x04000615 RID: 1557
			public int pixelCacheSize = 101;

			// Token: 0x04000616 RID: 1558
			public bool otherStreamsAvailable;
		}

		// Token: 0x0200015C RID: 348
		public struct Stats
		{
			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00034454 File Offset: 0x00032654
			public float framesPartiallyComparedPercent
			{
				get
				{
					return (float)this.framesPartiallyCompared / (float)this.totalFramesCompared * 100f;
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0003446C File Offset: 0x0003266C
			public float framesFullyComparedPercent
			{
				get
				{
					return (float)this.framesFullyCompared / (float)this.totalFramesCompared * 100f;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00034484 File Offset: 0x00032684
			public float pixelCacheHitPercent
			{
				get
				{
					return (float)this.pixelCacheHits / (float)this.pixelCacheQueries * 100f;
				}
			}

			// Token: 0x04000617 RID: 1559
			public long totalFramesCompared;

			// Token: 0x04000618 RID: 1560
			public long framesPartiallyCompared;

			// Token: 0x04000619 RID: 1561
			public long framesFullyCompared;

			// Token: 0x0400061A RID: 1562
			public long pixelCacheQueries;

			// Token: 0x0400061B RID: 1563
			public long pixelCacheHits;

			// Token: 0x0400061C RID: 1564
			public int duplicateCount;
		}
	}
}
