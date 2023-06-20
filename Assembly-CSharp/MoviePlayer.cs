using System;
using System.Collections;
using System.IO;
using MP;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class MoviePlayer : MoviePlayerBase
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x060006D7 RID: 1751 RVA: 0x0002DDF0 File Offset: 0x0002BFF0
	// (remove) Token: 0x060006D8 RID: 1752 RVA: 0x0002DE0C File Offset: 0x0002C00C
	public event MoviePlayerBase.MovieEvent OnLoop;

	// Token: 0x060006D9 RID: 1753 RVA: 0x0002DE28 File Offset: 0x0002C028
	public bool Load(byte[] bytes)
	{
		return this.Load(bytes, null);
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0002DE34 File Offset: 0x0002C034
	public bool Load(byte[] bytes, LoadOptions loadOptions)
	{
		bool result;
		try
		{
			this.Load(new MovieSource
			{
				stream = new MemoryStream(bytes)
			}, loadOptions);
			result = true;
		}
		catch (Exception ex)
		{
			if (this.ShouldRethrow(ex, loadOptions))
			{
				throw ex;
			}
			result = false;
		}
		return result;
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0002DEA0 File Offset: 0x0002C0A0
	public bool Load(TextAsset textAsset)
	{
		this.source = textAsset;
		return this.Load(textAsset, null);
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0002DEB4 File Offset: 0x0002C0B4
	public bool Load(TextAsset textAsset, LoadOptions loadOptions)
	{
		bool result;
		try
		{
			this.source = textAsset;
			this.Load(new MovieSource
			{
				stream = new MemoryStream(textAsset.bytes)
			}, loadOptions);
			result = true;
		}
		catch (Exception ex)
		{
			if (this.ShouldRethrow(ex, loadOptions))
			{
				throw ex;
			}
			result = false;
		}
		return result;
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0002DF2C File Offset: 0x0002C12C
	public bool Load(Stream srcStream)
	{
		bool result;
		try
		{
			this.Load(new MovieSource
			{
				stream = srcStream
			}, null);
			result = true;
		}
		catch (Exception ex)
		{
			if (this.ShouldRethrow(ex, this.loadOptions))
			{
				throw ex;
			}
			result = false;
		}
		return result;
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0002DF98 File Offset: 0x0002C198
	public void LoadFromWwwAsync(string url, Action<MoviePlayer> doneCallback, Action<MoviePlayer, Exception> failCallback)
	{
		base.StartCoroutine(this.LoadFromWwwAsyncCoroutine(url, doneCallback, failCallback, null));
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0002DFAC File Offset: 0x0002C1AC
	public void LoadFromWwwAsync(string url, Action<MoviePlayer> doneCallback, Action<MoviePlayer, Exception> failCallback, LoadOptions loadOptions)
	{
		base.StartCoroutine(this.LoadFromWwwAsyncCoroutine(url, doneCallback, failCallback, loadOptions));
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0002DFC0 File Offset: 0x0002C1C0
	public void LoadFromResourceAsync(string url, Action<MoviePlayer> doneCallback, Action<MoviePlayer, Exception> failCallback)
	{
		base.StartCoroutine(this.LoadFromResourceAsyncCoroutine(url, doneCallback, failCallback, null));
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
	public void LoadFromResourceAsync(string url, Action<MoviePlayer> doneCallback, Action<MoviePlayer, Exception> failCallback, LoadOptions loadOptions)
	{
		base.StartCoroutine(this.LoadFromResourceAsyncCoroutine(url, doneCallback, failCallback, loadOptions));
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0002DFE8 File Offset: 0x0002C1E8
	public bool Load(string path)
	{
		return this.Load(path, null);
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0002DFF4 File Offset: 0x0002C1F4
	public bool Load(string path, LoadOptions loadOptions)
	{
		bool result;
		try
		{
			this.Load(new MovieSource
			{
				stream = File.OpenRead(path)
			}, loadOptions);
			result = true;
		}
		catch (Exception ex)
		{
			if (this.ShouldRethrow(ex, loadOptions))
			{
				throw ex;
			}
			result = false;
		}
		return result;
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0002E060 File Offset: 0x0002C260
	[ContextMenu("Reload")]
	public bool Reload()
	{
		bool result = true;
		if (this.source != null)
		{
			result = this.Load(this.source.bytes, this.loadOptions);
			this.lastVideoFrame = -1;
		}
		return result;
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x0002E0A0 File Offset: 0x0002C2A0
	private void Start()
	{
		this.Reload();
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0002E0AC File Offset: 0x0002C2AC
	private bool ShouldRethrow(Exception e, LoadOptions loadOptions)
	{
		if (loadOptions != null && loadOptions.enableExceptionThrow)
		{
			return true;
		}
		Debug.LogError(e);
		return false;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0002E0C8 File Offset: 0x0002C2C8
	protected override void Load(MovieSource source, LoadOptions loadOptions = null)
	{
		if (loadOptions == null)
		{
			loadOptions = this.loadOptions;
		}
		else
		{
			this.loadOptions = loadOptions;
		}
		bool flag = this.audioSource != null && !loadOptions.skipAudio;
		if (flag)
		{
			loadOptions.skipAudio = true;
		}
		if (flag)
		{
			this.audiobuffer = this.audioSource;
		}
		base.Load(source, loadOptions);
		if (!loadOptions.preloadVideo && this.movie.videoDecoder != null)
		{
			this.movie.videoDecoder.Decode(this.videoFrame);
		}
		this.UpdateRendererUVRect();
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0002E16C File Offset: 0x0002C36C
	private IEnumerator LoadFromWwwAsyncCoroutine(string url, Action<MoviePlayer> doneCallback, Action<MoviePlayer, Exception> failCallback, LoadOptions loadOptions)
	{
		WWW www = new WWW(url);
		double startTime = (double)Time.realtimeSinceStartup;
		while (startTime + (double)loadOptions.connectTimeout > (double)Time.realtimeSinceStartup)
		{
			yield return 1;
		}
		Exception exception = null;
		try
		{
			if (!www.isDone && startTime + (double)loadOptions.connectTimeout > (double)Time.realtimeSinceStartup)
			{
				throw new TimeoutException(string.Concat(new object[]
				{
					"Timeout ",
					loadOptions.connectTimeout,
					" seconds happens while loading \"",
					url,
					"\""
				}));
			}
			if (!string.IsNullOrEmpty(www.error))
			{
				throw new MpException(string.Concat(new string[]
				{
					"WWW error \"",
					www.error,
					"\" while loading \"",
					url,
					"\""
				}));
			}
			this.Load(www.bytes, loadOptions);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			exception = e;
		}
		if (exception == null || failCallback == null)
		{
			doneCallback(this);
		}
		else
		{
			failCallback(this, exception);
		}
		yield break;
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0002E1C4 File Offset: 0x0002C3C4
	private IEnumerator LoadFromResourceAsyncCoroutine(string path, Action<MoviePlayer> doneCallback, Action<MoviePlayer, Exception> failCallback, LoadOptions loadOptions)
	{
		string resourceName = (!path.EndsWith(".bytes")) ? path : path.Remove(path.Length - 6);
		ResourceRequest resourceRequest = Resources.LoadAsync(resourceName);
		while (!resourceRequest.isDone)
		{
			yield return 1;
		}
		Exception exception = null;
		try
		{
			if (resourceRequest.asset == null || resourceRequest.asset.GetType() != typeof(TextAsset))
			{
				throw new MpException("Resources.LoadAsync couldn't load \"" + resourceName + "\" as TextAsset");
			}
			this.Load(resourceRequest.asset as TextAsset, loadOptions);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			exception = e;
		}
		if (exception == null || failCallback == null)
		{
			doneCallback(this);
		}
		else
		{
			failCallback(this, exception);
		}
		yield break;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0002E21C File Offset: 0x0002C41C
	private void OnGUI()
	{
		if (this.movie == null || this.movie.demux == null || this.movie.demux.videoStreamInfo == null)
		{
			return;
		}
		if (this.drawToScreen && this.framebuffer != null)
		{
			Rect value = this.movie.frameUV[this.videoFrame % this.movie.frameUV.Length];
			base.DrawFramebufferToScreen(new Rect?(value));
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0002E2AC File Offset: 0x0002C4AC
	private void Update()
	{
		base.HandlePlayStop();
		bool wasSeeked = this.HandlePlayheadMove();
		this.HandleFrameDecode(wasSeeked);
		if (this.play)
		{
			this.HandleAudioSync();
			this.HandleLoop();
		}
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0002E2E4 File Offset: 0x0002C4E4
	protected bool HandlePlayheadMove()
	{
		bool flag = this.videoFrame != this.lastVideoFrame;
		bool flag2 = this.videoTime != this.lastVideoTime;
		if (flag)
		{
			this.videoTime = (float)this.videoFrame / base.framerate;
		}
		else if (this.play)
		{
			this.videoTime += ((!this.isReverse) ? Time.deltaTime : (-Time.deltaTime));
		}
		return flag || flag2;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0002E374 File Offset: 0x0002C574
	protected void HandleFrameDecode(bool wasSeeked)
	{
		if (this.movie == null)
		{
			return;
		}
		this.videoFrame = Mathf.FloorToInt(this.videoTime * base.framerate);
		if (this.lastVideoFrame != this.videoFrame)
		{
			if (!this.loadOptions.preloadVideo && this.movie.videoDecoder != null)
			{
				this.movie.videoDecoder.Decode(this.videoFrame);
			}
			this.UpdateRendererUVRect();
			if (!wasSeeked && this.lastVideoFrame != this.videoFrame - 1)
			{
				int num = this.videoFrame - this.lastVideoFrame - 1;
				this._framesDropped += num;
			}
		}
		this.lastVideoFrame = this.videoFrame;
		this.lastVideoTime = this.videoTime;
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0002E444 File Offset: 0x0002C644
	protected void HandleAudioSync()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (component == null || !component.enabled || component.clip == null)
		{
			return;
		}
		if (this.videoTime <= component.clip.length && Mathf.Abs(this.videoTime - component.time) > (float)this.maxSyncErrorFrames / base.framerate)
		{
			component.Stop();
			component.time = this.videoTime;
			component.Play();
			this._syncEvents++;
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
	protected void HandleLoop()
	{
		if (this.movie == null || this.movie.demux == null || this.movie.demux.videoStreamInfo == null)
		{
			return;
		}
		bool flag = (!this.isReverse) ? (this.videoTime >= this.movie.demux.videoStreamInfo.lengthSeconds) : (this.videoTime <= 0f);
		if (flag)
		{
			if (this.loop)
			{
				this.videoTime = ((!this.isReverse) ? 0f : this.movie.demux.videoStreamInfo.lengthSeconds);
				if (this.OnLoop != null)
				{
					this.OnLoop(this);
				}
				base.SendMessage("OnLoop", this, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				this.play = false;
			}
		}
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0002E5D4 File Offset: 0x0002C7D4
	public void UpdateRendererUVRect()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (this.movie != null && this.movie.frameUV != null && this.movie.frameUV.Length > 0)
		{
			Rect rect = this.movie.frameUV[this.videoFrame % this.movie.frameUV.Length];
			if (component != null && component.sharedMaterial != null)
			{
				component.sharedMaterial.SetTextureOffset(this.texturePropertyName, new Vector2(rect.x, rect.y));
				component.sharedMaterial.SetTextureScale(this.texturePropertyName, new Vector2(rect.width, rect.height));
			}
			if (this.material != null)
			{
				this.material.SetTextureOffset(this.texturePropertyName, new Vector2(rect.x, rect.y));
				this.material.SetTextureScale(this.texturePropertyName, new Vector2(rect.width, rect.height));
			}
		}
	}

	// Token: 0x0400050A RID: 1290
	public const string PACKAGE_VERSION = "v0.12";

	// Token: 0x0400050B RID: 1291
	public TextAsset source;

	// Token: 0x0400050C RID: 1292
	public AudioClip audioSource;

	// Token: 0x0400050D RID: 1293
	public LoadOptions loadOptions = LoadOptions.Default;

	// Token: 0x0400050E RID: 1294
	public float videoTime;

	// Token: 0x0400050F RID: 1295
	public int videoFrame;

	// Token: 0x04000510 RID: 1296
	public bool loop;

	// Token: 0x04000511 RID: 1297
	public bool isReverse;

	// Token: 0x04000512 RID: 1298
	protected float lastVideoTime;

	// Token: 0x04000513 RID: 1299
	protected int lastVideoFrame;
}
