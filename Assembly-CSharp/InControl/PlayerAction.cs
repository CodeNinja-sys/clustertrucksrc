using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000056 RID: 86
	public class PlayerAction : InputControlBase
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x0000BD30 File Offset: 0x00009F30
		public PlayerAction(string name, PlayerActionSet owner)
		{
			this.Raw = true;
			this.Name = name;
			this.Owner = owner;
			this.bindings = new ReadOnlyCollection<BindingSource>(this.visibleBindings);
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000BDBC File Offset: 0x00009FBC
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public string Name { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		public PlayerActionSet Owner { get; private set; }

		// Token: 0x060001F7 RID: 503 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public void AddDefaultBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			if (binding.BoundTo != null)
			{
				throw new InControlException("Binding source is already bound to action " + binding.BoundTo.Name);
			}
			if (!this.defaultBindings.Contains(binding))
			{
				this.defaultBindings.Add(binding);
				binding.BoundTo = this;
			}
			if (!this.regularBindings.Contains(binding))
			{
				this.regularBindings.Add(binding);
				binding.BoundTo = this;
				if (binding.IsValid)
				{
					this.visibleBindings.Add(binding);
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000BE84 File Offset: 0x0000A084
		public void AddDefaultBinding(params Key[] keys)
		{
			this.AddDefaultBinding(new KeyBindingSource(keys));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000BE94 File Offset: 0x0000A094
		public void AddDefaultBinding(Mouse control)
		{
			this.AddDefaultBinding(new MouseBindingSource(control));
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public void AddDefaultBinding(InputControlType control)
		{
			this.AddDefaultBinding(new DeviceBindingSource(control));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public bool AddBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			if (binding.BoundTo != null)
			{
				Debug.LogWarning("Binding source is already bound to action " + binding.BoundTo.Name);
				return false;
			}
			if (this.regularBindings.Contains(binding))
			{
				return false;
			}
			this.regularBindings.Add(binding);
			binding.BoundTo = this;
			if (binding.IsValid)
			{
				this.visibleBindings.Add(binding);
			}
			return true;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000BF34 File Offset: 0x0000A134
		public bool InsertBindingAt(int index, BindingSource binding)
		{
			if (index < 0 || index > this.visibleBindings.Count)
			{
				throw new InControlException("Index is out of range for bindings on this action.");
			}
			if (index == this.visibleBindings.Count)
			{
				return this.AddBinding(binding);
			}
			if (binding == null)
			{
				return false;
			}
			if (binding.BoundTo != null)
			{
				Debug.LogWarning("Binding source is already bound to action " + binding.BoundTo.Name);
				return false;
			}
			if (this.regularBindings.Contains(binding))
			{
				return false;
			}
			int index2 = (index != 0) ? this.regularBindings.IndexOf(this.visibleBindings[index]) : 0;
			this.regularBindings.Insert(index2, binding);
			binding.BoundTo = this;
			if (binding.IsValid)
			{
				this.visibleBindings.Insert(index, binding);
			}
			return true;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000C018 File Offset: 0x0000A218
		public bool ReplaceBinding(BindingSource findBinding, BindingSource withBinding)
		{
			if (findBinding == null || withBinding == null)
			{
				return false;
			}
			if (withBinding.BoundTo != null)
			{
				Debug.LogWarning("Binding source is already bound to action " + withBinding.BoundTo.Name);
				return false;
			}
			int num = this.regularBindings.IndexOf(findBinding);
			if (num < 0)
			{
				Debug.LogWarning("Binding source to replace is not present in this action.");
				return false;
			}
			Debug.Log("index = " + num);
			findBinding.BoundTo = null;
			this.regularBindings[num] = withBinding;
			withBinding.BoundTo = this;
			num = this.visibleBindings.IndexOf(findBinding);
			if (num >= 0)
			{
				this.visibleBindings[num] = withBinding;
			}
			return true;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C0D8 File Offset: 0x0000A2D8
		internal bool HasBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			BindingSource bindingSource = this.FindBinding(binding);
			return !(bindingSource == null) && bindingSource.BoundTo == this;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C114 File Offset: 0x0000A314
		internal BindingSource FindBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return null;
			}
			int num = this.regularBindings.IndexOf(binding);
			if (num >= 0)
			{
				return this.regularBindings[num];
			}
			return null;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000C154 File Offset: 0x0000A354
		internal void FindAndRemoveBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			int num = this.regularBindings.IndexOf(binding);
			if (num >= 0)
			{
				BindingSource bindingSource = this.regularBindings[num];
				if (bindingSource.BoundTo == this)
				{
					bindingSource.BoundTo = null;
					this.regularBindings.RemoveAt(num);
					this.UpdateVisibleBindings();
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		internal int CountBindingsOfType(BindingSourceType bindingSourceType)
		{
			int num = 0;
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo == this && bindingSource.BindingSourceType == bindingSourceType)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000C20C File Offset: 0x0000A40C
		internal void RemoveFirstBindingOfType(BindingSourceType bindingSourceType)
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo == this && bindingSource.BindingSourceType == bindingSourceType)
				{
					bindingSource.BoundTo = null;
					this.regularBindings.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000C270 File Offset: 0x0000A470
		internal int IndexOfFirstInvalidBinding()
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.regularBindings[i].IsValid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		public void RemoveBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			if (binding.BoundTo != this)
			{
				throw new InControlException("Cannot remove a binding source not bound to this action.");
			}
			binding.BoundTo = null;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public void RemoveBindingAt(int index)
		{
			if (index < 0 || index >= this.regularBindings.Count)
			{
				throw new InControlException("Index is out of range for bindings on this action.");
			}
			this.regularBindings[index].BoundTo = null;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000C31C File Offset: 0x0000A51C
		public void ClearBindings()
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				this.regularBindings[i].BoundTo = null;
			}
			this.regularBindings.Clear();
			this.visibleBindings.Clear();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000C370 File Offset: 0x0000A570
		public void ResetBindings()
		{
			this.ClearBindings();
			this.regularBindings.AddRange(this.defaultBindings);
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				bindingSource.BoundTo = this;
				if (bindingSource.IsValid)
				{
					this.visibleBindings.Add(bindingSource);
				}
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		public void ListenForBinding()
		{
			this.ListenForBindingReplacing(null);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public void ListenForBindingReplacing(BindingSource binding)
		{
			BindingListenOptions bindingListenOptions = this.ListenOptions ?? this.Owner.ListenOptions;
			bindingListenOptions.ReplaceBinding = binding;
			this.Owner.listenWithAction = this;
			int num = PlayerAction.bindingSourceListeners.Length;
			for (int i = 0; i < num; i++)
			{
				PlayerAction.bindingSourceListeners[i].Reset();
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C44C File Offset: 0x0000A64C
		public void StopListeningForBinding()
		{
			if (this.IsListeningForBinding)
			{
				this.Owner.listenWithAction = null;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000C468 File Offset: 0x0000A668
		public bool IsListeningForBinding
		{
			get
			{
				return this.Owner.listenWithAction == this;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000C478 File Offset: 0x0000A678
		public ReadOnlyCollection<BindingSource> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C480 File Offset: 0x0000A680
		private void RemoveOrphanedBindings()
		{
			int count = this.regularBindings.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (this.regularBindings[i].BoundTo != this)
				{
					this.regularBindings.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		internal void Update(ulong updateTick, float deltaTime, InputDevice device)
		{
			this.Device = device;
			this.UpdateBindings(updateTick, deltaTime);
			this.DetectBindings();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		private void UpdateBindings(ulong updateTick, float deltaTime)
		{
			int count = this.regularBindings.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo != this)
				{
					this.regularBindings.RemoveAt(i);
					this.visibleBindings.Remove(bindingSource);
				}
				else
				{
					float value = bindingSource.GetValue(this.Device);
					if (base.UpdateWithValue(value, updateTick, deltaTime))
					{
						this.LastInputType = bindingSource.BindingSourceType;
					}
				}
			}
			base.Commit();
			this.Enabled = this.Owner.Enabled;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000C58C File Offset: 0x0000A78C
		private void DetectBindings()
		{
			if (this.IsListeningForBinding)
			{
				BindingSource bindingSource = null;
				BindingListenOptions bindingListenOptions = this.ListenOptions ?? this.Owner.ListenOptions;
				int num = PlayerAction.bindingSourceListeners.Length;
				for (int i = 0; i < num; i++)
				{
					bindingSource = PlayerAction.bindingSourceListeners[i].Listen(bindingListenOptions, this.device);
					if (bindingSource != null)
					{
						break;
					}
				}
				if (bindingSource == null)
				{
					return;
				}
				Func<PlayerAction, BindingSource, bool> onBindingFound = bindingListenOptions.OnBindingFound;
				if (onBindingFound != null && !onBindingFound(this, bindingSource))
				{
					return;
				}
				if (this.HasBinding(bindingSource))
				{
					Action<PlayerAction, BindingSource, BindingSourceRejectionType> onBindingRejected = bindingListenOptions.OnBindingRejected;
					if (onBindingRejected != null)
					{
						onBindingRejected(this, bindingSource, BindingSourceRejectionType.DuplicateBindingOnAction);
					}
					return;
				}
				if (bindingListenOptions.UnsetDuplicateBindingsOnSet)
				{
					this.Owner.RemoveBinding(bindingSource);
				}
				if (!bindingListenOptions.AllowDuplicateBindingsPerSet && this.Owner.HasBinding(bindingSource))
				{
					Action<PlayerAction, BindingSource, BindingSourceRejectionType> onBindingRejected2 = bindingListenOptions.OnBindingRejected;
					if (onBindingRejected2 != null)
					{
						onBindingRejected2(this, bindingSource, BindingSourceRejectionType.DuplicateBindingOnActionSet);
					}
					return;
				}
				this.StopListeningForBinding();
				if (bindingListenOptions.ReplaceBinding == null)
				{
					if (bindingListenOptions.MaxAllowedBindingsPerType > 0U)
					{
						while ((long)this.CountBindingsOfType(bindingSource.BindingSourceType) >= (long)((ulong)bindingListenOptions.MaxAllowedBindingsPerType))
						{
							this.RemoveFirstBindingOfType(bindingSource.BindingSourceType);
						}
					}
					else if (bindingListenOptions.MaxAllowedBindings > 0U)
					{
						while ((long)this.regularBindings.Count >= (long)((ulong)bindingListenOptions.MaxAllowedBindings))
						{
							int index = Mathf.Max(0, this.IndexOfFirstInvalidBinding());
							this.regularBindings.RemoveAt(index);
						}
					}
					this.AddBinding(bindingSource);
				}
				else
				{
					this.ReplaceBinding(bindingListenOptions.ReplaceBinding, bindingSource);
				}
				this.UpdateVisibleBindings();
				Action<PlayerAction, BindingSource> onBindingAdded = bindingListenOptions.OnBindingAdded;
				if (onBindingAdded != null)
				{
					onBindingAdded(this, bindingSource);
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C76C File Offset: 0x0000A96C
		private void UpdateVisibleBindings()
		{
			this.visibleBindings.Clear();
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.IsValid)
				{
					this.visibleBindings.Add(bindingSource);
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000C800 File Offset: 0x0000AA00
		internal InputDevice Device
		{
			get
			{
				if (this.device == null)
				{
					this.device = this.Owner.Device;
					this.UpdateVisibleBindings();
				}
				return this.device;
			}
			set
			{
				if (this.device != value)
				{
					this.device = value;
					this.UpdateVisibleBindings();
				}
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C81C File Offset: 0x0000AA1C
		internal void Load(BinaryReader reader)
		{
			this.ClearBindings();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				BindingSourceType bindingSourceType = (BindingSourceType)reader.ReadInt32();
				BindingSource bindingSource;
				switch (bindingSourceType)
				{
				case BindingSourceType.DeviceBindingSource:
					bindingSource = new DeviceBindingSource();
					break;
				case BindingSourceType.KeyBindingSource:
					bindingSource = new KeyBindingSource();
					break;
				case BindingSourceType.MouseBindingSource:
					bindingSource = new MouseBindingSource();
					break;
				case BindingSourceType.UnknownDeviceBindingSource:
					bindingSource = new UnknownDeviceBindingSource();
					break;
				default:
					throw new InControlException("Don't know how to load BindingSourceType: " + bindingSourceType);
				}
				bindingSource.Load(reader);
				this.AddBinding(bindingSource);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		internal void Save(BinaryWriter writer)
		{
			this.RemoveOrphanedBindings();
			writer.Write(this.Name);
			int count = this.regularBindings.Count;
			writer.Write(count);
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				writer.Write((int)bindingSource.BindingSourceType);
				bindingSource.Save(writer);
			}
		}

		// Token: 0x040001BB RID: 443
		public BindingListenOptions ListenOptions;

		// Token: 0x040001BC RID: 444
		public BindingSourceType LastInputType;

		// Token: 0x040001BD RID: 445
		private List<BindingSource> defaultBindings = new List<BindingSource>();

		// Token: 0x040001BE RID: 446
		private List<BindingSource> regularBindings = new List<BindingSource>();

		// Token: 0x040001BF RID: 447
		private List<BindingSource> visibleBindings = new List<BindingSource>();

		// Token: 0x040001C0 RID: 448
		private readonly ReadOnlyCollection<BindingSource> bindings;

		// Token: 0x040001C1 RID: 449
		private static readonly BindingSourceListener[] bindingSourceListeners = new BindingSourceListener[]
		{
			new DeviceBindingSourceListener(),
			new UnknownDeviceBindingSourceListener(),
			new KeyBindingSourceListener(),
			new MouseBindingSourceListener()
		};

		// Token: 0x040001C2 RID: 450
		private InputDevice device;
	}
}
