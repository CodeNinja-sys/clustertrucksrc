using System;
using InControl;
using UnityEngine;

namespace MultiplayerBasicExample
{
	// Token: 0x0200003C RID: 60
	public class Player : MonoBehaviour
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00008C70 File Offset: 0x00006E70
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00008C78 File Offset: 0x00006E78
		public InputDevice Device { get; set; }

		// Token: 0x0600014F RID: 335 RVA: 0x00008C84 File Offset: 0x00006E84
		private void Start()
		{
			this.cachedRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008C94 File Offset: 0x00006E94
		private void Update()
		{
			if (this.Device == null)
			{
				this.cachedRenderer.material.color = new Color(1f, 1f, 1f, 0.2f);
			}
			else
			{
				this.cachedRenderer.material.color = this.GetColorFromInput();
				base.transform.Rotate(Vector3.down, 500f * Time.deltaTime * this.Device.Direction.X, Space.World);
				base.transform.Rotate(Vector3.right, 500f * Time.deltaTime * this.Device.Direction.Y, Space.World);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008D4C File Offset: 0x00006F4C
		private Color GetColorFromInput()
		{
			if (this.Device.Action1)
			{
				return Color.green;
			}
			if (this.Device.Action2)
			{
				return Color.red;
			}
			if (this.Device.Action3)
			{
				return Color.blue;
			}
			if (this.Device.Action4)
			{
				return Color.yellow;
			}
			return Color.white;
		}

		// Token: 0x040000F1 RID: 241
		private Renderer cachedRenderer;
	}
}
