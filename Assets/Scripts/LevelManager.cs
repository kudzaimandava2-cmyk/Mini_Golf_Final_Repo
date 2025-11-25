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
        playerRecord = GameObject.Find("Player Record")?.GetComponent<PlayerRecord>();
        if (playerRecord == null || playerRecord.playerList.Count == 0) return;

        playerIndex = 0;
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        // Set up ball color
        if (ball != null && playerIndex < playerRecord.playerColours.Length)
        {
            ball.SetupBall(playerRecord.playerColours[playerIndex]);
        }

        // Set player name
        if (labelPlayerName != null && playerIndex < playerRecord.playerList.Count)
        {
            labelPlayerName.ForceMeshUpdate(); // TMP-safe initialization
            labelPlayerName.text = playerRecord.playerList[playerIndex].name;
        }
    }

    public void NextPlayer(int previousPutts)
    {
        if (playerRecord == null) return;

        // Save previous player's putts
        playerRecord.AddPutts(playerIndex, previousPutts);

        // Move to next player
        if (playerIndex < playerRecord.playerList.Count - 1)
        {
            playerIndex++;
            SetupPlayer(); // Correctly sets up ball and player UI
        }
        else
        {
            // Last player: move to next level or scoreboard
            if (playerRecord.levelIndex >= playerRecord.levels.Length - 1)
            {
                Debug.Log("Scoreboard");
                // TODO: Load scoreboard scene here
            }
            else
            {
                playerRecord.levelIndex++;
                SceneManager.LoadScene(playerRecord.levels[playerRecord.levelIndex]);
            }
        }
    }
}
