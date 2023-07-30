using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

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


    private string publicLeaderboardKey = "1f3224236e1bba874a424de406871d1babb2ef1345046d4c345bb741ddd72a26";

    private void Start()
    {
        //gets leaderboard on load
        getLeaderboard();

        //resets player when loading, so that new entries can be put
        LeaderboardCreator.ResetPlayer();

        //calculates the score while submitting
        currentScore = (PlayerPrefs.GetInt("Round") * PlayerPrefs.GetInt("Coins"));
        
        //shows the current score as UI
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
}
