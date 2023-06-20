using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F5 RID: 757
public class menuBlack : MonoBehaviour
{
	// Token: 0x060011D8 RID: 4568 RVA: 0x00072CA4 File Offset: 0x00070EA4
	private void Start()
	{
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x00072CA8 File Offset: 0x00070EA8
	private void Update()
	{
		this.img.color = new Color(this.img.color.r, this.img.color.g, this.img.color.b, this.img.color.a - Time.deltaTime);
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x00072D18 File Offset: 0x00070F18
	public void SetBlack()
	{
		this.img.color = new Color(this.img.color.r, this.img.color.g, this.img.color.b, 1f);
	}

	// Token: 0x04000EF6 RID: 3830
	public Image img;
}
