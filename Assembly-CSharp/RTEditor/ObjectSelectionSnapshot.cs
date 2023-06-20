using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x0200018F RID: 399
	public class ObjectSelectionSnapshot
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00039894 File Offset: 0x00037A94
		public HashSet<GameObject> SelectedGameObjects
		{
			get
			{
				return this._selectedGameObjects;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0003989C File Offset: 0x00037A9C
		public GameObject LastSelectedGameObject
		{
			get
			{
				return this._lastSelectedGameObject;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000398A4 File Offset: 0x00037AA4
		public void TakeSnapshot()
		{
			EditorObjectSelection instance = MonoSingletonBase<EditorObjectSelection>.Instance;
			this._selectedGameObjects = new HashSet<GameObject>(instance.SelectedGameObjects);
			this._lastSelectedGameObject = instance.LastSelectedGameObject;
		}

		// Token: 0x040006E8 RID: 1768
		private HashSet<GameObject> _selectedGameObjects;

		// Token: 0x040006E9 RID: 1769
		private GameObject _lastSelectedGameObject;
	}
}
