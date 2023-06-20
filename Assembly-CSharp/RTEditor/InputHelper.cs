using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001BD RID: 445
	public static class InputHelper
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x00044BE4 File Offset: 0x00042DE4
		public static bool IsAnyCtrlOrCommandKeyPressed()
		{
			return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00044C2C File Offset: 0x00042E2C
		public static bool IsAnyShiftKeyPressed()
		{
			return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00044C4C File Offset: 0x00042E4C
		public static bool WasLeftMouseButtonPressedInCurrentFrame()
		{
			return Input.GetMouseButtonDown(0);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00044C54 File Offset: 0x00042E54
		public static bool WasLeftMouseButtonReleasedInCurrentFrame()
		{
			return Input.GetMouseButtonUp(0);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00044C5C File Offset: 0x00042E5C
		public static bool WasRightMouseButtonPressedInCurrentFrame()
		{
			return Input.GetMouseButtonDown(1);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00044C64 File Offset: 0x00042E64
		public static bool WasRightMouseButtonReleasedInCurrentFrame()
		{
			return Input.GetMouseButtonUp(1);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00044C6C File Offset: 0x00042E6C
		public static bool WasMiddleMouseButtonPressedInCurrentFrame()
		{
			return Input.GetMouseButtonDown(2);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00044C74 File Offset: 0x00042E74
		public static bool WasMiddleMouseButtonReleasedInCurrentFrame()
		{
			return Input.GetMouseButtonUp(2);
		}
	}
}
