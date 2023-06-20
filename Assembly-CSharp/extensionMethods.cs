using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using InControl;
using UnityEngine;

// Token: 0x0200027C RID: 636
public static class extensionMethods
{
	// Token: 0x06000F40 RID: 3904 RVA: 0x00063744 File Offset: 0x00061944
	public static T[] FromToIndex<T>(this T[] data, int begin, int end)
	{
		List<T> list = new List<!!0>();
		for (int i = begin; i < end; i++)
		{
			list.Add(data[i]);
		}
		return list.ToArray();
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00063780 File Offset: 0x00061980
	public static int IndexOfPhilip(this int[] array, int mapToLoad)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == mapToLoad)
			{
				return i + 1;
			}
		}
		throw new Exception("Cannot Find Map: " + mapToLoad + " In Mapcycle!");
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x000637C8 File Offset: 0x000619C8
	public static levelEditorManager.Map Clone(this levelEditorManager.Map mapToClone)
	{
		levelEditorManager.Map map = new levelEditorManager.Map();
		foreach (levelEditorManager.Block block in mapToClone.tiles)
		{
			levelEditorManager.Block b = block;
			map.tiles.Add(new levelEditorManager.Block(b, 1337));
		}
		map.settings = mapToClone.settings;
		return map;
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x00063854 File Offset: 0x00061A54
	public static bool ContainsModifier(this Dictionary<Modifier, int> currentModifierDic, Modifier modToCheck)
	{
		foreach (KeyValuePair<Modifier, int> keyValuePair in currentModifierDic)
		{
			if (keyValuePair.Key.Equals(modToCheck))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x000638CC File Offset: 0x00061ACC
	public static getModifierOptions.Parameter[] Copy(this getModifierOptions.Parameter[] arr)
	{
		if (arr == null)
		{
			return null;
		}
		getModifierOptions.Parameter[] array = new getModifierOptions.Parameter[arr.Length];
		for (int i = 0; i < arr.Length; i++)
		{
			array[i] = arr[i].copy();
		}
		return array;
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x0006390C File Offset: 0x00061B0C
	public static ObjectBehaviourContainer[] SortByWeight(this ObjectBehaviourContainer[] arr)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			for (int j = 0; j < arr.Length - 1; j++)
			{
				if (arr[j].Behaviour > arr[j + 1].Behaviour)
				{
					ObjectBehaviourContainer objectBehaviourContainer = arr[j + 1];
					arr[j + 1] = arr[j];
					arr[j] = objectBehaviourContainer;
				}
			}
		}
		return arr;
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x00063970 File Offset: 0x00061B70
	public static ObjectParameterContainer[] ToContainerParameters(this getModifierOptions.Parameter[] parameters)
	{
		if (parameters == null)
		{
			return null;
		}
		List<ObjectParameterContainer> list = new List<ObjectParameterContainer>();
		for (int i = 0; i < parameters.Length; i++)
		{
			list.Add(new ObjectParameterContainer(parameters[i].getName(), parameters[i].getValue()));
		}
		return list.ToArray();
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x000639C0 File Offset: 0x00061BC0
	public static levelEditorManager.Block ToBlock(this GameObject gameObj)
	{
		return levelEditorManager.Instance().getCurrentMap.getTileAt(gameObj.transform);
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x000639D8 File Offset: 0x00061BD8
	public static List<levelEditorManager.Block> ToBlockList(this GameObject[] objList)
	{
		levelEditorManager.Map getCurrentMap = levelEditorManager.Instance().getCurrentMap;
		List<levelEditorManager.Block> list = new List<levelEditorManager.Block>();
		for (int i = 0; i < objList.Length; i++)
		{
			list.Add(getCurrentMap.getTileAt(objList[i].transform));
		}
		return list;
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x00063A20 File Offset: 0x00061C20
	public static List<string> ToList(this string[] arr)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < arr.Length; i++)
		{
			list.Add(arr[i]);
		}
		return list;
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x00063A54 File Offset: 0x00061C54
	public static List<GameObject> ToList(this GameObject[] arr)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < arr.Length; i++)
		{
			list.Add(arr[i]);
		}
		return list;
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x00063A88 File Offset: 0x00061C88
	public static Vector3 ToVector3(this string s)
	{
		Vector3 result;
		try
		{
			string[] array = s.Substring(1, s.Length - 2).Split(new char[]
			{
				','
			});
			float x = float.Parse(array[0]);
			float y = float.Parse(array[1]);
			float z = float.Parse(array[2]);
			Vector3 vector = new Vector3(x, y, z);
			result = vector;
		}
		catch
		{
			result = Vector3.one;
		}
		return result;
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x00063B18 File Offset: 0x00061D18
	public static bool EqualsValues(this string[] arr1, string[] arr2)
	{
		if (arr1.Length != arr2.Length)
		{
			return false;
		}
		for (int i = 0; i < arr1.Length; i++)
		{
			if (!arr1[i].Equals(arr2[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x00063B5C File Offset: 0x00061D5C
	public static bool IsValid(this string fileName)
	{
		foreach (char c in Path.GetInvalidFileNameChars())
		{
			Debug.Log("Invalid CHar: " + c);
		}
		return !string.IsNullOrEmpty(fileName) && fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0 && !fileName.Contains(".") && !fileName.Contains(",") && !fileName.Contains(":");
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x00063BEC File Offset: 0x00061DEC
	public static Vector3 setYValue(this Vector3 vec3, float yValue)
	{
		return new Vector3(vec3.x, yValue, vec3.z);
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x00063C04 File Offset: 0x00061E04
	public static float strip(this float _float, int decimals)
	{
		return float.Parse(_float.ToString("F" + decimals.ToString()));
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x00063C30 File Offset: 0x00061E30
	public static Vector3 setXValue(this Vector3 vec3, float xValue)
	{
		return new Vector3(xValue, vec3.y, vec3.z);
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00063C48 File Offset: 0x00061E48
	public static Vector3 setZValue(this Vector3 vec3, float zValue)
	{
		return new Vector3(vec3.x, vec3.y, zValue);
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00063C60 File Offset: 0x00061E60
	public static bool getInput(bool controller)
	{
		if (controller)
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			return activeDevice.AnyButton.IsPressed || activeDevice.LeftTrigger.IsPressed || activeDevice.RightTrigger.IsPressed || activeDevice.RightStick.Value.magnitude > 0.01f || activeDevice.LeftStick.Value.magnitude > 0.01f || activeDevice.RightBumper.IsPressed || activeDevice.LeftBumper.IsPressed;
		}
		return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetAxis("Mouse X") > 0.1f || Input.GetAxis("Mouse Y") > 0.1f;
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x00063D84 File Offset: 0x00061F84
	public static string[] syncHelpTexts(string[] _from, string[] _to)
	{
		for (int i = 0; i < _from.Length; i++)
		{
			if (string.IsNullOrEmpty(_from[i]))
			{
				_to[i] = string.Empty;
			}
		}
		return _to;
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x00063DC0 File Offset: 0x00061FC0
	public static string ColorToHex(Color32 color)
	{
		return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00063E08 File Offset: 0x00062008
	public static Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
		return new Color32(r, g, b, byte.MaxValue);
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x00063E60 File Offset: 0x00062060
	public static Color newAlphaColor(Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, 255f / alpha);
	}
}
