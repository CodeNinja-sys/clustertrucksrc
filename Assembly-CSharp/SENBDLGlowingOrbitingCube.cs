using System;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class SENBDLGlowingOrbitingCube : MonoBehaviour
{
	// Token: 0x0600110F RID: 4367 RVA: 0x0006EB9C File Offset: 0x0006CD9C
	private Vector3 Vec3(float x)
	{
		return new Vector3(x, x, x);
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0006EBA8 File Offset: 0x0006CDA8
	private void Start()
	{
		base.transform.localScale = this.Vec3(1.5f);
		this.pulseSpeed = UnityEngine.Random.Range(4f, 8f);
		this.phase = UnityEngine.Random.Range(0f, 6.2831855f);
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x0006EBF8 File Offset: 0x0006CDF8
	private void Update()
	{
		Color color = SENBDLGlobal.mainCube.glowColor;
		color.r = 1f - color.r;
		color.g = 1f - color.g;
		color.b = 1f - color.b;
		color = Color.Lerp(color, Color.white, 0.1f);
		color *= Mathf.Pow(Mathf.Sin(Time.time * this.pulseSpeed + this.phase) * 0.49f + 0.51f, 2f);
		base.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
		base.GetComponent<Light>().color = color;
	}

	// Token: 0x04000E2A RID: 3626
	private float pulseSpeed;

	// Token: 0x04000E2B RID: 3627
	private float phase;
}
