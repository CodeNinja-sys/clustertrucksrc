using System;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class SuperTruck : MonoBehaviour
{
	// Token: 0x06001137 RID: 4407 RVA: 0x0007030C File Offset: 0x0006E50C
	private void OnEnable()
	{
		this.truck.SetActive(false);
		this.super.SetActive(false);
		this.t = -1f;
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x00070334 File Offset: 0x0006E534
	private void Update()
	{
		this.t += Time.unscaledDeltaTime;
		if (this.t > 1f)
		{
			this.t = 0f;
			if (this.super.activeInHierarchy)
			{
				this.truck.SetActive(true);
				this.super.SetActive(false);
				this.au.PlayOneShot(this.truckClip);
			}
			else
			{
				this.super.SetActive(true);
				this.truck.SetActive(false);
				this.au.PlayOneShot(this.superClip);
			}
		}
	}

	// Token: 0x04000E64 RID: 3684
	public GameObject super;

	// Token: 0x04000E65 RID: 3685
	public GameObject truck;

	// Token: 0x04000E66 RID: 3686
	public AudioSource au;

	// Token: 0x04000E67 RID: 3687
	public AudioClip superClip;

	// Token: 0x04000E68 RID: 3688
	public AudioClip truckClip;

	// Token: 0x04000E69 RID: 3689
	private float t = -1f;
}
