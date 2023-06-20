using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class epic : AbilityBaseClass
{
	// Token: 0x06000F34 RID: 3892 RVA: 0x00063354 File Offset: 0x00061554
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x0006335C File Offset: 0x0006155C
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00063364 File Offset: 0x00061564
	private void Start()
	{
		this.au = base.GetComponent<AudioSource>();
		Time.timeScale = 1f;
		this.topStart = this.topBar.position;
		this.botStart = this.botBar.position;
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x000633AC File Offset: 0x000615AC
	private void OnEnable()
	{
		this.au = base.GetComponent<AudioSource>();
		Time.timeScale = 1f;
		this.topBar.gameObject.SetActive(true);
		this.botBar.gameObject.SetActive(true);
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x000633F4 File Offset: 0x000615F4
	private void OnDisable()
	{
		this.topBar.gameObject.SetActive(false);
		this.botBar.gameObject.SetActive(false);
		this.epicness.enabled = false;
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x00063430 File Offset: 0x00061630
	private void Update()
	{
		if (this.isGoing)
		{
			this.topBar.position = Vector3.Lerp(this.topBar.position, this.topPoint.position, Time.deltaTime * 5f);
			this.botBar.position = Vector3.Lerp(this.botBar.position, this.botPoint.position, Time.deltaTime * 5f);
			this.epicness.enabled = true;
			if (Time.timeScale > 0.9f)
			{
				info.playerControlledTime -= Time.unscaledDeltaTime * 2f;
			}
			info.scoreMultiplier = 2f;
			this.au.volume = Mathf.Lerp(this.au.volume, 1f, Time.deltaTime * 5f);
			audioOptions.muteMusic = true;
		}
		else
		{
			this.topBar.position = Vector3.Lerp(this.topBar.position, this.topStart, Time.deltaTime * 5f);
			this.botBar.position = Vector3.Lerp(this.botBar.position, this.botStart, Time.deltaTime * 5f);
			this.epicness.enabled = false;
			if (Time.timeScale < 1f)
			{
				info.playerControlledTime += Time.unscaledDeltaTime * 2f;
			}
			else
			{
				info.playerControlledTime = 1f;
			}
			info.scoreMultiplier = 1f;
			this.au.volume = Mathf.Lerp(this.au.volume, 0f, Time.deltaTime * 5f);
			audioOptions.muteMusic = false;
		}
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x000635F0 File Offset: 0x000617F0
	public override void Stop()
	{
		this.isGoing = false;
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000635FC File Offset: 0x000617FC
	public override void Go()
	{
		this.isGoing = true;
	}

	// Token: 0x04000C05 RID: 3077
	private bool isGoing;

	// Token: 0x04000C06 RID: 3078
	public Transform topBar;

	// Token: 0x04000C07 RID: 3079
	public Transform botBar;

	// Token: 0x04000C08 RID: 3080
	public Transform topPoint;

	// Token: 0x04000C09 RID: 3081
	public Transform botPoint;

	// Token: 0x04000C0A RID: 3082
	private Vector3 topStart;

	// Token: 0x04000C0B RID: 3083
	private Vector3 botStart;

	// Token: 0x04000C0C RID: 3084
	public MonoBehaviour epicness;

	// Token: 0x04000C0D RID: 3085
	private AudioSource au;
}
