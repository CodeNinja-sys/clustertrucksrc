using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002FD RID: 765
public class scoreScreenHandler : MonoBehaviour
{
	// Token: 0x06001206 RID: 4614 RVA: 0x0007343C File Offset: 0x0007163C
	private void OnEnable()
	{
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x00073440 File Offset: 0x00071640
	public void addScore(float scoreValue, string scoreName, string scoreDescription)
	{
		this.scores.Add(new scoreScreenHandler.ScoreClass(scoreValue, scoreName, scoreDescription));
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x00073458 File Offset: 0x00071658
	private void Update()
	{
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x0007345C File Offset: 0x0007165C
	public void StartScoreScreen()
	{
		base.StartCoroutine(this.ShowScore());
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0007346C File Offset: 0x0007166C
	public void getTricks(float f)
	{
		string scoreDescription = string.Empty;
		if (f > 0f)
		{
			scoreDescription = string.Empty;
		}
		if (f > 500f)
		{
			scoreDescription = string.Empty;
		}
		if (f > 1000f)
		{
			scoreDescription = string.Empty;
		}
		if (f > 2000f)
		{
			scoreDescription = string.Empty;
		}
		if (f > 3000f)
		{
			scoreDescription = string.Empty;
		}
		if (f > 4000f)
		{
			scoreDescription = "AWESOME";
		}
		if (f > 5000f)
		{
			scoreDescription = "FANTASTIC";
		}
		if (f > 6000f)
		{
			scoreDescription = "AMAZING";
		}
		if (f > 7000f)
		{
			scoreDescription = "unbelievable";
		}
		if (f > 8000f)
		{
			scoreDescription = "incredible";
		}
		if (f > 8000f)
		{
			scoreDescription = "trucking incredible";
		}
		if (f > 10000f)
		{
			scoreDescription = "mother trucking incredble";
		}
		if (f > 100000f)
		{
			scoreDescription = "We dont believe you";
		}
		if (f > 0f)
		{
			this.addScore(f, "tricks", scoreDescription);
		}
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x00073574 File Offset: 0x00071774
	public void getTimeBonus(float f)
	{
		string empty = string.Empty;
		if (f > 0f)
		{
			empty = string.Empty;
		}
		if (f > 300f)
		{
			empty = string.Empty;
		}
		if (f > 400f)
		{
			empty = string.Empty;
		}
		if (f > 500f)
		{
			empty = string.Empty;
		}
		if (f > 600f)
		{
			empty = string.Empty;
		}
		if (f > 700f)
		{
			empty = string.Empty;
		}
		if (f > 1000f)
		{
			empty = string.Empty;
		}
		if (f > 0f)
		{
			this.addScore(f, "speed bonus", empty);
		}
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x00073618 File Offset: 0x00071818
	private IEnumerator ShowScore()
	{
		base.transform.parent.parent.GetComponentInChildren<AssignGhostInfo>(true).Reset();
		base.GetComponentInChildren<LeaderboardGhostHandler>(true).UpdateDropdown();
		this.buttons.SetActive(false);
		this.leaderBoardsGhostButtons.SetActive(false);
		this.scoreStuff.SetActive(true);
		this.firstAbilityScreen.SetActive(false);
		if (info.PlayingGhostFromLeaderBoard)
		{
			this.leaderBoardsGhostButtons.SetActive(true);
		}
		else
		{
			this.buttons.SetActive(true);
		}
		bool firstAbility = false;
		int totalPoints = 0;
		for (int i = 0; i < this.scores.Count; i++)
		{
			totalPoints += (int)this.scores[i].Score;
			pointsHandler.AddPoints(this.scores[i].Score);
		}
		if (pointsHandler.Points > 15000f && PlayerPrefs.GetInt("firstAbility", 0) != 1)
		{
			PlayerPrefs.SetInt("firstAbility", 1);
			firstAbility = true;
			this.buttons.SetActive(false);
			this.leaderBoardsGhostButtons.SetActive(false);
		}
		yield return new WaitForSeconds(0.3f);
		for (int j = 0; j < this.scores.Count; j++)
		{
			this.ScoreAnim.Play("appear");
			this.scoreNameText.text = this.scores[j].Name.ToUpper();
			this.scoreDescriptionText.text = this.scores[j].Description.ToUpper();
			this.scoreValueText.text = "0";
			yield return new WaitForSeconds(0.1f);
			float extraTime = 0.1f;
			int i2 = 0;
			while ((float)i2 < this.scores[j].Score)
			{
				this.scoreValueText.text = i2.ToString("F0");
				if (extraTime > 0f)
				{
					extraTime -= Time.deltaTime;
				}
				this.au.PlayOneShot(this.count);
				yield return null;
				i2 += 20 + (int)(this.scores[j].Score / 50f);
			}
			this.scoreValueText.text = this.scores[j].Score.ToString("F0");
			yield return new WaitForSeconds(0.2f + extraTime);
			this.au.PlayOneShot(this.swosh);
			this.ScoreAnim.Play("add");
			yield return new WaitForSeconds(0.35f);
			this.addScoreToFinal(this.scores[j].Score);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.5f);
		this.pointsValue.text = pointsHandler.Points.ToString("F0");
		this.pointsAnim.Play("Go");
		this.au.PlayOneShot(this.swosh);
		if (firstAbility)
		{
			yield return new WaitForSeconds(1f);
			this.scoreStuff.SetActive(false);
			this.firstAbilityScreen.SetActive(true);
			this.au.PlayOneShot(this.add);
			this.firstAbilityScreen.GetComponent<shakeObject>().shake = 20f;
		}
		else
		{
			yield return new WaitForSeconds(1f);
			this.leaderboardMan.transform.parent.gameObject.SetActive(true);
			this.leaderboardMan.SmallEnable(info.currentLevel);
			this.leaderboardMan.transform.parent.GetComponent<Animator>().Play("leaderboardsEnter");
		}
		yield break;
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x00073634 File Offset: 0x00071834
	private void addScoreToFinal(float f)
	{
		this.au.PlayOneShot(this.add);
		this.finalScore += f;
		this.finalValue.text = this.finalScore.ToString("F0");
		this.shake.shake = 20f;
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x0007368C File Offset: 0x0007188C
	public void Reset()
	{
		this.leaderboardMan.transform.parent.gameObject.SetActive(false);
		this.leaderboardMan.DisableMenu();
		this.finalScore = 0f;
		this.finalValue.text = "0";
		this.scores.Clear();
	}

	// Token: 0x04000F26 RID: 3878
	public float finalScore;

	// Token: 0x04000F27 RID: 3879
	public Text scoreValueText;

	// Token: 0x04000F28 RID: 3880
	public Text scoreNameText;

	// Token: 0x04000F29 RID: 3881
	public Text scoreDescriptionText;

	// Token: 0x04000F2A RID: 3882
	public Text finalValue;

	// Token: 0x04000F2B RID: 3883
	public Text pointsValue;

	// Token: 0x04000F2C RID: 3884
	public Animator ScoreAnim;

	// Token: 0x04000F2D RID: 3885
	public Animator pointsAnim;

	// Token: 0x04000F2E RID: 3886
	public shakeObject shake;

	// Token: 0x04000F2F RID: 3887
	public List<scoreScreenHandler.ScoreClass> scores = new List<scoreScreenHandler.ScoreClass>();

	// Token: 0x04000F30 RID: 3888
	public AudioSource au;

	// Token: 0x04000F31 RID: 3889
	public AudioClip smack;

	// Token: 0x04000F32 RID: 3890
	public AudioClip count;

	// Token: 0x04000F33 RID: 3891
	public AudioClip add;

	// Token: 0x04000F34 RID: 3892
	public AudioClip swosh;

	// Token: 0x04000F35 RID: 3893
	public GameObject firstAbilityScreen;

	// Token: 0x04000F36 RID: 3894
	public GameObject buttons;

	// Token: 0x04000F37 RID: 3895
	public GameObject leaderBoardsGhostButtons;

	// Token: 0x04000F38 RID: 3896
	public GameObject scoreStuff;

	// Token: 0x04000F39 RID: 3897
	public leaderboardsManager leaderboardMan;

	// Token: 0x020002FE RID: 766
	public class ScoreClass
	{
		// Token: 0x0600120F RID: 4623 RVA: 0x000736E8 File Offset: 0x000718E8
		public ScoreClass(float score, string name, string description)
		{
			this._score = score;
			this._name = name;
			this._description = description;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00073708 File Offset: 0x00071908
		public float Score
		{
			get
			{
				return this._score;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00073710 File Offset: 0x00071910
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00073718 File Offset: 0x00071918
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x04000F3A RID: 3898
		private float _score;

		// Token: 0x04000F3B RID: 3899
		private string _name;

		// Token: 0x04000F3C RID: 3900
		private string _description;
	}
}
