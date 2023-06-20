using MP;

namespace MP.Decoder
{
	public class VideoDecoderMJPEG : VideoDecoderUnity
	{
		public VideoDecoderMJPEG(VideoStreamInfo streamInfo) : base(default(VideoStreamInfo))
		{
		}

		public bool interlacedEvenFieldIsLower;
	}
}
