using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000138 RID: 312
public class LeaderboardGhostHandler : MonoBehaviour
{
	// Token: 0x1700013B RID: 315
	// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0002D94C File Offset: 0x0002BB4C
	public static LeaderboardGhostHandler.GhostMethod CurrentGhostMethod
	{
		get
		{
			return (LeaderboardGhostHandler.GhostMethod)LeaderboardGhostHandler._ghostMethod;
		}
	}

	// Token: 0x1700013C RID: 316
	// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0002D954 File Offset: 0x0002BB54
	public static int SetGhostMethod
	{
		set
		{
			LeaderboardGhostHandler._ghostMethod = value;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0002D95C File Offset: 0x0002BB5C
	// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0002D964 File Offset: 0x0002BB64
	public static CSteamID CurrentOtherGhost
	{
		get
		{
			return LeaderboardGhostHandler._currentOtherGhost;
		}
		set
		{
			LeaderboardGhostHandler._currentOtherGhost = value;
			Debug.Log(string.Concat(new object[]
			{
				"OtherGhost Changed to: Name: ",
				SteamFriends.GetFriendPersonaName(LeaderboardGhostHandler._currentOtherGhost),
				"  :  ",
				LeaderboardGhostHandler._currentOtherGhost
			}));
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0002D9B4 File Offset: 0x0002BBB4
	private void Awake()
	{
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
	private void OnEnable()
	{
		if (LeaderboardGhostHandler.CurrentOtherGhost != (CSteamID)0UL)
		{
			this.ghostDropdown.captionText.text = SteamFriends.GetFriendPersonaName(LeaderboardGhostHandler.CurrentOtherGhost);
		}
		this.UpdateDropdown();
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0002D9FC File Offset: 0x0002BBFC
	public void GhostMethodChanged(int newMethod)
	{
		LeaderboardGhostHandler._ghostMethod = newMethod;
		Debug.Log("GhostMethod Changed: " + LeaderboardGhostHandler.CurrentGhostMethod.ToString());
		if (LeaderboardGhostHandler._ghostMethod != 4)
		{
			LeaderboardGhostHandler.CurrentOtherGhost = new CSteamID(0UL);
			leaderboardsManager.FlushAllGhosts();
			this.ClearTemporaryName();
		}
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0002DA50 File Offset: 0x0002BC50
	private void ClearTemporaryName()
	{
		if (this.ghostDropdown.options.Count > 4)
		{
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>(this.ghostDropdown.options);
			while (list.Count > 4)
			{
				list.RemoveAt(list.Count - 1);
			}
			this.ghostDropdown.ClearOptions();
			this.ghostDropdown.AddOptions(list);
		}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0002DABC File Offset: 0x0002BCBC
	public void SetTemporaryDropdownValue(string value)
	{
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>(this.ghostDropdown.options);
		while (list.Count > 4)
		{
			list.RemoveAt(list.Count - 1);
		}
		list.Add(new Dropdown.OptionData(value));
		this.ghostDropdown.ClearOptions();
		this.ghostDropdown.AddOptions(list);
		this.ghostDropdown.value = Enum.GetValues(typeof(LeaderboardGhostHandler.GhostMethod)).Length - 1;
		this.ghostDropdown.RefreshShownValue();
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0002DB48 File Offset: 0x0002BD48
	public void UpdateDropdown()
	{
		if (LeaderboardGhostHandler.CurrentOtherGhost != (CSteamID)0UL && LeaderboardGhostHandler._ghostMethod == 4)
		{
			Debug.Log("Other Ghost Skip Value Update!");
			return;
		}
		if (info.PlayingGhostFromLeaderBoard)
		{
			Debug.Log("Plaing ghosts from leaderboard: Returning");
			return;
		}
		this.ClearTemporaryName();
		this.ghostDropdown.value = LeaderboardGhostHandler._ghostMethod;
		this.ghostDropdown.RefreshShownValue();
		if (LeaderboardGhostHandler._ghostMethod == 0)
		{
			leaderboardsManager.FlushAllGhosts();
		}
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
	public void Flush()
	{
		Debug.Log("Flushing GhostMethod: " + Time.frameCount);
		LeaderboardGhostHandler._ghostMethod = 0;
		if (this.ghostDropdown != null)
		{
			this.ghostDropdown.value = LeaderboardGhostHandler._ghostMethod;
			this.ghostDropdown.RefreshShownValue();
		}
	}

	// Token: 0x040004F8 RID: 1272
	private static int _ghostMethod;

	// Token: 0x040004F9 RID: 1273
	private static CSteamID _currentOtherGhost;

	// Token: 0x040004FA RID: 1274
	[SerializeField]
	private Dropdown ghostDropdown;

	// Token: 0x02000139 RID: 313
	public enum GhostMethod
	{
		// Token: 0x040004FC RID: 1276
		None,
		// Token: 0x040004FD RID: 1277
		Self,
		// Token: 0x040004FE RID: 1278
		BestOther,
		// Token: 0x040004FF RID: 1279
		BestFriend,
		// Token: 0x04000500 RID: 1280
		Other
	}
}
