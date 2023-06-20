using System;
using MP;
using UnityEngine;

// Token: 0x0200013F RID: 319
public abstract class MoviePlayerBase : MonoBehaviour
{
	// Token: 0x1400000C RID: 12
	// (add) Token: 0x060006F2 RID: 1778 RVA: 0x0002E73C File Offset: 0x0002C93C
	// (remove) Token: 0x060006F3 RID: 1779 RVA: 0x0002E758 File Offset: 0x0002C958
	public event MoviePlayerBase.MovieEvent OnPlay;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x060006F4 RID: 1780 RVA: 0x0002E774 File Offset: 0x0002C974
	// (remove) Token: 0x060006F5 RID: 1781 RVA: 0x0002E790 File Offset: 0x0002C990
	public event MoviePlayerBase.MovieEvent OnStop;

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0002E7AC File Offset: 0x0002C9AC
	public int framesSkipped
	{
		get
		{
			return this._framesDropped;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0002E7B4 File Offset: 0x0002C9B4
	public int syncEvents
	{
		get
		{
			return this._syncEvents;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0002E7BC File Offset: 0x0002C9BC
	// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0002E818 File Offset: 0x0002CA18
	public float framerate
	{
		get
		{
			return (this.movie == null || this.movie.demux == null || this.movie.demux.videoStreamInfo == null) ? 30f : this.movie.demux.videoStreamInfo.framerate;
		}
		set
		{
			if (this is MoviePlayer && this.movie != null && this.movie.demux != null && this.movie.demux.videoStreamInfo != null)
			{
				MoviePlayer moviePlayer = this as MoviePlayer;
				moviePlayer.videoTime = ((value != 0f) ? (moviePlayer.videoTime * moviePlayer.framerate / value) : 0f);
				this.movie.demux.videoStreamInfo.framerate = value;
			}
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x060006FA RID: 1786 RVA: 0x0002E8A8 File Offset: 0x0002CAA8
	public float lengthSeconds
	{
		get
		{
			return (this.movie == null || this.movie.demux == null || this.movie.demux.videoStreamInfo == null) ? 0f : this.movie.demux.videoStreamInfo.lengthSeconds;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x060006FB RID: 1787 RVA: 0x0002E904 File Offset: 0x0002CB04
	public int lengthFrames
	{
		get
		{
			return (this.movie == null || this.movie.demux == null || this.movie.demux.videoStreamInfo == null) ? 0 : this.movie.demux.videoStreamInfo.frameCount;
		}
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0002E95C File Offset: 0x0002CB5C
	protected virtual void Load(MovieSource source, LoadOptions loadOptions = null)
	{
		Texture2D texture2D;
		AudioClip x;
		Movie movie = MoviePlayerUtil.Load(source, out texture2D, out x, loadOptions);
		if (this.movie != null)
		{
			MoviePlayerUtil.Unload(this.movie);
		}
		this.movie = movie;
		this._framesDropped = 0;
		this._syncEvents = 0;
		this.framebuffer = texture2D;
		if (x != null)
		{
			this.audiobuffer = x;
		}
		this.Bind();
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0002E9C0 File Offset: 0x0002CBC0
	[ContextMenu("Unload (disconnect)")]
	public void Unload()
	{
		if (this.movie != null)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (component != null)
			{
				component.Stop();
			}
			MoviePlayerUtil.Unload(this.movie);
			this.movie = null;
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0002EA04 File Offset: 0x0002CC04
	protected void Bind()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			component.sharedMaterial.SetTexture(this.texturePropertyName, this.framebuffer);
		}
		if (this.material != null)
		{
			this.material.SetTexture(this.texturePropertyName, this.framebuffer);
		}
		AudioSource audioSource = base.GetComponent<AudioSource>();
		if (this.audiobuffer != null)
		{
			if (audioSource == null)
			{
				audioSource = base.gameObject.AddComponent<AudioSource>();
			}
			audioSource.clip = this.audiobuffer;
			audioSource.playOnAwake = false;
		}
		else if (audioSource != null)
		{
			audioSource.Stop();
			audioSource.clip = null;
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0002EAC4 File Offset: 0x0002CCC4
	protected void DrawFramebufferToScreen(Rect? sourceUV = null)
	{
		Rect screenRect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		if (this.screenMode == MoviePlayerBase.ScreenMode.CustomRect)
		{
			screenRect = this.customScreenRect;
		}
		else if (this.screenMode != MoviePlayerBase.ScreenMode.Stretch)
		{
			float num = (float)this.movie.demux.videoStreamInfo.width / (float)this.movie.demux.videoStreamInfo.height;
			float num2 = (float)Screen.width / (float)Screen.height;
			if (this.screenMode == MoviePlayerBase.ScreenMode.Crop)
			{
				if (num2 < num)
				{
					screenRect.height = Mathf.Round(screenRect.width / num);
				}
				else
				{
					screenRect.width = Mathf.Round(screenRect.height * num);
				}
			}
			else if (this.screenMode == MoviePlayerBase.ScreenMode.Fill)
			{
				if (num2 < num)
				{
					screenRect.width = Mathf.Round((float)Screen.height * num);
				}
				else
				{
					screenRect.height = Mathf.Round((float)Screen.width / num);
				}
			}
			screenRect.x = Mathf.Round(((float)Screen.width - screenRect.width) / 2f);
			screenRect.y = Mathf.Round(((float)Screen.height - screenRect.height) / 2f);
		}
		GUI.depth = this.screenGuiDepth;
		Event current = Event.current;
		if (current == null || current.type == EventType.Repaint)
		{
			if (sourceUV == null)
			{
				Graphics.DrawTexture(screenRect, this.framebuffer, this.material);
			}
			else
			{
				Graphics.DrawTexture(screenRect, this.framebuffer, sourceUV.Value, 0, 0, 0, 0, this.material);
			}
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0002EC7C File Offset: 0x0002CE7C
	protected void HandlePlayStop()
	{
		if (this.play != this.lastPlay)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.play)
			{
				if (this.OnPlay != null)
				{
					this.OnPlay(this);
				}
				base.SendMessage("OnPlay", this, SendMessageOptions.DontRequireReceiver);
				if (component != null)
				{
					component.Play();
					if (this is MoviePlayer)
					{
						component.time = ((MoviePlayer)this).videoTime;
					}
				}
			}
			else
			{
				if (this.OnStop != null)
				{
					this.OnStop(this);
				}
				base.SendMessage("OnStop", this, SendMessageOptions.DontRequireReceiver);
				if (component != null)
				{
					component.Stop();
				}
			}
			this.lastPlay = this.play;
		}
	}

	// Token: 0x04000515 RID: 1301
	public Texture2D framebuffer;

	// Token: 0x04000516 RID: 1302
	public AudioClip audiobuffer;

	// Token: 0x04000517 RID: 1303
	public bool drawToScreen;

	// Token: 0x04000518 RID: 1304
	[Obsolete("Use property named material instead, it works exactly like otherMaterial did. In later version otherMaterial will be removed")]
	public Material otherMaterial;

	// Token: 0x04000519 RID: 1305
	public Material material;

	// Token: 0x0400051A RID: 1306
	public string texturePropertyName = "_MainTex";

	// Token: 0x0400051B RID: 1307
	public MoviePlayerBase.ScreenMode screenMode;

	// Token: 0x0400051C RID: 1308
	public int screenGuiDepth;

	// Token: 0x0400051D RID: 1309
	public Rect customScreenRect = new Rect(0f, 0f, 100f, 100f);

	// Token: 0x0400051E RID: 1310
	public bool play;

	// Token: 0x0400051F RID: 1311
	public Movie movie;

	// Token: 0x04000520 RID: 1312
	public int maxSyncErrorFrames = 2;

	// Token: 0x04000521 RID: 1313
	protected int _framesDropped;

	// Token: 0x04000522 RID: 1314
	protected int _syncEvents;

	// Token: 0x04000523 RID: 1315
	protected bool lastPlay;

	// Token: 0x02000140 RID: 320
	public enum ScreenMode
	{
		// Token: 0x04000527 RID: 1319
		Crop,
		// Token: 0x04000528 RID: 1320
		Fill,
		// Token: 0x04000529 RID: 1321
		Stretch,
		// Token: 0x0400052A RID: 1322
		CustomRect
	}

	// Token: 0x02000314 RID: 788
	// (Invoke) Token: 0x0600125E RID: 4702
	public delegate void MovieEvent(MoviePlayerBase caller);
}
