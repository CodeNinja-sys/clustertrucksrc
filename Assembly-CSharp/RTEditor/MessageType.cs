using System;

namespace RTEditor
{
	// Token: 0x020001D3 RID: 467
	public enum MessageType
	{
		// Token: 0x040007C6 RID: 1990
		GizmoTransformedObjects,
		// Token: 0x040007C7 RID: 1991
		GizmoTransformOperationWasUndone,
		// Token: 0x040007C8 RID: 1992
		GizmoTransformOperationWasRedone,
		// Token: 0x040007C9 RID: 1993
		ObjectSelectionChanged,
		// Token: 0x040007CA RID: 1994
		TransformSpaceChanged,
		// Token: 0x040007CB RID: 1995
		GizmosTurnedOff,
		// Token: 0x040007CC RID: 1996
		TransformPivotPointChanged,
		// Token: 0x040007CD RID: 1997
		ActiveGizmoTypeChanged,
		// Token: 0x040007CE RID: 1998
		VertexSnappingEnabled,
		// Token: 0x040007CF RID: 1999
		VertexSnappingDisabled,
		// Token: 0x040007D0 RID: 2000
		ObjectDeleted,
		// Token: 0x040007D1 RID: 2001
		ObjectAdded,
		// Token: 0x040007D2 RID: 2002
		TruckBrushColorAdded
	}
}
