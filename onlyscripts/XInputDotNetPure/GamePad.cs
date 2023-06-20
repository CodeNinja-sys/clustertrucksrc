using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{
	// Token: 0x0200010A RID: 266
	public class GamePad
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x000298D4 File Offset: 0x00027AD4
		public static GamePadState GetState(PlayerIndex playerIndex)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(GamePadState.RawState)));
			uint num = Imports.XInputGamePadGetState((uint)playerIndex, intPtr);
			GamePadState.RawState rawState = (GamePadState.RawState)Marshal.PtrToStructure(intPtr, typeof(GamePadState.RawState));
			return new GamePadState(num == 0U, rawState);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00029920 File Offset: 0x00027B20
		public static void SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			Imports.XInputGamePadSetState((uint)playerIndex, leftMotor, rightMotor);
		}
	}
}
