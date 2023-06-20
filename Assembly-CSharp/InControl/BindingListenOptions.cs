using System;

namespace InControl
{
	// Token: 0x02000047 RID: 71
	public class BindingListenOptions
	{
		// Token: 0x0400010E RID: 270
		public bool IncludeControllers = true;

		// Token: 0x0400010F RID: 271
		public bool IncludeUnknownControllers;

		// Token: 0x04000110 RID: 272
		public bool IncludeNonStandardControls = true;

		// Token: 0x04000111 RID: 273
		public bool IncludeMouseButtons;

		// Token: 0x04000112 RID: 274
		public bool IncludeKeys = true;

		// Token: 0x04000113 RID: 275
		public bool IncludeModifiersAsFirstClassKeys;

		// Token: 0x04000114 RID: 276
		public uint MaxAllowedBindings;

		// Token: 0x04000115 RID: 277
		public uint MaxAllowedBindingsPerType;

		// Token: 0x04000116 RID: 278
		public bool AllowDuplicateBindingsPerSet;

		// Token: 0x04000117 RID: 279
		public bool UnsetDuplicateBindingsOnSet;

		// Token: 0x04000118 RID: 280
		public BindingSource ReplaceBinding;

		// Token: 0x04000119 RID: 281
		public Func<PlayerAction, BindingSource, bool> OnBindingFound;

		// Token: 0x0400011A RID: 282
		public Action<PlayerAction, BindingSource> OnBindingAdded;

		// Token: 0x0400011B RID: 283
		public Action<PlayerAction, BindingSource, BindingSourceRejectionType> OnBindingRejected;
	}
}
