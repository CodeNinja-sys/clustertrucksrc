using UnityEngine;

public class FeatureDemoUI : MonoBehaviour
{
	public enum Mode
	{
		FILE = 0,
		STREAM = 1,
	}

	public Mode mode;
	public MoviePlayer moviePlayer;
	public MovieStreamer movieStreamer;
	public string fileLoadDir;
	public string streamLoadUrl;
	public bool showLoadWindow;
	public bool showStatsWindow;
	public bool showControlsWindow;
	public bool showInfoWindow;
}
