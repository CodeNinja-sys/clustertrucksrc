using System;

namespace RTEditor
{
	// Token: 0x02000192 RID: 402
	public class GizmosTurnOffAction : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x00039CA0 File Offset: 0x00037EA0
		public GizmosTurnOffAction(GizmoType activeGizmoType)
		{
			this._activeGizmoType = activeGizmoType;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00039CB0 File Offset: 0x00037EB0
		public void Execute()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			if (!instance.AreGizmosTurnedOff)
			{
				instance.TurnOffGizmos();
				GizmosTurnedOffMessage.SendToInterestedListeners(this._activeGizmoType);
				MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00039CEC File Offset: 0x00037EEC
		public void Undo()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			GizmoType activeGizmoType = instance.ActiveGizmoType;
			instance.ActiveGizmoType = this._activeGizmoType;
			ActiveGizmoTypeChangedMessage.SendToInterestedListeners(activeGizmoType, this._activeGizmoType);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00039D20 File Offset: 0x00037F20
		public void Redo()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			instance.TurnOffGizmos();
			GizmosTurnedOffMessage.SendToInterestedListeners(this._activeGizmoType);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00039D44 File Offset: 0x00037F44
		public void Destruct()
		{
		}

		// Token: 0x040006EC RID: 1772
		private GizmoType _activeGizmoType;
	}
}
