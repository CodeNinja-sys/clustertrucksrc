using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class RotationController : MonoBehaviour
{
	// Token: 0x060000A1 RID: 161 RVA: 0x00005E28 File Offset: 0x00004028
	private void Update()
	{
		if (Input.GetAxis("Horizontal") != 0f)
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			eulerAngles.y += Input.GetAxis("Horizontal") * this.speed;
			base.transform.eulerAngles = eulerAngles;
		}
	}

	// Token: 0x04000092 RID: 146
	public float speed = 1f;
}
