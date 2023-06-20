using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class LastBoss : MonoBehaviour
{
	// Token: 0x060006B3 RID: 1715 RVA: 0x0002CE64 File Offset: 0x0002B064
	private void Awake()
	{
		this.mAttackSource.clip = this.mBossPunchClip;
		this.mAttackFlameSource.clip = this.mBossFlameClip;
		this.mBackFireAudioSource.clip = this.mBossFlameClip;
		this.mTimeUntilIdlePlays = (float)UnityEngine.Random.Range(3, 6);
		this.mIndexOfNextIdle = UnityEngine.Random.Range(0, this.mIdleClips.Length);
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0002CEC8 File Offset: 0x0002B0C8
	private void Start()
	{
		this.devil = base.transform.GetChild(0).transform;
		this.backSide = this.devil.FindChild("backTruck");
		this.otherArms = this.devil.FindChild("otherArms");
		this.leftArms = this.devil.FindChild("leftArms");
		this.flameArm = this.devil.FindChild("flameArm");
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0002CF44 File Offset: 0x0002B144
	private void CheckIdleSound()
	{
		if (this.mBossScreamSource.isPlaying)
		{
			return;
		}
		if (this.mBackFireAudioSource.isPlaying)
		{
			return;
		}
		this.mTimeUntilIdlePlays -= Time.deltaTime;
		if (this.mIdleClips[this.mIndexOfNextIdle].length > this.hitCd && !this.mIsClimbing)
		{
			return;
		}
		if (this.mTimeUntilIdlePlays <= 0f && !this.mBossScreamSource.isPlaying)
		{
			this.mBossScreamSource.PlayOneShot(this.mIdleClips[this.mIndexOfNextIdle]);
			this.mTimeUntilIdlePlays = (float)UnityEngine.Random.Range(1, 4);
			this.mIndexOfNextIdle = UnityEngine.Random.Range(0, this.mIdleClips.Length);
		}
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0002D008 File Offset: 0x0002B208
	private void Update()
	{
		this.CheckIdleSound();
		if (Input.GetKeyDown(KeyCode.P))
		{
			this.Damage();
		}
		this.truckRemover.enabled = true;
		if (this.timeSincePhaseChange < 10f && this.timeSincePhaseChange > 2f)
		{
			this.angryFire.enableEmission = true;
			this.playerKiller.enabled = true;
			if (!this.mBackFireAudioSource.isPlaying)
			{
				this.mBackFireAudioSource.Play();
			}
		}
		else
		{
			this.angryFire.enableEmission = false;
			this.playerKiller.enabled = false;
			this.mBackFireAudioSource.Stop();
		}
		if (this.timeSincePhaseChange < 40f)
		{
			this.trigger.enabled = false;
		}
		else
		{
			this.trigger.enabled = true;
		}
		if (this.target)
		{
			this.mIsClimbing = (this.climbing > 0f || this.target.position.y > 50f);
			if (this.mIsClimbing && this.timeSincePhaseChange > 30f)
			{
				if (this.lavaFloor.position.y < 115f)
				{
					this.lavaFloor.transform.position += Time.deltaTime * 4.5f * Vector3.up;
				}
				else
				{
					this.lavaFloor.transform.position += Time.deltaTime * 0.2f * Vector3.up;
				}
			}
			else
			{
				this.lavaFloor.transform.position = Vector3.Lerp(this.lavaFloor.position, new Vector3(0f, -45f, 0f), Time.deltaTime * 1f);
			}
			if (this.target.position.y > 50f)
			{
				this.truckRemover.enabled = true;
			}
			else
			{
				this.truckRemover.enabled = false;
			}
			this.rightArmRoot.localPosition = Vector3.Lerp(this.rightArmRoot.localPosition, new Vector3(-8f, 0.7f, this.rightArmPos), Time.deltaTime * 0.5f);
			this.timeSincePhaseChange += Time.deltaTime;
			if (this.climbing < 0f && this.target.position.y < 45f)
			{
				this.rotFix.enabled = true;
				this.hitCd -= Time.deltaTime;
				if (this.phase > 0 && this.timeSincePhaseChange > 5f)
				{
					if (this.hitCd < 0f)
					{
						this.rightArmPos = UnityEngine.Random.Range(0f, 10f);
						this.rightArm.CrossFade("ArmsHit", 0.3f, 0);
						Camera.main.GetComponent<cameraEffects>().SetShake(0.2f, Vector3.zero);
						if (this.mBossScreamSource.isPlaying)
						{
							this.mBossScreamSource.Stop();
						}
						this.mBossScreamSource.PlayOneShot(this.mBossGroanClip);
						this.hitCd = 10f;
					}
					if (this.hitCd < 4f || this.hitCd > 9.5f)
					{
						Vector3 vector = this.otherArms.InverseTransformPoint(this.target.position + this.target.GetComponent<Rigidbody>().velocity * 5f);
						this.otherArms.Rotate(Vector3.up * vector.x * Time.deltaTime * 1f);
						this.leftArms.Rotate(Vector3.up * vector.x * Time.deltaTime * 0.7f);
					}
				}
				if (this.phase > 1)
				{
					if (this.fireHasStarted)
					{
						if (this.timeSincePhaseChange > 12f)
						{
							if (!this.mAttackFlameSource.isPlaying)
							{
								this.mAttackFlameSource.Play();
							}
							this.fire.enableEmission = true;
							this.sparks.enableEmission = true;
							this.fireCol.enabled = true;
							this.flameArm.Rotate(Vector3.up * Time.deltaTime * 14f);
						}
					}
					else
					{
						this.direction = Mathf.Clamp(this.flameArm.transform.InverseTransformPoint(this.target.position).x * 100f, -1f, 1f);
						this.flameArm.Rotate(Vector3.up * this.direction * Time.deltaTime * 30f);
						this.mAttackFlameSource.Stop();
						this.fire.enableEmission = false;
						this.sparks.enableEmission = false;
						this.fireCol.enabled = false;
						if (this.timeSincePhaseChange > 10f)
						{
							this.fireHasStarted = true;
						}
					}
				}
				if (this.phase > 2)
				{
				}
			}
			else
			{
				this.rotFix.enabled = false;
				this.climbing -= Time.deltaTime;
				this.otherArms.localPosition = Vector3.Lerp(this.otherArms.localPosition, new Vector3(0f, 32f, 0f), Time.deltaTime * this.timeSincePhaseChange * (25f - this.climbing) * 4f);
				this.otherArms.localRotation = Quaternion.Lerp(this.otherArms.localRotation, Quaternion.Euler(new Vector3(0f, -45f, 0f)), Time.deltaTime * (25f - this.climbing) * 2f);
				this.otherArms.localPosition = Vector3.Lerp(this.otherArms.localPosition, new Vector3(0f, 32f, 0f), Time.deltaTime * this.timeSincePhaseChange * (25f - this.climbing) * 2f);
				this.leftArms.localRotation = Quaternion.Lerp(this.leftArms.localRotation, Quaternion.Euler(new Vector3(0f, -45f, 0f)), Time.deltaTime * (25f - this.climbing) * 1.5f);
				this.fire.enableEmission = false;
				this.sparks.enableEmission = false;
				this.fireCol.enabled = false;
				this.arm.enabled = false;
				this.hit.enabled = false;
			}
		}
		else
		{
			this.target = UnityEngine.Object.FindObjectOfType<player>().transform;
		}
		if (this.climbing < 23f)
		{
			this.arm.enabled = true;
			this.hit.enabled = true;
		}
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0002D75C File Offset: 0x0002B95C
	public void Damage()
	{
		this.mBossScreamSource.PlayOneShot(this.mBossDamageClip);
		this.climbing = -10f;
		if (this.phase == 3)
		{
			this.timeSincePhaseChange = 0f;
			this.TheEnd();
			return;
		}
		if (this.timeSincePhaseChange > 40f)
		{
			this.timeSincePhaseChange = 0f;
			Camera.main.GetComponent<cameraEffects>().SetShake(2f, Vector3.zero);
			this.ButtonMouth.Play("close");
			this.fireHasStarted = false;
			this.phase++;
			this.target.GetComponent<Rigidbody>().AddForce(Vector3.up * 50f, ForceMode.VelocityChange);
			if (this.phase > 2)
			{
				this.lasers.SetActive(true);
			}
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0002D834 File Offset: 0x0002BA34
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.climbing = 25f;
			this.rightArm.CrossFade("ArmLongHit", 0.3f, 0);
			this.leftArm.CrossFade("ArmHold", 0.3f, 0);
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0002D890 File Offset: 0x0002BA90
	private void TheEnd()
	{
		Singleton<HaxxBossSound>.Instance.ForceKill();
		this.target.root.GetComponentInChildren<ScoreChecker>().enabled = false;
		this.endExplosion.gameObject.SetActive(true);
		this.target.GetComponent<Rigidbody>().AddForce(Vector3.up * 150f, ForceMode.VelocityChange);
		Camera.main.GetComponent<cameraEffects>().SetShake(0.5f, Vector3.zero);
		base.StartCoroutine(this.Boom());
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0002D914 File Offset: 0x0002BB14
	private IEnumerator Boom()
	{
		this.mBossScreamSource.PlayOneShot(this.mDeathClip);
		yield return new WaitForSeconds(1f);
		Camera.main.GetComponent<cameraEffects>().SetShake(0.5f, Vector3.zero);
		yield return new WaitForSeconds(0.4f);
		Camera.main.GetComponent<cameraEffects>().SetShake(0.6f, Vector3.zero);
		yield return new WaitForSeconds(0.3f);
		Camera.main.GetComponent<cameraEffects>().SetShake(0.7f, Vector3.zero);
		yield return new WaitForSeconds(0.3f);
		Camera.main.GetComponent<cameraEffects>().SetShake(1f, Vector3.zero);
		yield return new WaitForSeconds(1f);
		Camera.main.GetComponent<cameraEffects>().SetShake(2f, Vector3.zero);
		yield return new WaitForSeconds(0.5f);
		this.credits.SetActive(true);
		yield return new WaitForSeconds(1f);
		this.theRest.SetActive(false);
		UnityEngine.Object.Destroy(this.target.root.gameObject);
		yield return null;
		yield break;
	}

	// Token: 0x040004C6 RID: 1222
	private Transform target;

	// Token: 0x040004C7 RID: 1223
	private Transform devil;

	// Token: 0x040004C8 RID: 1224
	private Transform backSide;

	// Token: 0x040004C9 RID: 1225
	private Transform otherArms;

	// Token: 0x040004CA RID: 1226
	private Transform leftArms;

	// Token: 0x040004CB RID: 1227
	private Transform flameArm;

	// Token: 0x040004CC RID: 1228
	public ParticleSystem fire;

	// Token: 0x040004CD RID: 1229
	public ParticleSystem sparks;

	// Token: 0x040004CE RID: 1230
	public ParticleSystem angryFire;

	// Token: 0x040004CF RID: 1231
	public Collider fireCol;

	// Token: 0x040004D0 RID: 1232
	private float climbing = -1f;

	// Token: 0x040004D1 RID: 1233
	private float direction;

	// Token: 0x040004D2 RID: 1234
	public Animator leftArm;

	// Token: 0x040004D3 RID: 1235
	public Animator rightArm;

	// Token: 0x040004D4 RID: 1236
	public RotationFix rotFix;

	// Token: 0x040004D5 RID: 1237
	private int phase = 1;

	// Token: 0x040004D6 RID: 1238
	private float hitCd = 5f;

	// Token: 0x040004D7 RID: 1239
	private bool fireHasStarted;

	// Token: 0x040004D8 RID: 1240
	private float timeSincePhaseChange = 60f;

	// Token: 0x040004D9 RID: 1241
	public Transform rightArmRoot;

	// Token: 0x040004DA RID: 1242
	public Transform lavaFloor;

	// Token: 0x040004DB RID: 1243
	private float rightArmPos;

	// Token: 0x040004DC RID: 1244
	public Collider trigger;

	// Token: 0x040004DD RID: 1245
	public Collider arm;

	// Token: 0x040004DE RID: 1246
	public Collider hit;

	// Token: 0x040004DF RID: 1247
	public Collider truckRemover;

	// Token: 0x040004E0 RID: 1248
	public Collider playerKiller;

	// Token: 0x040004E1 RID: 1249
	public GameObject lasers;

	// Token: 0x040004E2 RID: 1250
	public GameObject credits;

	// Token: 0x040004E3 RID: 1251
	public GameObject theRest;

	// Token: 0x040004E4 RID: 1252
	public Animator ButtonMouth;

	// Token: 0x040004E5 RID: 1253
	public GameObject endExplosion;

	// Token: 0x040004E6 RID: 1254
	[SerializeField]
	[Header("Sound")]
	private AudioSource mMusicSource;

	// Token: 0x040004E7 RID: 1255
	[SerializeField]
	private AudioSource mBossScreamSource;

	// Token: 0x040004E8 RID: 1256
	[SerializeField]
	private AudioSource mAttackSource;

	// Token: 0x040004E9 RID: 1257
	[SerializeField]
	private AudioSource mAttackFlameSource;

	// Token: 0x040004EA RID: 1258
	[SerializeField]
	private AudioSource mBackFireAudioSource;

	// Token: 0x040004EB RID: 1259
	[SerializeField]
	private AudioClip[] mScreamClips;

	// Token: 0x040004EC RID: 1260
	[SerializeField]
	private AudioClip[] mIdleClips;

	// Token: 0x040004ED RID: 1261
	[SerializeField]
	private AudioClip mDeathClip;

	// Token: 0x040004EE RID: 1262
	[SerializeField]
	private AudioClip mMusicIntro;

	// Token: 0x040004EF RID: 1263
	[SerializeField]
	private AudioClip mMusicLoop;

	// Token: 0x040004F0 RID: 1264
	[SerializeField]
	private AudioClip mBossPunchClip;

	// Token: 0x040004F1 RID: 1265
	[SerializeField]
	private AudioClip mBossFlameClip;

	// Token: 0x040004F2 RID: 1266
	[SerializeField]
	private AudioClip mBossGroanClip;

	// Token: 0x040004F3 RID: 1267
	[SerializeField]
	private AudioClip mBossDamageClip;

	// Token: 0x040004F4 RID: 1268
	[SerializeField]
	private AudioClip mBossRecoverClip;

	// Token: 0x040004F5 RID: 1269
	private float mTimeUntilIdlePlays;

	// Token: 0x040004F6 RID: 1270
	private int mIndexOfNextIdle;

	// Token: 0x040004F7 RID: 1271
	private bool mIsClimbing;
}
