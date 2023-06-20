using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000268 RID: 616
public class audioOptions : MonoBehaviour
{
	// Token: 0x06000EE6 RID: 3814 RVA: 0x000607C0 File Offset: 0x0005E9C0
	private void Start()
	{
		audioOptions.muteMusic = false;
		if (PlayerPrefs.GetFloat("master") > 0f)
		{
			this.masterSlider.value = PlayerPrefs.GetFloat("master");
			AudioListener.volume = PlayerPrefs.GetFloat("master") - 0.1f;
			audioOptions.masterVolume = PlayerPrefs.GetFloat("master") - 0.1f;
		}
		else
		{
			PlayerPrefs.SetFloat("master", 1.1f);
			this.masterSlider.value = 1.1f;
			AudioListener.volume = 1f;
			audioOptions.masterVolume = 1f;
			this.masterText.text = "100";
		}
		if (PlayerPrefs.GetFloat("music") > 0f)
		{
			this.musicSlider.value = PlayerPrefs.GetFloat("music");
			this.musicAU.volume = PlayerPrefs.GetFloat("music") - 0.1f;
			audioOptions.musicVolume = PlayerPrefs.GetFloat("music") - 0.1f;
		}
		else
		{
			PlayerPrefs.SetFloat("music", 0.6f);
			this.musicSlider.value = 0.6f;
			this.musicAU.volume = 0.5f;
			audioOptions.musicVolume = 0.5f;
			this.musicText.text = "50";
		}
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x00060918 File Offset: 0x0005EB18
	private void Update()
	{
		if (audioOptions.muteMusic)
		{
			this.musicAU.mute = true;
		}
		else
		{
			this.musicAU.mute = false;
		}
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00060944 File Offset: 0x0005EB44
	public void ChangeMaster()
	{
		PlayerPrefs.SetFloat("master", this.masterSlider.value);
		AudioListener.volume = this.masterSlider.value - 0.1f;
		audioOptions.masterVolume = this.musicSlider.value - 0.1f;
		this.masterText.text = ((this.masterSlider.value - 0.1f) * 100f).ToString("F0");
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x000609C4 File Offset: 0x0005EBC4
	public void ChangeMusic()
	{
		PlayerPrefs.SetFloat("music", this.musicSlider.value);
		this.musicAU.volume = this.musicSlider.value - 0.1f;
		audioOptions.musicVolume = this.musicSlider.value - 0.1f;
		this.musicText.text = ((this.musicSlider.value - 0.1f) * 100f).ToString("F0");
	}

	// Token: 0x04000B84 RID: 2948
	public Slider masterSlider;

	// Token: 0x04000B85 RID: 2949
	public Slider musicSlider;

	// Token: 0x04000B86 RID: 2950
	public AudioSource musicAU;

	// Token: 0x04000B87 RID: 2951
	public Text masterText;

	// Token: 0x04000B88 RID: 2952
	public Text musicText;

	// Token: 0x04000B89 RID: 2953
	public static float masterVolume;

	// Token: 0x04000B8A RID: 2954
	public static float musicVolume;

	// Token: 0x04000B8B RID: 2955
	public static bool muteMusic;
}
