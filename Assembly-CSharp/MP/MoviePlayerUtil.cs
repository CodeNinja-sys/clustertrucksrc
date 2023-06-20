using System;
using System.IO;
using UnityEngine;

namespace MP
{
	// Token: 0x02000160 RID: 352
	public class MoviePlayerUtil
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x000344D0 File Offset: 0x000326D0
		public static Movie Load(Stream srcStream, out Texture2D targetFramebuffer, LoadOptions loadOptions = null)
		{
			AudioClip audioClip;
			return MoviePlayerUtil.Load(new MovieSource
			{
				stream = srcStream
			}, out targetFramebuffer, out audioClip, loadOptions);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000344F4 File Offset: 0x000326F4
		public static Movie Load(Stream srcStream, out Texture2D targetFramebuffer, out AudioClip targetAudioBuffer, LoadOptions loadOptions = null)
		{
			return MoviePlayerUtil.Load(new MovieSource
			{
				stream = srcStream
			}, out targetFramebuffer, out targetAudioBuffer, loadOptions);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00034518 File Offset: 0x00032718
		public static Movie Load(string srcUrl, out Texture2D targetFramebuffer, LoadOptions loadOptions = null)
		{
			AudioClip audioClip;
			return MoviePlayerUtil.Load(new MovieSource
			{
				url = srcUrl
			}, out targetFramebuffer, out audioClip, loadOptions);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0003453C File Offset: 0x0003273C
		public static Movie Load(string srcUrl, out Texture2D targetFramebuffer, out AudioClip targetAudioBuffer, LoadOptions loadOptions = null)
		{
			return MoviePlayerUtil.Load(new MovieSource
			{
				url = srcUrl
			}, out targetFramebuffer, out targetAudioBuffer, loadOptions);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00034560 File Offset: 0x00032760
		public static Movie Load(MovieSource source, out Texture2D targetFramebuffer, out AudioClip targetAudioBuffer, LoadOptions loadOptions = null)
		{
			if (loadOptions == null)
			{
				loadOptions = LoadOptions.Default;
			}
			if (source.stream == null && source.url == null)
			{
				throw new MpException("Either source.stream or source.url must be provided");
			}
			targetFramebuffer = null;
			targetAudioBuffer = null;
			Movie movie = new Movie();
			movie.sourceStream = source.stream;
			if (source.url != null)
			{
				movie.demux = ((loadOptions.demuxOverride == null) ? Streamer.forUrl(source.url) : loadOptions.demuxOverride);
				((Streamer)movie.demux).Connect(source.url, loadOptions);
			}
			else
			{
				movie.demux = ((loadOptions.demuxOverride == null) ? Demux.forSource(source.stream) : loadOptions.demuxOverride);
				movie.demux.Init(source.stream, loadOptions);
			}
			if (movie.demux.hasVideo && !loadOptions.skipVideo)
			{
				VideoStreamInfo videoStreamInfo = movie.demux.videoStreamInfo;
				movie.videoDecoder = VideoDecoder.CreateFor(videoStreamInfo);
				movie.videoDecoder.Init(out targetFramebuffer, movie.demux, loadOptions);
				if (loadOptions.preloadVideo)
				{
					movie.frameUV = MoviePlayerUtil.UnpackFramesToAtlas(movie.videoDecoder, ref targetFramebuffer, videoStreamInfo.frameCount);
				}
				else
				{
					movie.frameUV = new Rect[]
					{
						new Rect(0f, 0f, 1f, 1f)
					};
				}
			}
			if (movie.demux.hasAudio && !loadOptions.skipAudio)
			{
				movie.audioDecoder = AudioDecoder.CreateFor(movie.demux.audioStreamInfo);
				movie.audioDecoder.Init(out targetAudioBuffer, movie.demux, loadOptions);
			}
			return movie;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00034720 File Offset: 0x00032920
		public static void Unload(Movie movie)
		{
			if (movie != null)
			{
				if (movie.sourceStream != null)
				{
					movie.sourceStream.Dispose();
					movie.sourceStream = null;
				}
				if (movie.videoDecoder != null)
				{
					movie.videoDecoder.Shutdown();
					movie.videoDecoder = null;
				}
				if (movie.audioDecoder != null)
				{
					movie.audioDecoder.Shutdown();
					movie.audioDecoder = null;
				}
				if (movie.demux != null)
				{
					movie.demux.Shutdown(false);
					movie.demux = null;
				}
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000347A8 File Offset: 0x000329A8
		private static Rect[] UnpackFramesToAtlas(VideoDecoder videoDecoder, ref Texture2D framebuffer, int frameCount)
		{
			if (frameCount < 1)
			{
				throw new MpException("Expecting at least 1 video frame");
			}
			int num = 8192;
			videoDecoder.Position = 0;
			videoDecoder.DecodeNext();
			int width = framebuffer.width;
			int height = framebuffer.height;
			int num2 = num / width;
			int num3 = num / height;
			if (frameCount > num2 * num3)
			{
				throw new MpException(string.Concat(new object[]
				{
					frameCount,
					" ",
					width,
					"x",
					height,
					" video frames can't fit into ",
					num,
					"x",
					num,
					" atlas texture. Consider lowering frame count or resolution, or disable video preloading"
				}));
			}
			Texture2D[] array = new Texture2D[frameCount];
			array[0] = MoviePlayerUtil.CloneTexture(framebuffer);
			for (int i = 1; i < frameCount; i++)
			{
				videoDecoder.DecodeNext();
				array[i] = MoviePlayerUtil.CloneTexture(framebuffer);
			}
			Rect[] result = framebuffer.PackTextures(array, 0, num);
			for (int j = 0; j < frameCount; j++)
			{
				UnityEngine.Object.Destroy(array[j]);
			}
			return result;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000348D0 File Offset: 0x00032AD0
		private static Texture2D CloneTexture(Texture2D srcTex)
		{
			Texture2D texture2D = new Texture2D(srcTex.width, srcTex.height, srcTex.format, srcTex.mipmapCount > 1);
			texture2D.SetPixels32(srcTex.GetPixels32());
			return texture2D;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0003490C File Offset: 0x00032B0C
		public static byte[] ExtractRawAudio(Stream sourceStream)
		{
			Demux demux;
			return MoviePlayerUtil.ExtractRawAudio(sourceStream, out demux);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00034924 File Offset: 0x00032B24
		public static byte[] ExtractRawAudio(Stream sourceStream, out Demux demux)
		{
			demux = Demux.forSource(sourceStream);
			demux.Init(sourceStream, null);
			if (!demux.hasAudio)
			{
				return null;
			}
			byte[] result = new byte[demux.audioStreamInfo.lengthBytes];
			demux.ReadAudioSamples(out result, demux.audioStreamInfo.sampleCount);
			return result;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0003497C File Offset: 0x00032B7C
		public static byte[] ExtractRawVideo(Stream sourceStream)
		{
			Demux demux;
			return MoviePlayerUtil.ExtractRawVideo(sourceStream, out demux);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00034994 File Offset: 0x00032B94
		public static byte[] ExtractRawVideo(Stream sourceStream, out Demux demux)
		{
			demux = Demux.forSource(sourceStream);
			demux.Init(sourceStream, null);
			if (!demux.hasVideo)
			{
				return null;
			}
			byte[] array = new byte[demux.videoStreamInfo.lengthBytes];
			int num = 0;
			int num2;
			do
			{
				byte[] sourceArray;
				num2 = demux.ReadVideoFrame(out sourceArray);
				Array.Copy(sourceArray, 0, array, num, num2);
				num += num2;
			}
			while (num2 > 0);
			return array;
		}

		// Token: 0x0400062E RID: 1582
		private const int MAX_DESKTOP_ATLAS_WH = 8192;

		// Token: 0x0400062F RID: 1583
		private const int MAX_MOBILE_ATLAS_WH = 2048;
	}
}
