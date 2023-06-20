using System;
using System.Collections.Generic;
using InControl;
using UnityEngine;

namespace MultiplayerBasicExample
{
	// Token: 0x0200003D RID: 61
	public class PlayerManager : MonoBehaviour
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00008E60 File Offset: 0x00007060
		private void Start()
		{
			InputManager.OnDeviceDetached += this.OnDeviceDetached;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008E74 File Offset: 0x00007074
		private void Update()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			if (this.JoinButtonWasPressedOnDevice(activeDevice) && this.ThereIsNoPlayerUsingDevice(activeDevice))
			{
				this.CreatePlayer(activeDevice);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008EA8 File Offset: 0x000070A8
		private bool JoinButtonWasPressedOnDevice(InputDevice inputDevice)
		{
			return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008EF4 File Offset: 0x000070F4
		private Player FindPlayerUsingDevice(InputDevice inputDevice)
		{
			int count = this.players.Count;
			for (int i = 0; i < count; i++)
			{
				Player player = this.players[i];
				if (player.Device == inputDevice)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008F3C File Offset: 0x0000713C
		private bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice)
		{
			return this.FindPlayerUsingDevice(inputDevice) == null;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008F4C File Offset: 0x0000714C
		private void OnDeviceDetached(InputDevice inputDevice)
		{
			Player player = this.FindPlayerUsingDevice(inputDevice);
			if (player != null)
			{
				this.RemovePlayer(player);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008F74 File Offset: 0x00007174
		private Player CreatePlayer(InputDevice inputDevice)
		{
			if (this.players.Count < 4)
			{
				Vector3 position = this.playerPositions[0];
				this.playerPositions.RemoveAt(0);
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.playerPrefab, position, Quaternion.identity);
				Player component = gameObject.GetComponent<Player>();
				component.Device = inputDevice;
				this.players.Add(component);
				return component;
			}
			return null;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008FE0 File Offset: 0x000071E0
		private void RemovePlayer(Player player)
		{
			this.playerPositions.Insert(0, player.transform.position);
			this.players.Remove(player);
			player.Device = null;
			UnityEngine.Object.Destroy(player.gameObject);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00009024 File Offset: 0x00007224
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
				GUI.Label(new Rect(10f, num, 300f, num + 22f), "Press a button to join!");
				num += 22f;
			}
		}

		// Token: 0x040000F3 RID: 243
		private const int maxPlayers = 4;

		// Token: 0x040000F4 RID: 244
		public GameObject playerPrefab;

		// Token: 0x040000F5 RID: 245
		private List<Vector3> playerPositions = new List<Vector3>
		{
			new Vector3(-1f, 1f, -10f),
			new Vector3(1f, 1f, -10f),
			new Vector3(-1f, -1f, -10f),
			new Vector3(1f, -1f, -10f)
		};

		// Token: 0x040000F6 RID: 246
		private List<Player> players = new List<Player>(4);
	}
}
