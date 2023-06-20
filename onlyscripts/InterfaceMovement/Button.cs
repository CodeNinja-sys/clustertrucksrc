using System;
using UnityEngine;

namespace InterfaceMovement
{
	// Token: 0x02000038 RID: 56
	public class Button : MonoBehaviour
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00008A48 File Offset: 0x00006C48
		private void Start()
		{
			this.cachedRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008A58 File Offset: 0x00006C58
		private void Update()
		{
			bool flag = base.transform.parent.GetComponent<ButtonManager>().focusedButton == this;
			Color color = this.cachedRenderer.material.color;
			color.a = Mathf.MoveTowards(color.a, (!flag) ? 0.5f : 1f, Time.deltaTime * 3f);
			this.cachedRenderer.material.color = color;
		}

		// Token: 0x040000EA RID: 234
		private Renderer cachedRenderer;

		// Token: 0x040000EB RID: 235
		public Button up;

		// Token: 0x040000EC RID: 236
		public Button down;

		// Token: 0x040000ED RID: 237
		public Button left;

		// Token: 0x040000EE RID: 238
		public Button right;
	}
}
