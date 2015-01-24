using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour 
{
    int p1Score, p2Score, p3Score, p4Score = 0;

    public void AddScore(int player, int amount)
    {
        switch(player)
        {
            case 1:
                {
                    p1Score += amount;
                    break;
                }
            case 2:
                {
                    p2Score += amount;
                    break;
                }
            case 3:
                {
                    p3Score += amount;
                    break;
                }
            case 4:
                {
                    p4Score += amount;
                    break;
                }
            default:
                {
                    Debug.Log("ERROR - " + player + " Is invalid!");
                    break;
                }
        }
    }

    public void CheckForNegativeScore()
    {
        if (p1Score < 0) p1Score = 0;
        if (p2Score < 0) p2Score = 0;
        if (p3Score < 0) p3Score = 0;
        if (p4Score < 0) p4Score = 0;
    }
}
