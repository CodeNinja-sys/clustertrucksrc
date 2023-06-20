using System;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class impactSounds : MonoBehaviour
{
	// Token: 0x06000F7F RID: 3967 RVA: 0x000652D0 File Offset: 0x000634D0
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
		this.myCar = base.transform.parent.GetComponent<car>();
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x00065300 File Offset: 0x00063500
	private void Update()
	{
		this.cd += Time.deltaTime;
		this.cdTire += Time.deltaTime;
		Vector3 vector = base.transform.InverseTransformDirection(this.rig.velocity);
		if (Mathf.Abs(vector.x) > 1f && this.cdTire > 2f && this.myCar.lastGrounded < 0.2f)
		{
			this.au.PlayOneShot(this.tires[UnityEngine.Random.Range(0, this.tires.Length)], Mathf.Clamp(vector.x / 2.5f, 0.2f, 0.5f));
			this.cdTire = 0f;
		}
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x000653CC File Offset: 0x000635CC
	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.root != base.transform.root && this.cd > 1f && other.transform.tag != "Player")
		{
			this.au.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
			if (this.myCarDamage.lastVel.magnitude > this.limit + 8f)
			{
				switch (UnityEngine.Random.Range(0, 3))
				{
				case 0:
					this.au.PlayOneShot(this.big1, 0.4f);
					break;
				case 1:
					this.au.PlayOneShot(this.big2, 0.4f);
					break;
				case 2:
					this.au.PlayOneShot(this.big3, 0.4f);
					break;
				}
				this.cd = 0f;
			}
			else if (this.myCarDamage.lastVel.magnitude > this.limit + 4f)
			{
				switch (UnityEngine.Random.Range(0, 3))
				{
				case 0:
					this.au.PlayOneShot(this.medium1, 0.3f);
					break;
				case 1:
					this.au.PlayOneShot(this.medium2, 0.3f);
					break;
				case 2:
					this.au.PlayOneShot(this.medium3, 0.3f);
					break;
				}
				this.cd = 0f;
			}
			else if (this.myCarDamage.lastVel.magnitude > this.limit)
			{
				switch (UnityEngine.Random.Range(0, 3))
				{
				case 0:
					this.au.PlayOneShot(this.small1, 0.2f);
					break;
				case 1:
					this.au.PlayOneShot(this.small2, 0.2f);
					break;
				case 2:
					this.au.PlayOneShot(this.small3, 0.2f);
					break;
				}
				this.cd = 0f;
			}
		}
	}

	// Token: 0x04000C52 RID: 3154
	public car myCar;

	// Token: 0x04000C53 RID: 3155
	public carCheckDamage myCarDamage;

	// Token: 0x04000C54 RID: 3156
	public AudioClip small1;

	// Token: 0x04000C55 RID: 3157
	public AudioClip small2;

	// Token: 0x04000C56 RID: 3158
	public AudioClip small3;

	// Token: 0x04000C57 RID: 3159
	public AudioClip medium1;

	// Token: 0x04000C58 RID: 3160
	public AudioClip medium2;

	// Token: 0x04000C59 RID: 3161
	public AudioClip medium3;

	// Token: 0x04000C5A RID: 3162
	public AudioClip big1;

	// Token: 0x04000C5B RID: 3163
	public AudioClip big2;

	// Token: 0x04000C5C RID: 3164
	public AudioClip big3;

	// Token: 0x04000C5D RID: 3165
	public AudioClip[] tires;

	// Token: 0x04000C5E RID: 3166
	public AudioSource au;

	// Token: 0x04000C5F RID: 3167
	private Rigidbody rig;

	// Token: 0x04000C60 RID: 3168
	private float cd;

	// Token: 0x04000C61 RID: 3169
	private float cdTire;

	// Token: 0x04000C62 RID: 3170
	private float limit = 10f;
}
