using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace MP.Net
{
	// Token: 0x02000162 RID: 354
	public class HttpMjpegStreamer : Streamer
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00034A38 File Offset: 0x00032C38
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00034A88 File Offset: 0x00032C88
		public string Status
		{
			get
			{
				object obj = this.locker;
				string status;
				lock (obj)
				{
					status = this._status;
				}
				return status;
			}
			private set
			{
				object obj = this.locker;
				lock (obj)
				{
					this._status = value;
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00034AD4 File Offset: 0x00032CD4
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00034B24 File Offset: 0x00032D24
		public long BytesReceived
		{
			get
			{
				object obj = this.locker;
				long bytesReceived;
				lock (obj)
				{
					bytesReceived = this._bytesReceived;
				}
				return bytesReceived;
			}
			private set
			{
				object obj = this.locker;
				lock (obj)
				{
					this._bytesReceived = value;
				}
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00034B70 File Offset: 0x00032D70
		public override void Connect(string url, LoadOptions loadOptions = null)
		{
			if (loadOptions != null)
			{
				base.videoStreamInfo = ((loadOptions.videoStreamInfo == null) ? new VideoStreamInfo() : loadOptions.videoStreamInfo);
				this.timeout = loadOptions.connectTimeout;
			}
			else
			{
				base.videoStreamInfo = new VideoStreamInfo();
				this.timeout = 10f;
			}
			base.videoStreamInfo.codecFourCC = 1196444237U;
			base.videoStreamInfo.frameCount = 0;
			base.videoStreamInfo.framerate = 0f;
			this.frameRingBuffer = new byte[1][];
			this.receivedFrameCount = 0;
			this.shouldStop = false;
			this.thread = new Thread(new ParameterizedThreadStart(this.ThreadRun));
			this.thread.Start(url);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00034C38 File Offset: 0x00032E38
		public override void Shutdown(bool force = false)
		{
			this.shouldStop = true;
			if (force && this.thread != null)
			{
				this.thread.Interrupt();
				this.thread = null;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00034C74 File Offset: 0x00032E74
		public override bool IsConnected
		{
			get
			{
				return this.thread != null && this.thread.IsAlive && this.connected;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00034CA8 File Offset: 0x00032EA8
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00034CB0 File Offset: 0x00032EB0
		public override int VideoPosition
		{
			get
			{
				return this.receivedFrameCount;
			}
			set
			{
				throw new NotSupportedException("Can't seek a live stream");
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00034CBC File Offset: 0x00032EBC
		public override int ReadVideoFrame(out byte[] targetBuf)
		{
			object obj = this.locker;
			lock (obj)
			{
				targetBuf = ((this.receivedFrameCount <= 0) ? null : this.frameRingBuffer[this.receivedFrameCount % this.frameRingBuffer.Length]);
			}
			return (targetBuf == null) ? 0 : targetBuf.Length;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00034D3C File Offset: 0x00032F3C
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x00034D48 File Offset: 0x00032F48
		public override int AudioPosition
		{
			get
			{
				throw new NotSupportedException("There's no audio stream in HTTP MJPEG stream");
			}
			set
			{
				throw new NotSupportedException("Can't seek a live stream");
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00034D54 File Offset: 0x00032F54
		public override int ReadAudioSamples(out byte[] targetBuf, int sampleCount)
		{
			throw new NotSupportedException("There's no audio stream in HTTP MJPEG stream");
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00034D60 File Offset: 0x00032F60
		private void FrameReceived(byte[] bytes)
		{
			this.Status = "Received frame " + this.receivedFrameCount;
			object obj = this.locker;
			lock (obj)
			{
				this.frameRingBuffer[this.receivedFrameCount % this.frameRingBuffer.Length] = bytes;
				this.receivedFrameCount++;
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00034DE4 File Offset: 0x00032FE4
		private void ThreadRun(object url)
		{
			Stream stream = null;
			try
			{
				this.connected = false;
				this.Status = "Connecting to " + url;
				WebRequest webRequest = WebRequest.Create((string)url);
				webRequest.Timeout = (int)(this.timeout * 1000f);
				webRequest.ContentType = "image/png,image/*;q=0.8,*/*;q=0.5";
				this.BytesReceived = 0L;
				stream = webRequest.GetResponse().GetResponseStream();
				BinaryReader binaryReader = new BinaryReader(new BufferedStream(stream), new ASCIIEncoding());
				List<byte> list = new List<byte>(131072);
				this.Status = "Connected. Waiting for the first frame...";
				this.connected = true;
				int num = 0;
				bool flag = false;
				while (!this.shouldStop)
				{
					byte b = binaryReader.ReadByte();
					this.BytesReceived += 1L;
					if (flag)
					{
						if (list.Count > 1048576)
						{
							list.Clear();
							flag = false;
						}
						else
						{
							list.Add(b);
						}
					}
					if (num == 0)
					{
						if (b == 255)
						{
							num = 1;
						}
					}
					else if (num == 1)
					{
						if (b == 216)
						{
							list.Clear();
							list.Add(byte.MaxValue);
							list.Add(216);
							flag = true;
						}
						else if (b == 217)
						{
							this.FrameReceived(list.ToArray());
							flag = false;
							Thread.Sleep(1);
						}
						num = 0;
					}
				}
				this.Status = "Closing the connection";
			}
			catch (Exception ex)
			{
				this.Status = ex.ToString();
			}
			finally
			{
				this.connected = false;
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00034FBC File Offset: 0x000331BC
		private static string ReadLine(BinaryReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			char c;
			while ((c = reader.ReadChar()) != '\n')
			{
				if (c != '\r' && c != '\n')
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000630 RID: 1584
		private const int INITIAL_BYTE_BUFFER_SIZE = 131072;

		// Token: 0x04000631 RID: 1585
		private const int MAX_BYTE_BUFFER_SIZE = 1048576;

		// Token: 0x04000632 RID: 1586
		private object locker = new object();

		// Token: 0x04000633 RID: 1587
		private string _status;

		// Token: 0x04000634 RID: 1588
		private int receivedFrameCount;

		// Token: 0x04000635 RID: 1589
		private long _bytesReceived;

		// Token: 0x04000636 RID: 1590
		private Thread thread;

		// Token: 0x04000637 RID: 1591
		private float timeout;

		// Token: 0x04000638 RID: 1592
		private byte[][] frameRingBuffer;

		// Token: 0x04000639 RID: 1593
		private volatile bool shouldStop;

		// Token: 0x0400063A RID: 1594
		private volatile bool connected;
	}
}
