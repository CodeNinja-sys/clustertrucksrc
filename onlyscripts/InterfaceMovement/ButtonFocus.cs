using System;
using UnityEngine;

namespace InterfaceMovement
{
	// Token: 0x02000039 RID: 57
	public class ButtonFocus : MonoBehaviour
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00008AE0 File Offset: 0x00006CE0
		private void Update()
		{
			Button focusedButton = base.transform.parent.GetComponent<ButtonManager>().focusedButton;
			base.transform.position = Vector3.MoveTowards(base.transform.position, focusedButton.transform.position, Time.deltaTime * 10f);
		}
	}
}
