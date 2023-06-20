using System;
using UnityEngine;

// Token: 0x02000297 RID: 663
public class moveBackAndForthForce : MonoBehaviour
{
	// Token: 0x06000FDC RID: 4060 RVA: 0x00066BE0 File Offset: 0x00064DE0
	private void Start()
	{
		this.speed += UnityEngine.Random.Range(-this.spread, this.spread);
		this.targetPos = this.to.localPosition;
		this.startPos = base.transform.localPosition;
		this.rig = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00066C3C File Offset: 0x00064E3C
	private void Update()
	{
		if (this.targetPos == this.to.localPosition)
		{
			if (base.transform.localPosition.x < this.targetPos.x)
			{
				this.rig.AddRelativeForce(new Vector3(this.speed * Time.deltaTime, 0f, 0f));
			}
			if (base.transform.localPosition.x > this.targetPos.x && !this.justOnce)
			{
				this.targetPos = this.startPos;
			}
		}
		else
		{
			this.rig.AddRelativeForce(new Vector3(-this.speed * Time.deltaTime, 0f, 0f));
			if (base.transform.localPosition.x < this.targetPos.x)
			{
				this.targetPos = this.to.localPosition;
			}
		}
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x00066D48 File Offset: 0x00064F48
	private void FixedUpdate()
	{
		this.rig.velocity *= 0.9f;
	}

	// Token: 0x04000CB6 RID: 3254
	public Transform to;

	// Token: 0x04000CB7 RID: 3255
	public float speed = 10f;

	// Token: 0x04000CB8 RID: 3256
	public float spread = 2f;

	// Token: 0x04000CB9 RID: 3257
	private Vector3 targetPos;

	// Token: 0x04000CBA RID: 3258
	private Vector3 startPos;

	// Token: 0x04000CBB RID: 3259
	public bool justOnce;

	// Token: 0x04000CBC RID: 3260
	private Rigidbody rig;
}
