using System;

namespace XInputDotNetPure
{
	// Token: 0x02000105 RID: 261
	public struct GamePadState
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x000295DC File Offset: 0x000277DC
		internal GamePadState(bool isConnected, GamePadState.RawState rawState)
		{
			this.isConnected = isConnected;
			if (!isConnected)
			{
				rawState.dwPacketNumber = 0U;
				rawState.Gamepad.dwButtons = 0;
				rawState.Gamepad.bLeftTrigger = 0;
				rawState.Gamepad.bRightTrigger = 0;
				rawState.Gamepad.sThumbLX = 0;
				rawState.Gamepad.sThumbLY = 0;
				rawState.Gamepad.sThumbRX = 0;
				rawState.Gamepad.sThumbRY = 0;
			}
			this.packetNumber = rawState.dwPacketNumber;
			this.buttons = new GamePadButtons(((rawState.Gamepad.dwButtons & 16) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 32) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 64) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 128) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 256) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 512) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 4096) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 8192) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 16384) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 32768) == 0) ? ButtonState.Released : ButtonState.Pressed);
			this.dPad = new GamePadDPad(((rawState.Gamepad.dwButtons & 1) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 2) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 4) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 8) == 0) ? ButtonState.Released : ButtonState.Pressed);
			this.thumbSticks = new GamePadThumbSticks(new GamePadThumbSticks.StickValue((float)rawState.Gamepad.sThumbLX / 32767f, (float)rawState.Gamepad.sThumbLY / 32767f), new GamePadThumbSticks.StickValue((float)rawState.Gamepad.sThumbRX / 32767f, (float)rawState.Gamepad.sThumbRY / 32767f));
			this.triggers = new GamePadTriggers((float)rawState.Gamepad.bLeftTrigger / 255f, (float)rawState.Gamepad.bRightTrigger / 255f);
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0002989C File Offset: 0x00027A9C
		public uint PacketNumber
		{
			get
			{
				return this.packetNumber;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000298A4 File Offset: 0x00027AA4
		public bool IsConnected
		{
			get
			{
				return this.isConnected;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000298AC File Offset: 0x00027AAC
		public GamePadButtons Buttons
		{
			get
			{
				return this.buttons;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x000298B4 File Offset: 0x00027AB4
		public GamePadDPad DPad
		{
			get
			{
				return this.dPad;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x000298BC File Offset: 0x00027ABC
		public GamePadTriggers Triggers
		{
			get
			{
				return this.triggers;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x000298C4 File Offset: 0x00027AC4
		public GamePadThumbSticks ThumbSticks
		{
			get
			{
				return this.thumbSticks;
			}
		}

		// Token: 0x0400041D RID: 1053
		private bool isConnected;

		// Token: 0x0400041E RID: 1054
		private uint packetNumber;

		// Token: 0x0400041F RID: 1055
		private GamePadButtons buttons;

		// Token: 0x04000420 RID: 1056
		private GamePadDPad dPad;

		// Token: 0x04000421 RID: 1057
		private GamePadThumbSticks thumbSticks;

		// Token: 0x04000422 RID: 1058
		private GamePadTriggers triggers;

		// Token: 0x02000106 RID: 262
		internal struct RawState
		{
			// Token: 0x04000423 RID: 1059
			public uint dwPacketNumber;

			// Token: 0x04000424 RID: 1060
			public GamePadState.RawState.GamePad Gamepad;

			// Token: 0x02000107 RID: 263
			public struct GamePad
			{
				// Token: 0x04000425 RID: 1061
				public ushort dwButtons;

				// Token: 0x04000426 RID: 1062
				public byte bLeftTrigger;

				// Token: 0x04000427 RID: 1063
				public byte bRightTrigger;

				// Token: 0x04000428 RID: 1064
				public short sThumbLX;

				// Token: 0x04000429 RID: 1065
				public short sThumbLY;

				// Token: 0x0400042A RID: 1066
				public short sThumbRX;

				// Token: 0x0400042B RID: 1067
				public short sThumbRY;
			}
		}

		// Token: 0x02000108 RID: 264
		private enum ButtonsConstants
		{
			// Token: 0x0400042D RID: 1069
			DPadUp = 1,
			// Token: 0x0400042E RID: 1070
			DPadDown,
			// Token: 0x0400042F RID: 1071
			DPadLeft = 4,
			// Token: 0x04000430 RID: 1072
			DPadRight = 8,
			// Token: 0x04000431 RID: 1073
			Start = 16,
			// Token: 0x04000432 RID: 1074
			Back = 32,
			// Token: 0x04000433 RID: 1075
			LeftThumb = 64,
			// Token: 0x04000434 RID: 1076
			RightThumb = 128,
			// Token: 0x04000435 RID: 1077
			LeftShoulder = 256,
			// Token: 0x04000436 RID: 1078
			RightShoulder = 512,
			// Token: 0x04000437 RID: 1079
			A = 4096,
			// Token: 0x04000438 RID: 1080
			B = 8192,
			// Token: 0x04000439 RID: 1081
			X = 16384,
			// Token: 0x0400043A RID: 1082
			Y = 32768
		}
	}
}
