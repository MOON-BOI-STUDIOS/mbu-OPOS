using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    [SerializeField]
    private TMP_InputField inputName;
    [SerializeField]
    private TextMeshProUGUI currentScoreUI;
    private int currentScore;


    public string publicLeaderboardKey;

    [SerializeField]
    private TMP_InputField raceGameScore;
   
    private void Start()
    {
        //gets leaderboard on load
        getLeaderboard();

        //resets player when loading, so that new entries can be put
        LeaderboardCreator.ResetPlayer();

        //calculates the score while submitting
        currentScore = (PlayerPrefs.GetInt("Round") * PlayerPrefs.GetInt("Coins"));
        
        //shows the current score as UI
        if (SceneManager.GetActiveScene().name!="BikeRace")
        currentScoreUI.text = currentScore.ToString();
    }


    //gets leaderboard from the internet
    public void getLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {

            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for(int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
                
            }
        }));
    }


    //adds new entry to the leaderboard
    public void setLeaderboardEntry()
    {
        
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, inputName.text, currentScore, ((msg) =>
        {            getLeaderboard();        }));
    }

    public void setLeaderboardEntryforrace()
    {

        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, inputName.text, (int)RaceGameManager.inst.score , ((msg) =>
        { getLeaderboard(); }));
    }
}
