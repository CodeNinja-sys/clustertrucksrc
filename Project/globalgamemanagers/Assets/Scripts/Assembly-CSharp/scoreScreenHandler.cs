using UnityEngine;
using UnityEngine.UI;

public class scoreScreenHandler : MonoBehaviour
{
	public float finalScore;
	public Text scoreValueText;
	public Text scoreNameText;
	public Text scoreDescriptionText;
	public Text finalValue;
	public Text pointsValue;
	public Animator ScoreAnim;
	public Animator pointsAnim;
	public shakeObject shake;
	public AudioSource au;
	public AudioClip smack;
	public AudioClip count;
	public AudioClip add;
	public AudioClip swosh;
	public GameObject firstAbilityScreen;
	public GameObject buttons;
	public GameObject leaderBoardsGhostButtons;
	public GameObject scoreStuff;
	public leaderboardsManager leaderboardMan;
}
