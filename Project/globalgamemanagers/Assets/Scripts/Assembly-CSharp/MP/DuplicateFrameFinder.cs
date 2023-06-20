using UnityEngine;

namespace MP
{
	public class DuplicateFrameFinder
	{
		public class Options
		{
			public float maxImageDiff;
			public int maxPixelDiff;
			public int maxLookbackFrames;
			public int toneCompareDistrust;
			public int pixelCacheSize;
			public bool otherStreamsAvailable;
		}

		public DuplicateFrameFinder(VideoDecoder videoDecoder, Texture2D framebuffer, int frameOffset, int frameCount, DuplicateFrameFinder.Options options)
		{
		}

	}
}
