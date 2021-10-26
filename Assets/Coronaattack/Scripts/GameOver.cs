using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private string message1 = "NO SURPRISE. \n YOU’RE A LOSER";
    private string message2 = "DAYUUUUM! \n YOU WERE ALMOST COOL";
    private string message3 = "COULD YOU BE ANYMORE DED?";
    private string message4 = "YOU GOT BEAT!";
    private string message5 = "F*CK! YOU DIED";
    private string message6 = "SHESH! \n THAT WAS WEAK";
    private string message7 = "HERE WE GO AGAIN. \n TRY NOT TO SUCK THIS TIME";

    private string message8 = "WOW! YOU'RE GETTING THE HANG OF THIS!";
    private string message9 = "YOU'VE ALMOST GOT IT! \n TRY AGAIN!";
    private string message10 = "GOOD JOB! \n YOU’RE A FAST LEARNER!";
    private string message11 = "ANOTHER ROUND ANYONE?";
    private string message12 = "ALMOST THERE! \n LET’S GO AGAIN.";
    private string message13 = "YOU’RE QUITE THE HERO!";
    private string message14 = "LOOKS LIKE YOU DON’T NEED A CAPE TO BE A HERO ;)";

    private string message15 = "THAT WAS SENSATIONAL!";
    private string message16 = "LOOK AT THAT SCORE! \n YOU’RE A WARRIOR!";
    private string message17 = "WHAT A RUSH! \n YOU’RE AMAZING!";
    private string message18 = "CAN YOU BE MY PANDEMIC HERO TOO?";
    private string message19 = "YOU’RE SAVING THE WORLD!";
    private string message20 = "THE AVENGERS NEED YOU!";
    private string message21 = "A TRUE HERO ALWAYS TRIES AGAIN!";

    [SerializeField]
    private string[] loserMessages = new string[7];

    [SerializeField]
    private string[] averageMessages = new string[7];

    [SerializeField]
    private string[] heroMessages = new string[7];


    [SerializeField]
    private Text gameOverMessage;

    [SerializeField]
    Text score, bestScore;

    ScoreManager scoreManager;

    private GameObject share, tryAgain, leaderboard, tryAgainWeb;

    // Start is called before the first frame update
    void OnEnable()
        
    {
        loserMessages[0] = message1;
        loserMessages[1] = message2;
        loserMessages[2] = message3;
        loserMessages[3] = message4;
        loserMessages[4] = message5;
        loserMessages[5] = message6;
        loserMessages[6] = message7;

        averageMessages[0] = message8;
        averageMessages[1] = message9;
        averageMessages[2] = message10;
        averageMessages[3] = message11;
        averageMessages[4] = message12;
        averageMessages[5] = message13;
        averageMessages[6] = message14;

        heroMessages[0] = message15;
        heroMessages[1] = message16;
        heroMessages[2] = message17;
        heroMessages[3] = message18;
        heroMessages[4] = message19;
        heroMessages[5] = message20;
        heroMessages[6] = message21;
    }

    private void Start()
    {         
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        bestScore.text = PlayerPrefs.GetInt("HighScore").ToString();

        if (scoreManager.score <= 50)
        {
            gameOverMessage.text = loserMessages[Random.Range(0, 6)];
        }
        else if(scoreManager.score >= 51 && scoreManager.score <= 100)
        {
            gameOverMessage.text = averageMessages[Random.Range(0, 6)];
        }
        else
        {
            gameOverMessage.text = heroMessages[Random.Range(0, 6)];
        }

        score.text = scoreManager.ScoreText.text;

        share = GameObject.Find("ShareButton");
        tryAgain = GameObject.Find("TryAgainButton");
        leaderboard = GameObject.Find("LeaderboardButton");
        tryAgainWeb = GameObject.Find("TryAgainWeb");

#if UNITY_WEBGL
        Destroy(share);
        Destroy(tryAgain);
        Destroy(leaderboard);

#endif

#if UNITY_ANDROID
        Destroy(tryAgainWeb);
#endif

    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
    
}

    

