using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000057 RID: 87
	public abstract class PlayerActionSet
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000C928 File Offset: 0x0000AB28
		protected PlayerActionSet()
		{
			this.Actions = new ReadOnlyCollection<PlayerAction>(this.actions);
			this.Enabled = true;
			InputManager.AttachPlayerActionSet(this);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000C990 File Offset: 0x0000AB90
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000C998 File Offset: 0x0000AB98
		public InputDevice Device { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public ReadOnlyCollection<PlayerAction> Actions { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		public ulong UpdateTick { get; protected set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		public bool Enabled { get; set; }

		// Token: 0x0600021F RID: 543 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public void Destroy()
		{
			InputManager.DetachPlayerActionSet(this);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		protected PlayerAction CreatePlayerAction(string name)
		{
			PlayerAction playerAction = new PlayerAction(name, this);
			playerAction.Device = (this.Device ?? InputManager.ActiveDevice);
			if (this.actionsByName.ContainsKey(name))
			{
				throw new InControlException("Action '" + name + "' already exists in this set.");
			}
			this.actions.Add(playerAction);
			this.actionsByName.Add(name, playerAction);
			return playerAction;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CA58 File Offset: 0x0000AC58
		protected PlayerOneAxisAction CreateOneAxisPlayerAction(PlayerAction negativeAction, PlayerAction positiveAction)
		{
			PlayerOneAxisAction playerOneAxisAction = new PlayerOneAxisAction(negativeAction, positiveAction);
			this.oneAxisActions.Add(playerOneAxisAction);
			return playerOneAxisAction;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		protected PlayerTwoAxisAction CreateTwoAxisPlayerAction(PlayerAction negativeXAction, PlayerAction positiveXAction, PlayerAction negativeYAction, PlayerAction positiveYAction)
		{
			PlayerTwoAxisAction playerTwoAxisAction = new PlayerTwoAxisAction(negativeXAction, positiveXAction, negativeYAction, positiveYAction);
			this.twoAxisActions.Add(playerTwoAxisAction);
			return playerTwoAxisAction;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		internal void Update(ulong updateTick, float deltaTime)
		{
			InputDevice device = this.Device ?? InputManager.ActiveDevice;
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				PlayerAction playerAction = this.actions[i];
				playerAction.Update(updateTick, deltaTime, device);
				if (playerAction.UpdateTick > this.UpdateTick)
				{
					this.UpdateTick = playerAction.UpdateTick;
					this.LastInputType = playerAction.LastInputType;
				}
			}
			int count2 = this.oneAxisActions.Count;
			for (int j = 0; j < count2; j++)
			{
				this.oneAxisActions[j].Update(updateTick, deltaTime);
			}
			int count3 = this.twoAxisActions.Count;
			for (int k = 0; k < count3; k++)
			{
				this.twoAxisActions[k].Update(updateTick, deltaTime);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public void Reset()
		{
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].ResetBindings();
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public void ClearInputState()
		{
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].ClearInputState();
			}
			int count2 = this.oneAxisActions.Count;
			for (int j = 0; j < count2; j++)
			{
				this.oneAxisActions[j].ClearInputState();
			}
			int count3 = this.twoAxisActions.Count;
			for (int k = 0; k < count3; k++)
			{
				this.twoAxisActions[k].ClearInputState();
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000CC74 File Offset: 0x0000AE74
		internal bool HasBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.actions[i].HasBinding(binding))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		internal void RemoveBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].FindAndRemoveBinding(binding);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000CD14 File Offset: 0x0000AF14
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		public BindingListenOptions ListenOptions
		{
			get
			{
				return this.listenOptions;
			}
			set
			{
				this.listenOptions = (value ?? new BindingListenOptions());
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CD34 File Offset: 0x0000AF34
		public string Save()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					binaryWriter.Write(66);
					binaryWriter.Write(73);
					binaryWriter.Write(78);
					binaryWriter.Write(68);
					binaryWriter.Write(1);
					int count = this.actions.Count;
					binaryWriter.Write(count);
					for (int i = 0; i < count; i++)
					{
						this.actions[i].Save(binaryWriter);
					}
				}
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000CE20 File Offset: 0x0000B020
		public void Load(string data)
		{
			if (data == null)
			{
				return;
			}
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(data)))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						if (binaryReader.ReadUInt32() != 1145981250U)
						{
							throw new Exception("Unknown data format.");
						}
						if (binaryReader.ReadUInt16() != 1)
						{
							throw new Exception("Unknown data version.");
						}
						int num = binaryReader.ReadInt32();
						for (int i = 0; i < num; i++)
						{
							PlayerAction playerAction;
							if (this.actionsByName.TryGetValue(binaryReader.ReadString(), out playerAction))
							{
								playerAction.Load(binaryReader);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Provided state could not be loaded:\n" + ex.Message);
				this.Reset();
			}
		}

		// Token: 0x040001C5 RID: 453
		public BindingSourceType LastInputType;

		// Token: 0x040001C6 RID: 454
		private List<PlayerAction> actions = new List<PlayerAction>();

		// Token: 0x040001C7 RID: 455
		private List<PlayerOneAxisAction> oneAxisActions = new List<PlayerOneAxisAction>();

		// Token: 0x040001C8 RID: 456
		private List<PlayerTwoAxisAction> twoAxisActions = new List<PlayerTwoAxisAction>();

		// Token: 0x040001C9 RID: 457
		private Dictionary<string, PlayerAction> actionsByName = new Dictionary<string, PlayerAction>();

		// Token: 0x040001CA RID: 458
		private BindingListenOptions listenOptions = new BindingListenOptions();

		// Token: 0x040001CB RID: 459
		internal PlayerAction listenWithAction;
	}
}
