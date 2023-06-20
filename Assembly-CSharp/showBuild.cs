using System;
using UnityEngine;

// Token: 0x020002B5 RID: 693
[RequireComponent(typeof(AudioSource))]
public class showBuild : MonoBehaviour
{
	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06001073 RID: 4211 RVA: 0x0006B2A8 File Offset: 0x000694A8
	public bool isPlaying
	{
		get
		{
			return this.m_isPlaying;
		}
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x0006B2B0 File Offset: 0x000694B0
	public static showBuild Instance()
	{
		if (!showBuild._showBuildInstance)
		{
			showBuild._showBuildInstance = (UnityEngine.Object.FindObjectOfType(typeof(showBuild)) as showBuild);
		}
		return showBuild._showBuildInstance;
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0006B2E0 File Offset: 0x000694E0
	private void Awake()
	{
		GameObject gameObject = UnityEngine.Object.FindObjectOfType(typeof(showBuild)) as GameObject;
		if (gameObject != null && !gameObject.Equals(base.gameObject))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this._movieHandler = base.GetComponent<MoviePlayer>();
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x0006B344 File Offset: 0x00069544
	private void Start()
	{
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x0006B348 File Offset: 0x00069548
	private void Update()
	{
		this.checkForInput();
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x0006B350 File Offset: 0x00069550
	private void checkForInput()
	{
		if (Input.anyKeyDown && this.isPlaying)
		{
			this.stopVideo();
		}
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0006B370 File Offset: 0x00069570
	public void playVideo()
	{
		base.GetComponent<AudioListener>().enabled = true;
		this.m_isPlaying = true;
		this._movieHandler.play = true;
		this._movieHandler.drawToScreen = true;
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x0006B3A0 File Offset: 0x000695A0
	public void stopVideo()
	{
		this.m_isPlaying = false;
		this._movieHandler.drawToScreen = false;
		this._movieHandler.play = false;
		this._movieHandler.videoFrame = 0;
		base.GetComponent<AudioListener>().enabled = false;
		Manager.Instance().NewGame();
		UnityEngine.Object.FindObjectOfType<MenuMusic>().GetComponent<AudioSource>().Play();
	}

	// Token: 0x04000D87 RID: 3463
	private MoviePlayer _movieHandler;

	// Token: 0x04000D88 RID: 3464
	private static showBuild _showBuildInstance;

	// Token: 0x04000D89 RID: 3465
	private bool m_isPlaying;
}
