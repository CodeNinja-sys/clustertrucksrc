using System;
using System.IO;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000054 RID: 84
	public class MouseBindingSource : BindingSource
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000B998 File Offset: 0x00009B98
		internal MouseBindingSource()
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		public MouseBindingSource(Mouse mouseControl)
		{
			this.Control = mouseControl;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000B9F0 File Offset: 0x00009BF0
		public Mouse Control { get; protected set; }

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B9FC File Offset: 0x00009BFC
		internal static bool SafeGetMouseButton(int button)
		{
			try
			{
				return Input.GetMouseButton(button);
			}
			catch (ArgumentException)
			{
			}
			return false;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000BA40 File Offset: 0x00009C40
		internal static bool ButtonIsPressed(Mouse control)
		{
			int num = MouseBindingSource.buttonTable[(int)control];
			return num >= 0 && MouseBindingSource.SafeGetMouseButton(num);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000BA64 File Offset: 0x00009C64
		public override float GetValue(InputDevice inputDevice)
		{
			int num = MouseBindingSource.buttonTable[(int)this.Control];
			if (num >= 0)
			{
				return (!MouseBindingSource.SafeGetMouseButton(num)) ? 0f : 1f;
			}
			switch (this.Control)
			{
			case Mouse.NegativeX:
				return -Mathf.Min(Input.GetAxisRaw("mouse x") * MouseBindingSource.ScaleX, 0f);
			case Mouse.PositiveX:
				return Mathf.Max(0f, Input.GetAxisRaw("mouse x") * MouseBindingSource.ScaleX);
			case Mouse.NegativeY:
				return -Mathf.Min(Input.GetAxisRaw("mouse y") * MouseBindingSource.ScaleY, 0f);
			case Mouse.PositiveY:
				return Mathf.Max(0f, Input.GetAxisRaw("mouse y") * MouseBindingSource.ScaleY);
			case Mouse.PositiveScrollWheel:
				return Mathf.Max(0f, Input.GetAxisRaw("mouse z") * MouseBindingSource.ScaleZ);
			case Mouse.NegativeScrollWheel:
				return -Mathf.Min(Input.GetAxisRaw("mouse z") * MouseBindingSource.ScaleZ, 0f);
			default:
				return 0f;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000BB78 File Offset: 0x00009D78
		public override bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000BB88 File Offset: 0x00009D88
		public override string Name
		{
			get
			{
				return this.Control.ToString();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000BB9C File Offset: 0x00009D9C
		public override string DeviceName
		{
			get
			{
				return "Mouse";
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			MouseBindingSource mouseBindingSource = other as MouseBindingSource;
			return mouseBindingSource != null && this.Control == mouseBindingSource.Control;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			MouseBindingSource mouseBindingSource = other as MouseBindingSource;
			return mouseBindingSource != null && this.Control == mouseBindingSource.Control;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000BC1C File Offset: 0x00009E1C
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000BC30 File Offset: 0x00009E30
		internal override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.MouseBindingSource;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000BC34 File Offset: 0x00009E34
		internal override void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000BC44 File Offset: 0x00009E44
		internal override void Load(BinaryReader reader)
		{
			this.Control = (Mouse)reader.ReadInt32();
		}

		// Token: 0x040001B4 RID: 436
		public static float ScaleX = 0.2f;

		// Token: 0x040001B5 RID: 437
		public static float ScaleY = 0.2f;

		// Token: 0x040001B6 RID: 438
		public static float ScaleZ = 0.2f;

		// Token: 0x040001B7 RID: 439
		private static readonly int[] buttonTable = new int[]
		{
			-1,
			0,
			1,
			2,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			3,
			4,
			5,
			6,
			7,
			8
		};
	}
}
