using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord : MonoBehaviour
{
    public List<Player> playerList = new List<Player>();
    public string[] levels;
    public Color[] playerColours;
    [HideInInspector] public int levelIndex;

    void Awake()
    {
        // Make this object persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayer(string name)
    {
        // Safety check: prevent out of range if colours < players
        int colorIndex = Mathf.Clamp(playerList.Count, 0, playerColours.Length - 1);

        playerList.Add(new Player(name, playerColours[colorIndex], levels.Length));
    }

    public void AddPutts(int playerIndex, int PuttCount)
    {
        playerList[playerIndex].putts[levelIndex] = PuttCount;
    }

    public class Player
    {
        public string name;
        public Color colour;
        public int[] putts;

        public Player(string newName, Color newColor, int levelCount)
        {
            name = newName;
            colour = newColor;
            putts = new int[levelCount];
        }
    }
}
