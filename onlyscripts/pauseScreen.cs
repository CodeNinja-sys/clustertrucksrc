using System;
using InControl;
using Steamworks;
using UnityEngine;

// Token: 0x020002A2 RID: 674
public class pauseScreen : MonoBehaviour
{
	// Token: 0x0600100F RID: 4111 RVA: 0x00067BD4 File Offset: 0x00065DD4
	private void Start()
	{
		info.playing = true;
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x00067BDC File Offset: 0x00065DDC
	private void Update()
	{
		if (!info.paused && this.menu.activeInHierarchy)
		{
			this.menu.SetActive(false);
		}
		if (info.paused && !this.menu.activeInHierarchy)
		{
			this.menu.SetActive(true);
		}
		if ((Input.GetKeyDown(KeyCode.Escape) || InputManager.ActiveDevice.MenuWasPressed) && !info.isShowBuild && !GameManager.Playtesting)
		{
			if (!info.paused)
			{
				info.paused = true;
			}
			else
			{
				info.paused = false;
			}
		}
		if (info.paused)
		{
			info.gameTime = 0f;
		}
		else
		{
			info.gameTime = 1f;
		}
		if (info.playing && !info.paused && Cursor.visible)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		if ((!info.playing || info.paused) && !Cursor.visible)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x00067CF8 File Offset: 0x00065EF8
	public void Resume()
	{
		info.ResetTime();
		info.paused = false;
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x00067D08 File Offset: 0x00065F08
	public void Options()
	{
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x00067D0C File Offset: 0x00065F0C
	public void LevelSelect()
	{
		leaderboardsManager.FlushAllGhosts();
		Singleton<DataRecorder>.Instance.StopRecording();
		Singleton<RecordedDataPrefabPlayer>.Instance.StopPlayback();
		Manager.Instance().GoToLevelSelect();
		Manager.Instance().ClearLevel();
		info.playing = false;
		info.paused = false;
		info.ResetTime();
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00067D58 File Offset: 0x00065F58
	public void Ability()
	{
		this.Menu();
		Manager.Instance().myMenuBar.ChangeMenu(Manager.Instance().myMenuBar.abMenu);
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x00067D8C File Offset: 0x00065F8C
	public void Menu()
	{
		LeaderboardGhostHandler.CurrentOtherGhost = new CSteamID(0UL);
		leaderboardsManager.FlushAllGhosts();
		Singleton<DataRecorder>.Instance.StopRecording();
		Singleton<RecordedDataPrefabPlayer>.Instance.StopPlayback();
		this.menu.SetActive(false);
		info.paused = false;
		info.playing = false;
		info.paused = false;
		Manager.Instance().OpenMenuFromGame();
		info.ResetTime();
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x00067DEC File Offset: 0x00065FEC
	public void MenuWon()
	{
		info.currentLevel++;
		this.Menu();
	}

	// Token: 0x04000CF3 RID: 3315
	public GameObject menu;
}
