using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Text scoreText;
	public Text infoText;
	public int levelDistance;
	public GameObject winScreen;
	public GameObject deathScreen;
	public bool won;
	public scoreScreenHandler scoreScreenH;
	public ScoreChecker scoreCheck;
}
