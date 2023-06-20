using System;
using System.Diagnostics;
using UnityEngine;

namespace MP.Decoder
{
	// Token: 0x02000158 RID: 344
	public abstract class VideoDecoderUnity : VideoDecoder
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x00033C9C File Offset: 0x00031E9C
		public VideoDecoderUnity(VideoStreamInfo streamInfo = null)
		{
			this.streamInfo = streamInfo;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00033CAC File Offset: 0x00031EAC
		public override void Init(out Texture2D framebuffer, Demux demux, LoadOptions loadOptions = null)
		{
			if (demux == null)
			{
				throw new ArgumentException("Missing Demux to get video frames from");
			}
			this.framebuffer = new Texture2D(4, 4, TextureFormat.RGB24, false);
			framebuffer = this.framebuffer;
			this.demux = demux;
			this._lastFrameDecodeTime = 0f;
			this._totalDecodeTime = 0f;
			this.watch = new Stopwatch();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00033D0C File Offset: 0x00031F0C
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00033D50 File Offset: 0x00031F50
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00033D60 File Offset: 0x00031F60
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

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00033D70 File Offset: 0x00031F70
		public override float lastFrameDecodeTime
		{
			get
			{
				return this._lastFrameDecodeTime;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00033D78 File Offset: 0x00031F78
		public override int lastFrameSizeBytes
		{
			get
			{
				return this._lastFrameSizeBytes;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00033D80 File Offset: 0x00031F80
		public override float totalDecodeTime
		{
			get
			{
				return this._totalDecodeTime;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00033D88 File Offset: 0x00031F88
		public override long totalSizeBytes
		{
			get
			{
				return this._totalSizeBytes;
			}
		}

		// Token: 0x040005FD RID: 1533
		protected Texture2D framebuffer;

		// Token: 0x040005FE RID: 1534
		protected VideoStreamInfo streamInfo;

		// Token: 0x040005FF RID: 1535
		protected Demux demux;

		// Token: 0x04000600 RID: 1536
		protected float _lastFrameDecodeTime;

		// Token: 0x04000601 RID: 1537
		protected int _lastFrameSizeBytes;

		// Token: 0x04000602 RID: 1538
		protected float _totalDecodeTime;

		// Token: 0x04000603 RID: 1539
		protected long _totalSizeBytes;

		// Token: 0x04000604 RID: 1540
		protected Stopwatch watch;
	}
}
