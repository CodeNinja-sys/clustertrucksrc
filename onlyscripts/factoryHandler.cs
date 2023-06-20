using System;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class factoryHandler : MonoBehaviour
{
	// Token: 0x060011A2 RID: 4514 RVA: 0x00071CA8 File Offset: 0x0006FEA8
	private void Start()
	{
		this.factoryMat.SetColor("_EmissionColor", this.c1 * 15f);
		this.skyMat.SetColor("_Color1", this.csa1);
		this.skyMat.SetColor("_Color2", this.csa2);
		RenderSettings.fogColor = this.fog1;
		this.Light.color = this.lc1;
		RenderSettings.fogDensity = 0.0025f;
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x00071D28 File Offset: 0x0006FF28
	private void Update()
	{
		if (Camera.main == null)
		{
			return;
		}
		this.counter += Time.deltaTime;
		this.startCounter += Time.deltaTime;
		if (this.startCounter > 12f && this.startCounter < 20f)
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
				Camera.main.GetComponent<cameraEffects>().SetShake(0.3f, Vector3.zero);
				this.au.PlayOneShot(this.au.clip);
			}
			if (this.c1Bool)
			{
				this.factoryMat.SetColor("_EmissionColor", this.c1 * 15f);
				this.skyMat.SetColor("_Color1", this.csa1);
				this.skyMat.SetColor("_Color2", this.csa2);
				RenderSettings.fogColor = this.fog1;
				this.Light.color = this.lc1;
			}
			else
			{
				this.factoryMat.SetColor("_EmissionColor", this.c2 * 15f);
				this.skyMat.SetColor("_Color1", this.csb1);
				this.skyMat.SetColor("_Color2", this.csb2);
				RenderSettings.fogColor = this.fog2;
				this.Light.color = this.lc2;
			}
		}
		else if (!this.c1Bool && this.startCounter > 20f)
		{
			this.factoryMat.SetColor("_EmissionColor", this.c1 * 15f);
			this.skyMat.SetColor("_Color1", this.csa1);
			this.skyMat.SetColor("_Color2", this.csa2);
			RenderSettings.fogColor = this.fog1;
			this.Light.color = this.lc1;
		}
	}

	// Token: 0x04000EC2 RID: 3778
	public Material factoryMat;

	// Token: 0x04000EC3 RID: 3779
	public Material skyMat;

	// Token: 0x04000EC4 RID: 3780
	public Color c1;

	// Token: 0x04000EC5 RID: 3781
	public Color c2;

	// Token: 0x04000EC6 RID: 3782
	public Color csa1;

	// Token: 0x04000EC7 RID: 3783
	public Color csa2;

	// Token: 0x04000EC8 RID: 3784
	public Color csb1;

	// Token: 0x04000EC9 RID: 3785
	public Color csb2;

	// Token: 0x04000ECA RID: 3786
	public Color fog1;

	// Token: 0x04000ECB RID: 3787
	public Color fog2;

	// Token: 0x04000ECC RID: 3788
	public Color lc1;

	// Token: 0x04000ECD RID: 3789
	public Color lc2;

	// Token: 0x04000ECE RID: 3790
	public Light Light;

	// Token: 0x04000ECF RID: 3791
	public AudioSource au;

	// Token: 0x04000ED0 RID: 3792
	private float counter = 10f;

	// Token: 0x04000ED1 RID: 3793
	private float speed = 1f;

	// Token: 0x04000ED2 RID: 3794
	private bool c1Bool = true;

	// Token: 0x04000ED3 RID: 3795
	private float startCounter;
}
