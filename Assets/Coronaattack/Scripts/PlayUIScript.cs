using UnityEngine;
using UnityEngine.UI;

public class PlayUIScript : MonoBehaviour
{
    #if UNITY_ANDROID
    public static PlayUIScript Instance { get; private set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    [SerializeField]
    private Text pointsTxt;

    public void GetPoint()
    {
        PlayManagerScript.Instance.IncrementCounter();
    }

    public void Restart()
    {
        PlayManagerScript.Instance.RestartGame();
    }

    public void Increment()
    {
        //PlayGamesScript.IncrementAchievement(GPGSIds.achievement_incremental_achievement, 5);
    }

    public void Unlock()
    {
        //PlayGamesScript.UnlockAchievement(GPGSIds.achievement_standard_achievement);
    }

    public void ShowAchievements()
    {
        PlayGamesScript.ShowAchievementsUI();
    }

    public void ShowLeaderboards()
    {
        PlayGamesScript.ShowLeaderboardsUI();
    }

    public void UpdatePointsText()
    {
        pointsTxt.text = PlayManagerScript.Counter.ToString();
    }
#endif
}