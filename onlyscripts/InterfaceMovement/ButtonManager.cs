using System;
using InControl;
using UnityEngine;

namespace InterfaceMovement
{
	// Token: 0x0200003A RID: 58
	public class ButtonManager : MonoBehaviour
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00008B3C File Offset: 0x00006D3C
		private void Awake()
		{
			this.filteredDirection = new TwoAxisInputControl();
			this.filteredDirection.StateThreshold = 0.5f;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008B5C File Offset: 0x00006D5C
		private void Update()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			this.filteredDirection.Filter(activeDevice.Direction, Time.deltaTime);
			if (this.filteredDirection.Left.WasRepeated)
			{
				Debug.Log("!!!");
			}
			if (this.filteredDirection.Up.WasPressed)
			{
				this.MoveFocusTo(this.focusedButton.up);
			}
			if (this.filteredDirection.Down.WasPressed)
			{
				this.MoveFocusTo(this.focusedButton.down);
			}
			if (this.filteredDirection.Left.WasPressed)
			{
				this.MoveFocusTo(this.focusedButton.left);
			}
			if (this.filteredDirection.Right.WasPressed)
			{
				this.MoveFocusTo(this.focusedButton.right);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008C3C File Offset: 0x00006E3C
		private void MoveFocusTo(Button newFocusedButton)
		{
			if (newFocusedButton != null)
			{
				this.focusedButton = newFocusedButton;
			}
		}

		// Token: 0x040000EF RID: 239
		public Button focusedButton;

		// Token: 0x040000F0 RID: 240
		private TwoAxisInputControl filteredDirection;
	}
}
