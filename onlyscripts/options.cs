using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029F RID: 671
public class options : MonoBehaviour
{
	// Token: 0x06000FFA RID: 4090 RVA: 0x0006759C File Offset: 0x0006579C
	private void Awake()
	{
		if (PlayerPrefs.GetInt("FirstTime1") != 1)
		{
			this.SetFirstTime();
			this.SetPlayerPrefs();
			PlayerPrefs.SetInt("FirstTime1", 1);
		}
		else
		{
			this.GetPlayerPrefs();
		}
		this.SetButtonsAndSliders();
		this.Apply();
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x000675E8 File Offset: 0x000657E8
	private void SetFirstTime()
	{
		options.bloom = true;
		options.aberr = true;
		options.vignette = true;
		options.ssao = true;
		options.FOV = 90;
		options.shadowD = 150;
		options.vSync = true;
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x0006761C File Offset: 0x0006581C
	public void Apply()
	{
		QualitySettings.shadowDistance = (float)options.shadowD;
		if (options.vSync)
		{
			QualitySettings.vSyncCount = 1;
		}
		else
		{
			QualitySettings.vSyncCount = 0;
		}
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x00067650 File Offset: 0x00065850
	private void GetPlayerPrefs()
	{
		options.bloom = Convert.ToBoolean(PlayerPrefs.GetInt("bloom"));
		options.aberr = Convert.ToBoolean(PlayerPrefs.GetInt("aberr"));
		options.vignette = Convert.ToBoolean(PlayerPrefs.GetInt("vignette"));
		options.ssao = Convert.ToBoolean(PlayerPrefs.GetInt("ssao"));
		options.vSync = Convert.ToBoolean(PlayerPrefs.GetInt("vSync"));
		options.AA = PlayerPrefs.GetInt("AA");
		options.shadowD = PlayerPrefs.GetInt("shadowD");
		options.FOV = PlayerPrefs.GetInt("FOV");
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x000676F0 File Offset: 0x000658F0
	private void SetPlayerPrefs()
	{
		PlayerPrefs.SetInt("bloom", Convert.ToInt32(options.bloom));
		PlayerPrefs.SetInt("aberr", Convert.ToInt32(options.aberr));
		PlayerPrefs.SetInt("vignette", Convert.ToInt32(options.vignette));
		PlayerPrefs.SetInt("ssao", Convert.ToInt32(options.ssao));
		PlayerPrefs.SetInt("vSync", Convert.ToInt32(options.vSync));
		PlayerPrefs.SetInt("AA", options.AA);
		PlayerPrefs.SetInt("shadowD", options.shadowD);
		PlayerPrefs.SetInt("FOV", options.FOV);
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x00067790 File Offset: 0x00065990
	private void SetButtonsAndSliders()
	{
		if (options.bloom)
		{
			this.bloomB.transform.FindChild("value").GetComponent<Text>().text = "ON";
		}
		else
		{
			this.bloomB.transform.FindChild("value").GetComponent<Text>().text = "OFF";
		}
		if (options.aberr)
		{
			this.aberrB.transform.FindChild("value").GetComponent<Text>().text = "ON";
		}
		else
		{
			this.aberrB.transform.FindChild("value").GetComponent<Text>().text = "OFF";
		}
		if (options.vignette)
		{
			this.vignetteB.transform.FindChild("value").GetComponent<Text>().text = "ON";
		}
		else
		{
			this.vignetteB.transform.FindChild("value").GetComponent<Text>().text = "OFF";
		}
		if (options.ssao)
		{
			this.ssaoB.transform.FindChild("value").GetComponent<Text>().text = "ON";
		}
		else
		{
			this.ssaoB.transform.FindChild("value").GetComponent<Text>().text = "OFF";
		}
		if (options.vSync)
		{
			this.vSyncB.transform.FindChild("value").GetComponent<Text>().text = "ON";
		}
		else
		{
			this.vSyncB.transform.FindChild("value").GetComponent<Text>().text = "OFF";
		}
		this.AAB.transform.FindChild("value").GetComponent<Text>().text = options.AA.ToString();
		this.FOVS.value = (float)options.FOV;
		this.shadowDistanceS.value = (float)options.shadowD;
		this.shadowText.text = options.shadowD.ToString("F0");
		this.fovText.text = options.FOV.ToString("F0");
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x000679D0 File Offset: 0x00065BD0
	public void SetBloom()
	{
		options.bloom = !options.bloom;
		this.SetPlayerPrefs();
		this.SetButtonsAndSliders();
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x000679EC File Offset: 0x00065BEC
	public void SetAberration()
	{
		options.aberr = !options.aberr;
		this.SetPlayerPrefs();
		this.SetButtonsAndSliders();
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x00067A08 File Offset: 0x00065C08
	public void SetVignette()
	{
		options.vignette = !options.vignette;
		this.SetPlayerPrefs();
		this.SetButtonsAndSliders();
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x00067A24 File Offset: 0x00065C24
	public void SetSSAO()
	{
		options.ssao = !options.ssao;
		this.SetPlayerPrefs();
		this.SetButtonsAndSliders();
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x00067A40 File Offset: 0x00065C40
	public void SetVSYNC()
	{
		options.vSync = !options.vSync;
		this.SetPlayerPrefs();
		this.SetButtonsAndSliders();
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x00067A5C File Offset: 0x00065C5C
	public void SetFOV()
	{
		options.FOV = (int)this.FOVS.value;
		this.SetPlayerPrefs();
		this.SetButtonsAndSliders();
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x00067A7C File Offset: 0x00065C7C
	public void SetShadowD()
	{
		options.shadowD = (int)this.shadowDistanceS.value;
		this.SetPlayerPrefs();
		this.SetButtonsAndSliders();
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x00067A9C File Offset: 0x00065C9C
	public void SetAA()
	{
		if (options.AA == 2)
		{
			options.AA = 4;
		}
		else if (options.AA == 4)
		{
			options.AA = 8;
		}
		else if (options.AA == 8)
		{
			options.AA = 2;
		}
		this.SetPlayerPrefs();
		this.SetButtonsAndSliders();
	}

	// Token: 0x04000CDF RID: 3295
	public static bool bloom = true;

	// Token: 0x04000CE0 RID: 3296
	public static bool aberr = true;

	// Token: 0x04000CE1 RID: 3297
	public static bool vignette = true;

	// Token: 0x04000CE2 RID: 3298
	public static bool ssao = true;

	// Token: 0x04000CE3 RID: 3299
	public static bool vSync = true;

	// Token: 0x04000CE4 RID: 3300
	public static int AA = 8;

	// Token: 0x04000CE5 RID: 3301
	public static int shadowD = 200;

	// Token: 0x04000CE6 RID: 3302
	public static int FOV = 90;

	// Token: 0x04000CE7 RID: 3303
	public static float saturation;

	// Token: 0x04000CE8 RID: 3304
	public Button bloomB;

	// Token: 0x04000CE9 RID: 3305
	public Button aberrB;

	// Token: 0x04000CEA RID: 3306
	public Button vignetteB;

	// Token: 0x04000CEB RID: 3307
	public Button ssaoB;

	// Token: 0x04000CEC RID: 3308
	public Button AAB;

	// Token: 0x04000CED RID: 3309
	public Button vSyncB;

	// Token: 0x04000CEE RID: 3310
	public Slider FOVS;

	// Token: 0x04000CEF RID: 3311
	public Slider shadowDistanceS;

	// Token: 0x04000CF0 RID: 3312
	public Text fovText;

	// Token: 0x04000CF1 RID: 3313
	public Text shadowText;
}
