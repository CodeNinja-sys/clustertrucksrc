using System;

namespace RTEditor
{
	// Token: 0x02000191 RID: 401
	public class TransformSpaceChangeAction : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x00039BE4 File Offset: 0x00037DE4
		public TransformSpaceChangeAction(TransformSpace oldTransformSpace, TransformSpace newTransformSpace)
		{
			this._oldTransformSpace = oldTransformSpace;
			this._newTransformSpace = newTransformSpace;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00039BFC File Offset: 0x00037DFC
		public void Execute()
		{
			if (this._oldTransformSpace != this._newTransformSpace)
			{
				MonoSingletonBase<EditorGizmoSystem>.Instance.TransformSpace = this._newTransformSpace;
				TransformSpaceChangedMessage.SendToInterestedListeners(this._oldTransformSpace, this._newTransformSpace);
				MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00039C3C File Offset: 0x00037E3C
		public void Undo()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			instance.TransformSpace = this._oldTransformSpace;
			TransformSpaceChangedMessage.SendToInterestedListeners(this._newTransformSpace, this._oldTransformSpace);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00039C6C File Offset: 0x00037E6C
		public void Redo()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			instance.TransformSpace = this._newTransformSpace;
			TransformSpaceChangedMessage.SendToInterestedListeners(this._oldTransformSpace, this._newTransformSpace);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00039C9C File Offset: 0x00037E9C
		public void Destruct()
		{
		}

		// Token: 0x040006EA RID: 1770
		private TransformSpace _oldTransformSpace;

		// Token: 0x040006EB RID: 1771
		private TransformSpace _newTransformSpace;
	}
}
