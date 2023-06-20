using System;

namespace MP
{
	[Serializable]
	public class LoadOptions
	{
		public bool _3DSound;
		public bool preloadAudio;
		public bool preloadVideo;
		public bool skipVideo;
		public bool skipAudio;
		public float connectTimeout;
		public bool enableExceptionThrow;
	}
}
