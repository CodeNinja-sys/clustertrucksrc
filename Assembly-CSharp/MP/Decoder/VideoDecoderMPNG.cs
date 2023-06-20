using System;
using UnityEngine;

namespace MP.Decoder
{
	// Token: 0x02000156 RID: 342
	public class VideoDecoderMPNG : VideoDecoderUnity
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x000336A0 File Offset: 0x000318A0
		public VideoDecoderMPNG(VideoStreamInfo streamInfo = null) : base(streamInfo)
		{
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000336B8 File Offset: 0x000318B8
		public override void DecodeNext()
		{
			if (this.framebuffer == null)
			{
				return;
			}
			this.watch.Reset();
			this.watch.Start();
			byte[] array;
			int lastFrameSizeBytes = this.demux.ReadVideoFrame(out array);
			bool flag = this.framebuffer.LoadImage(array);
			if (flag && this.lastFbWidth > 0)
			{
				if (this.framebuffer.width != this.lastFbWidth || this.framebuffer.height != this.lastFbHeight)
				{
					flag = false;
				}
				this.lastFbWidth = this.framebuffer.width;
				this.lastFbHeight = this.framebuffer.height;
			}
			if (flag)
			{
				this.framebuffer.Apply(false);
			}
			else
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Couldn't decode frame ",
					this.demux.VideoPosition - 1,
					" from ",
					array.Length,
					" bytes"
				}));
			}
			this.watch.Stop();
			this._lastFrameDecodeTime = (float)(0.0010000000474974513 * this.watch.Elapsed.TotalMilliseconds);
			this._lastFrameSizeBytes = lastFrameSizeBytes;
			this._totalDecodeTime += this._lastFrameDecodeTime;
			this._totalSizeBytes += (long)this._lastFrameSizeBytes;
		}

		// Token: 0x040005EF RID: 1519
		public const uint FOURCC_MPNG = 1196314701U;

		// Token: 0x040005F0 RID: 1520
		protected int lastFbWidth = -1;

		// Token: 0x040005F1 RID: 1521
		protected int lastFbHeight = -1;
	}
}
