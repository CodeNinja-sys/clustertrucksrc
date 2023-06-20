using System;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class car : MonoBehaviour
{
	// Token: 0x06000EFC RID: 3836 RVA: 0x0006128C File Offset: 0x0005F48C
	private void Start()
	{
		if (this.mainRig != null)
		{
			this.forward = this.mainRig.transform.FindChild("Forward");
		}
		this.wheels = base.transform.GetComponentsInChildren<WheelCollider>();
		if (this.waitTime > 0f)
		{
			this.slowAcc = 0f;
		}
		car.numberOfTrucks++;
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x00061300 File Offset: 0x0005F500
	public void RemoveTruck()
	{
		if (this.removed)
		{
			return;
		}
		this.removed = true;
		UnityEngine.Object.Destroy(base.gameObject);
		car.numberOfTrucks--;
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x00061338 File Offset: 0x0005F538
	private void Update()
	{
		this.lastGrounded += Time.deltaTime;
		if (!this.mainRig.gameObject.activeInHierarchy && !this.secondRig.gameObject.activeInHierarchy)
		{
			this.RemoveTruck();
		}
		if (this.useActive)
		{
		}
		this.waitTime -= Time.deltaTime;
		if (this.waitTime > 0f)
		{
			this.breakObject.SetActive(true);
		}
		else if (this.breakObject.activeInHierarchy)
		{
			this.breakObject.SetActive(false);
		}
		if (this.active)
		{
			if (this.mainRig != null && this.secondRig != null && this.lastGrounded < 0.3f && this.mainRig.velocity.magnitude > this.myVelocity.magnitude)
			{
				this.myVelocity = this.mainRig.velocity;
			}
			if (this.speed > 15000f * this.speedMultiplier && this.engine != null)
			{
				this.engine.pitch = this.speed / (20000f * this.speedMultiplier);
			}
			else
			{
				this.engine.pitch = 0.75f;
			}
			this.life += Time.deltaTime;
			if (!this.hasFallen && this.secondRig != null)
			{
				if (this.secondRig.transform.eulerAngles.z > 150f && this.secondRig.transform.localRotation.eulerAngles.z < 358f)
				{
					this.mainRig.AddTorque(Time.deltaTime * this.mainRig.transform.forward * 5000f);
				}
				if (this.secondRig.transform.eulerAngles.z < 150f && this.secondRig.transform.localRotation.eulerAngles.z > 2f)
				{
					this.mainRig.AddTorque(Time.deltaTime * this.mainRig.transform.forward * -5000f);
				}
			}
			if (this.targetPoint != Vector3.zero)
			{
				if (Vector3.Distance(this.mainRig.transform.position, this.targetPoint) < 10f)
				{
					this.targetPoint = Vector3.zero;
				}
				else
				{
					Vector3 vector = this.mainRig.transform.InverseTransformPoint(this.targetPoint);
					int num = 7;
					if (vector.x > (float)num)
					{
						vector.x = (float)num;
					}
					if (vector.x < (float)(-(float)num))
					{
						vector.x = (float)(-(float)num);
					}
					this.mainRig.AddTorque(Time.deltaTime * this.mainRig.transform.up * 200f * this.turnMultiplier * vector.x);
					if (this.secondRig != null)
					{
						this.secondRig.AddTorque(Time.deltaTime * this.secondRig.transform.forward * vector.x * -500f);
					}
					this.speed = 17000f * this.speedMultiplier - Mathf.Abs(vector.x) * 40f;
				}
			}
			if (this.life < 5f)
			{
				this.speed = this.life * 5000f * this.speedMultiplier;
			}
			if (this.waitTime < 0f && this.slowAcc < 1f)
			{
				this.slowAcc += Time.deltaTime / 3f;
			}
		}
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x00061784 File Offset: 0x0005F984
	private void CheckPlayer()
	{
		if (this.player == null)
		{
			this.player = GameObject.Find("player").transform.FindChild("hitbox");
		}
		if (this.mainRig != null)
		{
			if (Vector3.Distance(this.mainRig.transform.position, this.player.position) > 200f)
			{
				this.active = false;
			}
			else
			{
				this.active = true;
			}
		}
		else if (this.secondRig != null)
		{
			if (Vector3.Distance(this.secondRig.transform.position, this.player.position) > 200f)
			{
				this.active = false;
			}
			else
			{
				this.active = true;
			}
		}
		if (this.mainRig != null && this.mainRig.useGravity != this.active)
		{
			this.mainRig.useGravity = this.active;
			this.mainRig.velocity *= 0f;
			this.mainRig.gameObject.GetComponent<Renderer>().enabled = this.active;
			foreach (wheel wheel in this.mainRig.GetComponentsInChildren<wheel>(true))
			{
				Rigidbody component = wheel.GetComponent<Rigidbody>();
				component.useGravity = this.active;
				component.velocity *= 0f;
				wheel.gameObject.SetActive(this.active);
			}
		}
		if (this.secondRig != null && this.secondRig.useGravity != this.active)
		{
			this.secondRig.useGravity = this.active;
			this.secondRig.velocity *= 0f;
			this.secondRig.gameObject.GetComponent<Renderer>().enabled = this.active;
			foreach (wheel wheel2 in this.secondRig.GetComponentsInChildren<wheel>(true))
			{
				Rigidbody component2 = wheel2.GetComponent<Rigidbody>();
				component2.useGravity = this.active;
				component2.velocity *= 0f;
				wheel2.gameObject.SetActive(this.active);
			}
		}
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00061A0C File Offset: 0x0005FC0C
	private void CheckStatusEffects()
	{
		if (this.electricity > 0f)
		{
			this.electricity -= Time.deltaTime * 2f;
			Color color = Color.Lerp(this.rend1.material.color, this.electricityColor, Time.deltaTime * 15f);
			if (this.rend1 != null)
			{
				this.rend1.material.color = color;
			}
			if (this.rend2 != null)
			{
				this.rend2.material.color = color;
			}
			this.electricityParts.emission.enabled = true;
		}
		else if (this.rend1.material.color != Color.white)
		{
			Color color2 = Color.Lerp(this.rend1.material.color, Color.white, Time.deltaTime * 5f);
			if (this.rend1 != null)
			{
				this.rend1.material.color = color2;
			}
			if (this.rend2 != null)
			{
				this.rend2.material.color = color2;
			}
			this.electricityParts.emission.enabled = false;
		}
		if (this.killField != null)
		{
			if (this.electricity > 0f)
			{
				this.killField.SetActive(true);
			}
			else
			{
				this.killField.SetActive(false);
			}
		}
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x00061BA0 File Offset: 0x0005FDA0
	private void FixedUpdate()
	{
		if (this.lastGrounded < 0.2f)
		{
			float drag = 0.7f;
			this.mainRig.drag = drag;
			this.secondRig.drag = drag;
			this.secondRig.velocity = this.secondRig.velocity - Vector3.Project(this.secondRig.velocity, this.secondRig.transform.right) * 0.05f;
		}
		else
		{
			this.mainRig.drag = 0f;
			this.secondRig.drag = 0f;
		}
		if (this.active)
		{
			this.myVelocity *= 0.99f;
			if (this.secondRig != null)
			{
				if (Mathf.Abs(this.secondRig.velocity.y) > 32f)
				{
					this.secondRig.velocity = new Vector3(this.secondRig.velocity.x, this.secondRig.velocity.y * 0.99f, this.secondRig.velocity.z);
				}
				if (this.secondRig.transform.rotation.eulerAngles.z <= 300f && this.secondRig.transform.rotation.eulerAngles.z >= 60f)
				{
					this.hasFallen = true;
				}
				if (!this.hasFallen)
				{
					this.secondRig.angularVelocity = new Vector3(this.secondRig.angularVelocity.x, this.secondRig.angularVelocity.y * 0.9f, this.secondRig.angularVelocity.z * 0.8f);
				}
			}
		}
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x00061DB0 File Offset: 0x0005FFB0
	public void AddForceToMainRig(float multi)
	{
		this.lastGrounded = 0f;
		if (!this.active)
		{
			return;
		}
		if (this.mainRig != null && !this.hasFallen && this.waitTime < 0f && this.secondRig != null)
		{
			this.mainRig.AddForce(multi * this.speed * this.mainRig.transform.forward * this.slowAcc * Time.deltaTime * 15f / Mathf.Clamp(this.mainRig.velocity.magnitude, 1f, 20f));
		}
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x00061E80 File Offset: 0x00060080
	public void RoadTarget(Transform other)
	{
		if (other.GetComponent<wayPointType>() != null)
		{
			int type = other.GetComponent<wayPointType>().getType();
			if (other.GetComponent<wayPointType>().getType() == this.m_type || type == -2)
			{
				this.targetPoint = other.position;
				return;
			}
			if (type == 5)
			{
				this.targetPoint = other.position;
			}
		}
		else
		{
			this.targetPoint = other.position;
		}
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x00061EFC File Offset: 0x000600FC
	private void Wait(float t)
	{
		this.waitTime = t;
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x00061F08 File Offset: 0x00060108
	public void setType(int type)
	{
		this.m_type = type;
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x00061F14 File Offset: 0x00060114
	public int getType()
	{
		return this.m_type;
	}

	// Token: 0x04000BA8 RID: 2984
	public Rigidbody mainRig;

	// Token: 0x04000BA9 RID: 2985
	public Rigidbody secondRig;

	// Token: 0x04000BAA RID: 2986
	private Transform forward;

	// Token: 0x04000BAB RID: 2987
	private float speed;

	// Token: 0x04000BAC RID: 2988
	private Vector3 targetPoint;

	// Token: 0x04000BAD RID: 2989
	private WheelCollider[] wheels;

	// Token: 0x04000BAE RID: 2990
	public bool hasFallen;

	// Token: 0x04000BAF RID: 2991
	private float life;

	// Token: 0x04000BB0 RID: 2992
	public AudioSource engine;

	// Token: 0x04000BB1 RID: 2993
	public float speedMultiplier = 1f;

	// Token: 0x04000BB2 RID: 2994
	public float turnMultiplier = 1f;

	// Token: 0x04000BB3 RID: 2995
	public bool tankMovement;

	// Token: 0x04000BB4 RID: 2996
	private Transform player;

	// Token: 0x04000BB5 RID: 2997
	[HideInInspector]
	public Vector3 myVelocity;

	// Token: 0x04000BB6 RID: 2998
	[HideInInspector]
	public float lastGrounded;

	// Token: 0x04000BB7 RID: 2999
	public float waitTime;

	// Token: 0x04000BB8 RID: 3000
	private int m_type;

	// Token: 0x04000BB9 RID: 3001
	public float electricity;

	// Token: 0x04000BBA RID: 3002
	public Renderer rend1;

	// Token: 0x04000BBB RID: 3003
	public Renderer rend2;

	// Token: 0x04000BBC RID: 3004
	public Color electricityColor;

	// Token: 0x04000BBD RID: 3005
	public ParticleSystem electricityParts;

	// Token: 0x04000BBE RID: 3006
	public GameObject killField;

	// Token: 0x04000BBF RID: 3007
	public GameObject breakObject;

	// Token: 0x04000BC0 RID: 3008
	private float slowAcc = 1f;

	// Token: 0x04000BC1 RID: 3009
	private bool active = true;

	// Token: 0x04000BC2 RID: 3010
	private bool useActive;

	// Token: 0x04000BC3 RID: 3011
	public static int numberOfTrucks;

	// Token: 0x04000BC4 RID: 3012
	private bool removed;
}
