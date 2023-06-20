using System;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class musicManager : MonoBehaviour
{
	// Token: 0x060011E2 RID: 4578 RVA: 0x00072DF8 File Offset: 0x00070FF8
	private void Start()
	{
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x00072DFC File Offset: 0x00070FFC
	private void Update()
	{
		if (info.playing)
		{
			this.lowPass.cutoffFrequency = Mathf.Lerp(this.lowPass.cutoffFrequency, 22000f * Mathf.Pow(Time.timeScale, 1f), Time.unscaledDeltaTime * 10f);
		}
		else
		{
			this.lowPass.cutoffFrequency = Mathf.Lerp(this.lowPass.cutoffFrequency, 800f * Mathf.Pow(Time.timeScale, 1.2f), Time.unscaledDeltaTime * 10f);
		}
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x00072E90 File Offset: 0x00071090
	public void StartFirstSong()
	{
		this.StartSong(this.startThisSong);
		Debug.Log("hello?");
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x00072EA8 File Offset: 0x000710A8
	public void StopSong()
	{
		this.musicAU.Stop();
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x00072EB8 File Offset: 0x000710B8
	public void StartSong(int i)
	{
		switch (i)
		{
		case 1:
			this.musicAU.clip = this.world1M;
			break;
		case 2:
			this.musicAU.clip = this.world2M;
			break;
		case 3:
			this.musicAU.clip = this.world3M;
			break;
		case 4:
			this.musicAU.clip = this.world4M;
			break;
		case 5:
			this.musicAU.clip = this.world5M;
			break;
		case 6:
			this.musicAU.clip = this.world6M;
			break;
		case 7:
			this.musicAU.clip = this.world7M;
			break;
		case 8:
			this.musicAU.clip = this.world8M;
			break;
		case 9:
			this.musicAU.clip = this.world9M;
			break;
		case 10:
			this.musicAU.clip = this.world9M;
			break;
		}
		if (!this.musicAU.isPlaying)
		{
			this.musicAU.Play();
		}
	}

	// Token: 0x04000EF8 RID: 3832
	public AudioClip menuM;

	// Token: 0x04000EF9 RID: 3833
	public AudioClip world1M;

	// Token: 0x04000EFA RID: 3834
	public AudioClip world2M;

	// Token: 0x04000EFB RID: 3835
	public AudioClip world3M;

	// Token: 0x04000EFC RID: 3836
	public AudioClip world4M;

	// Token: 0x04000EFD RID: 3837
	public AudioClip world5M;

	// Token: 0x04000EFE RID: 3838
	public AudioClip world6M;

	// Token: 0x04000EFF RID: 3839
	public AudioClip world7M;

	// Token: 0x04000F00 RID: 3840
	public AudioClip world8M;

	// Token: 0x04000F01 RID: 3841
	public AudioClip world9M;

	// Token: 0x04000F02 RID: 3842
	public AudioClip bossM;

	// Token: 0x04000F03 RID: 3843
	private float musicVolume;

	// Token: 0x04000F04 RID: 3844
	public AudioLowPassFilter lowPass;

	// Token: 0x04000F05 RID: 3845
	public AudioSource musicAU;

	// Token: 0x04000F06 RID: 3846
	private float currentPlayThroughTime;

	// Token: 0x04000F07 RID: 3847
	public int startThisSong;
}
