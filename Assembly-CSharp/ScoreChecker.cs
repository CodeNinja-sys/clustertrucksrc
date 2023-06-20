using System;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class ScoreChecker : MonoBehaviour
{
	// Token: 0x06000B44 RID: 2884 RVA: 0x00046634 File Offset: 0x00044834
	private void Start()
	{
		this.firstTry = true;
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00046640 File Offset: 0x00044840
	private void Update()
	{
		if (this.player.lastGrounded > 2f + info.extraAirTime && this.player.hasLandedFirstTime)
		{
			if (this.airTime == null)
			{
				this.airTime = this.handler.AddScore(0f, "AIR TIME", 1);
			}
			else if (!this.airTime.LOVE)
			{
				this.airTime = this.handler.AddScore(0f, "AIR TIME", 1);
			}
		}
		else if (this.airTime != null && this.airTime.LOVE)
		{
			this.airTime.LOVE = false;
		}
		if (this.airTime != null && this.airTime.LOVE)
		{
			this.airTime.score += Time.deltaTime * 500f * info.scoreMultiplier;
		}
		if (this.timeBonus > 0f)
		{
			this.timeBonus -= Time.unscaledDeltaTime * 100f;
		}
		else
		{
			this.timeBonus = 0f;
		}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00046774 File Offset: 0x00044974
	public void CheckEndGameScores()
	{
		if (!info.SkippingCurrentLevel)
		{
			steam_WorkshopHandler.Instance().UploadScoreToLeaderBoard(info.currentLevel.ToString(), float.Parse(GameManager.mapTime.ToString("F3")), (int)Mathf.Ceil(this.handler.totalScore));
		}
		else
		{
			Debug.Log("Skipped Level: No scores will be uploaded!");
		}
		info.SkippingCurrentLevel = false;
		this.scoreScreenHandler.getTricks(this.handler.totalScore);
		this.scoreScreenHandler.getTimeBonus(this.timeBonus);
		if (this.firstTry)
		{
			this.scoreScreenHandler.addScore(1000f, "ACE", "You completed the level on the first try");
		}
		if (info.completedLevels == info.currentLevel && info.currentLevel % 10 == 0)
		{
			this.scoreScreenHandler.addScore(2500f, "CONQUERED", "You completed a world");
		}
		this.scoreScreenHandler.addScore(500f, "DONE", "You completed a level");
		this.scoreScreenHandler.StartScoreScreen();
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00046888 File Offset: 0x00044A88
	public void StartCounting()
	{
		if (UnityEngine.Object.FindObjectOfType<goal>() != null)
		{
			info.levelLenght = Vector3.Distance(base.transform.root.position, UnityEngine.Object.FindObjectOfType<goal>().transform.position);
		}
		this.timeBonus = info.levelLenght * 6f;
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x000468E0 File Offset: 0x00044AE0
	private void OnEnable()
	{
		this.StartCounting();
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x000468E8 File Offset: 0x00044AE8
	public void Reset()
	{
		this.airTime = null;
		this.handler.Reset();
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x000468FC File Offset: 0x00044AFC
	public void CutIt(bool v)
	{
		if (!v)
		{
			this.firstTry = false;
		}
		this.handler.CutIt(v);
	}

	// Token: 0x040007E5 RID: 2021
	public player player;

	// Token: 0x040007E6 RID: 2022
	public scoreHandler handler;

	// Token: 0x040007E7 RID: 2023
	public scoreScreenHandler scoreScreenHandler;

	// Token: 0x040007E8 RID: 2024
	private scoreHandler.GameScore airTime;

	// Token: 0x040007E9 RID: 2025
	private scoreHandler.GameScore speedBonus;

	// Token: 0x040007EA RID: 2026
	[HideInInspector]
	public bool firstTry = true;

	// Token: 0x040007EB RID: 2027
	[HideInInspector]
	public bool lastInWorld;

	// Token: 0x040007EC RID: 2028
	private float timeBonus;

	// Token: 0x040007ED RID: 2029
	private float superSpeed;
}
