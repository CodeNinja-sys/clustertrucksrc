using System;
using UnityEngine;

// Token: 0x02000296 RID: 662
public class moveBackAndForth : MonoBehaviour
{
	// Token: 0x06000FD8 RID: 4056 RVA: 0x00066A24 File Offset: 0x00064C24
	private void Start()
	{
		this.speed += UnityEngine.Random.Range(-this.spread, this.spread);
		this.targetPos = this.to.localPosition;
		this.startPos = base.transform.localPosition;
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x00066A74 File Offset: 0x00064C74
	private void Update()
	{
		if (this.targetPos == this.to.localPosition)
		{
			if (base.transform.localPosition.x < this.targetPos.x)
			{
				this.vel = new Vector3(this.vel.x + this.speed * Time.deltaTime, 0f, 0f);
			}
			if (base.transform.localPosition.x > this.targetPos.x && !this.justOnce)
			{
				this.targetPos = this.startPos;
			}
		}
		else
		{
			this.vel = new Vector3(this.vel.x - this.speed * Time.deltaTime, 0f, 0f);
			if (base.transform.localPosition.x < this.targetPos.x)
			{
				this.targetPos = this.to.localPosition;
			}
		}
		base.transform.Translate(this.vel * Time.deltaTime, Space.Self);
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x00066BA8 File Offset: 0x00064DA8
	private void FixedUpdate()
	{
		this.vel *= 0.9f;
	}

	// Token: 0x04000CAF RID: 3247
	public Transform to;

	// Token: 0x04000CB0 RID: 3248
	public float speed = 10f;

	// Token: 0x04000CB1 RID: 3249
	public float spread = 2f;

	// Token: 0x04000CB2 RID: 3250
	[HideInInspector]
	public Vector3 vel = new Vector3(0f, 0f, 0f);

	// Token: 0x04000CB3 RID: 3251
	[HideInInspector]
	public Vector3 targetPos;

	// Token: 0x04000CB4 RID: 3252
	[HideInInspector]
	public Vector3 startPos;

	// Token: 0x04000CB5 RID: 3253
	public bool justOnce;
}
