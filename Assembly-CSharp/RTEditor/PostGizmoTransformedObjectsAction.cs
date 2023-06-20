using System;
using System.Collections.Generic;

namespace RTEditor
{
	// Token: 0x02000196 RID: 406
	public class PostGizmoTransformedObjectsAction : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00039F3C File Offset: 0x0003813C
		public PostGizmoTransformedObjectsAction(List<ObjectTransformSnapshot> preTransformObjectSnapshots, List<ObjectTransformSnapshot> postTransformObjectSnapshot, Gizmo gizmoWhichTransformedObjects)
		{
			this._preTransformObjectSnapshots = new List<ObjectTransformSnapshot>(preTransformObjectSnapshots);
			this._postTransformObjectSnapshot = new List<ObjectTransformSnapshot>(postTransformObjectSnapshot);
			this._gizmoWhichTransformedObjects = gizmoWhichTransformedObjects;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00039F64 File Offset: 0x00038164
		public void Execute()
		{
			GizmoTransformedObjectsMessage.SendToInterestedListeners(this._gizmoWhichTransformedObjects);
			MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00039F7C File Offset: 0x0003817C
		public void Undo()
		{
			foreach (ObjectTransformSnapshot objectTransformSnapshot in this._preTransformObjectSnapshots)
			{
				objectTransformSnapshot.ApplySnapshot();
			}
			GizmoTransformOperationWasUndoneMessage.SendToInterestedListeners(this._gizmoWhichTransformedObjects);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00039FEC File Offset: 0x000381EC
		public void Redo()
		{
			foreach (ObjectTransformSnapshot objectTransformSnapshot in this._postTransformObjectSnapshot)
			{
				objectTransformSnapshot.ApplySnapshot();
			}
			GizmoTransformOperationWasRedoneMessage.SendToInterestedListeners(this._gizmoWhichTransformedObjects);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0003A05C File Offset: 0x0003825C
		public void Destruct()
		{
		}

		// Token: 0x040006F3 RID: 1779
		private List<ObjectTransformSnapshot> _preTransformObjectSnapshots;

		// Token: 0x040006F4 RID: 1780
		private List<ObjectTransformSnapshot> _postTransformObjectSnapshot;

		// Token: 0x040006F5 RID: 1781
		private Gizmo _gizmoWhichTransformedObjects;
	}
}
