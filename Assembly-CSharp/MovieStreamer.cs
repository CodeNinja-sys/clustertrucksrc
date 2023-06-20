using System;
using MP;
using MP.Net;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class MovieStreamer : MoviePlayerBase
{
	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000702 RID: 1794 RVA: 0x0002ED64 File Offset: 0x0002CF64
	public bool IsConnected
	{
		get
		{
			return this.movie != null && this.movie.demux != null && ((Streamer)this.movie.demux).IsConnected;
		}
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
	public bool Load(string srcUrl)
	{
		return this.Load(srcUrl, null);
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0002EDB4 File Offset: 0x0002CFB4
	public bool Load(string srcUrl, LoadOptions loadOptions)
	{
		this.sourceUrl = srcUrl;
		if (loadOptions == null)
		{
			loadOptions = this.loadOptions;
		}
		else
		{
			this.loadOptions = loadOptions;
		}
		bool result;
		try
		{
			base.Load(new MovieSource
			{
				url = srcUrl
			}, loadOptions);
			result = true;
		}
		catch (Exception ex)
		{
			if (loadOptions.enableExceptionThrow)
			{
				throw ex;
			}
			Debug.LogError(ex);
			result = false;
		}
		return result;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0002EE40 File Offset: 0x0002D040
	[ContextMenu("Reconnect")]
	public bool ReConnect()
	{
		bool result = true;
		if (!string.IsNullOrEmpty(this.sourceUrl))
		{
			result = this.Load(this.sourceUrl, this.loadOptions);
		}
		return result;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0002EE74 File Offset: 0x0002D074
	private void Start()
	{
		this.ReConnect();
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0002EE80 File Offset: 0x0002D080
	private void OnGUI()
	{
		if (!this.IsConnected || !this.movie.demux.hasVideo)
		{
			return;
		}
		if (this.drawToScreen && this.framebuffer != null && ((Streamer)this.movie.demux).VideoPosition > 0)
		{
			base.DrawFramebufferToScreen(null);
		}
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0002EEF4 File Offset: 0x0002D0F4
	private void Update()
	{
		if (this.movie != null && this.movie.demux != null && this.movie.demux is HttpMjpegStreamer)
		{
			this.status = ((HttpMjpegStreamer)this.movie.demux).Status;
			this.bytesReceived = ((HttpMjpegStreamer)this.movie.demux).BytesReceived;
		}
		base.HandlePlayStop();
		if (this.play)
		{
			this.HandleFrameDecode();
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0002EF80 File Offset: 0x0002D180
	protected void HandleFrameDecode()
	{
		if (!this.IsConnected || !this.movie.demux.hasVideo || this.movie.videoDecoder == null)
		{
			return;
		}
		if (this.movie.videoDecoder.Position != this.lastVideoFrame)
		{
			if (this.movie.videoDecoder.Position >= 0)
			{
				this.movie.videoDecoder.DecodeNext();
				this.movie.demux.videoStreamInfo.width = this.framebuffer.width;
				this.movie.demux.videoStreamInfo.height = this.framebuffer.height;
			}
			this.lastVideoFrame = this.movie.videoDecoder.Position;
		}
	}

	// Token: 0x0400052B RID: 1323
	public string sourceUrl;

	// Token: 0x0400052C RID: 1324
	public LoadOptions loadOptions = LoadOptions.Default;

	// Token: 0x0400052D RID: 1325
	public string status;

	// Token: 0x0400052E RID: 1326
	public long bytesReceived;

	// Token: 0x0400052F RID: 1327
	private int lastVideoFrame = -1;
}
