using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D1 RID: 721
public class SliderValueVisual : MonoBehaviour
{
	// Token: 0x06001109 RID: 4361 RVA: 0x0006E9E0 File Offset: 0x0006CBE0
	private void Awake()
	{
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0006E9E4 File Offset: 0x0006CBE4
	public void OnValueChanged()
	{
		if (this.slider == null)
		{
			return;
		}
		this.valueText.text = this.slider.value.ToString("F" + this.numOfDecimals.ToString()) + this.extension;
		CustomSkyBoxHandler.Instance.SkyBoxValueChanged(this.Gradient, this.slider);
	}

	// Token: 0x04000E20 RID: 3616
	public int numOfDecimals = 2;

	// Token: 0x04000E21 RID: 3617
	public string extension = string.Empty;

	// Token: 0x04000E22 RID: 3618
	public bool Gradient;

	// Token: 0x04000E23 RID: 3619
	public Slider slider;

	// Token: 0x04000E24 RID: 3620
	public Text valueText;
}
