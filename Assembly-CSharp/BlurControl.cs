using System;
using UnityEngine;

// Token: 0x020001E3 RID: 483
public class BlurControl : MonoBehaviour
{
	// Token: 0x06000B64 RID: 2916 RVA: 0x00046DF8 File Offset: 0x00044FF8
	private void Start()
	{
		this.value = 0f;
		base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00046E30 File Offset: 0x00045030
	private void Update()
	{
		if (Input.GetButton("Up"))
		{
			this.value += Time.deltaTime;
			if (this.value > 20f)
			{
				this.value = 20f;
			}
			base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
		}
		else if (Input.GetButton("Down"))
		{
			this.value = (this.value - Time.deltaTime) % 20f;
			if (this.value < 0f)
			{
				this.value = 0f;
			}
			base.transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", this.value);
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x00046F00 File Offset: 0x00045100
	private void OnGUI()
	{
		GUI.TextArea(new Rect(10f, 10f, 200f, 50f), "Press the 'Up' and 'Down' arrows \nto interact with the blur plane\nCurrent value: " + this.value);
	}

	// Token: 0x04000814 RID: 2068
	private float value;
}
