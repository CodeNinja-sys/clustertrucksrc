using System;

namespace InControl
{
	// Token: 0x02000059 RID: 89
	public class PlayerTwoAxisAction : TwoAxisInputControl
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		internal PlayerTwoAxisAction(PlayerAction negativeXAction, PlayerAction positiveXAction, PlayerAction negativeYAction, PlayerAction positiveYAction)
		{
			this.negativeXAction = negativeXAction;
			this.positiveXAction = positiveXAction;
			this.negativeYAction = negativeYAction;
			this.positiveYAction = positiveYAction;
			this.InvertYAxis = false;
			this.Raw = true;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		public bool InvertYAxis { get; set; }

		// Token: 0x06000231 RID: 561 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		internal void Update(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.negativeXAction, this.positiveXAction, false);
			float y = Utility.ValueFromSides(this.negativeYAction, this.positiveYAction, InputManager.InvertYAxis || this.InvertYAxis);
			base.UpdateWithAxes(x, y, updateTick, deltaTime);
		}

		// Token: 0x040001D2 RID: 466
		private PlayerAction negativeXAction;

		// Token: 0x040001D3 RID: 467
		private PlayerAction positiveXAction;

		// Token: 0x040001D4 RID: 468
		private PlayerAction negativeYAction;

		// Token: 0x040001D5 RID: 469
		private PlayerAction positiveYAction;
	}
}
