using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x0200019F RID: 415
	public class EditorUndoRedoSystem : MonoSingletonBase<EditorUndoRedoSystem>
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0003A528 File Offset: 0x00038728
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0003A530 File Offset: 0x00038730
		public int ActionLimit
		{
			get
			{
				return this._actionLimit;
			}
			set
			{
				this.ChangeActionLimit(value);
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0003A53C File Offset: 0x0003873C
		public void ClearActions()
		{
			foreach (IUndoableAndRedoableAction undoableAndRedoableAction in this._actionStack)
			{
				undoableAndRedoableAction.Destruct();
			}
			this._actionStack.Clear();
			this._actionStackPointer = -1;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0003A5B4 File Offset: 0x000387B4
		public void RegisterAction(IUndoableAndRedoableAction action)
		{
			if (this._actionStackPointer < 0)
			{
				this._actionStack.Add(action);
				this._actionStackPointer = 0;
			}
			else
			{
				if (this._actionStackPointer < this._actionStack.Count - 1)
				{
					int num = this._actionStackPointer + 1;
					int num2 = this._actionStack.Count - num;
					for (int i = num; i < num2; i++)
					{
						this._actionStack[i].Destruct();
					}
					this._actionStack.RemoveRange(num, num2);
				}
				this._actionStack.Add(action);
				if (this._actionStack.Count > this._actionLimit)
				{
					this._actionStack.RemoveAt(0);
				}
				this._actionStackPointer = this._actionStack.Count - 1;
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0003A688 File Offset: 0x00038888
		public void Undo()
		{
			if (this._actionStack.Count == 0 || this._actionStackPointer < 0)
			{
				return;
			}
			IUndoableAndRedoableAction undoableAndRedoableAction = this._actionStack[this._actionStackPointer];
			undoableAndRedoableAction.Undo();
			this._actionStackPointer--;
			levelEditorManager.Instance().updateMapForGizmos();
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0003A6E4 File Offset: 0x000388E4
		public void Redo()
		{
			if (this._actionStack.Count == 0 || this._actionStackPointer == this._actionStack.Count - 1)
			{
				return;
			}
			this._actionStackPointer++;
			IUndoableAndRedoableAction undoableAndRedoableAction = this._actionStack[this._actionStackPointer];
			undoableAndRedoableAction.Redo();
			levelEditorManager.Instance().updateMapForGizmos();
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0003A74C File Offset: 0x0003894C
		private void ChangeActionLimit(int newActionLimit)
		{
			int actionLimit = this._actionLimit;
			this._actionLimit = Mathf.Max(1, newActionLimit);
			if (Application.isPlaying && this._actionLimit < actionLimit && this._actionStackPointer >= this._actionLimit)
			{
				for (int i = 0; i < actionLimit - this._actionLimit; i++)
				{
					this._actionStack[i].Destruct();
				}
				this._actionStack.RemoveRange(0, actionLimit - this._actionLimit);
				this._actionStackPointer = this._actionStack.Count - 1;
			}
		}

		// Token: 0x040006FD RID: 1789
		[SerializeField]
		private int _actionLimit = 50;

		// Token: 0x040006FE RID: 1790
		private List<IUndoableAndRedoableAction> _actionStack = new List<IUndoableAndRedoableAction>();

		// Token: 0x040006FF RID: 1791
		private int _actionStackPointer = -1;
	}
}
