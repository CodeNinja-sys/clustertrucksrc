using System;
using System.IO;
using MP;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class FeatureDemoUI : MonoBehaviour
{
	// Token: 0x17000144 RID: 324
	// (get) Token: 0x0600070B RID: 1803 RVA: 0x0002F12C File Offset: 0x0002D32C
	private MoviePlayerBase moviePlayerBase
	{
		get
		{
			return (this.mode != FeatureDemoUI.Mode.FILE) ? this.movieStreamer : this.moviePlayer;
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0002F14C File Offset: 0x0002D34C
	private void DoLoadWindowForFile()
	{
		GUILayout.Label("Directory", new GUILayoutOption[0]);
		this.fileLoadDir = GUILayout.TextArea(this.fileLoadDir, new GUILayoutOption[]
		{
			GUILayout.MinHeight(36f)
		});
		if (this.lastLoadDir != this.fileLoadDir)
		{
			try
			{
				this.dirsInDir = Directory.GetDirectories(this.fileLoadDir);
				this.filesInDir = Directory.GetFiles(this.fileLoadDir);
				this.loadError = null;
			}
			catch (Exception ex)
			{
				this.loadError = ex.Message;
			}
		}
		this.lastLoadDir = this.fileLoadDir;
		if (!string.IsNullOrEmpty(this.loadError))
		{
			GUILayout.Label(this.loadError, new GUILayoutOption[0]);
		}
		else
		{
			this.loadScrollPos = GUILayout.BeginScrollView(this.loadScrollPos, false, true, new GUILayoutOption[0]);
			GUIStyle guistyle = new GUIStyle(GUI.skin.button);
			guistyle.alignment = TextAnchor.MiddleLeft;
			if (this.dirsInDir != null)
			{
				if (GUILayout.Button("..", guistyle, new GUILayoutOption[0]))
				{
					this.fileLoadDir = Directory.GetParent(this.fileLoadDir).FullName;
				}
				for (int i = 0; i < this.dirsInDir.Length; i++)
				{
					string fullPath = Path.GetFullPath(this.dirsInDir[i]);
					if (GUILayout.Button(Path.GetFileName(fullPath) + "/", guistyle, new GUILayoutOption[0]))
					{
						this.fileLoadDir = fullPath.Replace("\\", "/");
					}
				}
			}
			if (this.filesInDir != null)
			{
				for (int j = 0; j < this.filesInDir.Length; j++)
				{
					if (GUILayout.Button(Path.GetFileName(this.filesInDir[j]), guistyle, new GUILayoutOption[0]))
					{
						this.loadOptions.videoStreamInfo = new VideoStreamInfo();
						this.loadOptions.videoStreamInfo.codecFourCC = 1196444237U;
						((MoviePlayer)this.moviePlayerBase).Load(this.filesInDir[j], this.loadOptions);
					}
				}
			}
			GUILayout.EndScrollView();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			this.loadOptions.skipAudio = GUILayout.Toggle(this.loadOptions.skipAudio, "Skip audio", new GUILayoutOption[]
			{
				GUILayout.Width(120f)
			});
			this.loadOptions.preloadAudio = GUILayout.Toggle(this.loadOptions.preloadAudio, "Preload audio", new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			this.loadOptions.skipVideo = GUILayout.Toggle(this.loadOptions.skipVideo, "Skip video", new GUILayoutOption[]
			{
				GUILayout.Width(120f)
			});
			this.loadOptions._3DSound = GUILayout.Toggle(this.loadOptions._3DSound, "3D sound", new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
		}
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0002F45C File Offset: 0x0002D65C
	private void DoLoadWindowForUrl()
	{
		MovieStreamer movieStreamer = (MovieStreamer)this.moviePlayerBase;
		GUILayout.Label("Steram URL", new GUILayoutOption[0]);
		this.streamLoadUrl = GUILayout.TextArea(this.streamLoadUrl, new GUILayoutOption[]
		{
			GUILayout.MinHeight(36f)
		});
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Connect timeout seconds", new GUILayoutOption[0]);
		this.streamConnectTimeoutStr = GUILayout.TextArea(this.streamConnectTimeoutStr, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		if (!movieStreamer.IsConnected)
		{
			if (GUILayout.Button("Connect to URL and Play", new GUILayoutOption[]
			{
				GUILayout.Height(36f)
			}))
			{
				if (!float.TryParse(this.streamConnectTimeoutStr, out this.loadOptions.connectTimeout))
				{
					this.streamConnectTimeoutStr = "10";
					this.loadOptions.connectTimeout = 10f;
				}
				movieStreamer.Load(this.streamLoadUrl, this.loadOptions);
				movieStreamer.play = true;
			}
		}
		else if (GUILayout.Button("Disconnect", new GUILayoutOption[]
		{
			GUILayout.Height(36f)
		}))
		{
			movieStreamer.Unload();
		}
		GUILayout.Space(10f);
		GUILayout.Label("Status (read only)", new GUILayoutOption[0]);
		GUILayout.TextArea((movieStreamer.status != null) ? movieStreamer.status : string.Empty, new GUILayoutOption[]
		{
			GUILayout.ExpandHeight(true)
		});
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0002F5E0 File Offset: 0x0002D7E0
	private void DoLoadWindow(int windowID)
	{
		if (this.moviePlayerBase is MoviePlayer)
		{
			this.DoLoadWindowForFile();
		}
		else if (this.moviePlayerBase is MovieStreamer)
		{
			this.DoLoadWindowForUrl();
		}
		GUI.DragWindow(new Rect(0f, 0f, 4000f, 4000f));
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0002F63C File Offset: 0x0002D83C
	private void DoStatsWindow(int windowID)
	{
		if (this.moviePlayerBase.movie != null && this.moviePlayerBase.movie.demux != null)
		{
			if (this.moviePlayerBase.movie.videoDecoder != null)
			{
				float num = this.moviePlayerBase.movie.videoDecoder.lastFrameDecodeTime * this.moviePlayerBase.movie.demux.videoStreamInfo.framerate * 100f;
				float num2 = this.moviePlayerBase.movie.videoDecoder.lastFrameDecodeTime * 1000f;
				float num3 = this.moviePlayerBase.movie.videoDecoder.totalDecodeTime * 1000f;
				float num4 = (float)this.moviePlayerBase.movie.videoDecoder.lastFrameSizeBytes;
				this.DrawLabelValue("Thread load", num.ToString("0") + "%", 120);
				this.DrawLabelValue("Frames skipped", this.moviePlayerBase.framesSkipped.ToString(), 120);
				this.DrawLabelValue("Sync events", this.moviePlayerBase.syncEvents.ToString(), 120);
				this.DrawLabelValue("Last frame decode", num2.ToString() + " ms", 120);
				this.DrawLabelValue("Total video decode", num3.ToString() + " ms", 120);
				this.DrawLabelValue("Last frame size", num4.ToString() + " bytes", 120);
			}
			if (this.moviePlayerBase.movie.audioDecoder != null)
			{
				this.DrawLabelValue("Total audio decode", (this.moviePlayerBase.movie.audioDecoder.totalDecodeTime * 1000f).ToString() + " ms", 120);
			}
		}
		else
		{
			GUILayout.Label("No movie loaded", new GUILayoutOption[0]);
		}
		GUI.DragWindow(new Rect(0f, 0f, 4000f, 4000f));
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0002F84C File Offset: 0x0002DA4C
	private void DoControlsWindow(int windowID)
	{
		if (this.moviePlayerBase is MoviePlayer)
		{
			MoviePlayer moviePlayer = (MoviePlayer)this.moviePlayerBase;
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Video time", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			});
			float videoTime;
			if (float.TryParse(GUILayout.TextField(moviePlayer.videoTime.ToString(), new GUILayoutOption[0]), out videoTime))
			{
				moviePlayer.videoTime = videoTime;
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Video frame", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			});
			int videoFrame;
			if (int.TryParse(GUILayout.TextField(moviePlayer.videoFrame.ToString(), new GUILayoutOption[0]), out videoFrame))
			{
				moviePlayer.videoFrame = videoFrame;
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Loop", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			});
			moviePlayer.loop = GUILayout.Toggle(moviePlayer.loop, string.Empty, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			AudioSource component = moviePlayer.GetComponent<AudioSource>();
			if (component != null)
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("Mute audio", new GUILayoutOption[]
				{
					GUILayout.Width(100f)
				});
				component.mute = GUILayout.Toggle(component.mute, string.Empty, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
			}
		}
		else if (this.moviePlayerBase is MovieStreamer)
		{
			GUILayout.Label("Not much here (use Load window to connect to http streams)", new GUILayoutOption[0]);
		}
		this.moviePlayerBase.drawToScreen = GUILayout.Toggle(this.moviePlayerBase.drawToScreen, " draw directly to screen", new GUILayoutOption[0]);
		if (this.moviePlayerBase.drawToScreen)
		{
			this.moviePlayerBase.screenMode = (MoviePlayerBase.ScreenMode)GUILayout.SelectionGrid((int)this.moviePlayerBase.screenMode, Enum.GetNames(typeof(MoviePlayerBase.ScreenMode)), 4, new GUILayoutOption[0]);
		}
		GUI.DragWindow(new Rect(0f, 0f, 4000f, 4000f));
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0002FA70 File Offset: 0x0002DC70
	private void DrawLabelValue(string label, string value, int labelWidth = 100)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label(label, new GUILayoutOption[]
		{
			GUILayout.Width((float)labelWidth)
		});
		GUILayout.Label(value, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
	private void DoInfoWindow(int windowID)
	{
		if (this.moviePlayerBase.movie != null)
		{
			Demux demux = this.moviePlayerBase.movie.demux;
			this.DrawLabelValue("Demux", (demux == null) ? "N/A" : demux.GetType().ToString(), 100);
			if (demux != null)
			{
				if (demux.hasVideo)
				{
					VideoStreamInfo videoStreamInfo = this.moviePlayerBase.movie.demux.videoStreamInfo;
					VideoDecoder videoDecoder = this.moviePlayerBase.movie.videoDecoder;
					this.DrawLabelValue("Video fourCC", RiffParser.FromFourCC(videoStreamInfo.codecFourCC), 100);
					this.DrawLabelValue("Video decoder", (videoDecoder == null) ? "N/A" : videoDecoder.GetType().ToString(), 100);
					this.DrawLabelValue("bitsPerPixel", videoStreamInfo.bitsPerPixel.ToString(), 100);
					this.DrawLabelValue("frameCount", videoStreamInfo.frameCount.ToString(), 100);
					this.DrawLabelValue("frame size", videoStreamInfo.width + "x" + videoStreamInfo.height, 100);
					this.DrawLabelValue("framerate", videoStreamInfo.framerate.ToString(), 100);
					this.DrawLabelValue("lengthBytes", videoStreamInfo.lengthBytes.ToString(), 100);
					this.DrawLabelValue("lengthSeconds", videoStreamInfo.lengthSeconds.ToString(), 100);
				}
				else
				{
					GUILayout.Label("No video stream found", new GUILayoutOption[0]);
				}
				if (demux.hasAudio)
				{
					AudioStreamInfo audioStreamInfo = this.moviePlayerBase.movie.demux.audioStreamInfo;
					AudioDecoder audioDecoder = this.moviePlayerBase.movie.audioDecoder;
					this.DrawLabelValue("Audio fourCC", RiffParser.FromFourCC(audioStreamInfo.codecFourCC), 100);
					this.DrawLabelValue("Audio decoder", (audioDecoder == null) ? "N/A" : audioDecoder.GetType().ToString(), 100);
					this.DrawLabelValue("audioFormat", audioStreamInfo.audioFormat.ToString("X"), 100);
					this.DrawLabelValue("channels", audioStreamInfo.channels.ToString(), 100);
					this.DrawLabelValue("sampleCount", audioStreamInfo.sampleCount.ToString(), 100);
					this.DrawLabelValue("sampleSize", audioStreamInfo.sampleSize.ToString(), 100);
					this.DrawLabelValue("sampleRate", audioStreamInfo.sampleRate.ToString(), 100);
					this.DrawLabelValue("lengthBytes", audioStreamInfo.lengthBytes.ToString(), 100);
					this.DrawLabelValue("lengthSeconds", audioStreamInfo.lengthSeconds.ToString(), 100);
				}
				else
				{
					GUILayout.Label("No audio stream found", new GUILayoutOption[0]);
				}
			}
		}
		else
		{
			GUILayout.Label("No movie loaded", new GUILayoutOption[0]);
		}
		GUI.DragWindow(new Rect(0f, 0f, 4000f, 4000f));
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0002FDAC File Offset: 0x0002DFAC
	private void DoPlaybackControls()
	{
		if (GUI.Button(new Rect(0f, (float)(Screen.height - 40), 40f, 40f), (!this.moviePlayerBase.play) ? ">" : "||"))
		{
			this.moviePlayerBase.play = !this.moviePlayerBase.play;
		}
		if (this.moviePlayerBase is MoviePlayer)
		{
			MoviePlayer moviePlayer = (MoviePlayer)this.moviePlayerBase;
			if (moviePlayer.movie != null && moviePlayer.movie.demux != null && moviePlayer.movie.demux.hasVideo)
			{
				this.slider = (float)moviePlayer.videoFrame;
				this.slider = GUI.HorizontalSlider(new Rect(50f, (float)(Screen.height - 20), (float)(Screen.width - 60), 20f), this.slider, 0f, (float)moviePlayer.movie.demux.videoStreamInfo.frameCount);
				moviePlayer.videoFrame = (int)this.slider;
			}
		}
		else
		{
			MovieStreamer movieStreamer = (MovieStreamer)this.moviePlayerBase;
			GUI.Label(new Rect(50f, (float)(Screen.height - 40), 200f, 20f), movieStreamer.bytesReceived + " bytes received");
			GUI.Label(new Rect(50f, (float)(Screen.height - 20), (float)(Screen.width - 60), 20f), (movieStreamer.status != null) ? movieStreamer.status : string.Empty);
		}
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0002FF54 File Offset: 0x0002E154
	private void OnGUI()
	{
		if (this.hideAllUI)
		{
			return;
		}
		if (this.showLoadWindow)
		{
			this.loadWindowRect = GUI.Window(0, this.loadWindowRect, new GUI.WindowFunction(this.DoLoadWindow), "Load");
		}
		if (this.showStatsWindow)
		{
			this.statsWindowRect = GUI.Window(1, this.statsWindowRect, new GUI.WindowFunction(this.DoStatsWindow), "Stats");
		}
		if (this.showControlsWindow)
		{
			this.controlsWindowRect = GUI.Window(2, this.controlsWindowRect, new GUI.WindowFunction(this.DoControlsWindow), "Controls");
		}
		if (this.showInfoWindow)
		{
			this.infoWindowRect = GUI.Window(3, this.infoWindowRect, new GUI.WindowFunction(this.DoInfoWindow), "Info");
		}
		this.DoPlaybackControls();
		GUI.Label(new Rect((float)(Screen.width - 150), 10f, 200f, 70f), "MoviePlayer v0.12\nfeature demo\nby SHUU Games 2015");
		this.mode = (FeatureDemoUI.Mode)GUI.SelectionGrid(new Rect((float)(Screen.width - 150), 66f, 138f, 22f), (int)this.mode, new string[]
		{
			"FILE",
			"STREAM"
		}, 2);
		GUI.Label(new Rect((float)(Screen.width - 150), (float)(Screen.height - 105), 200f, 105f), "L - Load window\nS - Stats window\nC - Controls window\nI - Info window\nH - show/hide all UI");
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x000300CC File Offset: 0x0002E2CC
	private void Update()
	{
		if (this.mode != this.lastMode)
		{
			this.moviePlayer.gameObject.SetActive(this.mode == FeatureDemoUI.Mode.FILE);
			this.movieStreamer.gameObject.SetActive(this.mode == FeatureDemoUI.Mode.STREAM);
			this.lastMode = this.mode;
		}
		if (Input.GetKeyUp(KeyCode.L))
		{
			this.showLoadWindow = !this.showLoadWindow;
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			this.showStatsWindow = !this.showStatsWindow;
		}
		if (Input.GetKeyUp(KeyCode.C))
		{
			this.showControlsWindow = !this.showControlsWindow;
		}
		if (Input.GetKeyUp(KeyCode.I))
		{
			this.showInfoWindow = !this.showInfoWindow;
		}
		if (Input.GetKeyUp(KeyCode.H))
		{
			this.hideAllUI = !this.hideAllUI;
		}
		if (this.hideAllUI && Input.GetMouseButtonDown(0))
		{
			this.hideAllUI = false;
		}
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	// Token: 0x04000530 RID: 1328
	public FeatureDemoUI.Mode mode;

	// Token: 0x04000531 RID: 1329
	public MoviePlayer moviePlayer;

	// Token: 0x04000532 RID: 1330
	public MovieStreamer movieStreamer;

	// Token: 0x04000533 RID: 1331
	public string fileLoadDir = "C:/";

	// Token: 0x04000534 RID: 1332
	public string streamLoadUrl = "http://";

	// Token: 0x04000535 RID: 1333
	private float slider;

	// Token: 0x04000536 RID: 1334
	private Rect loadWindowRect = new Rect(20f, 20f, 240f, 500f);

	// Token: 0x04000537 RID: 1335
	private Rect infoWindowRect = new Rect(270f, 20f, 240f, 500f);

	// Token: 0x04000538 RID: 1336
	private Rect statsWindowRect = new Rect(520f, 20f, 240f, 170f);

	// Token: 0x04000539 RID: 1337
	private Rect controlsWindowRect = new Rect(520f, 200f, 240f, 170f);

	// Token: 0x0400053A RID: 1338
	private FeatureDemoUI.Mode lastMode;

	// Token: 0x0400053B RID: 1339
	private Vector2 loadScrollPos = Vector2.zero;

	// Token: 0x0400053C RID: 1340
	private string lastLoadDir = string.Empty;

	// Token: 0x0400053D RID: 1341
	private string loadError;

	// Token: 0x0400053E RID: 1342
	private string streamConnectTimeoutStr = "10";

	// Token: 0x0400053F RID: 1343
	private string[] dirsInDir;

	// Token: 0x04000540 RID: 1344
	private string[] filesInDir;

	// Token: 0x04000541 RID: 1345
	private LoadOptions loadOptions = LoadOptions.Default;

	// Token: 0x04000542 RID: 1346
	private bool hideAllUI;

	// Token: 0x04000543 RID: 1347
	public bool showLoadWindow;

	// Token: 0x04000544 RID: 1348
	public bool showStatsWindow;

	// Token: 0x04000545 RID: 1349
	public bool showControlsWindow;

	// Token: 0x04000546 RID: 1350
	public bool showInfoWindow;

	// Token: 0x02000143 RID: 323
	public enum Mode
	{
		// Token: 0x04000548 RID: 1352
		FILE,
		// Token: 0x04000549 RID: 1353
		STREAM
	}
}
