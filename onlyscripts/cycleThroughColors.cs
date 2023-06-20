using System;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class cycleThroughColors : MonoBehaviour
{
	// Token: 0x06000F17 RID: 3863 RVA: 0x00062384 File Offset: 0x00060584
	private void Start()
	{
		if (this.colors == null || this.colors.Length < 2)
		{
			Debug.Log("Need to setup colors array in inspector");
		}
		this.nextIndex = (this.currentIndex + 1) % this.colors.Length;
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x000623CC File Offset: 0x000605CC
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > this.changeColourTime)
		{
			this.currentIndex = (this.currentIndex + 1) % this.colors.Length;
			this.nextIndex = (this.currentIndex + 1) % this.colors.Length;
			this.timer = 0f;
		}
		this.mat.SetColor("_EmissionColor", Color.Lerp(this.colors[this.currentIndex] * 15f, this.colors[this.nextIndex] * 15f, this.timer / this.changeColourTime));
	}

	// Token: 0x04000BD7 RID: 3031
	public Color[] colors;

	// Token: 0x04000BD8 RID: 3032
	public Material mat;

	// Token: 0x04000BD9 RID: 3033
	public int currentIndex;

	// Token: 0x04000BDA RID: 3034
	private int nextIndex;

	// Token: 0x04000BDB RID: 3035
	public float changeColourTime = 2f;

	// Token: 0x04000BDC RID: 3036
	private float lastChange;

	// Token: 0x04000BDD RID: 3037
	private float timer;
}
