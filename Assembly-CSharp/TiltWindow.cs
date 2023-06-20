using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class TiltWindow : MonoBehaviour
{
	// Token: 0x060000AE RID: 174 RVA: 0x00005FB4 File Offset: 0x000041B4
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00005FD4 File Offset: 0x000041D4
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		float x = Mathf.Clamp((mousePosition.x - num) / num, -1f, 1f);
		float y = Mathf.Clamp((mousePosition.y - num2) / num2, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(x, y), Time.deltaTime * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.range.y, this.mRot.x * this.range.x, 0f);
	}

	// Token: 0x0400009E RID: 158
	public Vector2 range = new Vector2(5f, 3f);

	// Token: 0x0400009F RID: 159
	private Transform mTrans;

	// Token: 0x040000A0 RID: 160
	private Quaternion mStart;

	// Token: 0x040000A1 RID: 161
	private Vector2 mRot = Vector2.zero;
}
