using UnityEngine;
using MP;

public class MoviePlayer : MoviePlayerBase
{
	public TextAsset source;
	public AudioClip audioSource;
	public LoadOptions loadOptions;
	public float videoTime;
	public int videoFrame;
	public bool loop;
	public bool isReverse;
}
