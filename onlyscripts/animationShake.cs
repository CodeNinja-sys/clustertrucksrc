using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class animationShake : MonoBehaviour
{
	// Token: 0x0600117A RID: 4474 RVA: 0x00070FBC File Offset: 0x0006F1BC
	private void Start()
	{
		this.au = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x00070FCC File Offset: 0x0006F1CC
	private void Shake()
	{
		if (this.usePos)
		{
			Camera.main.GetComponent<cameraEffects>().SetShake(this.strength, base.transform.position);
		}
		else
		{
			Camera.main.GetComponent<cameraEffects>().SetShake(this.strength, Vector3.zero);
		}
		if (this.au != null)
		{
			this.au.PlayOneShot(this.au.clip);
		}
	}

	// Token: 0x04000EA6 RID: 3750
	private AudioSource au;

	// Token: 0x04000EA7 RID: 3751
	public float strength = 0.3f;

	// Token: 0x04000EA8 RID: 3752
	public bool usePos = true;
}
