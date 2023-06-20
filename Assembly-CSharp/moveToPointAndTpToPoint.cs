using System;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class moveToPointAndTpToPoint : MonoBehaviour
{
	// Token: 0x06000FED RID: 4077 RVA: 0x0006714C File Offset: 0x0006534C
	private void Start()
	{
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x00067150 File Offset: 0x00065350
	private void Update()
	{
		base.transform.Translate(Vector3.Normalize(this.point1.position - base.transform.position) * Time.deltaTime * this.speed);
		if (Vector3.Distance(base.transform.position, this.point1.position) < 1f)
		{
			base.transform.position = this.point2.position;
		}
	}

	// Token: 0x04000CCD RID: 3277
	public Transform point1;

	// Token: 0x04000CCE RID: 3278
	public Transform point2;

	// Token: 0x04000CCF RID: 3279
	public float speed = 10f;
}
