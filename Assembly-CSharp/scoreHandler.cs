using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002FB RID: 763
public class scoreHandler : MonoBehaviour
{
	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000731B0 File Offset: 0x000713B0
	public static scoreHandler Instance
	{
		get
		{
			return scoreHandler._Instance;
		}
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x000731B8 File Offset: 0x000713B8
	private void Awake()
	{
		if (scoreHandler._Instance != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		scoreHandler._Instance = this;
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x000731D8 File Offset: 0x000713D8
	private void Start()
	{
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x000731DC File Offset: 0x000713DC
	private void Update()
	{
		if (!this.busy && this.scoreQueue.Count > 0 && !this.dead && !this.won)
		{
			scoreHandler.GameScore score = this.scoreQueue.Dequeue() as scoreHandler.GameScore;
			base.StartCoroutine(this.ShowScore(score));
			this.busy = true;
		}
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x00073244 File Offset: 0x00071444
	public scoreHandler.GameScore AddScore(float scoreValue, string scoreName, int type)
	{
		scoreValue *= info.scoreMultiplier;
		scoreHandler.GameScore gameScore = new scoreHandler.GameScore(scoreValue, scoreName, type);
		if (type == 1)
		{
			gameScore.LOVE = true;
		}
		this.scoreQueue.Enqueue(gameScore);
		return gameScore;
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x00073280 File Offset: 0x00071480
	private void AddScoreToTotal(float scoreToAdd)
	{
		this.totalScore += scoreToAdd;
		this.totalText.text = this.totalScore.ToString("F0");
		this.shake.shake = 25f;
		this.audioSource.PlayOneShot(this.addSound, 0.5f);
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x000732DC File Offset: 0x000714DC
	public void CutIt(bool v)
	{
		if (!v)
		{
			this.dead = true;
			this.scoreQueue.Clear();
			base.StartCoroutine(this.killScore());
		}
		else
		{
			this.won = true;
			for (int i = 0; i < this.scoreQueue.Count; i++)
			{
				scoreHandler.GameScore gameScore = this.scoreQueue.Dequeue() as scoreHandler.GameScore;
				this.AddScoreToTotal(gameScore.score);
			}
		}
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x00073354 File Offset: 0x00071554
	private IEnumerator killScore()
	{
		this.totalScore = 0f;
		this.shake.shake = 25f;
		this.totalText.text = "0";
		this.scoreAmim.SetBool("dead", true);
		yield return new WaitForSeconds(0f);
		yield break;
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x00073370 File Offset: 0x00071570
	private IEnumerator ShowScore(scoreHandler.GameScore score)
	{
		if (this.dead)
		{
			yield break;
		}
		if (this.won)
		{
			this.AddScoreToTotal(score.score);
			yield break;
		}
		this.valueText.text = score.score.ToString("F0");
		this.nameText.text = score.Name.ToUpper();
		if (score.Type == 1)
		{
			this.scoreAmim.Play("enterSoft");
		}
		else
		{
			this.scoreAmim.Play("enter");
			this.audioSource.PlayOneShot(this.boomSound);
			if (info.scoreMultiplier > 1.5f)
			{
				this.au.PlayOneShot(this.au.clip);
			}
		}
		while (score.LOVE)
		{
			if (this.dead)
			{
				yield break;
			}
			if (this.won)
			{
				this.AddScoreToTotal(score.score);
				yield break;
			}
			this.shake2.shake = 4f;
			this.valueText.text = score.score.ToString("F0");
			this.audioSource.PlayOneShot(this.tickSound, 0.5f);
			yield return new WaitForSeconds(0f);
		}
		if (this.dead)
		{
			yield break;
		}
		if (this.won)
		{
			this.AddScoreToTotal(score.score);
			yield break;
		}
		if (score.Type == 1)
		{
			this.scoreAmim.Play("boom");
			this.audioSource.PlayOneShot(this.boomSound);
			if (info.scoreMultiplier > 1.5f)
			{
				this.au.PlayOneShot(this.au.clip);
			}
		}
		if (this.won)
		{
			this.AddScoreToTotal(score.score);
			yield break;
		}
		yield return new WaitForSeconds(0.5f);
		if (this.dead)
		{
			yield break;
		}
		if (this.won)
		{
			this.AddScoreToTotal(score.score);
			yield break;
		}
		this.scoreAmim.Play("leave");
		this.audioSource.PlayOneShot(this.swoshSound);
		yield return new WaitForSeconds(0.3f);
		if (this.dead)
		{
			yield break;
		}
		if (this.won)
		{
			this.AddScoreToTotal(score.score);
			yield break;
		}
		this.AddScoreToTotal(score.score);
		this.busy = false;
		yield break;
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x0007339C File Offset: 0x0007159C
	public void Reset()
	{
		this.totalScore = 0f;
		this.totalText.text = "0";
		this.scoreAmim.SetBool("dead", false);
		this.scoreQueue.Clear();
		this.busy = false;
		this.dead = false;
		this.won = false;
	}

	// Token: 0x04000F0F RID: 3855
	public Queue scoreQueue = new Queue();

	// Token: 0x04000F10 RID: 3856
	public Text valueText;

	// Token: 0x04000F11 RID: 3857
	public Text nameText;

	// Token: 0x04000F12 RID: 3858
	public Text totalText;

	// Token: 0x04000F13 RID: 3859
	public Animator scoreAmim;

	// Token: 0x04000F14 RID: 3860
	public shakeObject shake;

	// Token: 0x04000F15 RID: 3861
	public shakeObject shake2;

	// Token: 0x04000F16 RID: 3862
	private scoreHandler.GameScore _currentTrick;

	// Token: 0x04000F17 RID: 3863
	public AudioSource au;

	// Token: 0x04000F18 RID: 3864
	public AudioSource audioSource;

	// Token: 0x04000F19 RID: 3865
	public AudioClip boomSound;

	// Token: 0x04000F1A RID: 3866
	public AudioClip tickSound;

	// Token: 0x04000F1B RID: 3867
	public AudioClip swoshSound;

	// Token: 0x04000F1C RID: 3868
	public AudioClip addSound;

	// Token: 0x04000F1D RID: 3869
	private bool busy;

	// Token: 0x04000F1E RID: 3870
	private bool dead;

	// Token: 0x04000F1F RID: 3871
	private bool won;

	// Token: 0x04000F20 RID: 3872
	[HideInInspector]
	public float totalScore;

	// Token: 0x04000F21 RID: 3873
	private static scoreHandler _Instance;

	// Token: 0x020002FC RID: 764
	public class GameScore
	{
		// Token: 0x06001202 RID: 4610 RVA: 0x000733F8 File Offset: 0x000715F8
		public GameScore(float score, string name, int type)
		{
			this.score = score;
			this._name = name;
			this._type = type;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00073418 File Offset: 0x00071618
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00073420 File Offset: 0x00071620
		public int Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000F22 RID: 3874
		public float score;

		// Token: 0x04000F23 RID: 3875
		private string _name;

		// Token: 0x04000F24 RID: 3876
		private int _type;

		// Token: 0x04000F25 RID: 3877
		public bool LOVE;
	}
}
