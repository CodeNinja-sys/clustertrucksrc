using System;
using System.Collections;
using InControl;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EE RID: 494
public class GameManager : MonoBehaviour
{
	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0004861C File Offset: 0x0004681C
	// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00048624 File Offset: 0x00046824
	public static float mapTime
	{
		get
		{
			return GameManager._mapTime;
		}
		set
		{
			if (value == 0f)
			{
				Debug.Log("Reseted Maptime: ");
			}
			GameManager._mapTime = value;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00048644 File Offset: 0x00046844
	public static bool Playtesting
	{
		get
		{
			return GameManager._playTesting;
		}
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0004864C File Offset: 0x0004684C
	private void Awake()
	{
		GameManager._playTesting = false;
		this.playTrailer = info.isShowBuild;
		this._player = UnityEngine.Object.FindObjectOfType<player>();
		this._rig = this._player.GetComponent<Rigidbody>();
		this.PlayerClock = UnityEngine.Object.FindObjectOfType<PlayerClock>();
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00048694 File Offset: 0x00046894
	private void Start()
	{
		this.hitbox = base.transform.FindChild("hitbox");
		this.startPos = this.hitbox.position;
		this.man = UnityEngine.Object.FindObjectOfType<Manager>();
		this.whiteScreen = false;
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x000486DC File Offset: 0x000468DC
	private void OnEnable()
	{
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x000486E0 File Offset: 0x000468E0
	private void Update()
	{
		if (info.isShowBuild && showBuild.Instance() != null && !showBuild.Instance().isPlaying && this.playTrailer)
		{
			this.checkPlayerInput();
		}
		if (this.man != null && info.playing)
		{
			GameManager.mapTime += Time.deltaTime;
			if (this.PlayerClock.gameObject.activeInHierarchy)
			{
				if (info.ShowClock || Singleton<RecordedDataPrefabPlayer>.Instance.P_PlaybackState == RecordedDataPrefabPlayer.PlaybackState.Playing)
				{
					this.PlayerClock.SetTimeText(GameManager.mapTime.ToString("F3").Replace('.', ':'));
				}
				else
				{
					this.PlayerClock.Flush();
				}
			}
		}
		if (this.man != null && ((Input.GetKeyDown(KeyCode.Alpha9) && Input.GetKey(KeyCode.Alpha7) && !Input.GetKey(KeyCode.Alpha8)) || (this.man.usingMapcyclePublic && (Input.GetKey(KeyCode.Y) || InputManager.ActiveDevice.Action4.WasPressed))) && Application.isEditor)
		{
			if (info.playing)
			{
				if (!Application.isEditor)
				{
					info.SkippingCurrentLevel = true;
				}
				this.WinLevel();
			}
			else
			{
				this.man.NextLevel();
			}
		}
		if (Manager.canSkipCurrentMap && (InputManager.ActiveDevice.Action4.WasPressed || Input.GetKeyDown(KeyCode.Y)))
		{
			if (!Application.isEditor)
			{
				info.SkippingCurrentLevel = true;
			}
			this.WinLevel();
		}
		if (Input.GetKeyDown(KeyCode.R) && info.playing)
		{
			if (!GameManager._playTesting)
			{
				if (info.playing)
				{
					this.RestartLevel();
				}
			}
			else
			{
				levelEditorManager.Instance().restartPlayTest();
			}
		}
		if (this.done)
		{
			this.sinceWon += Time.unscaledDeltaTime;
		}
		if ((Input.anyKeyDown || InputManager.ActiveDevice.AnyButton.WasPressed) && !this.levelOverUsed && this.sinceWon > 0.2f && !this.won)
		{
			if (!GameManager._playTesting)
			{
				this.sinceWon = 0f;
				this.levelOverUsed = true;
				if (this.won)
				{
					this.NextLevel();
				}
				else
				{
					this.RestartLevel();
				}
			}
			else
			{
				levelEditorManager.Instance().restartPlayTest();
			}
		}
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00048980 File Offset: 0x00046B80
	private void LateUpdate()
	{
		if (Input.anyKey || Input.GetAxis("Mouse X") > 0f || Input.GetAxis("Mouse Y") > 0f || InputManager.ActiveDevice.AnyButton.IsPressed || InputManager.ActiveDevice.LeftStick.Value.magnitude > 0.1f || InputManager.ActiveDevice.RightStick.Value.magnitude > 0.1f || InputManager.ActiveDevice.LeftTrigger.IsPressed || InputManager.ActiveDevice.RightTrigger.IsPressed)
		{
			this._afkTimer = 0f;
		}
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x00048A48 File Offset: 0x00046C48
	private void LevelOver()
	{
		this.done = true;
		if (this.man != null)
		{
			info.playing = false;
		}
		this.hitbox.SendMessage("StopMoving");
		Camera.main.GetComponent<cameraEffects>().SetShake(1f, Vector3.zero);
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x00048A9C File Offset: 0x00046C9C
	public void WinLevel()
	{
		if (!this.done)
		{
			if (!GameManager._playTesting)
			{
				info.lastPlayedLevel = info.currentLevel;
				info.playing = false;
				Singleton<DataRecorder>.Instance.SaveToFile();
				this.man.WinLevel();
				this.scoreCheck.CutIt(true);
			}
			this.LevelOver();
			this.won = true;
			GameManager.totalDeaths += GameManager.mapDeaths;
			GameManager.totalTime += GameManager.mapTime;
			GameManager.mapDeaths = 0;
			base.StartCoroutine(this.popUI(this.winScreen.transform));
		}
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x00048B3C File Offset: 0x00046D3C
	public void LoseLevel()
	{
		if (!GameManager.Playtesting)
		{
			if (!this.done)
			{
				this.scoreCheck.CutIt(false);
				this.LevelOver();
				this.won = false;
				GameManager.mapDeaths++;
				base.StartCoroutine(this.popUI(this.deathScreen.transform));
			}
		}
		else if (!this.done)
		{
			this.LevelOver();
			this.won = false;
			base.StartCoroutine(this.popUI(this.deathScreen.transform));
		}
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00048BD0 File Offset: 0x00046DD0
	private IEnumerator popUI(Transform tra)
	{
		yield return new WaitForSeconds(0.2f);
		tra.gameObject.SetActive(true);
		Camera.main.GetComponent<cameraEffects>().SetUIShake(2f);
		if (this.won && !GameManager.Playtesting)
		{
			this.scoreCheck.CheckEndGameScores();
		}
		yield return new WaitForSeconds(1f);
		if (GameManager.Playtesting)
		{
			levelEditorManager.Instance().PlaytestEscape();
		}
		yield break;
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x00048BFC File Offset: 0x00046DFC
	public void NextLevel()
	{
		this.scoreCheck.firstTry = true;
		if (this.man != null)
		{
			this.man.NextLevel();
		}
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x00048C34 File Offset: 0x00046E34
	public void RestartLevel()
	{
		if (GameManager.mapTime > 5f)
		{
			GameManager.mapDeaths++;
		}
		this.scoreCheck.firstTry = false;
		this.man.RestartLevel();
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x00048C74 File Offset: 0x00046E74
	private void checkPlayerInput()
	{
		this._afkTimer += Time.deltaTime;
		if (this._afkTimer > 15f)
		{
			this._afkTimer = 0f;
			showBuild.Instance().playVideo();
			Manager.Instance().ClearLevel();
			info.currentLevel = 0;
			helpText.reset();
			UnityEngine.Object.FindObjectOfType<MenuMusic>().GetComponent<AudioSource>().Stop();
			AudioListener.volume = 1f;
		}
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x00048CE8 File Offset: 0x00046EE8
	public void ResetMe()
	{
		if (!leaderboardsManager.P_HasGhost)
		{
			base.GetComponentInChildren<AssignGhostInfo>(true).Reset();
		}
		info.playing = true;
		this.scoreScreenH.Reset();
		this.scoreCheck.Reset();
		this.whiteScreen = false;
		this.won = false;
		this.done = false;
		this.levelOverUsed = false;
		this.sinceWon = 0f;
		GameManager.mapTime = 0f;
		this.deathScreen.SetActive(false);
		this.winScreen.SetActive(false);
		info.paused = false;
		Ray ray = new Ray(Vector3.zero + Vector3.up * 5f, Vector3.down);
		float num = 0f;
		foreach (RaycastHit raycastHit in Physics.RaycastAll(ray, 10f))
		{
			if (raycastHit.point.y > num && raycastHit.transform.gameObject.layer == 0)
			{
				num = raycastHit.point.y;
			}
		}
		this._player.transform.position = new Vector3(0f, num + 2f, 0f);
		this._player.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		this._player.canMove = true;
		this._player.dead = false;
		this._player.frozen = false;
		this._player.framesSinceStart = 0f;
		this._player.transform.FindChild("camHolder").transform.localRotation = Quaternion.identity;
		this._player.hasLandedFirstTime = false;
		this._player.lastGrounded = 0f;
		this._player.hasTouchedGround = true;
		this._player.jumpCd = 1f;
		this._rig.useGravity = true;
		this._rig.velocity *= 0f;
		RecordingDataTransmitterTransform recordingDataTransmitterTransform = this._player.gameObject.AddComponent<RecordingDataTransmitterTransform>();
		recordingDataTransmitterTransform.P_PrefabName = "PlayerGhost";
		recordingDataTransmitterTransform.Start();
		Debug.Log("Added Player Transmitter");
		this._player.FindTruck();
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x00048F50 File Offset: 0x00047150
	public void playTest()
	{
		GameManager._playTesting = true;
	}

	// Token: 0x0400083C RID: 2108
	private const int TRAILER_TIME = 15;

	// Token: 0x0400083D RID: 2109
	private bool playTrailer;

	// Token: 0x0400083E RID: 2110
	private player _player;

	// Token: 0x0400083F RID: 2111
	private Rigidbody _rig;

	// Token: 0x04000840 RID: 2112
	private float distance;

	// Token: 0x04000841 RID: 2113
	private Vector3 startPos;

	// Token: 0x04000842 RID: 2114
	public Text scoreText;

	// Token: 0x04000843 RID: 2115
	public Text infoText;

	// Token: 0x04000844 RID: 2116
	public int levelDistance = 500;

	// Token: 0x04000845 RID: 2117
	private Manager man;

	// Token: 0x04000846 RID: 2118
	private Transform hitbox;

	// Token: 0x04000847 RID: 2119
	public GameObject winScreen;

	// Token: 0x04000848 RID: 2120
	public GameObject deathScreen;

	// Token: 0x04000849 RID: 2121
	private PlayerClock PlayerClock;

	// Token: 0x0400084A RID: 2122
	private bool done;

	// Token: 0x0400084B RID: 2123
	[HideInInspector]
	public bool won;

	// Token: 0x0400084C RID: 2124
	private float _afkTimer;

	// Token: 0x0400084D RID: 2125
	private static float _mapTime;

	// Token: 0x0400084E RID: 2126
	public static float totalTime;

	// Token: 0x0400084F RID: 2127
	public static int mapDeaths;

	// Token: 0x04000850 RID: 2128
	public static int totalDeaths;

	// Token: 0x04000851 RID: 2129
	private bool whiteScreen;

	// Token: 0x04000852 RID: 2130
	private float sinceWon;

	// Token: 0x04000853 RID: 2131
	private bool levelOverUsed;

	// Token: 0x04000854 RID: 2132
	public scoreScreenHandler scoreScreenH;

	// Token: 0x04000855 RID: 2133
	public ScoreChecker scoreCheck;

	// Token: 0x04000856 RID: 2134
	private static bool _playTesting;
}
