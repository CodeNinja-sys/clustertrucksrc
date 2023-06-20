using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A0 RID: 672
public class optionsMenu : MonoBehaviour
{
	// Token: 0x06001009 RID: 4105 RVA: 0x00067AFC File Offset: 0x00065CFC
	private void Start()
	{
		if (PlayerPrefs.GetFloat("sens") > 0f)
		{
			this.sensSlider.value = PlayerPrefs.GetFloat("sens");
			player.sensitivity = PlayerPrefs.GetFloat("sens") * 3f;
			player.controlSensitivity = PlayerPrefs.GetFloat("sens") * 2f;
		}
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x00067B5C File Offset: 0x00065D5C
	private void Update()
	{
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00067B60 File Offset: 0x00065D60
	public void changeSens()
	{
		PlayerPrefs.SetFloat("sens", this.sensSlider.value);
		player.sensitivity = this.sensSlider.value * 3f;
		player.controlSensitivity = this.sensSlider.value * 2f;
	}

	// Token: 0x04000CF2 RID: 3314
	public Slider sensSlider;
}
