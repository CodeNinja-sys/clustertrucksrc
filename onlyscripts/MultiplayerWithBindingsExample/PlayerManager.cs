using System;
using System.Collections.Generic;
using InControl;
using UnityEngine;

namespace MultiplayerWithBindingsExample
{
	// Token: 0x02000040 RID: 64
	public class PlayerManager : MonoBehaviour
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000950C File Offset: 0x0000770C
		private void OnEnable()
		{
			InputManager.OnDeviceDetached += this.OnDeviceDetached;
			this.keyboardListener = PlayerActions.CreateWithKeyboardBindings();
			this.joystickListener = PlayerActions.CreateWithJoystickBindings();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00009538 File Offset: 0x00007738
		private void OnDisable()
		{
			InputManager.OnDeviceDetached -= this.OnDeviceDetached;
			this.joystickListener.Destroy();
			this.keyboardListener.Destroy();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00009564 File Offset: 0x00007764
		private void Update()
		{
			if (this.JoinButtonWasPressedOnListener(this.joystickListener))
			{
				InputDevice activeDevice = InputManager.ActiveDevice;
				if (this.ThereIsNoPlayerUsingJoystick(activeDevice))
				{
					this.CreatePlayer(activeDevice);
				}
			}
			if (this.JoinButtonWasPressedOnListener(this.keyboardListener) && this.ThereIsNoPlayerUsingKeyboard())
			{
				this.CreatePlayer(null);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000095C0 File Offset: 0x000077C0
		private bool JoinButtonWasPressedOnListener(PlayerActions actions)
		{
			return actions.Green.WasPressed || actions.Red.WasPressed || actions.Blue.WasPressed || actions.Yellow.WasPressed;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000960C File Offset: 0x0000780C
		private Player FindPlayerUsingJoystick(InputDevice inputDevice)
		{
			int count = this.players.Count;
			for (int i = 0; i < count; i++)
			{
				Player player = this.players[i];
				if (player.Actions.Device == inputDevice)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009658 File Offset: 0x00007858
		private bool ThereIsNoPlayerUsingJoystick(InputDevice inputDevice)
		{
			return this.FindPlayerUsingJoystick(inputDevice) == null;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009668 File Offset: 0x00007868
		private Player FindPlayerUsingKeyboard()
		{
			int count = this.players.Count;
			for (int i = 0; i < count; i++)
			{
				Player player = this.players[i];
				if (player.Actions == this.keyboardListener)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000096B4 File Offset: 0x000078B4
		private bool ThereIsNoPlayerUsingKeyboard()
		{
			return this.FindPlayerUsingKeyboard() == null;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000096C4 File Offset: 0x000078C4
		private void OnDeviceDetached(InputDevice inputDevice)
		{
			Player player = this.FindPlayerUsingJoystick(inputDevice);
			if (player != null)
			{
				this.RemovePlayer(player);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000096EC File Offset: 0x000078EC
		private Player CreatePlayer(InputDevice inputDevice)
		{
			if (this.players.Count < 4)
			{
				Vector3 position = this.playerPositions[0];
				this.playerPositions.RemoveAt(0);
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.playerPrefab, position, Quaternion.identity);
				Player component = gameObject.GetComponent<Player>();
				if (inputDevice == null)
				{
					component.Actions = this.keyboardListener;
				}
				else
				{
					PlayerActions playerActions = PlayerActions.CreateWithJoystickBindings();
					playerActions.Device = inputDevice;
					component.Actions = playerActions;
				}
				this.players.Add(component);
				return component;
			}
			return null;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000977C File Offset: 0x0000797C
		private void RemovePlayer(Player player)
		{
			this.playerPositions.Insert(0, player.transform.position);
			this.players.Remove(player);
			player.Actions = null;
			UnityEngine.Object.Destroy(player.gameObject);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000097C0 File Offset: 0x000079C0
		private void OnGUI()
		{
			float num = 10f;
			GUI.Label(new Rect(10f, num, 300f, num + 22f), string.Concat(new object[]
			{
				"Active players: ",
				this.players.Count,
				"/",
				4
			}));
			num += 22f;
			if (this.players.Count < 4)
			{
				GUI.Label(new Rect(10f, num, 300f, num + 22f), "Press a button or a/s/d/f key to join!");
				num += 22f;
			}
		}

		// Token: 0x04000102 RID: 258
		private const int maxPlayers = 4;

		// Token: 0x04000103 RID: 259
		public GameObject playerPrefab;

		// Token: 0x04000104 RID: 260
		private List<Vector3> playerPositions = new List<Vector3>
		{
			new Vector3(-1f, 1f, -10f),
			new Vector3(1f, 1f, -10f),
			new Vector3(-1f, -1f, -10f),
			new Vector3(1f, -1f, -10f)
		};

		// Token: 0x04000105 RID: 261
		private List<Player> players = new List<Player>(4);

		// Token: 0x04000106 RID: 262
		private PlayerActions keyboardListener;

		// Token: 0x04000107 RID: 263
		private PlayerActions joystickListener;
	}
}
