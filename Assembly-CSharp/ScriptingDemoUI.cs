using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using MP;
using MP.AVI;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class ScriptingDemoUI : MonoBehaviour
{
	// Token: 0x06000717 RID: 1815 RVA: 0x00030220 File Offset: 0x0002E420
	[ContextMenu("Load file and play forever (play mode)")]
	private void LoadFileAndPlayForever()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		MoviePlayer moviePlayer = base.GetComponent<MoviePlayer>();
		if (moviePlayer == null)
		{
			moviePlayer = base.gameObject.AddComponent<MoviePlayer>();
		}
		moviePlayer.Load(this.infile);
		moviePlayer.drawToScreen = true;
		moviePlayer.play = true;
		moviePlayer.loop = true;
		moviePlayer.OnLoop += delegate(MoviePlayerBase mp)
		{
			Debug.Log("Loop it");
		};
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0003029C File Offset: 0x0002E49C
	[ContextMenu("Connect and play MJPEG HTTP stream")]
	private void ConnectAndPlayAStream()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		MovieStreamer movieStreamer = base.GetComponent<MovieStreamer>();
		if (movieStreamer == null)
		{
			movieStreamer = base.gameObject.AddComponent<MovieStreamer>();
		}
		movieStreamer.Load(this.httpStreamUrl);
		movieStreamer.drawToScreen = true;
		movieStreamer.play = true;
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x000302F0 File Offset: 0x0002E4F0
	[ContextMenu("Stop and unload (play mode)")]
	private void StopAndUnload()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		MoviePlayer component = base.GetComponent<MoviePlayer>();
		if (component != null)
		{
			component.play = false;
			component.Unload();
		}
		MovieStreamer component2 = base.GetComponent<MovieStreamer>();
		if (component2 != null)
		{
			component2.Unload();
		}
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00030344 File Offset: 0x0002E544
	[ContextMenu("Extract one frame")]
	private void ExtractOneFrame()
	{
		Texture2D texture2D;
		Movie movie = MoviePlayerUtil.Load(File.OpenRead(this.infile), out texture2D, null);
		if (movie.demux.hasVideo)
		{
			movie.videoDecoder.Decode(this.frame);
		}
		File.WriteAllBytes(this.frameOutFile, texture2D.EncodeToPNG());
		Debug.Log("Extracted frame " + this.frame);
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x000303B4 File Offset: 0x0002E5B4
	[ContextMenu("Drop half the frames remux")]
	private void DropHalfTheFramesRemux()
	{
		this.RunInBackgroundOrNot(delegate
		{
			Stream sourceStream = File.OpenRead(this.infile);
			Demux demux = Demux.forSource(sourceStream);
			demux.Init(sourceStream, null);
			Stream dstStream = File.OpenWrite(this.outfile);
			Remux remux = new AviRemux(32, 2000000000);
			VideoStreamInfo videoStreamInfo = new VideoStreamInfo(demux.videoStreamInfo);
			videoStreamInfo.framerate /= 2f;
			remux.Init(dstStream, videoStreamInfo, demux.audioStreamInfo);
			int num;
			do
			{
				byte[] frameBytes;
				num = demux.ReadVideoFrame(out frameBytes);
				if (num > 0)
				{
					int sampleCount = (int)((float)demux.audioStreamInfo.sampleRate / demux.videoStreamInfo.framerate);
					byte[] sampleBytes;
					int size = demux.ReadAudioSamples(out sampleBytes, sampleCount);
					if (demux.VideoPosition % 2 == 1)
					{
						remux.WriteNextVideoFrame(frameBytes, num);
					}
					remux.WriteNextAudioSamples(sampleBytes, size);
				}
			}
			while (num > 0);
			remux.Shutdown();
			demux.Shutdown(false);
		});
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x000303C8 File Offset: 0x0002E5C8
	[ContextMenu("Instant movie switching")]
	private void InstantMovieSwitching()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		base.StartCoroutine(this.InstantMovieSwitchingCoroutine());
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x000303E4 File Offset: 0x0002E5E4
	private IEnumerator InstantMovieSwitchingCoroutine()
	{
		this.LoadFileAndPlayForever();
		MoviePlayer moviePlayer = base.GetComponent<MoviePlayer>();
		for (;;)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 2f));
			moviePlayer.Load(this.infile);
		}
		yield break;
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00030400 File Offset: 0x0002E600
	[ContextMenu("Capture webcam to file")]
	private void CaptureWebcamToFile()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		base.StartCoroutine(this.CaptureWebcamToFileCoroutine());
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0003041C File Offset: 0x0002E61C
	private IEnumerator CaptureWebcamToFileCoroutine()
	{
		string webcamStreamUrl = "webcam://";
		Streamer streamer = Streamer.forUrl(webcamStreamUrl);
		streamer.Connect(webcamStreamUrl, null);
		VideoStreamInfo vi = new VideoStreamInfo(streamer.videoStreamInfo);
		vi.framerate = 15f;
		AviRemux remux = new AviRemux(32, 2000000000);
		remux.Init(File.OpenWrite(this.outfile), vi, null);
		float captureStartTime = Time.realtimeSinceStartup;
		int lastRealFrameNr = -1;
		int realFrameNr;
		do
		{
			this.frame = streamer.VideoPosition;
			byte[] buf;
			int bytesCnt = streamer.ReadVideoFrame(out buf);
			realFrameNr = Mathf.RoundToInt((Time.realtimeSinceStartup - captureStartTime) * vi.framerate);
			if (realFrameNr - lastRealFrameNr > 1)
			{
				Debug.LogWarning("Output framerate too high, possibly just skipped " + (realFrameNr - lastRealFrameNr) + " frames");
			}
			while (lastRealFrameNr < realFrameNr)
			{
				remux.WriteNextVideoFrame(buf, bytesCnt);
				lastRealFrameNr++;
			}
			yield return 1;
		}
		while (realFrameNr < 150);
		remux.Shutdown();
		streamer.Shutdown(false);
		Debug.Log("Done capturing");
		yield break;
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00030438 File Offset: 0x0002E638
	[ContextMenu("Extract raw video")]
	private void ExtractRawVideo()
	{
		this.RunInBackgroundOrNot(delegate
		{
			File.WriteAllBytes(this.outfile, MoviePlayerUtil.ExtractRawVideo(File.OpenRead(this.infile)));
		});
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0003044C File Offset: 0x0002E64C
	[ContextMenu("Extract raw audio")]
	private void ExtractRawAudio()
	{
		this.RunInBackgroundOrNot(delegate
		{
			File.WriteAllBytes(this.outfile, MoviePlayerUtil.ExtractRawAudio(File.OpenRead(this.infile)));
		});
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00030460 File Offset: 0x0002E660
	private void OnGUI()
	{
		GUI.depth = -1;
		GUI.Label(new Rect(10f, 3f, (float)(Screen.width - 220), 70f), "Open ScriptingDemoUI.cs an see what these methods do, then try them out. Most of these don't need PLAY mode");
		int num = 25;
		GUILayout.BeginArea(new Rect(10f, 40f, (float)(Screen.width - 220), 200f));
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (GUILayout.Button("LoadFileAndPlayForever", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.LoadFileAndPlayForever();
		}
		if (GUILayout.Button("ConnectAndPlayAStream", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.ConnectAndPlayAStream();
		}
		if (GUILayout.Button("StopAndUnload", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.StopAndUnload();
		}
		if (GUILayout.Button("ExtractOneFrame", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.ExtractOneFrame();
		}
		if (GUILayout.Button("DropHalfTheFramesRemux", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.DropHalfTheFramesRemux();
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (GUILayout.Button("InstantMovieSwitching", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.InstantMovieSwitching();
		}
		if (GUILayout.Button("ExtractRawVideo", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.ExtractRawVideo();
		}
		if (GUILayout.Button("ExtractRawAudio", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.ExtractRawVideo();
		}
		if (GUILayout.Button("CaptureWebcamToFile", new GUILayoutOption[]
		{
			GUILayout.Height((float)num)
		}))
		{
			this.CaptureWebcamToFile();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		GUI.Label(new Rect((float)(Screen.width - 170), 10f, 200f, 70f), "MoviePlayer v0.12\nscripting demo\nby SHUU Games 2014");
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00030664 File Offset: 0x0002E864
	private void RunInBackgroundOrNot(ScriptingDemoUI.Action action)
	{
		if (this.runInSeparateThread)
		{
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
			{
				action();
			};
			backgroundWorker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				Debug.Log("Background thread done");
			};
			backgroundWorker.RunWorkerAsync();
		}
		else
		{
			action();
			Debug.Log("Done");
		}
	}

	// Token: 0x0400054A RID: 1354
	public string infile = "Assets/MoviePlayer/Sample movies/Sintel (480p mjpeg, 2ch24khz alaw).avi.bytes";

	// Token: 0x0400054B RID: 1355
	public string outfile = "out.bytes";

	// Token: 0x0400054C RID: 1356
	public string frameOutFile = "out.png";

	// Token: 0x0400054D RID: 1357
	public int frame = 150;

	// Token: 0x0400054E RID: 1358
	public string httpStreamUrl = "http://194.126.108.66:8883/1396114247132";

	// Token: 0x0400054F RID: 1359
	public bool runInSeparateThread;

	// Token: 0x02000315 RID: 789
	// (Invoke) Token: 0x06001262 RID: 4706
	private delegate void Action();
}
