using System;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class SetMenuWorld : MonoBehaviour
{
	// Token: 0x060010F7 RID: 4343 RVA: 0x0006E580 File Offset: 0x0006C780
	private void Awake()
	{
		if (PlayerPrefs.GetInt("BeatGame", 0) == 1)
		{
			SetMenuWorld.randomWorld = UnityEngine.Random.Range(0, 8);
		}
		this.OnEnable();
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0006E5A8 File Offset: 0x0006C7A8
	private void OnEnable()
	{
		foreach (GameObject gameObject in this.worlds)
		{
			gameObject.SetActive(false);
		}
		if (PlayerPrefs.GetInt("BeatGame", 0) == 1)
		{
			this.worlds[SetMenuWorld.randomWorld].SetActive(true);
			this.musicMan.startThisSong = SetMenuWorld.randomWorld + 1;
		}
		else
		{
			this.worlds[info.currentProgressWorld - 1].SetActive(true);
			this.musicMan.startThisSong = info.currentProgressWorld;
		}
		if (info.currentProgressWorld == 4 || info.currentProgressWorld == 7 || info.currentProgressWorld == 9)
		{
			info.darkWorld = true;
		}
		else
		{
			info.darkWorld = false;
		}
	}

	// Token: 0x04000E13 RID: 3603
	public GameObject[] worlds;

	// Token: 0x04000E14 RID: 3604
	public static int randomWorld;

	// Token: 0x04000E15 RID: 3605
	public musicManager musicMan;
}
