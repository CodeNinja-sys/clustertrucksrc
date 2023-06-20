using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x0200018C RID: 396
	[Serializable]
	public class ObjectSelectionRectangle : ObjectSelectionShape
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00039658 File Offset: 0x00037858
		public ObjectSelectionRectangleDrawSettings DrawSettings
		{
			get
			{
				return this._drawSettings;
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00039660 File Offset: 0x00037860
		public override List<GameObject> GetIntersectingGameObjects(List<GameObject> gameObjects, Camera camera)
		{
			if (!base.IsEnclosingRectangleBigEnoughForSelection())
			{
				return new List<GameObject>();
			}
			List<GameObject> list = new List<GameObject>();
			foreach (GameObject gameObject in gameObjects)
			{
				if (this._enclosingRectangle.Overlaps(gameObject.GetScreenRectangle(camera), true))
				{
					list.Add(gameObject);
				}
			}
			return list;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000396F4 File Offset: 0x000378F4
		public override void Draw()
		{
			if (this._isVisible)
			{
				GLPrimitives.Draw2DFilledRectangle(this._enclosingRectangle, this._drawSettings.FillColor, this._fillMaterial);
				GLPrimitives.Draw2DRectangleBorderLines(this._enclosingRectangle, this._drawSettings.BorderLineColor, this._borderLineMaterial);
			}
		}

		// Token: 0x040006E1 RID: 1761
		[SerializeField]
		private ObjectSelectionRectangleDrawSettings _drawSettings = new ObjectSelectionRectangleDrawSettings();
	}
}
