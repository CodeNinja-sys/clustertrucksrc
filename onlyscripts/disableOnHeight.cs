using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000275 RID: 629
public class disableOnHeight : MonoBehaviour
{
	// Token: 0x06000F27 RID: 3879 RVA: 0x00062D40 File Offset: 0x00060F40
	private void Start()
	{
		this.bar = UnityEngine.Object.FindObjectOfType<menuBar>();
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x00062D50 File Offset: 0x00060F50
	private void Update()
	{
		if (this.bar.firstObject == base.transform.parent.parent.parent.gameObject)
		{
			foreach (Button button in this.buttons)
			{
				if (button.transform.position.y > this.top.position.y)
				{
					button.interactable = false;
				}
				else if (button.transform.position.y < this.bot.position.y)
				{
					button.interactable = false;
				}
				else
				{
					button.interactable = true;
				}
			}
		}
		else
		{
			foreach (Button button2 in this.buttons)
			{
				button2.interactable = false;
			}
		}
	}

	// Token: 0x04000BED RID: 3053
	public Button[] buttons;

	// Token: 0x04000BEE RID: 3054
	public Transform top;

	// Token: 0x04000BEF RID: 3055
	public Transform bot;

	// Token: 0x04000BF0 RID: 3056
	private menuBar bar;
}
