using System;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class SpinBackAndForth : MonoBehaviour
{
	// Token: 0x06000E96 RID: 3734 RVA: 0x0005E578 File Offset: 0x0005C778
	private void Start()
	{
		if (this._UseOffset)
		{
			this._Offset = -base.transform.position.z / 4f;
		}
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x0005E5B0 File Offset: 0x0005C7B0
	private void Update()
	{
		this._Time += Time.deltaTime;
		float num = Mathf.Sin((this._Time - this._Offset * this._OffsetSize) / this._Period);
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, num * this._Angle, 0f));
	}

	// Token: 0x04000B24 RID: 2852
	public float _Angle;

	// Token: 0x04000B25 RID: 2853
	public float _Period;

	// Token: 0x04000B26 RID: 2854
	public float _OffsetSize = 0.1f;

	// Token: 0x04000B27 RID: 2855
	public bool _UseOffset = true;

	// Token: 0x04000B28 RID: 2856
	private float _Offset;

	// Token: 0x04000B29 RID: 2857
	private float _Time;
}
