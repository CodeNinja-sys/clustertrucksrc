using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000004 RID: 4
public class AddEffect : MonoBehaviour
{
	// Token: 0x06000008 RID: 8 RVA: 0x000024E8 File Offset: 0x000006E8
	private void Start()
	{
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000024EC File Offset: 0x000006EC
	private void Update()
	{
		if (this.fadingToWhite)
		{
			this.img.color = new Color(this.img.color.r, this.img.color.g, this.img.color.b, this.img.color.a + Time.deltaTime * 3f);
			this.waiting += Time.deltaTime;
			if (this.waiting > 2f)
			{
				this.img2.gameObject.SetActive(true);
				this.img2.color = new Color(this.img2.color.r, this.img2.color.g, this.img2.color.b, this.img2.color.a + Time.deltaTime * 3f);
				if (this.waiting > 3f)
				{
					UnityEngine.Object.Destroy(base.transform.root.gameObject);
					UnityEngine.Object.FindObjectOfType<EndingOrb>().ShowCredits();
				}
			}
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000263C File Offset: 0x0000083C
	public void FadeToWhite()
	{
		this.fadingToWhite = true;
		this.img.gameObject.SetActive(true);
	}

	// Token: 0x04000014 RID: 20
	public Image img;

	// Token: 0x04000015 RID: 21
	public Image img2;

	// Token: 0x04000016 RID: 22
	private bool fadingToWhite;

	// Token: 0x04000017 RID: 23
	private float waiting;
}
