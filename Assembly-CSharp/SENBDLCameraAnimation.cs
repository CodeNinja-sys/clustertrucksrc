using System;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class SENBDLCameraAnimation : MonoBehaviour
{
	// Token: 0x0600110C RID: 4364 RVA: 0x0006EA60 File Offset: 0x0006CC60
	private void Start()
	{
		this.randomRotation = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
		this.randomRotation = Vector3.Normalize(this.randomRotation);
		this.randomModRotation = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
		this.randomModRotation = Vector3.Normalize(this.randomModRotation);
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0006EB00 File Offset: 0x0006CD00
	private void Update()
	{
		float d = 15f + Mathf.Pow(Mathf.Cos(Time.time * 3.1415927f / 15f) * 0.5f + 0.5f, 3f) * 35f;
		Vector3 position = Quaternion.Euler(this.randomRotation * Time.time * 25f) * (Vector3.up * d);
		base.transform.position = position;
		base.transform.LookAt(Vector3.zero);
	}

	// Token: 0x04000E25 RID: 3621
	private Vector3 randomRotation;

	// Token: 0x04000E26 RID: 3622
	private Vector3 randomModRotation;
}
