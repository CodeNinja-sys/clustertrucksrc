using System;

namespace RTEditor
{
	// Token: 0x02000193 RID: 403
	public class TransformPivotPointChangeAction : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x00039D48 File Offset: 0x00037F48
		public TransformPivotPointChangeAction(TransformPivotPoint oldPivotPoint, TransformPivotPoint newPivotPoint)
		{
			this._oldPivotPoint = oldPivotPoint;
			this._newPivotPoint = newPivotPoint;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00039D60 File Offset: 0x00037F60
		public void Execute()
		{
			if (this._oldPivotPoint != this._newPivotPoint)
			{
				EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
				instance.TransformPivotPoint = this._newPivotPoint;
				TransformPivotPointChangedMessage.SendToInterestedListeners(this._oldPivotPoint, this._newPivotPoint);
				MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00039DAC File Offset: 0x00037FAC
		public void Undo()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			instance.TransformPivotPoint = this._oldPivotPoint;
			TransformPivotPointChangedMessage.SendToInterestedListeners(this._newPivotPoint, this._oldPivotPoint);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00039DDC File Offset: 0x00037FDC
		public void Redo()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			instance.TransformPivotPoint = this._newPivotPoint;
			TransformPivotPointChangedMessage.SendToInterestedListeners(this._oldPivotPoint, this._newPivotPoint);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00039E0C File Offset: 0x0003800C
		public void Destruct()
		{
		}

		// Token: 0x040006ED RID: 1773
		private TransformPivotPoint _oldPivotPoint;

		// Token: 0x040006EE RID: 1774
		private TransformPivotPoint _newPivotPoint;
	}
}
