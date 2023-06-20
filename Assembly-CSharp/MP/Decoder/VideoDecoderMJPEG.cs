using System;
using UnityEngine;

namespace MP.Decoder
{
	// Token: 0x02000155 RID: 341
	public class VideoDecoderMJPEG : VideoDecoderUnity
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x000331A4 File Offset: 0x000313A4
		public VideoDecoderMJPEG(VideoStreamInfo streamInfo = null) : base(streamInfo)
		{
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000331D8 File Offset: 0x000313D8
		public override void Shutdown()
		{
			base.Shutdown();
			if (this.field != null)
			{
				if (Application.isEditor)
				{
					UnityEngine.Object.DestroyImmediate(this.field);
				}
				else
				{
					UnityEngine.Object.Destroy(this.field);
				}
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00033224 File Offset: 0x00031424
		private int FindMarker(byte[] buf, int bufSize, byte marker)
		{
			for (int i = 1; i < bufSize; i++)
			{
				if (buf[i - 1] == 255 && buf[i] == marker)
				{
					return i - 1;
				}
			}
			return -1;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00033260 File Offset: 0x00031460
		private bool DecodeField(byte[] buf, int offset, int size, bool needToAddDHT, bool evenField)
		{
			if (offset != 0)
			{
				Array.Copy(buf, offset, buf, 0, size);
			}
			if (needToAddDHT)
			{
				if (size > buf.Length - VideoDecoderMJPEG.mjpegDefaultDHT.Length)
				{
					Debug.LogError("Demux didn't allocate extra " + VideoDecoderMJPEG.mjpegDefaultDHT.Length + "B for adding DHT, but we need it (a bug in Demux class)");
					return false;
				}
				int num = this.FindMarker(buf, size, 218);
				if (num >= 0)
				{
					Array.Copy(buf, num, buf, num + VideoDecoderMJPEG.mjpegDefaultDHT.Length, size - num);
					Array.Copy(VideoDecoderMJPEG.mjpegDefaultDHT, 0, buf, num, VideoDecoderMJPEG.mjpegDefaultDHT.Length);
				}
			}
			bool flag = this.field.LoadImage(buf);
			if (flag)
			{
				if (this.framebuffer.width != this.field.width || this.framebuffer.height != this.field.height * 2)
				{
					this.framebuffer.Resize(this.field.width, this.field.height * 2, this.field.format, false);
				}
				int num2 = (!(evenField ^ this.interlacedEvenFieldIsLower)) ? 0 : 1;
				for (int i = 0; i < this.framebuffer.height / 2; i++)
				{
					Color[] pixels = this.field.GetPixels(0, i, this.framebuffer.width, 1);
					this.framebuffer.SetPixels(0, i * 2 + num2, this.framebuffer.width, 1, pixels);
				}
			}
			return flag;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000333E0 File Offset: 0x000315E0
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
			if (num > 0)
			{
				int num2 = this.FindMarker(array, num, 224);
				bool flag = num2 >= 0 && array[num2 + 4] == 65 && array[num2 + 5] == 86 && array[num2 + 6] == 73 && array[num2 + 7] == 49;
				bool flag2 = flag && array[num2 + 8] != 0;
				bool flag4;
				if (flag2)
				{
					bool flag3 = array[num2 + 8] == 2;
					if (this.field == null)
					{
						this.field = new Texture2D(4, 4, TextureFormat.RGB24, false);
					}
					flag4 = this.DecodeField(array, 0, num, flag, flag3);
					if (flag4)
					{
						int num3 = -1;
						if (flag)
						{
							num += VideoDecoderMJPEG.mjpegDefaultDHT.Length;
						}
						int num4 = this.FindMarker(array, num, 217);
						if (num > num4 + 6)
						{
							if (array[num4 + 2] == 255 && array[num4 + 3] == 216)
							{
								num3 = num4 + 2;
							}
							if (array[num4 + 4] == 255 && array[num4 + 5] == 216)
							{
								num3 = num4 + 4;
							}
						}
						int size = num - num3;
						if (num3 > 0)
						{
							flag4 = this.DecodeField(array, num3, size, flag, !flag3);
						}
					}
				}
				else
				{
					flag4 = this.framebuffer.LoadImage(array);
					if (flag4 && this.lastFbWidth > 0)
					{
						if (this.framebuffer.width != this.lastFbWidth || this.framebuffer.height != this.lastFbHeight)
						{
							flag4 = false;
						}
						this.lastFbWidth = this.framebuffer.width;
						this.lastFbHeight = this.framebuffer.height;
					}
				}
				if (flag4)
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
			}
			this.watch.Stop();
			this._lastFrameDecodeTime = (float)(0.0010000000474974513 * this.watch.Elapsed.TotalMilliseconds);
			this._lastFrameSizeBytes = num;
			this._totalDecodeTime += this._lastFrameDecodeTime;
			this._totalSizeBytes += (long)this._lastFrameSizeBytes;
		}

		// Token: 0x040005E6 RID: 1510
		public const uint FOURCC_MJPG = 1196444237U;

		// Token: 0x040005E7 RID: 1511
		public const uint FOURCC_CJPG = 1196444227U;

		// Token: 0x040005E8 RID: 1512
		public const uint FOURCC_ffds = 1935959654U;

		// Token: 0x040005E9 RID: 1513
		public const uint FOURCC_jpeg = 1734701162U;

		// Token: 0x040005EA RID: 1514
		public bool interlacedEvenFieldIsLower;

		// Token: 0x040005EB RID: 1515
		private Texture2D field;

		// Token: 0x040005EC RID: 1516
		protected int lastFbWidth = -1;

		// Token: 0x040005ED RID: 1517
		protected int lastFbHeight = -1;

		// Token: 0x040005EE RID: 1518
		private static byte[] mjpegDefaultDHT = new byte[]
		{
			byte.MaxValue,
			196,
			1,
			162,
			0,
			0,
			1,
			5,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			1,
			0,
			3,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			16,
			0,
			2,
			1,
			3,
			3,
			2,
			4,
			3,
			5,
			5,
			4,
			4,
			0,
			0,
			1,
			125,
			1,
			2,
			3,
			0,
			4,
			17,
			5,
			18,
			33,
			49,
			65,
			6,
			19,
			81,
			97,
			7,
			34,
			113,
			20,
			50,
			129,
			145,
			161,
			8,
			35,
			66,
			177,
			193,
			21,
			82,
			209,
			240,
			36,
			51,
			98,
			114,
			130,
			9,
			10,
			22,
			23,
			24,
			25,
			26,
			37,
			38,
			39,
			40,
			41,
			42,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
			131,
			132,
			133,
			134,
			135,
			136,
			137,
			138,
			146,
			147,
			148,
			149,
			150,
			151,
			152,
			153,
			154,
			162,
			163,
			164,
			165,
			166,
			167,
			168,
			169,
			170,
			178,
			179,
			180,
			181,
			182,
			183,
			184,
			185,
			186,
			194,
			195,
			196,
			197,
			198,
			199,
			200,
			201,
			202,
			210,
			211,
			212,
			213,
			214,
			215,
			216,
			217,
			218,
			225,
			226,
			227,
			228,
			229,
			230,
			231,
			232,
			233,
			234,
			241,
			242,
			243,
			244,
			245,
			246,
			247,
			248,
			249,
			250,
			17,
			0,
			2,
			1,
			2,
			4,
			4,
			3,
			4,
			7,
			5,
			4,
			4,
			0,
			1,
			2,
			119,
			0,
			1,
			2,
			3,
			17,
			4,
			5,
			33,
			49,
			6,
			18,
			65,
			81,
			7,
			97,
			113,
			19,
			34,
			50,
			129,
			8,
			20,
			66,
			145,
			161,
			177,
			193,
			9,
			35,
			51,
			82,
			240,
			21,
			98,
			114,
			209,
			10,
			22,
			36,
			52,
			225,
			37,
			241,
			23,
			24,
			25,
			26,
			38,
			39,
			40,
			41,
			42,
			53,
			54,
			55,
			56,
			57,
			58,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
			130,
			131,
			132,
			133,
			134,
			135,
			136,
			137,
			138,
			146,
			147,
			148,
			149,
			150,
			151,
			152,
			153,
			154,
			162,
			163,
			164,
			165,
			166,
			167,
			168,
			169,
			170,
			178,
			179,
			180,
			181,
			182,
			183,
			184,
			185,
			186,
			194,
			195,
			196,
			197,
			198,
			199,
			200,
			201,
			202,
			210,
			211,
			212,
			213,
			214,
			215,
			216,
			217,
			218,
			226,
			227,
			228,
			229,
			230,
			231,
			232,
			233,
			234,
			242,
			243,
			244,
			245,
			246,
			247,
			248,
			249,
			250
		};
	}
}
