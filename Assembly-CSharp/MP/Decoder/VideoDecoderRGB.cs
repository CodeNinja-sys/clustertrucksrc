using System;
using System.Diagnostics;
using UnityEngine;

namespace MP.Decoder
{
	// Token: 0x02000157 RID: 343
	public class VideoDecoderRGB : VideoDecoder
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x00033828 File Offset: 0x00031A28
		public VideoDecoderRGB(VideoStreamInfo info = null)
		{
			this.info = info;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00033838 File Offset: 0x00031A38
		public override void Init(out Texture2D framebuffer, Demux demux, LoadOptions loadOptions = null)
		{
			if (demux == null)
			{
				throw new ArgumentException("Missing Demux to get video frames from");
			}
			if (this.info == null || this.info.width <= 0 || this.info.height <= 0 || this.info.bitsPerPixel <= 0)
			{
				throw new ArgumentException("Can't initialize stream decoder without proper VideoStreamInfo");
			}
			if (this.info.bitsPerPixel != 16 && this.info.bitsPerPixel != 24 && this.info.bitsPerPixel != 32)
			{
				throw new ArgumentException("Only RGB555, RGB24 and ARGB32 pixel formats are supported");
			}
			this.framebuffer = new Texture2D(this.info.width, this.info.height, (this.info.bitsPerPixel != 32) ? TextureFormat.RGB24 : TextureFormat.ARGB32, false);
			framebuffer = this.framebuffer;
			this.rgbBuffer = new Color32[this.info.width * this.info.height];
			this.demux = demux;
			this._lastFrameDecodeTime = 0f;
			this._lastFrameSizeBytes = 0;
			this._totalDecodeTime = 0f;
			this._totalSizeBytes = 0L;
			this.watch = new Stopwatch();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0003397C File Offset: 0x00031B7C
		public override void Shutdown()
		{
			if (this.framebuffer != null)
			{
				if (Application.isEditor)
				{
					UnityEngine.Object.DestroyImmediate(this.framebuffer);
				}
				else
				{
					UnityEngine.Object.Destroy(this.framebuffer);
				}
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000339C0 File Offset: 0x00031BC0
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x000339D0 File Offset: 0x00031BD0
		public override int Position
		{
			get
			{
				return this.demux.VideoPosition;
			}
			set
			{
				this.demux.VideoPosition = value;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000339E0 File Offset: 0x00031BE0
		public override void DecodeNext()
		{
			if (this.framebuffer == null)
			{
				return;
			}
			this.watch.Reset();
			this.watch.Start();
			byte[] array;
			int num = this.demux.ReadVideoFrame(out array);
			int num2 = num / (this.info.bitsPerPixel / 8);
			if (num2 > this.rgbBuffer.Length)
			{
				throw new MpException("Too much data in frame " + (this.demux.VideoPosition - 1) + " to decode. Broken AVI?");
			}
			if (this.info.bitsPerPixel == 32)
			{
				for (int i = 0; i < num2; i++)
				{
					int num3 = i * 4;
					this.rgbBuffer[i].b = array[num3];
					this.rgbBuffer[i].g = array[num3 + 1];
					this.rgbBuffer[i].r = array[num3 + 2];
					this.rgbBuffer[i].a = array[num3 + 3];
				}
			}
			else if (this.info.bitsPerPixel == 24)
			{
				for (int j = 0; j < num2; j++)
				{
					int num4 = j * 3;
					this.rgbBuffer[j].b = array[num4];
					this.rgbBuffer[j].g = array[num4 + 1];
					this.rgbBuffer[j].r = array[num4 + 2];
				}
			}
			else if (this.info.bitsPerPixel == 16)
			{
				for (int k = 0; k < num2; k++)
				{
					int num5 = k * 2;
					int num6 = (int)array[num5 + 1] << 8 | (int)array[num5];
					this.rgbBuffer[k].b = (byte)(num6 << 3);
					this.rgbBuffer[k].g = (byte)(num6 >> 2 & 248);
					this.rgbBuffer[k].r = (byte)(num6 >> 7 & 248);
				}
			}
			this.framebuffer.SetPixels32(this.rgbBuffer);
			this.framebuffer.Apply(false);
			this.watch.Stop();
			this._lastFrameDecodeTime = (float)(0.0010000000474974513 * this.watch.Elapsed.TotalMilliseconds);
			this._lastFrameSizeBytes = this.rgbBuffer.Length;
			this._totalDecodeTime += this._lastFrameDecodeTime;
			this._totalSizeBytes += (long)this._lastFrameSizeBytes;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00033C7C File Offset: 0x00031E7C
		public override float lastFrameDecodeTime
		{
			get
			{
				return this._lastFrameDecodeTime;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00033C84 File Offset: 0x00031E84
		public override int lastFrameSizeBytes
		{
			get
			{
				return this._lastFrameSizeBytes;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00033C8C File Offset: 0x00031E8C
		public override float totalDecodeTime
		{
			get
			{
				return this._totalDecodeTime;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00033C94 File Offset: 0x00031E94
		public override long totalSizeBytes
		{
			get
			{
				return this._totalSizeBytes;
			}
		}

		// Token: 0x040005F2 RID: 1522
		public const uint FOURCC_NULL = 0U;

		// Token: 0x040005F3 RID: 1523
		public const uint FOURCC_DIB_ = 541215044U;

		// Token: 0x040005F4 RID: 1524
		private Texture2D framebuffer;

		// Token: 0x040005F5 RID: 1525
		private Color32[] rgbBuffer;

		// Token: 0x040005F6 RID: 1526
		private Demux demux;

		// Token: 0x040005F7 RID: 1527
		private VideoStreamInfo info;

		// Token: 0x040005F8 RID: 1528
		private float _lastFrameDecodeTime;

		// Token: 0x040005F9 RID: 1529
		private int _lastFrameSizeBytes;

		// Token: 0x040005FA RID: 1530
		private float _totalDecodeTime;

		// Token: 0x040005FB RID: 1531
		private long _totalSizeBytes;

		// Token: 0x040005FC RID: 1532
		private Stopwatch watch;
	}
}
