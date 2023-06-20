using System;
using InControl;

namespace MultiplayerWithBindingsExample
{
	// Token: 0x0200003F RID: 63
	public class PlayerActions : PlayerActionSet
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00009248 File Offset: 0x00007448
		public PlayerActions()
		{
			this.Green = base.CreatePlayerAction("Green");
			this.Red = base.CreatePlayerAction("Red");
			this.Blue = base.CreatePlayerAction("Blue");
			this.Yellow = base.CreatePlayerAction("Yellow");
			this.Left = base.CreatePlayerAction("Left");
			this.Right = base.CreatePlayerAction("Right");
			this.Up = base.CreatePlayerAction("Up");
			this.Down = base.CreatePlayerAction("Down");
			this.Rotate = base.CreateTwoAxisPlayerAction(this.Left, this.Right, this.Down, this.Up);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009308 File Offset: 0x00007508
		public static PlayerActions CreateWithKeyboardBindings()
		{
			PlayerActions playerActions = new PlayerActions();
			playerActions.Green.AddDefaultBinding(new Key[]
			{
				Key.A
			});
			playerActions.Red.AddDefaultBinding(new Key[]
			{
				Key.S
			});
			playerActions.Blue.AddDefaultBinding(new Key[]
			{
				Key.D
			});
			playerActions.Yellow.AddDefaultBinding(new Key[]
			{
				Key.F
			});
			playerActions.Up.AddDefaultBinding(new Key[]
			{
				Key.UpArrow
			});
			playerActions.Down.AddDefaultBinding(new Key[]
			{
				Key.DownArrow
			});
			playerActions.Left.AddDefaultBinding(new Key[]
			{
				Key.LeftArrow
			});
			playerActions.Right.AddDefaultBinding(new Key[]
			{
				Key.RightArrow
			});
			return playerActions;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000093CC File Offset: 0x000075CC
		public static PlayerActions CreateWithJoystickBindings()
		{
			PlayerActions playerActions = new PlayerActions();
			playerActions.Green.AddDefaultBinding(InputControlType.Action1);
			playerActions.Red.AddDefaultBinding(InputControlType.Action2);
			playerActions.Blue.AddDefaultBinding(InputControlType.Action3);
			playerActions.Yellow.AddDefaultBinding(InputControlType.Action4);
			playerActions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
			playerActions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
			playerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
			playerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
			playerActions.Up.AddDefaultBinding(InputControlType.DPadUp);
			playerActions.Down.AddDefaultBinding(InputControlType.DPadDown);
			playerActions.Left.AddDefaultBinding(InputControlType.DPadLeft);
			playerActions.Right.AddDefaultBinding(InputControlType.DPadRight);
			return playerActions;
		}

		// Token: 0x040000F9 RID: 249
		public PlayerAction Green;

		// Token: 0x040000FA RID: 250
		public PlayerAction Red;

		// Token: 0x040000FB RID: 251
		public PlayerAction Blue;

		// Token: 0x040000FC RID: 252
		public PlayerAction Yellow;

		// Token: 0x040000FD RID: 253
		public PlayerAction Left;

		// Token: 0x040000FE RID: 254
		public PlayerAction Right;

		// Token: 0x040000FF RID: 255
		public PlayerAction Up;

		// Token: 0x04000100 RID: 256
		public PlayerAction Down;

		// Token: 0x04000101 RID: 257
		public PlayerTwoAxisAction Rotate;
	}
}
