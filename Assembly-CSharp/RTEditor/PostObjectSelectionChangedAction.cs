using System;

namespace RTEditor
{
	// Token: 0x02000195 RID: 405
	public class PostObjectSelectionChangedAction : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00039EE4 File Offset: 0x000380E4
		public PostObjectSelectionChangedAction(ObjectSelectionSnapshot preChangeSelectionSnapshot, ObjectSelectionSnapshot postChangeSelectionSnapshot)
		{
			this._preChangeSelectionSnapshot = preChangeSelectionSnapshot;
			this._postChangeSelectionSnapshot = postChangeSelectionSnapshot;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00039EFC File Offset: 0x000380FC
		public void Execute()
		{
			ObjectSelectionChangedMessage.SendToInterestedListeners();
			MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00039F10 File Offset: 0x00038110
		public void Undo()
		{
			MonoSingletonBase<EditorObjectSelection>.Instance.ApplySnapshot(this._preChangeSelectionSnapshot);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00039F24 File Offset: 0x00038124
		public void Redo()
		{
			MonoSingletonBase<EditorObjectSelection>.Instance.ApplySnapshot(this._postChangeSelectionSnapshot);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00039F38 File Offset: 0x00038138
		public void Destruct()
		{
		}

		// Token: 0x040006F1 RID: 1777
		private ObjectSelectionSnapshot _preChangeSelectionSnapshot;

		// Token: 0x040006F2 RID: 1778
		private ObjectSelectionSnapshot _postChangeSelectionSnapshot;
	}
}
