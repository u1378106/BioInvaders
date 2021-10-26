using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public Text ScoreText;
	public float score, highScore;
	public TextMesh motivationalMessage;

	private bool isHighScore = false;

	private string message1 = "Woohoo !";
	private string message2 = "Nice !";
	private string message3 = "Amazing !";
	private string message4 = "Killin’ it!";
	private string message5 = "Incredible!";
	private string message6 = "Acing It!";
	private string message7 = "New High Score!";

	[SerializeField]
	private string[] motivationMsgs = new string[7];

	public GameObject confettis;

	SpawnManager spawnManager;

	private void Start()
	{
		ScoreText = GameManager.gm.GameplayUI.Find("ScorePlaceholder").Find("Score").GetComponent<Text>();

		//PlayerPrefs.SetInt("HighScore", 16);
		//PlayerPrefs.DeleteAll();

	    motivationMsgs[0] = message1;
		motivationMsgs[1] = message2;
		motivationMsgs[2] = message3;
		motivationMsgs[3] = message4;
		motivationMsgs[4] = message5;
		motivationMsgs[5] = message6;
		motivationMsgs[6] = message7;

		motivationalMessage.gameObject.SetActive(false);
		confettis.SetActive(false);

		spawnManager = GameObject.FindObjectOfType<SpawnManager>();      
	}

	private void Update()
	{
		score += Time.deltaTime;
		ScoreText.text = System.Math.Round(score, 0).ToString();

		if ((System.Math.Round(score, 0) >= PlayerPrefs.GetInt("HighScore")) && isHighScore == false && (PlayerPrefs.GetInt("isFirstGame") == 1))
		{           
			isHighScore = true;
			motivationalMessage.gameObject.SetActive(true);
			motivationalMessage.text = motivationMsgs[6];
			confettis.SetActive(true);
			//StartCoroutine(PauseEnemies());
		}

		else if (ScoreText.text == "20")
        {
			motivationalMessage.gameObject.SetActive(true);
			motivationalMessage.text = motivationMsgs[0];
		}
		else if (ScoreText.text == "50")
		{
			motivationalMessage.gameObject.SetActive(true);
			motivationalMessage.text = motivationMsgs[1];
		}
		else if (ScoreText.text == "100")
		{
			motivationalMessage.gameObject.SetActive(true);
			motivationalMessage.text = motivationMsgs[2];
		}
		else if (ScoreText.text == "150")
		{
			motivationalMessage.gameObject.SetActive(true);
			motivationalMessage.text = motivationMsgs[3];
		}
		else if (ScoreText.text == "200")
		{
			motivationalMessage.gameObject.SetActive(true);
			motivationalMessage.text = motivationMsgs[4];
		}
		else if (ScoreText.text == "250")
		{
			motivationalMessage.gameObject.SetActive(true);
			motivationalMessage.text = motivationMsgs[5];
		}
	}

    IEnumerator PauseEnemies()
    {
        
		yield return new WaitForSeconds(3f);

	}
}
