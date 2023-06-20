using System;
using InControl;
using UnityEngine;

namespace BindingsExample
{
	// Token: 0x02000036 RID: 54
	public class BindingsExample : MonoBehaviour
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000082D8 File Offset: 0x000064D8
		private void OnEnable()
		{
			this.playerActions = PlayerActions.CreateWithDefaultBindings();
			this.LoadBindings();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000082EC File Offset: 0x000064EC
		private void OnDisable()
		{
			this.playerActions.Destroy();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000082FC File Offset: 0x000064FC
		private void Start()
		{
			this.cachedRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000830C File Offset: 0x0000650C
		private void Update()
		{
			base.transform.Rotate(Vector3.down, 500f * Time.deltaTime * this.playerActions.Move.X, Space.World);
			base.transform.Rotate(Vector3.right, 500f * Time.deltaTime * this.playerActions.Move.Y, Space.World);
			Color a = (!this.playerActions.Fire.IsPressed) ? Color.white : Color.red;
			Color b = (!this.playerActions.Jump.IsPressed) ? Color.white : Color.green;
			this.cachedRenderer.material.color = Color.Lerp(a, b, 0.5f);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000083DC File Offset: 0x000065DC
		private void SaveBindings()
		{
			this.saveData = this.playerActions.Save();
			PlayerPrefs.SetString("Bindings", this.saveData);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00008400 File Offset: 0x00006600
		private void LoadBindings()
		{
			if (PlayerPrefs.HasKey("Bindings"))
			{
				this.saveData = PlayerPrefs.GetString("Bindings");
				this.playerActions.Load(this.saveData);
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00008440 File Offset: 0x00006640
		private void OnApplicationQuit()
		{
			PlayerPrefs.Save();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00008448 File Offset: 0x00006648
		private void OnGUI()
		{
			float num = 10f;
			GUI.Label(new Rect(10f, num, 300f, num + 22f), "Last Input Type: " + this.playerActions.LastInputType.ToString());
			num += 22f;
			int count = this.playerActions.Actions.Count;
			for (int i = 0; i < count; i++)
			{
				PlayerAction playerAction = this.playerActions.Actions[i];
				string text = playerAction.Name;
				if (playerAction.IsListeningForBinding)
				{
					text += " (Listening)";
				}
				text = text + " = " + playerAction.Value;
				GUI.Label(new Rect(10f, num, 300f, num + 22f), text);
				num += 22f;
				int count2 = playerAction.Bindings.Count;
				for (int j = 0; j < count2; j++)
				{
					BindingSource bindingSource = playerAction.Bindings[j];
					GUI.Label(new Rect(75f, num, 300f, num + 22f), bindingSource.DeviceName + ": " + bindingSource.Name);
					if (GUI.Button(new Rect(20f, num + 3f, 20f, 17f), "-"))
					{
						playerAction.RemoveBinding(bindingSource);
					}
					if (GUI.Button(new Rect(45f, num + 3f, 20f, 17f), "+"))
					{
						playerAction.ListenForBindingReplacing(bindingSource);
					}
					num += 22f;
				}
				if (GUI.Button(new Rect(20f, num + 3f, 20f, 17f), "+"))
				{
					playerAction.ListenForBinding();
				}
				if (GUI.Button(new Rect(50f, num + 3f, 50f, 17f), "Reset"))
				{
					playerAction.ResetBindings();
				}
				num += 25f;
			}
			if (GUI.Button(new Rect(20f, num + 3f, 50f, 22f), "Load"))
			{
				this.LoadBindings();
			}
			if (GUI.Button(new Rect(80f, num + 3f, 50f, 22f), "Save"))
			{
				this.SaveBindings();
			}
			if (GUI.Button(new Rect(140f, num + 3f, 50f, 22f), "Reset"))
			{
				this.playerActions.Reset();
			}
		}

		// Token: 0x040000DD RID: 221
		private Renderer cachedRenderer;

		// Token: 0x040000DE RID: 222
		private PlayerActions playerActions;

		// Token: 0x040000DF RID: 223
		private string saveData;
	}
}
