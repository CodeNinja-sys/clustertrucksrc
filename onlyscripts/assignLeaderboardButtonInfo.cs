using System;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000220 RID: 544
public class assignLeaderboardButtonInfo : MonoBehaviour
{
	// Token: 0x06000CB9 RID: 3257 RVA: 0x0004EAB0 File Offset: 0x0004CCB0
	public void assignButtonValues(string rank, string name, string score, string death, string date, PublishedFileId_t _id, SteamUGCDetails_t _details)
	{
		this.rankText.text = rank;
		this.nameText.text = name;
		this.scoreText.text = score;
		this.deathText.text = death;
		this.dateText.text = date;
		this.m_id = _id;
		this.m_mapDetails = _details;
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0004EB0C File Offset: 0x0004CD0C
	public void assignLeaderBoardButtons(string Rank, string Username, string Time, string Score, UGCHandle_t ghostData, leaderboardsManager parent, CSteamID steamID, bool newHighscore = false, string Date = "")
	{
		this.m_leaderBoardParent = parent;
		this.m_ghostDataHandle = ghostData;
		this.m_steamID = steamID;
		if (this.newHighscoreObject != null && newHighscore)
		{
			this.newHighscoreObject.SetActive(true);
		}
		if (!string.IsNullOrEmpty(Rank))
		{
			this.rankText.text = Rank;
		}
		else
		{
			this.rankText.gameObject.SetActive(false);
		}
		if (!string.IsNullOrEmpty(Username))
		{
			this.nameText.text = Username;
		}
		else
		{
			this.nameText.gameObject.SetActive(false);
		}
		if (!string.IsNullOrEmpty(Time))
		{
			this.scoreText.text = Time;
		}
		else
		{
			this.scoreText.gameObject.SetActive(false);
		}
		if (!string.IsNullOrEmpty(Score))
		{
			this.deathText.text = Score;
		}
		else
		{
			this.deathText.gameObject.SetActive(false);
		}
		if (this.dateText != null)
		{
			if (!string.IsNullOrEmpty(Date))
			{
				this.dateText.text = Date;
			}
			else
			{
				this.dateText.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x0004EC4C File Offset: 0x0004CE4C
	public void assignButtonValues(string rank = null, string name = null, string score = null, string death = null, string date = null)
	{
		if (rank != null)
		{
			this.rankText.text = rank;
		}
		else
		{
			this.rankText.gameObject.SetActive(false);
		}
		if (name != null)
		{
			this.nameText.text = name;
		}
		else
		{
			this.nameText.gameObject.SetActive(false);
		}
		if (score != null)
		{
			this.scoreText.text = score;
		}
		else
		{
			this.scoreText.gameObject.SetActive(false);
		}
		if (death != null)
		{
			this.deathText.text = death;
		}
		else
		{
			this.deathText.gameObject.SetActive(false);
		}
		if (date != null)
		{
			this.dateText.text = date;
		}
		else
		{
			this.dateText.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x0004ED28 File Offset: 0x0004CF28
	public string getMapTitle()
	{
		return this.rankText.text;
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0004ED38 File Offset: 0x0004CF38
	public PublishedFileId_t getSteamID()
	{
		return this.m_id;
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x0004ED40 File Offset: 0x0004CF40
	public SteamUGCDetails_t getDetails()
	{
		Debug.Log("Returning: " + this.m_mapDetails.m_rgchDescription);
		return this.m_mapDetails;
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x0004ED70 File Offset: 0x0004CF70
	public void ChangedGhostData()
	{
		RaceGhostButtonTag raceGhostButtonTag = UnityEngine.Object.FindObjectOfType<RaceGhostButtonTag>();
		if (raceGhostButtonTag != null)
		{
			raceGhostButtonTag.GetComponent<Button>().interactable = false;
		}
		this.m_leaderBoardParent.ChangeGhostData(this.m_ghostDataHandle);
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x0004EDAC File Offset: 0x0004CFAC
	public void SetNewGhostPlayer()
	{
		if (info.PlayingGhostFromLeaderBoard)
		{
			return;
		}
		Debug.Log(this.nameText.text + " ID: " + this.m_steamID);
		LeaderboardGhostHandler.SetGhostMethod = 4;
		UnityEngine.Object.FindObjectOfType<LeaderboardGhostHandler>().SetTemporaryDropdownValue(this.nameText.text);
		if (LeaderboardGhostHandler.CurrentOtherGhost != this.m_steamID)
		{
			leaderboardsManager.FlushOtherGhost();
			LeaderboardGhostHandler.CurrentOtherGhost = this.m_steamID;
		}
	}

	// Token: 0x0400094B RID: 2379
	public Text rankText;

	// Token: 0x0400094C RID: 2380
	public Text nameText;

	// Token: 0x0400094D RID: 2381
	public Text scoreText;

	// Token: 0x0400094E RID: 2382
	public Text deathText;

	// Token: 0x0400094F RID: 2383
	public Text dateText;

	// Token: 0x04000950 RID: 2384
	[SerializeField]
	private GameObject newHighscoreObject;

	// Token: 0x04000951 RID: 2385
	private PublishedFileId_t m_id;

	// Token: 0x04000952 RID: 2386
	private SteamUGCDetails_t m_mapDetails;

	// Token: 0x04000953 RID: 2387
	private UGCHandle_t m_ghostDataHandle;

	// Token: 0x04000954 RID: 2388
	private leaderboardsManager m_leaderBoardParent;

	// Token: 0x04000955 RID: 2389
	private CSteamID m_steamID;
}
