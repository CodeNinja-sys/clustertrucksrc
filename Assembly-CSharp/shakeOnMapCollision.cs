using System;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class shakeOnMapCollision : MonoBehaviour
{
	// Token: 0x0600106D RID: 4205 RVA: 0x0006AE7C File Offset: 0x0006907C
	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "kill" || other.gameObject.layer == 8)
		{
			Camera.main.GetComponent<cameraEffects>().SetShake(0.5f, base.transform.position);
		}
	}
}
