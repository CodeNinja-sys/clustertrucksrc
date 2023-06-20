using UnityEngine;
using UnityEngine.UI;

public class LevelSeletHandler : MonoBehaviour
{
	public Transform select;
	public Transform firstLevel;
	public Transform lastLevel;
	public bool beatWorld;
	public shakeObject selectShake;
	public GameObject[] worlds;
	public Text worldText;
	public Text worldTextUnder;
	public GameObject left;
	public GameObject right;
	public GameObject buttons;
	public Animator rightAnim;
}
