using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000197 RID: 407
	public class ObjectDeletedAction : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x0003A060 File Offset: 0x00038260
		public ObjectDeletedAction(levelEditorManager.Block[] blocks)
		{
			this._blocks = new levelEditorManager.Block[blocks.Length];
			for (int i = 0; i < blocks.Length; i++)
			{
				Debug.Log("Recieved Deleted Block: " + blocks[i].id + " Action!");
				this._blocks[i] = new levelEditorManager.Block(blocks[i]);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0003A0C4 File Offset: 0x000382C4
		public levelEditorManager.Block[] Blocks
		{
			get
			{
				return this._blocks;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0003A0CC File Offset: 0x000382CC
		public void Execute()
		{
			ObjectDeletedMessage.SendToInterestedListeners(this._blocks);
			MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0003A0E4 File Offset: 0x000382E4
		public void Undo()
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

		// Token: 0x06000935 RID: 2357 RVA: 0x0003A158 File Offset: 0x00038358
		public void Redo()
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

		// Token: 0x06000936 RID: 2358 RVA: 0x0003A1DC File Offset: 0x000383DC
		public void Destruct()
		{
			foreach (levelEditorManager.Block block in this._blocks)
			{
				if (block.go != null)
				{
					Debug.Log("Destruct: " + block.id, block.go);
					UnityEngine.Object.Destroy(block.go);
				}
			}
		}

		// Token: 0x040006F6 RID: 1782
		private levelEditorManager.Block[] _blocks;
	}
}
