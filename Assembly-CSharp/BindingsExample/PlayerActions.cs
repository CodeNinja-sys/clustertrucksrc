using System;
using InControl;
using UnityEngine;

namespace BindingsExample
{
	// Token: 0x02000037 RID: 55
	public class PlayerActions : PlayerActionSet
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00008710 File Offset: 0x00006910
		public PlayerActions()
		{
			this.Fire = base.CreatePlayerAction("Fire");
			this.Jump = base.CreatePlayerAction("Jump");
			this.Left = base.CreatePlayerAction("Move Left");
			this.Right = base.CreatePlayerAction("Move Right");
			this.Up = base.CreatePlayerAction("Move Up");
			this.Down = base.CreatePlayerAction("Move Down");
			this.Move = base.CreateTwoAxisPlayerAction(this.Left, this.Right, this.Down, this.Up);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000087B0 File Offset: 0x000069B0
		public static PlayerActions CreateWithDefaultBindings()
		{
			PlayerActions playerActions = new PlayerActions();
			playerActions.Fire.AddDefaultBinding(new Key[]
			{
				Key.Shift,
				Key.A
			});
			playerActions.Fire.AddDefaultBinding(InputControlType.Action1);
			playerActions.Fire.AddDefaultBinding(Mouse.LeftButton);
			playerActions.Jump.AddDefaultBinding(new Key[]
			{
				Key.Space
			});
			playerActions.Jump.AddDefaultBinding(InputControlType.Action3);
			playerActions.Jump.AddDefaultBinding(InputControlType.Back);
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
			playerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
			playerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
			playerActions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
			playerActions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
			playerActions.Left.AddDefaultBinding(InputControlType.DPadLeft);
			playerActions.Right.AddDefaultBinding(InputControlType.DPadRight);
			playerActions.Up.AddDefaultBinding(InputControlType.DPadUp);
			playerActions.Down.AddDefaultBinding(InputControlType.DPadDown);
			playerActions.Up.AddDefaultBinding(Mouse.PositiveY);
			playerActions.Down.AddDefaultBinding(Mouse.NegativeY);
			playerActions.Left.AddDefaultBinding(Mouse.NegativeX);
			playerActions.Right.AddDefaultBinding(Mouse.PositiveX);
			playerActions.ListenOptions.IncludeUnknownControllers = true;
			playerActions.ListenOptions.MaxAllowedBindings = 4U;
			playerActions.ListenOptions.OnBindingFound = delegate(PlayerAction action, BindingSource binding)
			{
				if (binding == new KeyBindingSource(new Key[]
				{
					Key.Escape
				}))
				{
					action.StopListeningForBinding();
					return false;
				}
				return true;
			};
			BindingListenOptions listenOptions = playerActions.ListenOptions;
			listenOptions.OnBindingAdded = (Action<PlayerAction, BindingSource>)Delegate.Combine(listenOptions.OnBindingAdded, new Action<PlayerAction, BindingSource>(delegate(PlayerAction action, BindingSource binding)
			{
				Debug.Log("Binding added... " + binding.DeviceName + ": " + binding.Name);
			}));
			BindingListenOptions listenOptions2 = playerActions.ListenOptions;
			listenOptions2.OnBindingRejected = (Action<PlayerAction, BindingSource, BindingSourceRejectionType>)Delegate.Combine(listenOptions2.OnBindingRejected, new Action<PlayerAction, BindingSource, BindingSourceRejectionType>(delegate(PlayerAction action, BindingSource binding, BindingSourceRejectionType reason)
			{
				Debug.Log("Binding rejected... " + reason);
			}));
			return playerActions;
		}

		// Token: 0x040000E0 RID: 224
		public PlayerAction Fire;

		// Token: 0x040000E1 RID: 225
		public PlayerAction Jump;

		// Token: 0x040000E2 RID: 226
		public PlayerAction Left;

		// Token: 0x040000E3 RID: 227
		public PlayerAction Right;

		// Token: 0x040000E4 RID: 228
		public PlayerAction Up;

		// Token: 0x040000E5 RID: 229
		public PlayerAction Down;

		// Token: 0x040000E6 RID: 230
		public PlayerTwoAxisAction Move;
	}
}
