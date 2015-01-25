using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour 
{
    int p1Score, p2Score, p3Score, p4Score = 0;
    int t1Score, t2Score = 0;
    public UIInterface uiInterface;

    public void AddScore(int player, int amount)
    {
        if (RuleManager.instance.teamMode)
        {
            switch (player)
            {
                case 1:
                case 3:
                    {
                        t1Score += amount;
                        CheckForNegativeScore();
                        uiInterface.UpdateInterface(UIInterface.InterfaceElement.T1Score, t1Score);
                        break;
                    }

                case 4:
                case 2:
                    {
                        t2Score += amount;
                        CheckForNegativeScore();
                        uiInterface.UpdateInterface(UIInterface.InterfaceElement.T2Score, t2Score);
                        break;
                    }
                default:
                    {
                        Debug.Log("ERROR - " + player + " Is invalid!");
                        break;
                    }
            }
        }
        else
        {
            switch (player)
            {
                case 1:
                    {
                        p1Score += amount;
                        CheckForNegativeScore();
                        uiInterface.UpdateInterface(UIInterface.InterfaceElement.P1Score, p1Score);
                        break;
                    }
                case 2:
                    {
                        p2Score += amount;
                        CheckForNegativeScore();
                        uiInterface.UpdateInterface(UIInterface.InterfaceElement.P2Score, p2Score);
                        break;
                    }
                case 3:
                    {
                        p3Score += amount;
                        CheckForNegativeScore();
                        uiInterface.UpdateInterface(UIInterface.InterfaceElement.P3Score, p3Score);
                        break;
                    }
                case 4:
                    {
                        p4Score += amount;
                        CheckForNegativeScore();
                        uiInterface.UpdateInterface(UIInterface.InterfaceElement.P4Score, p4Score);
                        break;
                    }
                default:
                    {
                        Debug.Log("ERROR - " + player + " Is invalid!");
                        break;
                    }
            }
        }
    }

    public void CheckForNegativeScore()
    {
        if (p1Score < 0) p1Score = 0;
        if (p2Score < 0) p2Score = 0;
        if (p3Score < 0) p3Score = 0;
        if (p4Score < 0) p4Score = 0;
        if (t1Score < 0) t1Score = 0;
        if (t2Score < 0) t2Score = 0;
    }
}
