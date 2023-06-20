using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000198 RID: 408
	public class ObjectAddedAction : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x0003A240 File Offset: 0x00038440
		public ObjectAddedAction(levelEditorManager.Block[] blocks)
		{
			this._blocks = blocks;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0003A250 File Offset: 0x00038450
		public levelEditorManager.Block[] Blocks
		{
			get
			{
				return this._blocks;
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0003A258 File Offset: 0x00038458
		public void Execute()
		{
			ObjectAddedMessage.SendToInterestedListeners(this._blocks);
			MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0003A270 File Offset: 0x00038470
		public void Redo()
		{
			foreach (levelEditorManager.Block block in this._blocks)
			{
				if (block.go != null)
				{
					Debug.Log("ADDING: " + block.id + " AGAIN!");
					block.go.SetActive(true);
				}
				levelEditorManager.Instance().getCurrentMap.setTile(block);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0003A2E4 File Offset: 0x000384E4
		public void Undo()
		{
			foreach (levelEditorManager.Block block in this._blocks)
			{
				if (block.go != null)
				{
					Debug.Log("DELETING: " + block.id + " AGAIN!");
					block.go.transform.SetParent(null);
					block.go.SetActive(false);
				}
				levelEditorManager.Instance().getCurrentMap.removeTile(block);
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0003A368 File Offset: 0x00038568
		public void Destruct()
		{
			foreach (levelEditorManager.Block block in this._blocks)
			{
				if (block.go != null)
				{
					UnityEngine.Object.Destroy(block.go);
				}
			}
		}

		// Token: 0x040006F7 RID: 1783
		private levelEditorManager.Block[] _blocks;
	}
}
