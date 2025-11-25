using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputPlayerName;
    public PlayerRecord playerRecord;
    public Button buttonStart;
    public Button buttonAddPlayer;

    private void Start()
    {
        // Start button only enabled when there is at least one player
        buttonStart.interactable = playerRecord.playerList.Count > 0;
    }

    public void ButtonAddPlayer()
    {
        if (string.IsNullOrWhiteSpace(inputPlayerName.text)) return;

        playerRecord.AddPlayer(inputPlayerName.text.Trim());

        buttonStart.interactable = true;
        inputPlayerName.text = "";
        inputPlayerName.ForceLabelUpdate();

        // Disable adding more players if max reached
        if (playerRecord.playerList.Count >= playerRecord.playerColours.Length)
            buttonAddPlayer.interactable = false;
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene(playerRecord.levels[0]);
    }
}
