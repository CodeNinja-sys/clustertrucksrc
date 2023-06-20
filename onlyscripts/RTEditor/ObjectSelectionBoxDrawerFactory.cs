using System;

namespace RTEditor
{
	// Token: 0x02000189 RID: 393
	public static class ObjectSelectionBoxDrawerFactory
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x00039548 File Offset: 0x00037748
		public static ObjectSelectionBoxDrawer CreateObjectSelectionBoxDrawer(ObjectSelectionBoxStyle objectSelectionBoxStyle)
		{
			if (objectSelectionBoxStyle == ObjectSelectionBoxStyle.CornerLines)
			{
				return new CornerLinesObjectSelectionBoxDrawer();
			}
			if (objectSelectionBoxStyle != ObjectSelectionBoxStyle.WireBox)
			{
				return null;
			}
			return new WireObjectSelectionBoxDrawer();
		}
	}
}
