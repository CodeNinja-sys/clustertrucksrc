using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026A RID: 618
public class blink : MonoBehaviour
{
	// Token: 0x06000EEE RID: 3822 RVA: 0x00060A68 File Offset: 0x0005EC68
	private void Start()
	{
		this.img = base.GetComponent<Image>();
		this.img.color = this.c1;
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x00060A88 File Offset: 0x0005EC88
	private void Update()
	{
		this.counter += Time.deltaTime;
		if (this.currentType == blink.type.hard)
		{
			if (this.counter > this.speed)
			{
				this.counter = 0f;
				if (this.img.color == this.c1)
				{
					this.img.color = this.c2;
				}
				else
				{
					this.img.color = this.c1;
				}
			}
		}
		else if (this.currentType == blink.type.lerp)
		{
			if (this.counter > 1f / this.speed)
			{
				this.counter = 0f;
				if (this.c1Bool)
				{
					this.c1Bool = false;
				}
				else
				{
					this.c1Bool = true;
				}
			}
			if (this.c1Bool)
			{
				this.img.color = Color.Lerp(this.img.color, this.c1, Time.deltaTime * this.speed * 2f);
			}
			else
			{
				this.img.color = Color.Lerp(this.img.color, this.c2, Time.deltaTime * this.speed * 2f);
			}
		}
		else if (this.currentType == blink.type.curve)
		{
		}
	}

	// Token: 0x04000B8C RID: 2956
	public Color c1;

	// Token: 0x04000B8D RID: 2957
	public Color c2;

	// Token: 0x04000B8E RID: 2958
	public float speed;

	// Token: 0x04000B8F RID: 2959
	private float counter;

	// Token: 0x04000B90 RID: 2960
	public blink.type currentType;

	// Token: 0x04000B91 RID: 2961
	private Image img;

	// Token: 0x04000B92 RID: 2962
	private bool c1Bool = true;

	// Token: 0x0200026B RID: 619
	public enum type
	{
		// Token: 0x04000B94 RID: 2964
		hard,
		// Token: 0x04000B95 RID: 2965
		lerp,
		// Token: 0x04000B96 RID: 2966
		curve
	}
}
