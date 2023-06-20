using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000188 RID: 392
	public abstract class ObjectSelectionBoxDrawer
	{
		// Token: 0x060008F3 RID: 2291
		public abstract void DrawObjectSelectionBoxes(HashSet<GameObject> selectedObjects, ObjectSelectionBoxDrawSettings objectSelectionBoxDrawSettings, Material objectSelectionBoxLineMaterial);
	}
}
