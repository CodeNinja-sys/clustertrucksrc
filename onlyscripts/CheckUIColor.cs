using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000017 RID: 23
public class CheckUIColor : MonoBehaviour
{
	// Token: 0x06000087 RID: 135 RVA: 0x00005920 File Offset: 0x00003B20
	private void OnEnable()
	{
		if (info.darkWorld || SetMenuWorld.randomWorld == 3 || SetMenuWorld.randomWorld == 6)
		{
			foreach (Image image in base.GetComponentsInChildren<Image>())
			{
				if (image.transform.parent.GetComponent<Slider>() == null && image.transform.parent.parent.GetComponent<Slider>() == null)
				{
					image.color = this.lightColor;
				}
			}
			foreach (Text text in base.GetComponentsInChildren<Text>())
			{
				text.color = this.lightColor;
			}
			foreach (Slider slider in base.GetComponentsInChildren<Slider>())
			{
				slider.colors = new ColorBlock
				{
					normalColor = this.lightColor,
					highlightedColor = new Color(0.5f, 0.5f, 0.5f),
					pressedColor = this.lightColor,
					disabledColor = this.lightColor,
					colorMultiplier = 1f
				};
			}
		}
		else
		{
			foreach (Image image2 in base.GetComponentsInChildren<Image>())
			{
				if (image2.transform.parent.GetComponent<Slider>() == null && image2.transform.parent.parent.GetComponent<Slider>() == null)
				{
					image2.color = this.dark;
				}
			}
			foreach (Text text2 in base.GetComponentsInChildren<Text>())
			{
				text2.color = this.dark;
			}
			foreach (Slider slider2 in base.GetComponentsInChildren<Slider>())
			{
				slider2.colors = new ColorBlock
				{
					normalColor = this.dark,
					highlightedColor = new Color(0.5f, 0.5f, 0.5f),
					pressedColor = this.dark,
					disabledColor = this.dark,
					colorMultiplier = 1f
				};
			}
		}
	}

	// Token: 0x04000083 RID: 131
	public Color dark;

	// Token: 0x04000084 RID: 132
	public Color lightColor;
}
