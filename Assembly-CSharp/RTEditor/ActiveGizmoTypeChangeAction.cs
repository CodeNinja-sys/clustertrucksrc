using System;

namespace RTEditor
{
	// Token: 0x02000194 RID: 404
	public class ActiveGizmoTypeChangeAction : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x00039E10 File Offset: 0x00038010
		public ActiveGizmoTypeChangeAction(GizmoType oldActiveGizmoType, GizmoType newActiveGizmoType)
		{
			this._oldActiveGizmoType = oldActiveGizmoType;
			this._newActiveGizmoType = newActiveGizmoType;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00039E28 File Offset: 0x00038028
		public void Execute()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			if (this._oldActiveGizmoType != this._newActiveGizmoType || instance.AreGizmosTurnedOff)
			{
				instance.ActiveGizmoType = this._newActiveGizmoType;
				ActiveGizmoTypeChangedMessage.SendToInterestedListeners(this._oldActiveGizmoType, this._newActiveGizmoType);
				MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00039E80 File Offset: 0x00038080
		public void Undo()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			instance.ActiveGizmoType = this._oldActiveGizmoType;
			ActiveGizmoTypeChangedMessage.SendToInterestedListeners(this._newActiveGizmoType, this._oldActiveGizmoType);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00039EB0 File Offset: 0x000380B0
		public void Redo()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			instance.ActiveGizmoType = this._newActiveGizmoType;
			ActiveGizmoTypeChangedMessage.SendToInterestedListeners(this._oldActiveGizmoType, this._newActiveGizmoType);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00039EE0 File Offset: 0x000380E0
		public void Destruct()
		{
		}

		// Token: 0x040006EF RID: 1775
		private GizmoType _oldActiveGizmoType;

		// Token: 0x040006F0 RID: 1776
		private GizmoType _newActiveGizmoType;
	}
}
