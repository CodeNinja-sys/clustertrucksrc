using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000294 RID: 660
public class menuColor : MonoBehaviour
{
	// Token: 0x06000FCE RID: 4046 RVA: 0x00066898 File Offset: 0x00064A98
	private void Start()
	{
		this.background.color = this.colors[info.currentWorld - 1];
		this.center.color = this.colors2[info.currentWorld - 1];
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x000668EC File Offset: 0x00064AEC
	private void Update()
	{
		this.background.color = Color.Lerp(this.background.color, this.colors[info.currentWorld - 1], Time.deltaTime * 0.5f);
		this.center.color = Color.Lerp(this.center.color, this.colors2[info.currentWorld - 1], Time.deltaTime * 0.5f);
	}

	// Token: 0x04000CA5 RID: 3237
	public Color[] colors;

	// Token: 0x04000CA6 RID: 3238
	public Color[] colors2;

	// Token: 0x04000CA7 RID: 3239
	public Image background;

	// Token: 0x04000CA8 RID: 3240
	public Image center;
}
