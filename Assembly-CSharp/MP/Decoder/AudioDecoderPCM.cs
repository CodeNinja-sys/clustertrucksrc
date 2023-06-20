using System;
using System.Diagnostics;
using UnityEngine;

namespace MP.Decoder
{
	// Token: 0x02000154 RID: 340
	public class AudioDecoderPCM : AudioDecoder
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00032D1C File Offset: 0x00030F1C
		public AudioDecoderPCM(AudioStreamInfo streamInfo)
		{
			this.streamInfo = streamInfo;
			if (streamInfo == null)
			{
				throw new ArgumentException("Can't initialize stream decoder without proper AudioStreamInfo");
			}
			if (streamInfo.audioFormat != 1U && streamInfo.audioFormat != 65534U && streamInfo.audioFormat != 6U && streamInfo.audioFormat != 7U)
			{
				throw new ArgumentException("Unsupported PCM format=0x" + streamInfo.audioFormat.ToString("X"));
			}
			int num = streamInfo.sampleSize / streamInfo.channels;
			if ((streamInfo.audioFormat == 65534U && num != 4) || (streamInfo.audioFormat != 65534U && num > 2))
			{
				throw new ArgumentException("Only 8bit and 16bit_le int, and 32bit float audio is supported. " + num * 8 + "bits given");
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00032E38 File Offset: 0x00031038
		public override void Init(out AudioClip audioClip, Demux demux, LoadOptions loadOptions = null)
		{
			if (loadOptions == null)
			{
				loadOptions = LoadOptions.Default;
			}
			if (demux == null)
			{
				throw new ArgumentException("Missing Demux to get audio samples for decoding");
			}
			this.demux = demux;
			this._totalDecodeTime = 0f;
			this.watch = new Stopwatch();
			this.audioClip = AudioClip.Create("_movie_audio_", this.streamInfo.sampleCount, this.streamInfo.channels, this.streamInfo.sampleRate, !loadOptions.preloadAudio, new AudioClip.PCMReaderCallback(this.OnAudioRead), new AudioClip.PCMSetPositionCallback(this.OnAudioSeek));
			audioClip = this.audioClip;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00032EDC File Offset: 0x000310DC
		public override void Shutdown()
		{
			if (this.audioClip != null)
			{
				if (Application.isEditor)
				{
					UnityEngine.Object.DestroyImmediate(this.audioClip);
				}
				else
				{
					UnityEngine.Object.Destroy(this.audioClip);
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00032F20 File Offset: 0x00031120
		public override float totalDecodeTime
		{
			get
			{
				return this._totalDecodeTime;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00032F28 File Offset: 0x00031128
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x00032F38 File Offset: 0x00031138
		public override int Position
		{
			get
			{
				return this.demux.AudioPosition;
			}
			set
			{
				this.demux.AudioPosition = value;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00032F48 File Offset: 0x00031148
		public override void DecodeNext(float[] data, int sampleCount)
		{
			if (data == null || this.demux == null)
			{
				return;
			}
			this.watch.Reset();
			this.watch.Start();
			int channels = this.streamInfo.channels;
			try
			{
				byte[] array;
				int num = this.demux.ReadAudioSamples(out array, sampleCount);
				for (int i = 0; i < channels; i++)
				{
					if (this.streamInfo.audioFormat == 1U)
					{
						int num2 = this.streamInfo.sampleSize / channels;
						if (num2 == 2)
						{
							for (int j = 0; j < num; j++)
							{
								int num3 = j * channels + i;
								int num4 = num3 * 2;
								short num5 = (short)((int)array[num4 + 1] << 8 | (int)array[num4]);
								data[num3] = (float)num5 / 32768f;
							}
						}
						else
						{
							for (int k = 0; k < num; k++)
							{
								int num6 = k * channels + i;
								data[num6] = (float)(array[num6] - 128) / 128f;
							}
						}
					}
					else if (this.streamInfo.audioFormat == 65534U)
					{
						Buffer.BlockCopy(array, 0, data, 0, num * this.streamInfo.sampleSize);
					}
					else if (this.streamInfo.audioFormat == 6U)
					{
						for (int l = 0; l < num; l++)
						{
							int num7 = l * channels + i;
							data[num7] = AudioDecoderPCM.ALawExpandLookupTable[(int)array[num7]];
						}
					}
					else if (this.streamInfo.audioFormat == 7U)
					{
						for (int m = 0; m < num; m++)
						{
							int num8 = m * channels + i;
							data[num8] = AudioDecoderPCM.uLawExpandLookupTable[(int)array[num8]];
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (!(ex is IndexOutOfRangeException) && !(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			this.watch.Stop();
			this._totalDecodeTime += (float)(0.0010000000474974513 * this.watch.Elapsed.TotalMilliseconds);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00033180 File Offset: 0x00031380
		public void OnAudioRead(float[] data)
		{
			this.DecodeNext(data, data.Length / this.streamInfo.channels);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00033198 File Offset: 0x00031398
		public void OnAudioSeek(int newPosition)
		{
			this.Position = newPosition;
		}

		// Token: 0x040005D9 RID: 1497
		public const uint FOURCC_MS = 1U;

		// Token: 0x040005DA RID: 1498
		public const uint FOURCC_0 = 0U;

		// Token: 0x040005DB RID: 1499
		public const uint FORMAT_UNCOMPRESSED = 1U;

		// Token: 0x040005DC RID: 1500
		public const uint FORMAT_UNCOMPRESSED_FLOAT = 65534U;

		// Token: 0x040005DD RID: 1501
		public const uint FORMAT_ALAW = 6U;

		// Token: 0x040005DE RID: 1502
		public const uint FORMAT_ULAW = 7U;

		// Token: 0x040005DF RID: 1503
		private AudioStreamInfo streamInfo;

		// Token: 0x040005E0 RID: 1504
		private Demux demux;

		// Token: 0x040005E1 RID: 1505
		private AudioClip audioClip;

		// Token: 0x040005E2 RID: 1506
		private float _totalDecodeTime;

		// Token: 0x040005E3 RID: 1507
		private Stopwatch watch;

		// Token: 0x040005E4 RID: 1508
		private static float[] ALawExpandLookupTable = new float[]
		{
			-0.167969f,
			-0.160156f,
			-0.183594f,
			-0.175781f,
			-0.136719f,
			-0.128906f,
			-0.152344f,
			-0.144531f,
			-0.230469f,
			-0.222656f,
			-0.246094f,
			-0.238281f,
			-0.199219f,
			-0.191406f,
			-0.214844f,
			-0.207031f,
			-0.083984f,
			-0.080078f,
			-0.091797f,
			-0.087891f,
			-0.068359f,
			-0.064453f,
			-0.076172f,
			-0.072266f,
			-0.115234f,
			-0.111328f,
			-0.123047f,
			-0.119141f,
			-0.099609f,
			-0.095703f,
			-0.107422f,
			-0.103516f,
			-0.671875f,
			-0.640625f,
			-0.734375f,
			-0.703125f,
			-0.546875f,
			-0.515625f,
			-0.609375f,
			-0.578125f,
			-0.921875f,
			-0.890625f,
			-0.984375f,
			-0.953125f,
			-0.796875f,
			-0.765625f,
			-0.859375f,
			-0.828125f,
			-0.335938f,
			-0.320312f,
			-0.367188f,
			-0.351562f,
			-0.273438f,
			-0.257812f,
			-0.304688f,
			-0.289062f,
			-0.460938f,
			-0.445312f,
			-0.492188f,
			-0.476562f,
			-0.398438f,
			-0.382812f,
			-0.429688f,
			-0.414062f,
			-0.010498f,
			-0.01001f,
			-0.011475f,
			-0.010986f,
			-0.008545f,
			-0.008057f,
			-0.009521f,
			-0.009033f,
			-0.014404f,
			-0.013916f,
			-0.015381f,
			-0.014893f,
			-0.012451f,
			-0.011963f,
			-0.013428f,
			-0.012939f,
			-0.002686f,
			-0.002197f,
			-0.003662f,
			-0.003174f,
			-0.000732f,
			-0.000244f,
			-0.001709f,
			-0.001221f,
			-0.006592f,
			-0.006104f,
			-0.007568f,
			-0.00708f,
			-0.004639f,
			-0.00415f,
			-0.005615f,
			-0.005127f,
			-0.041992f,
			-0.040039f,
			-0.045898f,
			-0.043945f,
			-0.03418f,
			-0.032227f,
			-0.038086f,
			-0.036133f,
			-0.057617f,
			-0.055664f,
			-0.061523f,
			-0.05957f,
			-0.049805f,
			-0.047852f,
			-0.053711f,
			-0.051758f,
			-0.020996f,
			-0.02002f,
			-0.022949f,
			-0.021973f,
			-0.01709f,
			-0.016113f,
			-0.019043f,
			-0.018066f,
			-0.028809f,
			-0.027832f,
			-0.030762f,
			-0.029785f,
			-0.024902f,
			-0.023926f,
			-0.026855f,
			-0.025879f,
			0.167969f,
			0.160156f,
			0.183594f,
			0.175781f,
			0.136719f,
			0.128906f,
			0.152344f,
			0.144531f,
			0.230469f,
			0.222656f,
			0.246094f,
			0.238281f,
			0.199219f,
			0.191406f,
			0.214844f,
			0.207031f,
			0.083984f,
			0.080078f,
			0.091797f,
			0.087891f,
			0.068359f,
			0.064453f,
			0.076172f,
			0.072266f,
			0.115234f,
			0.111328f,
			0.123047f,
			0.119141f,
			0.099609f,
			0.095703f,
			0.107422f,
			0.103516f,
			0.671875f,
			0.640625f,
			0.734375f,
			0.703125f,
			0.546875f,
			0.515625f,
			0.609375f,
			0.578125f,
			0.921875f,
			0.890625f,
			0.984375f,
			0.953125f,
			0.796875f,
			0.765625f,
			0.859375f,
			0.828125f,
			0.335938f,
			0.320312f,
			0.367188f,
			0.351562f,
			0.273438f,
			0.257812f,
			0.304688f,
			0.289062f,
			0.460938f,
			0.445312f,
			0.492188f,
			0.476562f,
			0.398438f,
			0.382812f,
			0.429688f,
			0.414062f,
			0.010498f,
			0.01001f,
			0.011475f,
			0.010986f,
			0.008545f,
			0.008057f,
			0.009521f,
			0.009033f,
			0.014404f,
			0.013916f,
			0.015381f,
			0.014893f,
			0.012451f,
			0.011963f,
			0.013428f,
			0.012939f,
			0.002686f,
			0.002197f,
			0.003662f,
			0.003174f,
			0.000732f,
			0.000244f,
			0.001709f,
			0.001221f,
			0.006592f,
			0.006104f,
			0.007568f,
			0.00708f,
			0.004639f,
			0.00415f,
			0.005615f,
			0.005127f,
			0.041992f,
			0.040039f,
			0.045898f,
			0.043945f,
			0.03418f,
			0.032227f,
			0.038086f,
			0.036133f,
			0.057617f,
			0.055664f,
			0.061523f,
			0.05957f,
			0.049805f,
			0.047852f,
			0.053711f,
			0.051758f,
			0.020996f,
			0.02002f,
			0.022949f,
			0.021973f,
			0.01709f,
			0.016113f,
			0.019043f,
			0.018066f,
			0.028809f,
			0.027832f,
			0.030762f,
			0.029785f,
			0.024902f,
			0.023926f,
			0.026855f,
			0.025879f
		};

		// Token: 0x040005E5 RID: 1509
		private static float[] uLawExpandLookupTable = new float[]
		{
			-0.980347f,
			-0.949097f,
			-0.917847f,
			-0.886597f,
			-0.855347f,
			-0.824097f,
			-0.792847f,
			-0.761597f,
			-0.730347f,
			-0.699097f,
			-0.667847f,
			-0.636597f,
			-0.605347f,
			-0.574097f,
			-0.542847f,
			-0.511597f,
			-0.488159f,
			-0.472534f,
			-0.456909f,
			-0.441284f,
			-0.425659f,
			-0.410034f,
			-0.394409f,
			-0.378784f,
			-0.363159f,
			-0.347534f,
			-0.331909f,
			-0.316284f,
			-0.300659f,
			-0.285034f,
			-0.269409f,
			-0.253784f,
			-0.242065f,
			-0.234253f,
			-0.22644f,
			-0.218628f,
			-0.210815f,
			-0.203003f,
			-0.19519f,
			-0.187378f,
			-0.179565f,
			-0.171753f,
			-0.16394f,
			-0.156128f,
			-0.148315f,
			-0.140503f,
			-0.13269f,
			-0.124878f,
			-0.119019f,
			-0.115112f,
			-0.111206f,
			-0.1073f,
			-0.103394f,
			-0.099487f,
			-0.095581f,
			-0.091675f,
			-0.087769f,
			-0.083862f,
			-0.079956f,
			-0.07605f,
			-0.072144f,
			-0.068237f,
			-0.064331f,
			-0.060425f,
			-0.057495f,
			-0.055542f,
			-0.053589f,
			-0.051636f,
			-0.049683f,
			-0.047729f,
			-0.045776f,
			-0.043823f,
			-0.04187f,
			-0.039917f,
			-0.037964f,
			-0.036011f,
			-0.034058f,
			-0.032104f,
			-0.030151f,
			-0.028198f,
			-0.026733f,
			-0.025757f,
			-0.02478f,
			-0.023804f,
			-0.022827f,
			-0.021851f,
			-0.020874f,
			-0.019897f,
			-0.018921f,
			-0.017944f,
			-0.016968f,
			-0.015991f,
			-0.015015f,
			-0.014038f,
			-0.013062f,
			-0.012085f,
			-0.011353f,
			-0.010864f,
			-0.010376f,
			-0.009888f,
			-0.009399f,
			-0.008911f,
			-0.008423f,
			-0.007935f,
			-0.007446f,
			-0.006958f,
			-0.00647f,
			-0.005981f,
			-0.005493f,
			-0.005005f,
			-0.004517f,
			-0.004028f,
			-0.003662f,
			-0.003418f,
			-0.003174f,
			-0.00293f,
			-0.002686f,
			-0.002441f,
			-0.002197f,
			-0.001953f,
			-0.001709f,
			-0.001465f,
			-0.001221f,
			-0.000977f,
			-0.000732f,
			-0.000488f,
			-0.000244f,
			0f,
			0.980347f,
			0.949097f,
			0.917847f,
			0.886597f,
			0.855347f,
			0.824097f,
			0.792847f,
			0.761597f,
			0.730347f,
			0.699097f,
			0.667847f,
			0.636597f,
			0.605347f,
			0.574097f,
			0.542847f,
			0.511597f,
			0.488159f,
			0.472534f,
			0.456909f,
			0.441284f,
			0.425659f,
			0.410034f,
			0.394409f,
			0.378784f,
			0.363159f,
			0.347534f,
			0.331909f,
			0.316284f,
			0.300659f,
			0.285034f,
			0.269409f,
			0.253784f,
			0.242065f,
			0.234253f,
			0.22644f,
			0.218628f,
			0.210815f,
			0.203003f,
			0.19519f,
			0.187378f,
			0.179565f,
			0.171753f,
			0.16394f,
			0.156128f,
			0.148315f,
			0.140503f,
			0.13269f,
			0.124878f,
			0.119019f,
			0.115112f,
			0.111206f,
			0.1073f,
			0.103394f,
			0.099487f,
			0.095581f,
			0.091675f,
			0.087769f,
			0.083862f,
			0.079956f,
			0.07605f,
			0.072144f,
			0.068237f,
			0.064331f,
			0.060425f,
			0.057495f,
			0.055542f,
			0.053589f,
			0.051636f,
			0.049683f,
			0.047729f,
			0.045776f,
			0.043823f,
			0.04187f,
			0.039917f,
			0.037964f,
			0.036011f,
			0.034058f,
			0.032104f,
			0.030151f,
			0.028198f,
			0.026733f,
			0.025757f,
			0.02478f,
			0.023804f,
			0.022827f,
			0.021851f,
			0.020874f,
			0.019897f,
			0.018921f,
			0.017944f,
			0.016968f,
			0.015991f,
			0.015015f,
			0.014038f,
			0.013062f,
			0.012085f,
			0.011353f,
			0.010864f,
			0.010376f,
			0.009888f,
			0.009399f,
			0.008911f,
			0.008423f,
			0.007935f,
			0.007446f,
			0.006958f,
			0.00647f,
			0.005981f,
			0.005493f,
			0.005005f,
			0.004517f,
			0.004028f,
			0.003662f,
			0.003418f,
			0.003174f,
			0.00293f,
			0.002686f,
			0.002441f,
			0.002197f,
			0.001953f,
			0.001709f,
			0.001465f,
			0.001221f,
			0.000977f,
			0.000732f,
			0.000488f,
			0.000244f,
			0f
		};
	}
}
