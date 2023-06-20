using System;

namespace InControl
{
	// Token: 0x02000055 RID: 85
	public class MouseBindingSourceListener : BindingSourceListener
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000BC5C File Offset: 0x00009E5C
		public void Reset()
		{
			this.detectFound = Mouse.None;
			this.detectPhase = 0;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeMouseButtons)
			{
				return null;
			}
			if (this.detectFound != Mouse.None && !MouseBindingSource.ButtonIsPressed(this.detectFound) && this.detectPhase == 2)
			{
				MouseBindingSource result = new MouseBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			Mouse mouse = this.ListenForControl();
			if (mouse != Mouse.None)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = mouse;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000BD00 File Offset: 0x00009F00
		private Mouse ListenForControl()
		{
			for (Mouse mouse = Mouse.None; mouse <= Mouse.Button9; mouse++)
			{
				if (MouseBindingSource.ButtonIsPressed(mouse))
				{
					return mouse;
				}
			}
			return Mouse.None;
		}

		// Token: 0x040001B9 RID: 441
		private Mouse detectFound;

		// Token: 0x040001BA RID: 442
		private int detectPhase;
	}
}
