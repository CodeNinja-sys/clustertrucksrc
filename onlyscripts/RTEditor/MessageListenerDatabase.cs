using System;
using System.Collections.Generic;

namespace RTEditor
{
	// Token: 0x020001D2 RID: 466
	public class MessageListenerDatabase : SingletonBase<MessageListenerDatabase>
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x000452E0 File Offset: 0x000434E0
		public void SendMessageToInterestedListeners(Message message)
		{
			HashSet<IMessageListener> listeners = null;
			if (this.TryGetListenersForMessage(message, out listeners))
			{
				this.SendMessageToListeners(message, listeners);
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00045308 File Offset: 0x00043508
		public void RegisterListenerForMessage(MessageType messageType, IMessageListener messageListener)
		{
			if (this.DoesListenerListenToMessage(messageType, messageListener))
			{
				return;
			}
			this.RegisterNewMessageTypeIfNecessary(messageType);
			this._messageTypeToMessageListeners[messageType].Add(messageListener);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00045340 File Offset: 0x00043540
		private bool DoesListenerListenToMessage(MessageType messageType, IMessageListener messageListener)
		{
			HashSet<IMessageListener> hashSet = null;
			return this._messageTypeToMessageListeners.TryGetValue(messageType, out hashSet) && hashSet.Contains(messageListener);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0004536C File Offset: 0x0004356C
		private void RegisterNewMessageTypeIfNecessary(MessageType messageType)
		{
			if (!this._messageTypeToMessageListeners.ContainsKey(messageType))
			{
				this._messageTypeToMessageListeners.Add(messageType, new HashSet<IMessageListener>());
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0004539C File Offset: 0x0004359C
		private bool TryGetListenersForMessage(Message message, out HashSet<IMessageListener> listeners)
		{
			listeners = null;
			if (this._messageTypeToMessageListeners.ContainsKey(message.Type))
			{
				listeners = this._messageTypeToMessageListeners[message.Type];
				return true;
			}
			return false;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000453D8 File Offset: 0x000435D8
		private void SendMessageToListeners(Message message, HashSet<IMessageListener> listeners)
		{
			foreach (IMessageListener messageListener in listeners)
			{
				messageListener.RespondToMessage(message);
			}
		}

		// Token: 0x040007C4 RID: 1988
		private Dictionary<MessageType, HashSet<IMessageListener>> _messageTypeToMessageListeners = new Dictionary<MessageType, HashSet<IMessageListener>>();
	}
}
