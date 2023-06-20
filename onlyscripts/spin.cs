using System;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public class spin : MonoBehaviour
{
	// Token: 0x06001089 RID: 4233 RVA: 0x0006BA90 File Offset: 0x00069C90
	private void Start()
	{
		this.spread = UnityEngine.Random.Range(-this.spread, this.spread);
		this.dir *= 1f - this.spread / this.dir.magnitude;
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x0006BAE0 File Offset: 0x00069CE0
	private void Update()
	{
		if (this.dir.magnitude != 0f)
		{
			base.transform.Rotate(this.dir * Time.deltaTime);
		}
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0006BB20 File Offset: 0x00069D20
	private void sendVectors(Vector3[] v)
	{
		this.dir = v[0];
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x0006BB34 File Offset: 0x00069D34
	private void sendInfo(float[] f)
	{
		this.spread = f[0];
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x0006BB40 File Offset: 0x00069D40
	public void Initalize(string[] _params)
	{
		this.dir = _params[0].ToVector3();
		this.dir = ((!(this.dir == Vector3.zero)) ? this.dir : new Vector3(1f, 0f, 0f));
		this.spread = float.Parse(_params[1]);
		this.spread = UnityEngine.Random.Range(-this.spread, this.spread);
		this.dir *= 1f - this.spread / this.dir.magnitude;
		Debug.Log(string.Concat(new object[]
		{
			"Initalizing: ",
			base.name,
			"With dir: ",
			this.dir,
			" Spread: ",
			this.spread
		}));
	}

	// Token: 0x04000DA1 RID: 3489
	public Vector3 dir = new Vector3(0f, 0f, 0f);

	// Token: 0x04000DA2 RID: 3490
	public float spread;

	// Token: 0x04000DA3 RID: 3491
	public bool alwaysCheck;
}
