using System;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class moveOnMouseMovement : MonoBehaviour
{
	// Token: 0x06000FE9 RID: 4073 RVA: 0x00067070 File Offset: 0x00065270
	private void Start()
	{
		this.startPos = base.transform.localPosition;
		this.v = this.publicV * 1E-05f;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x00067098 File Offset: 0x00065298
	private void Update()
	{
		this.velocity += new Vector3(Input.GetAxis("Mouse X") * this.v, Input.GetAxis("Mouse Y") * this.v, 0f);
		base.transform.Translate(this.velocity);
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.startPos, Time.unscaledDeltaTime * 10f);
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00067120 File Offset: 0x00065320
	private void FixedUpdate()
	{
		this.velocity *= 0.9f;
	}

	// Token: 0x04000CC9 RID: 3273
	private Vector3 startPos;

	// Token: 0x04000CCA RID: 3274
	private Vector3 velocity;

	// Token: 0x04000CCB RID: 3275
	public float publicV = 1f;

	// Token: 0x04000CCC RID: 3276
	private float v;
}
