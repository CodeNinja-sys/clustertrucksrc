using System;
using UnityEngine;

// Token: 0x020001ED RID: 493
public class Flamethrower : MonoBehaviour
{
	// Token: 0x06000BA2 RID: 2978 RVA: 0x00048334 File Offset: 0x00046534
	private void Start()
	{
		this.au.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
		if (this.interval > 0f)
		{
			this.useInterval = true;
			this.intervalCounter = this.interval;
		}
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00048374 File Offset: 0x00046574
	private void Update()
	{
		if (this.useInterval)
		{
			this.intervalCounter -= Time.deltaTime;
			if (this.intervalCounter < 0f)
			{
				this.intervalCounter = this.interval;
				this.waiting = !this.waiting;
				this.timeSinceSwitch = 0f;
			}
		}
		bool flag = true;
		if (!this.isOn || this.waiting)
		{
			flag = false;
		}
		this.timeSinceSwitch += Time.deltaTime;
		if (this.timeSinceSwitch > 0.2f)
		{
			this.collider1.enabled = flag;
		}
		if (this.timeSinceSwitch > 0.4f)
		{
			this.collider2.enabled = flag;
		}
		if (this.timeSinceSwitch > 0.7f)
		{
			this.collider3.enabled = flag;
		}
		if (flag)
		{
			this.parts.emission.enabled = true;
			this.parts2.emission.enabled = true;
			this.parts3.emission.enabled = true;
			this.au.volume = Mathf.Lerp(this.au.volume, 0.15f, Time.deltaTime * 10f);
			this.light1.intensity = Mathf.MoveTowards(this.light1.intensity, 2f, Time.deltaTime * 2f);
			this.light2.intensity = Mathf.MoveTowards(this.light2.intensity, 8f, Time.deltaTime * 8f);
		}
		else
		{
			this.parts.emission.enabled = false;
			this.parts2.emission.enabled = false;
			this.parts3.emission.enabled = false;
			this.au.volume = Mathf.Lerp(this.au.volume, -0.1f, Time.deltaTime * 10f);
			this.light1.intensity = Mathf.MoveTowards(this.light1.intensity, 0f, Time.deltaTime * 2f);
			this.light2.intensity = Mathf.MoveTowards(this.light2.intensity, 0f, Time.deltaTime * 8f);
		}
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x000485DC File Offset: 0x000467DC
	private void Pull()
	{
		this.isOn = !this.isOn;
		this.timeSinceSwitch = 0f;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x000485F8 File Offset: 0x000467F8
	private void sendInfo(float[] info)
	{
		this.interval = info[0];
	}

	// Token: 0x0400082D RID: 2093
	private float timeSinceSwitch;

	// Token: 0x0400082E RID: 2094
	public bool isOn = true;

	// Token: 0x0400082F RID: 2095
	public bool waiting;

	// Token: 0x04000830 RID: 2096
	public Collider collider1;

	// Token: 0x04000831 RID: 2097
	public Collider collider2;

	// Token: 0x04000832 RID: 2098
	public Collider collider3;

	// Token: 0x04000833 RID: 2099
	public ParticleSystem parts;

	// Token: 0x04000834 RID: 2100
	public ParticleSystem parts2;

	// Token: 0x04000835 RID: 2101
	public ParticleSystem parts3;

	// Token: 0x04000836 RID: 2102
	public Light light1;

	// Token: 0x04000837 RID: 2103
	public Light light2;

	// Token: 0x04000838 RID: 2104
	public float interval;

	// Token: 0x04000839 RID: 2105
	public float intervalCounter;

	// Token: 0x0400083A RID: 2106
	public bool useInterval;

	// Token: 0x0400083B RID: 2107
	public AudioSource au;
}
