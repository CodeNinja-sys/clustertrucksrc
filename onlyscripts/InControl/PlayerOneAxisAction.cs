using System;

namespace InControl
{
	// Token: 0x02000058 RID: 88
	public class PlayerOneAxisAction : OneAxisInputControl
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000CF4C File Offset: 0x0000B14C
		internal PlayerOneAxisAction(PlayerAction negativeAction, PlayerAction positiveAction)
		{
			this.negativeAction = negativeAction;
			this.positiveAction = positiveAction;
			this.Raw = true;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000CF6C File Offset: 0x0000B16C
		internal void Update(ulong updateTick, float deltaTime)
		{
			float value = Utility.ValueFromSides(this.negativeAction, this.positiveAction);
			base.CommitWithValue(value, updateTick, deltaTime);
		}

		// Token: 0x040001D0 RID: 464
		private PlayerAction negativeAction;

		// Token: 0x040001D1 RID: 465
		private PlayerAction positiveAction;
	}
}
