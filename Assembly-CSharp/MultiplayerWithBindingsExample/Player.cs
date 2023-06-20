using System;
using UnityEngine;

namespace MultiplayerWithBindingsExample
{
	// Token: 0x0200003E RID: 62
	public class Player : MonoBehaviour
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000090D4 File Offset: 0x000072D4
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000090DC File Offset: 0x000072DC
		public PlayerActions Actions { get; set; }

		// Token: 0x0600015F RID: 351 RVA: 0x000090E8 File Offset: 0x000072E8
		private void OnDisable()
		{
			if (this.Actions != null)
			{
				this.Actions.Destroy();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00009100 File Offset: 0x00007300
		private void Start()
		{
			this.cachedRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009110 File Offset: 0x00007310
		private void Update()
		{
			if (this.Actions == null)
			{
				this.cachedRenderer.material.color = new Color(1f, 1f, 1f, 0.2f);
			}
			else
			{
				this.cachedRenderer.material.color = this.GetColorFromInput();
				base.transform.Rotate(Vector3.down, 500f * Time.deltaTime * this.Actions.Rotate.X, Space.World);
				base.transform.Rotate(Vector3.right, 500f * Time.deltaTime * this.Actions.Rotate.Y, Space.World);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000091C8 File Offset: 0x000073C8
		private Color GetColorFromInput()
		{
			if (this.Actions.Green)
			{
				return Color.green;
			}
			if (this.Actions.Red)
			{
				return Color.red;
			}
			if (this.Actions.Blue)
			{
				return Color.blue;
			}
			if (this.Actions.Yellow)
			{
				return Color.yellow;
			}
			return Color.white;
		}

		// Token: 0x040000F7 RID: 247
		private Renderer cachedRenderer;
	}
}
