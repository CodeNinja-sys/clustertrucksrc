using System;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class growIntoSize : MonoBehaviour
{
	// Token: 0x06000F68 RID: 3944 RVA: 0x00064700 File Offset: 0x00062900
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x00064714 File Offset: 0x00062914
	private void Update()
	{
		if (!this.isDone)
		{
			this.velocity += Time.deltaTime * this.speed;
		}
		if (base.transform.localScale.x > this.targetSize)
		{
			this.isDone = true;
			this.velocity -= Time.deltaTime * this.speed;
		}
		base.transform.localScale = new Vector3(base.transform.localScale.x + this.velocity * Time.deltaTime, base.transform.localScale.y + this.velocity * Time.deltaTime, base.transform.localScale.x + this.velocity * Time.deltaTime);
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x000647F4 File Offset: 0x000629F4
	private void FixedUpdate()
	{
		this.velocity *= 0.8f;
		this.speed *= 0.9f;
	}

	// Token: 0x04000C2D RID: 3117
	public float targetSize = 1f;

	// Token: 0x04000C2E RID: 3118
	private float velocity;

	// Token: 0x04000C2F RID: 3119
	public float speed = 4f;

	// Token: 0x04000C30 RID: 3120
	private bool isDone;
}
