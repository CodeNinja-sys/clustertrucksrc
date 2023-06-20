using System;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class SetPositionByAspect : MonoBehaviour
{
	// Token: 0x060010FD RID: 4349 RVA: 0x0006E748 File Offset: 0x0006C948
	private void Start()
	{
		if ((double)Camera.main.aspect >= 1.7)
		{
			Debug.Log("16:9");
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, 22f, base.transform.localPosition.z);
		}
		else if ((double)Camera.main.aspect >= 1.5)
		{
			Debug.Log("3:2");
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, 26f, base.transform.localPosition.z);
		}
		else
		{
			Debug.Log("4:3");
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, 30f, base.transform.localPosition.z);
		}
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0006E864 File Offset: 0x0006CA64
	private void Update()
	{
	}
}
