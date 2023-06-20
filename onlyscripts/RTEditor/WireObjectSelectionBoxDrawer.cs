using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x0200018B RID: 395
	public class WireObjectSelectionBoxDrawer : ObjectSelectionBoxDrawer
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x00039580 File Offset: 0x00037780
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
			GLPrimitives.DrawWireBoxes(list, list2, objectSelectionBoxDrawSettings.SelectionBoxScaleFactor, MonoSingletonBase<EditorCamera>.Instance.Camera, objectSelectionBoxDrawSettings.SelectionBoxLineColor, objectSelectionBoxLineMaterial);
		}
	}
}
