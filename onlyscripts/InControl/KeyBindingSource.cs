using System;
using System.IO;

namespace InControl
{
	// Token: 0x0200004F RID: 79
	public class KeyBindingSource : BindingSource
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x0000A2F8 File Offset: 0x000084F8
		internal KeyBindingSource()
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A300 File Offset: 0x00008500
		public KeyBindingSource(KeyCombo keyCombo)
		{
			this.Control = keyCombo;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A310 File Offset: 0x00008510
		public KeyBindingSource(params Key[] keys)
		{
			this.Control = new KeyCombo(keys);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000A324 File Offset: 0x00008524
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000A32C File Offset: 0x0000852C
		public KeyCombo Control { get; protected set; }

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A338 File Offset: 0x00008538
		public override float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A358 File Offset: 0x00008558
		public override bool GetState(InputDevice inputDevice)
		{
			return this.Control.IsPressed;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000A374 File Offset: 0x00008574
		public override string Name
		{
			get
			{
				return this.Control.ToString();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000A390 File Offset: 0x00008590
		public override string DeviceName
		{
			get
			{
				return "Keyboard";
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A398 File Offset: 0x00008598
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			KeyBindingSource keyBindingSource = other as KeyBindingSource;
			return keyBindingSource != null && this.Control == keyBindingSource.Control;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A3DC File Offset: 0x000085DC
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			KeyBindingSource keyBindingSource = other as KeyBindingSource;
			return keyBindingSource != null && this.Control == keyBindingSource.Control;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A418 File Offset: 0x00008618
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000A434 File Offset: 0x00008634
		internal override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.KeyBindingSource;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A438 File Offset: 0x00008638
		internal override void Load(BinaryReader reader)
		{
			KeyCombo control = default(KeyCombo);
			control.Load(reader);
			this.Control = control;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A45C File Offset: 0x0000865C
		internal override void Save(BinaryWriter writer)
		{
			this.Control.Save(writer);
		}
	}
}
