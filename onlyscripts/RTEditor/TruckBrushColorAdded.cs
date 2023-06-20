using System;

namespace RTEditor
{
	// Token: 0x02000199 RID: 409
	public class TruckBrushColorAdded : IAction, IDestructableAction, IRedoableAction, IUndoableAction, IUndoableAndRedoableAction
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x0003A3B0 File Offset: 0x000385B0
		public TruckBrushColorAdded(levelEditorManager.Block block, int preColor, int postColor)
		{
			if (block.type != levelEditorManager.blockType.btruckblock)
			{
				throw new Exception("Invalid Block, Has to be a TRUCK for the truckbrush message!");
			}
			this._block = block;
			this._preColor = preColor;
			this._postColor = postColor;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0003A3E4 File Offset: 0x000385E4
		public levelEditorManager.Block Block
		{
			get
			{
				return this._block;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0003A3EC File Offset: 0x000385EC
		public int PreColor
		{
			get
			{
				return this._preColor;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0003A3F4 File Offset: 0x000385F4
		public int PostColor
		{
			get
			{
				return this._postColor;
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0003A3FC File Offset: 0x000385FC
		public void Execute()
		{
			TruckBrushColorAddedMessage.SendToInterestedListeners(this._block);
			MonoSingletonBase<EditorUndoRedoSystem>.Instance.RegisterAction(this);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0003A414 File Offset: 0x00038614
		public void Redo()
		{
			ObjectParameterContainer[] objsParams = this.Block.objsParams;
			Array.Find<ObjectParameterContainer>(objsParams, (ObjectParameterContainer index) => index.getName() == "WaypointType").setValue(this._postColor.ToString());
			this.Block.setObjParams(objsParams, false, true);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0003A470 File Offset: 0x00038670
		public void Undo()
		{
			ObjectParameterContainer[] objsParams = this.Block.objsParams;
			Array.Find<ObjectParameterContainer>(objsParams, (ObjectParameterContainer index) => index.getName() == "WaypointType").setValue(this._preColor.ToString());
			this.Block.setObjParams(objsParams, false, true);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0003A4CC File Offset: 0x000386CC
		public void Destruct()
		{
		}

		// Token: 0x040006F8 RID: 1784
		private levelEditorManager.Block _block;

		// Token: 0x040006F9 RID: 1785
		private int _preColor;

		// Token: 0x040006FA RID: 1786
		private int _postColor;
	}
}
