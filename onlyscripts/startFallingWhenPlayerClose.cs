using System;
using UnityEngine;

// Token: 0x020002BA RID: 698
public class startFallingWhenPlayerClose : MonoBehaviour
{
	// Token: 0x0600108F RID: 4239 RVA: 0x0006BC3C File Offset: 0x00069E3C
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
		this.play = UnityEngine.Object.FindObjectOfType<player>().transform;
		this.lenght = UnityEngine.Random.Range(100, 230);
		this.rend = base.GetComponent<MeshRenderer>();
		this.rend.material.EnableKeyword("_EMISSION");
		this.c = Color.white;
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0006BCA4 File Offset: 0x00069EA4
	private void Update()
	{
		if (this.play.position.z - (float)this.lenght < base.transform.position.z)
		{
			if (this.spr.localScale.x < 2.5f && this.sinceDrop < 3f)
			{
				this.changeSpeed += Time.deltaTime * 50f;
			}
			else
			{
				if (this.sinceDrop > 2f)
				{
					this.c = Color.Lerp(this.c, Color.black, Time.deltaTime * 2f);
				}
				this.rig.isKinematic = false;
				this.sinceDrop += Time.deltaTime;
				if (this.sinceDrop > 3f)
				{
					this.changeSpeed -= Time.deltaTime * 100f;
				}
			}
		}
		if (this.spr.localScale.x >= 0f && !this.done)
		{
			this.spr.localScale = new Vector3(this.spr.localScale.x + this.changeSpeed * Time.deltaTime, this.spr.localScale.y + this.changeSpeed * Time.deltaTime, 1f);
		}
		else
		{
			this.spr.localScale = Vector3.zero;
			if (this.sinceDrop > 3f)
			{
				this.done = true;
			}
		}
		this.rend.material.SetColor("_EmissionColor", this.c);
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0006BE6C File Offset: 0x0006A06C
	private void FixedUpdate()
	{
		this.changeSpeed *= 0.9f;
	}

	// Token: 0x04000DA4 RID: 3492
	private Rigidbody rig;

	// Token: 0x04000DA5 RID: 3493
	private Transform play;

	// Token: 0x04000DA6 RID: 3494
	private int lenght;

	// Token: 0x04000DA7 RID: 3495
	public Transform spr;

	// Token: 0x04000DA8 RID: 3496
	private MeshRenderer rend;

	// Token: 0x04000DA9 RID: 3497
	private float sinceDrop;

	// Token: 0x04000DAA RID: 3498
	private float changeSpeed;

	// Token: 0x04000DAB RID: 3499
	private Color c;

	// Token: 0x04000DAC RID: 3500
	private bool done;
}
