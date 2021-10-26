using UnityEngine;

public class PlayManagerScript : MonoBehaviour
{
#if UNITY_ANDROID
    public static PlayManagerScript Instance { get; private set; }
    public static int Counter { get; private set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    public void IncrementCounter()
    {
        Counter++;
        PlayUIScript.Instance.UpdatePointsText();
    }

    public void RestartGame()
    {
        PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_best_score, Counter);
        Counter = 0;
        PlayUIScript.Instance.UpdatePointsText();
    }
#endif
}