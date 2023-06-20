using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class CheckForSteamworks : MonoBehaviour
{
	// Token: 0x06000082 RID: 130 RVA: 0x000058D4 File Offset: 0x00003AD4
	private void Start()
	{
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000058D8 File Offset: 0x00003AD8
	private void Update()
	{
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000058DC File Offset: 0x00003ADC
	public void Check()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		if (!SteamManager.Initialized)
		{
			this.GoToMenu();
		}
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000058FC File Offset: 0x00003AFC
	public void GoToMenu()
	{
		Manager.Instance().OpenMenuFromGame();
		Manager.Instance().FadeMainMenu();
		info.currentLevel = 1;
	}
}
