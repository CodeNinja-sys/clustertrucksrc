using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

// Token: 0x02000222 RID: 546
public class dataBaseHandler : MonoBehaviour
{
	// Token: 0x06000CCA RID: 3274 RVA: 0x0004F03C File Offset: 0x0004D23C
	private void Awake()
	{
		if (UnityEngine.Object.FindObjectOfType<dataBaseHandler>().gameObject != base.gameObject)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0004F07C File Offset: 0x0004D27C
	private void Start()
	{
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0004F080 File Offset: 0x0004D280
	public IEnumerator PostTime(int mapID, string mapTime, int deaths, bool hej)
	{
		float time = float.Parse(mapTime);
		string hash = this.Md5Sum(mapID.ToString() + time.ToString() + deaths.ToString() + this.secretKey);
		string post_url = string.Concat(new object[]
		{
			this.addTimeURL,
			"mapid=",
			mapID,
			"&time=",
			time,
			"&deaths=",
			deaths,
			"&hash=",
			hash
		});
		Debug.Log(this.addTimeURL);
		WWW time_post = new WWW(post_url);
		yield return time_post;
		if (time_post.error != null)
		{
			MonoBehaviour.print("There was an error posting the score: " + time_post.error);
		}
		MonoBehaviour.print("Time Uploaded: " + time_post.text);
		yield break;
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0004F0C8 File Offset: 0x0004D2C8
	private IEnumerator GetScores()
	{
		WWW hs_get = new WWW(this.getScoreURL);
		yield return hs_get;
		if (hs_get.error != null)
		{
			MonoBehaviour.print("There was an error getting the high score: " + hs_get.error);
		}
		yield break;
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0004F0E4 File Offset: 0x0004D2E4
	private IEnumerator DownloadMap(uint id)
	{
		string download_url = this.downloadMapURL + "data=" + id;
		WWW map_get = new WWW(download_url);
		yield return map_get;
		if (map_get.error != null)
		{
			MonoBehaviour.print("There was an error getting the high score: " + map_get.error);
		}
		else
		{
			Debug.Log(map_get.text);
			string[] mapInfo = map_get.text.Split(new char[]
			{
				'\t'
			});
			string data = mapInfo[0];
			string name = mapInfo[1];
			Debug.Log("Mapname: " + name + "    Data: " + data);
			string path = Application.dataPath + "/maps/" + name + levelEditorManager.fileEnding;
			File.WriteAllText(path, data);
		}
		yield break;
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0004F110 File Offset: 0x0004D310
	public IEnumerator PostMap(string name, uint _size)
	{
		string data = File.ReadAllText(Application.dataPath + "/maps/" + name + levelEditorManager.fileEnding);
		string author = "PhillyG";
		uint size = _size;
		string hash = this.Md5Sum(string.Concat(new string[]
		{
			data,
			author,
			size.ToString(),
			name,
			this.secretKey
		}));
		string post_url = string.Concat(new object[]
		{
			this.addMapURL,
			"data=",
			WWW.EscapeURL(data),
			"&author=",
			WWW.EscapeURL(author),
			"&size=",
			size,
			"&name=",
			WWW.EscapeURL(name),
			"&hash=",
			hash
		});
		WWW map_post = new WWW(post_url);
		yield return map_post;
		string message = "Upload succesful!";
		if (map_post.error != null)
		{
			message = "Upload Failed! " + map_post.error;
			MonoBehaviour.print("There was an error posting the map: " + map_post.error);
		}
		MonoBehaviour.print(map_post.text);
		DisplayManager.Instance().DisplayMessage(message);
		ModalPanel.Instance().ClosePanel();
		yield break;
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0004F148 File Offset: 0x0004D348
	public string Md5Sum(string strToEncrypt)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		byte[] bytes = utf8Encoding.GetBytes(strToEncrypt);
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = md5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}

	// Token: 0x0400095B RID: 2395
	private string secretKey = "kebabtallrik2";

	// Token: 0x0400095C RID: 2396
	private string addTimeURL = "http://www.server.landfallgamestudio.com/php/addTime.php?";

	// Token: 0x0400095D RID: 2397
	private string addMapURL = "http://www.server.landfallgamestudio.com/php/addMap.php?";

	// Token: 0x0400095E RID: 2398
	private string downloadMapURL = "http://www.server.landfallgamestudio.com/php/downloadMap.php?";

	// Token: 0x0400095F RID: 2399
	public string getScoreURL = "http://localhost/display.php";
}
