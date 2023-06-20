using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002EA RID: 746
public class abilitySliderColor : MonoBehaviour
{
	// Token: 0x06001177 RID: 4471 RVA: 0x00070E50 File Offset: 0x0006F050
	private void OnEnable()
	{
		Image component = this.slider.transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>();
		component.color = this.c;
		Image component2 = this.slider.transform.FindChild("Handle Slide Area").FindChild("Handle").GetComponent<Image>();
		component2.color = this.c;
		Image component3 = this.slider.transform.FindChild("Background").GetComponent<Image>();
		component3.color = this.c3;
		component2.color = new Color(component2.color.r, component2.color.g, component2.color.b, 0f);
		component.color = new Color(component.color.r, component.color.g, component.color.b, 0f);
		component3.color = new Color(component3.color.r, component3.color.g, component3.color.b, 0f);
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x00070F9C File Offset: 0x0006F19C
	private void Update()
	{
	}

	// Token: 0x04000EA3 RID: 3747
	public Slider slider;

	// Token: 0x04000EA4 RID: 3748
	public Color c;

	// Token: 0x04000EA5 RID: 3749
	public Color c3 = new Color(0.15f, 0.15f, 0.15f, 0.5f);
}
