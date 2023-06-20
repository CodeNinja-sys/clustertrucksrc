using System;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public class truckSoundHandler : MonoBehaviour
{
	// Token: 0x060010D3 RID: 4307 RVA: 0x0006DFC4 File Offset: 0x0006C1C4
	private void Start()
	{
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0006DFC8 File Offset: 0x0006C1C8
	private void Update()
	{
		this.UpdateCD();
		if (this.frontRig.gameObject.activeInHierarchy)
		{
			if (this.loadRig.gameObject.activeInHierarchy)
			{
				base.transform.position = this.loadRig.position;
			}
			if (this.carDamageFront.deltaV.magnitude > 16f * this.carDamageFront.timeFix && this.carDamageFront.deltaV.magnitude < 25f * this.carDamageFront.timeFix)
			{
				switch (UnityEngine.Random.Range(0, 15))
				{
				case 0:
					this.au.PlayOneShot(this.honk1);
					break;
				case 1:
					this.au.PlayOneShot(this.honk1);
					break;
				case 2:
					this.au.PlayOneShot(this.doubleHonk1);
					break;
				case 4:
					this.au.PlayOneShot(this.doubleHonk2);
					break;
				case 5:
					this.au.PlayOneShot(this.longHonk1);
					break;
				case 6:
					this.au.PlayOneShot(this.longHonk2);
					break;
				}
			}
			if (this.carDamageFront.deltaV.magnitude > 20f || this.carDamageLoad.deltaV.magnitude > 20f)
			{
			}
		}
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x0006E150 File Offset: 0x0006C350
	private void UpdateCD()
	{
		this.longHonkCD -= Time.deltaTime;
	}

	// Token: 0x04000DF2 RID: 3570
	public AudioSource au;

	// Token: 0x04000DF3 RID: 3571
	public AudioSource loopSource;

	// Token: 0x04000DF4 RID: 3572
	public AudioClip[] engineSounds;

	// Token: 0x04000DF5 RID: 3573
	public AudioClip honk1;

	// Token: 0x04000DF6 RID: 3574
	public AudioClip honk2;

	// Token: 0x04000DF7 RID: 3575
	public AudioClip doubleHonk1;

	// Token: 0x04000DF8 RID: 3576
	public AudioClip doubleHonk2;

	// Token: 0x04000DF9 RID: 3577
	public AudioClip longHonk1;

	// Token: 0x04000DFA RID: 3578
	public AudioClip longHonk2;

	// Token: 0x04000DFB RID: 3579
	public AudioClip impact1;

	// Token: 0x04000DFC RID: 3580
	public AudioClip impact2;

	// Token: 0x04000DFD RID: 3581
	public AudioClip impact3;

	// Token: 0x04000DFE RID: 3582
	public carCheckDamage carDamageFront;

	// Token: 0x04000DFF RID: 3583
	public carCheckDamage carDamageLoad;

	// Token: 0x04000E00 RID: 3584
	public Rigidbody frontRig;

	// Token: 0x04000E01 RID: 3585
	public Rigidbody loadRig;

	// Token: 0x04000E02 RID: 3586
	private float bumpCD = 1f;

	// Token: 0x04000E03 RID: 3587
	private float longHonkCD;
}
