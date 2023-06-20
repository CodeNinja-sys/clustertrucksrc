using System;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public class BackAndForth : MonoBehaviour
{
	// Token: 0x06000B55 RID: 2901 RVA: 0x00046A74 File Offset: 0x00044C74
	private void Start()
	{
		this.startPos = base.transform.position;
		this.toPos = this.to.position;
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00046AA4 File Offset: 0x00044CA4
	private void Update()
	{
		if (this.goingHome)
		{
			this.targetPos = this.startPos;
		}
		else
		{
			this.targetPos = this.toPos;
		}
		base.transform.Translate(this.velocity * Time.deltaTime, Space.World);
		this.velocity += Vector3.Normalize(this.targetPos - base.transform.position) * this.speed * 0.1f;
		if (Vector3.Distance(base.transform.position, this.targetPos) < 1f)
		{
			this.goingHome = !this.goingHome;
		}
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00046B68 File Offset: 0x00044D68
	private void FixedUpdate()
	{
		this.velocity *= 0.9f;
	}

	// Token: 0x04000809 RID: 2057
	public Transform to;

	// Token: 0x0400080A RID: 2058
	public float speed = 2f;

	// Token: 0x0400080B RID: 2059
	private Vector3 startPos;

	// Token: 0x0400080C RID: 2060
	private Vector3 toPos;

	// Token: 0x0400080D RID: 2061
	private Vector3 targetPos = Vector3.zero;

	// Token: 0x0400080E RID: 2062
	private Vector3 velocity;

	// Token: 0x0400080F RID: 2063
	private bool goingHome;
}
