using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000186 RID: 390
	public class CornerLinesObjectSelectionBoxDrawer : ObjectSelectionBoxDrawer
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x000393C8 File Offset: 0x000375C8
		public override void DrawObjectSelectionBoxes(HashSet<GameObject> selectedObjects, ObjectSelectionBoxDrawSettings objectSelectionBoxDrawSettings, Material objectSelectionBoxLineMaterial)
		{
			List<Bounds> list = new List<Bounds>(selectedObjects.Count);
			List<Matrix4x4> list2 = new List<Matrix4x4>(selectedObjects.Count);
			foreach (GameObject gameObject in selectedObjects)
			{
				Bounds modelSpaceAABB = gameObject.GetModelSpaceAABB();
				if (modelSpaceAABB.IsValid())
				{
					list.Add(gameObject.GetModelSpaceAABB());
					list2.Add(gameObject.transform.localToWorldMatrix);
				}
			}
			GLPrimitives.DrawCornerLinesForBoxes(list, list2, objectSelectionBoxDrawSettings.SelectionBoxScaleFactor, objectSelectionBoxDrawSettings.SelectionBoxCornerLineLength, MonoSingletonBase<EditorCamera>.Instance.Camera, objectSelectionBoxDrawSettings.SelectionBoxLineColor, objectSelectionBoxLineMaterial);
		}
	}
}
