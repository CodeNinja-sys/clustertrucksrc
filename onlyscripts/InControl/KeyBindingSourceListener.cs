using System;

namespace InControl
{
	// Token: 0x02000050 RID: 80
	public class KeyBindingSourceListener : BindingSourceListener
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000A480 File Offset: 0x00008680
		public void Reset()
		{
			this.detectFound.Clear();
			this.detectPhase = 0;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000A494 File Offset: 0x00008694
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeKeys)
			{
				return null;
			}
			if (this.detectFound.Count > 0 && !this.detectFound.IsPressed && this.detectPhase == 2)
			{
				KeyBindingSource result = new KeyBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			KeyCombo keyCombo = KeyCombo.Detect(listenOptions.IncludeModifiersAsFirstClassKeys);
			if (keyCombo.Count > 0)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = keyCombo;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x04000199 RID: 409
		private KeyCombo detectFound;

		// Token: 0x0400019A RID: 410
		private int detectPhase;
	}
}
