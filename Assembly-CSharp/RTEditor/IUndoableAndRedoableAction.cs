using System;

namespace RTEditor
{
	// Token: 0x0200019E RID: 414
	public interface IUndoableAndRedoableAction : IDestructableAction, IRedoableAction, IUndoableAction
	{
	}
}
