using System;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public class CameraForce : MonoBehaviour
{
	// Token: 0x06000B6B RID: 2923 RVA: 0x00046FFC File Offset: 0x000451FC
	private void Start()
	{
		this.startPos = base.transform.localPosition;
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x00047010 File Offset: 0x00045210
	private void Update()
	{
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.force, Time.deltaTime * 5f);
		base.transform.localPosition = new Vector3(0f, Mathf.Clamp(base.transform.localPosition.y, -1.5f, 10f));
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(this.rot), Time.deltaTime * 3f);
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x000470B4 File Offset: 0x000452B4
	public void AddForce(Vector3 f)
	{
		f /= 1.3f;
		if (f.magnitude > this.force.magnitude)
		{
			this.force = f;
		}
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x000470E4 File Offset: 0x000452E4
	public void AddRot()
	{
		this.rot = new Vector3(10f, 0f, 0f);
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00047100 File Offset: 0x00045300
	private void FixedUpdate()
	{
		this.force *= 0.9f;
		this.rot *= 0.9f;
	}

	// Token: 0x04000815 RID: 2069
	private Vector3 startPos;

	// Token: 0x04000816 RID: 2070
	private Vector3 force = Vector3.zero;

	// Token: 0x04000817 RID: 2071
	private Vector3 rot = Vector3.zero;
}
