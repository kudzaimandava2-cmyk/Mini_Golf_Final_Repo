using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public BallController ball;
    public TextMeshProUGUI labelPlayerName;

    private PlayerRecord playerRecord;
    private int playerIndex;

    void Start()
    {
        playerRecord = GameObject.Find("Player Record").GetComponent<PlayerRecord>();

        playerIndex = 0;
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        // Set ball color
        ball.SetupBall(playerRecord.playerColours[playerIndex]);

        // Set player name text
        labelPlayerName.text = playerRecord.playerList[playerIndex].name;
        labelPlayerName.ForceMeshUpdate();  // TMP-safe update
    }

    public void NextPlayer(int previousPutts)
    {
        // Save last player's putts
        playerRecord.AddPutts(playerIndex, previousPutts);

        // Next player?
        if (playerIndex < playerRecord.playerList.Count - 1)
        {
            playerIndex++;
            SetupPlayer();
        }
        else
        {
            // Last player ? next level OR scoreboard
            if (playerRecord.levelIndex >= playerRecord.levels.Length - 1)
            {
                Debug.Log("Scoreboard");
                // TODO: SceneManager.LoadScene("Scoreboard");
            }
            else
            {
                playerRecord.levelIndex++;
                SceneManager.LoadScene(playerRecord.levels[playerRecord.levelIndex]);
            }
        }
    }
}
