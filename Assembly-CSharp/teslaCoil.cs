using System;
using UnityEngine;

// Token: 0x020002C0 RID: 704
public class teslaCoil : MonoBehaviour
{
	// Token: 0x060010C9 RID: 4297 RVA: 0x0006DC6C File Offset: 0x0006BE6C
	private void Start()
	{
		this.noise = new Vector3[(int)this.segments];
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0006DC80 File Offset: 0x0006BE80
	private void Update()
	{
		Vector3 position = Vector3.zero;
		Vector3 vector = Vector3.zero;
		Ray ray = new Ray(this.point1.position, this.point1.forward);
		RaycastHit[] array = Physics.RaycastAll(ray, 44f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].transform.tag == "car")
			{
				array[i].transform.root.GetComponent<car>().electricity = 0.3f;
				this.overlap.gameObject.SetActive(false);
			}
		}
		this.overlap.gameObject.SetActive(true);
		this.line2.SetColors(this.end, this.start);
		for (int j = 0; j < 24; j++)
		{
			vector += new Vector3(UnityEngine.Random.Range(-this.amount, this.amount), UnityEngine.Random.Range(-this.amount, this.amount), UnityEngine.Random.Range(-this.amount, this.amount));
			this.noise[j] += vector;
			this.noise[j] *= this.friction;
			vector *= 0.5f;
			this.noise[23] *= 0f;
			this.noise[0] *= 0f;
			Vector3 vector2 = this.point1.position + (this.point2.position - this.point1.position) / 24f * (float)j + this.noise[j];
			if (j < 12)
			{
				this.line.SetPosition(j, vector2);
				if (j == 11)
				{
					position = vector2;
					this.overlap.transform.position = position;
				}
			}
			else
			{
				this.line2.SetPosition(j - 12, vector2);
				if (j == 12)
				{
					this.line2.SetPosition(0, position);
				}
			}
		}
	}

	// Token: 0x04000DE4 RID: 3556
	public Color start;

	// Token: 0x04000DE5 RID: 3557
	public Color end;

	// Token: 0x04000DE6 RID: 3558
	public LineRenderer line;

	// Token: 0x04000DE7 RID: 3559
	public LineRenderer line2;

	// Token: 0x04000DE8 RID: 3560
	public Transform point1;

	// Token: 0x04000DE9 RID: 3561
	public Transform point2;

	// Token: 0x04000DEA RID: 3562
	public Transform pointer1;

	// Token: 0x04000DEB RID: 3563
	public Transform pointer2;

	// Token: 0x04000DEC RID: 3564
	public float segments = 25f;

	// Token: 0x04000DED RID: 3565
	private Vector3[] noise;

	// Token: 0x04000DEE RID: 3566
	public float amount = 0.3f;

	// Token: 0x04000DEF RID: 3567
	public float friction = 0.95f;

	// Token: 0x04000DF0 RID: 3568
	public ParticleSystem overlap;
}
