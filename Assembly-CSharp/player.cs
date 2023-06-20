using System;
using InControl;
using UnityEngine;

// Token: 0x020002A5 RID: 677
public class player : MonoBehaviour
{
	// Token: 0x0600101E RID: 4126 RVA: 0x00067F7C File Offset: 0x0006617C
	private void Awake()
	{
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x00067F80 File Offset: 0x00066180
	private void Start()
	{
		this.sHandler = scoreHandler.Instance;
		this.man = (Manager)UnityEngine.Object.FindObjectOfType(typeof(Manager));
		this.gMan = (GameManager)UnityEngine.Object.FindObjectOfType(typeof(GameManager));
		this.rig = base.gameObject.GetComponent<Rigidbody>();
		info.playing = true;
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x00067FE4 File Offset: 0x000661E4
	public bool getWalkingstate()
	{
		return this.walking;
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x00067FEC File Offset: 0x000661EC
	public bool getRunningState()
	{
		return this.running;
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x00067FF4 File Offset: 0x000661F4
	private void CheckInput()
	{
		this.playerTargetVector = Vector3.zero;
		if (this.boosting)
		{
			return;
		}
		if ((Input.GetButton("Left") || InputManager.ActiveDevice.LeftStickX.Value < -0.3f) && (!Input.GetButton("Right") || InputManager.ActiveDevice.LeftStickX.Value < 0.3f))
		{
			this.playerTargetVector = -base.transform.right;
			if (Input.GetButton("Forward") || InputManager.ActiveDevice.LeftStickY.Value > 0.3f)
			{
				this.playerTargetVector = Vector3.Normalize(-base.transform.right + base.transform.forward * 1.2f);
			}
		}
		if ((Input.GetButton("Right") || InputManager.ActiveDevice.LeftStickX.Value > 0.3f) && (!Input.GetButton("Left") || InputManager.ActiveDevice.LeftStickX.Value > -0.3f))
		{
			this.playerTargetVector = base.transform.right;
			if (Input.GetButton("Forward") || InputManager.ActiveDevice.LeftStickY.Value > 0.3f)
			{
				this.playerTargetVector = Vector3.Normalize(base.transform.right + base.transform.forward * 1.2f);
			}
		}
		if (Input.GetButton("Forward") || InputManager.ActiveDevice.LeftStickY.Value > 0.3f)
		{
			this.playerTargetVector = base.transform.forward;
		}
		else if (Input.GetButton("Back") || InputManager.ActiveDevice.LeftStickY.Value < -0.3f)
		{
			this.playerTargetVector = -base.transform.forward;
		}
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x00068210 File Offset: 0x00066410
	private void JumpRayHit()
	{
		if (Input.GetButton("Jump") || InputManager.ActiveDevice.Action1.IsPressed)
		{
			this.rig.AddForce(base.transform.forward * Time.deltaTime * 30f, ForceMode.VelocityChange);
			this.rig.AddForce(base.transform.up * Time.deltaTime * 90f, ForceMode.VelocityChange);
			Camera.main.GetComponent<cameraEffects>().SetShake(0.15f, Vector3.zero);
		}
		this.canForward = false;
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x000682B8 File Offset: 0x000664B8
	public void setBoost()
	{
		this.boosting = true;
		this.myAudioSource.pitch = 1.4f;
		this.myAudioSource.PlayOneShot(this.boost, 0.6f);
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x000682E8 File Offset: 0x000664E8
	private void CheckRecentCar()
	{
		this.recentCar += Time.deltaTime;
		if (!this.lastCar.mainRig.gameObject.activeInHierarchy || !this.lastCar.secondRig.gameObject.activeInHierarchy)
		{
			if (this.lastGrounded > 0.1f && this.recentCar < 1f)
			{
				this.sHandler.AddScore(500f, "Close call", 0);
			}
			this.recentCar = 10f;
		}
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x00068380 File Offset: 0x00066580
	public void FindTruck()
	{
		Ray ray = new Ray(Vector3.zero + Vector3.up * 2f, Vector3.down);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 20f, this.truckMask))
		{
			base.transform.position = raycastHit.point + Vector3.up * 1.3f;
		}
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x000683F8 File Offset: 0x000665F8
	private void OnEnable()
	{
		this.dead = false;
		this.sinceLastJump = 1f;
		this.lastGrounded = -1f;
		this.jumpCd = 1f;
		this.groundSpeed = 4f * info.speedMultiplier;
		this.airSpeed = 1.2f * info.speedMultiplier;
		this.forceAmount = 1500f * info.speedMultiplier;
		foreach (Collider collider in base.GetComponentsInChildren<Collider>())
		{
			collider.enabled = true;
		}
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x00068488 File Offset: 0x00066688
	private void Update()
	{
		this.framesSinceStart += 1f;
		this.windLoop.volume = Mathf.Clamp((Mathf.Abs(this.rig.velocity.y) - 15f) / 20f, 0f, 0.7f);
		if (this.recentCar < 5f && this.lastCar)
		{
			this.CheckRecentCar();
		}
		if (this.rig.velocity.y < 0f)
		{
			this.boosting = false;
		}
		this.cantMoveTime -= Time.deltaTime;
		this.canForward = true;
		this.CheckInput();
		if (this.framesSinceStart < 3f)
		{
			this.FindTruck();
		}
		if (this.framesSinceStart > 10f)
		{
			if (info.PauseFrames > 0)
			{
				return;
			}
			RaycastHit raycastHit;
			Physics.Raycast(base.transform.position + Vector3.down / 2f, base.transform.forward, out raycastHit, 1f, this.mask);
			RaycastHit raycastHit2;
			Physics.Raycast(base.transform.position + Vector3.up / 2f, base.transform.forward, out raycastHit2, 1f, this.mask);
			RaycastHit raycastHit3;
			Physics.Raycast(base.transform.position, base.transform.forward, out raycastHit3, 1f, this.mask);
			if (raycastHit.transform != null)
			{
				if (!raycastHit.collider.isTrigger && !this.hasTouchedGround && raycastHit.transform.tag == "car")
				{
					this.JumpRayHit();
				}
			}
			else if (raycastHit2.transform != null)
			{
				if (!raycastHit2.collider.isTrigger && !this.hasTouchedGround && raycastHit2.transform.tag == "car")
				{
					this.JumpRayHit();
				}
			}
			else if (raycastHit3.transform != null && !raycastHit3.collider.isTrigger && !this.hasTouchedGround && raycastHit3.transform.tag == "car")
			{
				this.JumpRayHit();
			}
			if (base.transform.position.y < -500f && this.man != null)
			{
				this.gMan.LoseLevel();
			}
			this.walking = false;
			this.running = false;
			if (this.canMove && !info.paused && this.cantMoveTime <= 0f)
			{
				this._xValue = Input.GetAxis("Mouse X");
				this._yValue = -Input.GetAxis("Mouse Y");
				if (Mathf.Abs(InputManager.ActiveDevice.RightStickX.Value) > 0.01f)
				{
					this._xValue = InputManager.ActiveDevice.RightStickX.Value * player.controlSensitivity;
				}
				if (Mathf.Abs(InputManager.ActiveDevice.RightStickY.Value) > 0.01f)
				{
					this._yValue = -InputManager.ActiveDevice.RightStickY.Value * player.controlSensitivity;
				}
				base.transform.Rotate(Vector3.up * player.sensitivity * this._xValue);
				this.camHolder.transform.Rotate(Vector3.right * player.sensitivity * this._yValue);
				if (this.lastGrounded < 0.2f && this.jumpCd > 0.3f)
				{
					this.rig.AddForce(Vector3.down * Time.deltaTime * 500f);
				}
				if ((Input.GetButton("Forward") || InputManager.ActiveDevice.LeftStickY.Value > 0.3f) && this.canForward)
				{
					this.walking = true;
					this.rig.AddForce(base.transform.forward * Time.deltaTime * this.forceAmount * 0.7f);
					if ((Input.GetButton("Sprint") || InputManager.ActiveDevice.RightTrigger.IsPressed) && this.lastGrounded < 0.1f)
					{
						this.running = true;
						this.rigMove = base.transform.forward * Time.deltaTime * this.groundSpeed * 1f + this.rigMove;
						this.rig.AddForce(base.transform.forward * Time.deltaTime * this.forceAmount * 1f);
					}
					if (this.hasTouchedGround)
					{
						this.rigMove = base.transform.forward * Time.deltaTime * this.groundSpeed + this.rigMove;
					}
					else
					{
						this.rigMove += base.transform.forward * Time.deltaTime * this.airSpeed * 2f;
						if (Time.timeScale < 0.8f)
						{
							this.rigMove += base.transform.forward * Time.deltaTime * this.airSpeed * 1f;
						}
					}
				}
				if (Input.GetButton("Back") || InputManager.ActiveDevice.LeftStickY.Value < -0.3f)
				{
					this.walking = true;
					this.rigMove = base.transform.forward * Time.deltaTime * -this.groundSpeed / 2f + this.rigMove;
					if (this.hasTouchedGround)
					{
						this.rig.AddForce(base.transform.forward * Time.deltaTime * -this.forceAmount);
					}
					else
					{
						this.rigMove += base.transform.forward * Time.deltaTime * -this.airSpeed;
					}
				}
				if (Input.GetButton("Left") || InputManager.ActiveDevice.LeftStickX.Value < -0.3f)
				{
					this.walking = true;
					this.rigMove = base.transform.right * Time.deltaTime * -this.groundSpeed / 4f + this.rigMove;
					if (this.hasTouchedGround)
					{
						this.rig.AddForce(base.transform.right * Time.deltaTime * -this.forceAmount);
					}
					else
					{
						this.rigMove += base.transform.right * Time.deltaTime * -this.airSpeed * 0.5f;
						this.rig.AddForce(base.transform.right * Time.deltaTime * -this.forceAmount / 2f);
					}
				}
				if (Input.GetButton("Right") || InputManager.ActiveDevice.LeftStickX.Value > 0.3f)
				{
					this.walking = true;
					this.rigMove = base.transform.right * Time.deltaTime * this.groundSpeed / 4f + this.rigMove;
					if (this.hasTouchedGround)
					{
						this.rig.AddForce(base.transform.right * Time.deltaTime * this.forceAmount);
					}
					else
					{
						this.rigMove += base.transform.right * Time.deltaTime * this.airSpeed * 0.5f;
						this.rig.AddForce(base.transform.right * Time.deltaTime * this.forceAmount / 2f);
					}
				}
				if ((Input.GetButton("Jump") || InputManager.ActiveDevice.Action1.IsPressed) && this.lastGrounded < 0.5f && this.jumpCd > 0.1f && this.hasTouchedGround && !this.boosting)
				{
					this.Jump(11f);
					this.jumpCd = 0f;
				}
				if ((Input.GetButton("Jump") || InputManager.ActiveDevice.Action1.IsPressed) && !this.boosting)
				{
					this.rig.AddForce(base.transform.up * 500f * Time.deltaTime);
				}
			}
			this.sinceLastJump += Time.deltaTime;
			this.lastGrounded += Time.deltaTime;
			if (this.hasTouchedGround)
			{
				this.jumpCd += Time.deltaTime;
			}
			if (this.boosting)
			{
			}
			if (this.lastGrounded > 0.2f)
			{
				this.camAnim.SetInteger("state", 0);
			}
			else if (this.running)
			{
				this.camAnim.SetInteger("state", 2);
				this.PlayStep(this.stepSpeed * 0.5f);
			}
			else if (this.walking)
			{
				this.camAnim.SetInteger("state", 1);
				this.PlayStep(this.stepSpeed);
			}
			else
			{
				this.camAnim.SetInteger("state", 0);
			}
		}
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x00068F5C File Offset: 0x0006715C
	public void Jump(float force)
	{
		this.sinceLastJump = 0f;
		this.rig.AddForce(this.playerTargetVector * 1f, ForceMode.VelocityChange);
		this.myAudioSource.pitch = 1f;
		this.myAudioSource.PlayOneShot(this.jump);
		this.hasTouchedGround = false;
		this.camForce.AddRot();
		if (this.rig.velocity.y < 0f)
		{
			this.rig.velocity = new Vector3(this.rig.velocity.x, Mathf.Clamp(-this.rig.velocity.y - 1f, 0f, 1f), this.rig.velocity.z);
		}
		this.rig.AddForce(base.transform.up * force, ForceMode.VelocityChange);
		Debug.Log("JUMP3");
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x00069068 File Offset: 0x00067268
	private void PlayStep(float f)
	{
		if (this.stepCD > f && this.lastGrounded < 0.1f)
		{
			this.stepAU.pitch = UnityEngine.Random.Range(0.9f, 1.05f);
			this.stepAU.PlayOneShot(this.step[UnityEngine.Random.Range(0, this.step.Length)]);
			this.stepCD = 0f;
		}
		if (this.hasTouchedGround)
		{
			this.stepCD += Time.deltaTime;
		}
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x000690F4 File Offset: 0x000672F4
	private void FixedUpdate()
	{
		this.rig.MovePosition(base.transform.position + this.rigMove * 1f);
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, base.transform.localRotation.eulerAngles.y, 0f));
		if (Mathf.Abs(this.rig.velocity.y) > 32f)
		{
			this.rig.velocity = new Vector3(this.rig.velocity.x, this.rig.velocity.y * 0.993f, this.rig.velocity.z);
		}
		if (this.hasTouchedGround && !this.boosting)
		{
			this.rigMove *= 0.7f;
		}
		else
		{
			this.rigMove *= 0.8f;
			if (this.walking)
			{
				this.rig.velocity = new Vector3(this.rig.velocity.x * info.drag, this.rig.velocity.y, this.rig.velocity.z * info.drag);
			}
		}
		if (this.canMove)
		{
			this.rig.AddForce(Vector3.down * 16f * Time.timeScale);
		}
		if (this.frozen)
		{
			this.rig.velocity *= 0.92f;
		}
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x000692D8 File Offset: 0x000674D8
	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag.Contains("kill"))
		{
			this.Die(0);
		}
		else if ((this.lastGrounded > 0.2f && !this.hasTouchedGround) || !this.hasLandedFirstTime)
		{
			if (other.transform.tag == "car" && this.hasLandedFirstTime)
			{
				car component = other.transform.root.GetComponent<car>();
				if (component != null)
				{
					other.transform.GetComponent<carCheckDamage>().SetImmunity(true);
					bool flag = false;
					if (this.lastCar && this.lastCar == component)
					{
						flag = true;
					}
					if ((component.lastGrounded > 0.3f || !component.enabled) && !flag)
					{
						this.sHandler.AddScore(350f, "flying truck jump", 0);
					}
				}
			}
			this.camForce.AddForce(Vector3.down * 5f);
			this.camForce.AddRot();
			this.landSource.volume = Mathf.Clamp(this.lastGrounded / 7f, 0.2f, 0.6f);
			this.landSource.PlayOneShot(this.land[4]);
			this.landSource.PlayOneShot(this.land[UnityEngine.Random.Range(0, 4)]);
			Camera.main.GetComponent<cameraEffects>().SetShake(Mathf.Clamp(this.lastGrounded - 2f, 0f, 0.6f), Vector3.zero);
			this.rigMove *= 0f;
			if (!this.hasLandedFirstTime)
			{
				this.hasLandedFirstTime = true;
				this.hasTouchedGround = true;
				this.lastGrounded = 0f;
			}
		}
		if (other.collider.GetComponent<EventTriggerCheck>())
		{
			Debug.Log("Foun trigger: ", other.collider.GetComponent<EventTriggerCheck>());
			other.collider.GetComponent<EventTriggerCheck>().OnPlayerTouch();
		}
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x000694FC File Offset: 0x000676FC
	public void Die(int cause)
	{
		if (this.man == null)
		{
			return;
		}
		if (!this.dead && !this.gMan.won)
		{
			if (this.man != null)
			{
				this.gMan.LoseLevel();
			}
			this.dead = true;
			this.deathSounds.volume = UnityEngine.Random.Range(0.4f, 0.5f);
			this.deathSounds.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
			if (cause == 0)
			{
				this.deathSounds.PlayOneShot(this.splat[UnityEngine.Random.Range(0, this.splat.Length)]);
			}
			if (cause == 1)
			{
				this.deathSounds.PlayOneShot(this.burn[UnityEngine.Random.Range(0, this.burn.Length)]);
			}
			if (cause == 2)
			{
				this.deathSounds.PlayOneShot(this.laser[UnityEngine.Random.Range(0, this.laser.Length)]);
			}
		}
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x00069604 File Offset: 0x00067804
	private void OnCollisionStay(Collision other)
	{
		if (other.transform.tag == "car")
		{
			car component = other.transform.root.GetComponent<car>();
			if (component != null && component.mainRig.gameObject.activeInHierarchy && component.secondRig.gameObject.activeInHierarchy)
			{
				this.lastCar = component;
				this.recentCar = 0f;
			}
		}
		if (this.sinceLastJump < 0.3f)
		{
			return;
		}
		this.lastGrounded = 0f;
		this.hasTouchedGround = true;
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x000696A8 File Offset: 0x000678A8
	private void StopMoving()
	{
		this.canMove = false;
		this.Freeze();
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x000696B8 File Offset: 0x000678B8
	public void Freeze()
	{
		this.gMan.LoseLevel();
		this.rig.useGravity = false;
		this.frozen = true;
	}

	// Token: 0x04000CFA RID: 3322
	private Manager man;

	// Token: 0x04000CFB RID: 3323
	private GameManager gMan;

	// Token: 0x04000CFC RID: 3324
	public static float sensitivity = 3f;

	// Token: 0x04000CFD RID: 3325
	public static float controlSensitivity = 1f;

	// Token: 0x04000CFE RID: 3326
	private float groundSpeed;

	// Token: 0x04000CFF RID: 3327
	private float airSpeed = 1.2f;

	// Token: 0x04000D00 RID: 3328
	private float forceAmount = 1200f;

	// Token: 0x04000D01 RID: 3329
	public LayerMask truckMask;

	// Token: 0x04000D02 RID: 3330
	private float airForce = 0.1f;

	// Token: 0x04000D03 RID: 3331
	[HideInInspector]
	public Rigidbody rig;

	// Token: 0x04000D04 RID: 3332
	public Transform camHolder;

	// Token: 0x04000D05 RID: 3333
	[HideInInspector]
	public bool canMove = true;

	// Token: 0x04000D06 RID: 3334
	[HideInInspector]
	public bool dead;

	// Token: 0x04000D07 RID: 3335
	[HideInInspector]
	public bool frozen;

	// Token: 0x04000D08 RID: 3336
	public AudioClip jump;

	// Token: 0x04000D09 RID: 3337
	public AudioClip boost;

	// Token: 0x04000D0A RID: 3338
	public AudioSource myAudioSource;

	// Token: 0x04000D0B RID: 3339
	public AudioSource landSource;

	// Token: 0x04000D0C RID: 3340
	public AudioSource deathSounds;

	// Token: 0x04000D0D RID: 3341
	public CameraForce camForce;

	// Token: 0x04000D0E RID: 3342
	public Animator camAnim;

	// Token: 0x04000D0F RID: 3343
	public bool hasTouchedGround = true;

	// Token: 0x04000D10 RID: 3344
	private float climbCD;

	// Token: 0x04000D11 RID: 3345
	public LayerMask mask;

	// Token: 0x04000D12 RID: 3346
	public float framesSinceStart;

	// Token: 0x04000D13 RID: 3347
	public float jumpCd = 1f;

	// Token: 0x04000D14 RID: 3348
	private bool running;

	// Token: 0x04000D15 RID: 3349
	private bool walking;

	// Token: 0x04000D16 RID: 3350
	private Vector3 playerTargetVector = Vector3.zero;

	// Token: 0x04000D17 RID: 3351
	private float stepCD;

	// Token: 0x04000D18 RID: 3352
	public AudioSource stepAU;

	// Token: 0x04000D19 RID: 3353
	public AudioClip[] step;

	// Token: 0x04000D1A RID: 3354
	public AudioClip[] land;

	// Token: 0x04000D1B RID: 3355
	private Vector3 rigMove = Vector3.zero;

	// Token: 0x04000D1C RID: 3356
	private Vector3 rigMoveAir = Vector3.zero;

	// Token: 0x04000D1D RID: 3357
	public float stepSpeed = 0.2f;

	// Token: 0x04000D1E RID: 3358
	private bool canForward;

	// Token: 0x04000D1F RID: 3359
	public CameraMovementRotation camRot;

	// Token: 0x04000D20 RID: 3360
	public float _xValue;

	// Token: 0x04000D21 RID: 3361
	public float _yValue;

	// Token: 0x04000D22 RID: 3362
	public float lastGrounded = -1f;

	// Token: 0x04000D23 RID: 3363
	public bool boosting;

	// Token: 0x04000D24 RID: 3364
	[HideInInspector]
	public bool hasLandedFirstTime;

	// Token: 0x04000D25 RID: 3365
	private scoreHandler sHandler;

	// Token: 0x04000D26 RID: 3366
	public car lastCar;

	// Token: 0x04000D27 RID: 3367
	public float recentCar;

	// Token: 0x04000D28 RID: 3368
	public float sinceLastJump = 1f;

	// Token: 0x04000D29 RID: 3369
	public AudioClip[] splat;

	// Token: 0x04000D2A RID: 3370
	public AudioClip[] burn;

	// Token: 0x04000D2B RID: 3371
	public AudioClip[] laser;

	// Token: 0x04000D2C RID: 3372
	public float cantMoveTime;

	// Token: 0x04000D2D RID: 3373
	public AudioSource windLoop;

	// Token: 0x04000D2E RID: 3374
	private RDTPlayer mTransmitter;
}
