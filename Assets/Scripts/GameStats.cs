using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour 
{
    int p1Score, p2Score, p3Score, p4Score = 0;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SetScore(int player, int amount)
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
}
