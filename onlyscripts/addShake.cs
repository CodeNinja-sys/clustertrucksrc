using System;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class addShake : MonoBehaviour
{
	// Token: 0x06000EE1 RID: 3809 RVA: 0x0006071C File Offset: 0x0005E91C
	private void Start()
	{
		Camera.main.GetComponent<cameraEffects>().SetShake(this.shake, Vector3.zero);
	}

	// Token: 0x04000B81 RID: 2945
	public float shake = 0.5f;
}
