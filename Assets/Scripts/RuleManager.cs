using UnityEngine;
using System.Collections;

public class RuleManager : MonoBehaviour 
{

    public enum Verbtype
    {
        Hit,
        Grab,
        Stun,
        Bring
    }

    public enum NounType
    {
        Ball,
        Player
    }

    public enum Rule
    {
        NO_RULE,                  // No current rule
        KICK,                     // Kicking the ball
        RED_BALL,                 // Sets target to red ball
        BLUE_BALL,                // Sets target to blue ball
        WHITE_BALL,               // Sets target to white blue
        SCORE,                    // Bring the ball to current zone
        KEEP,                     // Hold onto the ball
        DONOT                     // Negates the requirements of the current rule
    };

    Rule rule;                    // The current rule in play
    ArrayList switches;           // Switches currently in the scene

    Verbtype verbTarget;
    NounType nounTarget;
    string adjective = "";
    public GameObject[] players;  // Reference to the players in scene

    const float MAX_TIME = 10.0f; // Maximum time a rule can be in play
    float timer = 0.0f;           // A timer counting to MAX_TIME

	// Use this for initialization
	void Start () 
    {
        switches = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(rule != Rule.NO_RULE)
            timer += Time.deltaTime;

        if (timer >= MAX_TIME)
        {
            timer = 0.0f;
        }
	}

    public void AddSwitch(Switch gameSwitch)
    {
        switches.Add(gameSwitch);
        Debug.Log(gameSwitch + " Added");
    }

    public void Remove(Switch gameSwitch)
    {
        foreach (Switch s in switches)
        {
            if (s.Equals(gameSwitch))
                switches.Remove(s);
            Debug.Log(s + " Removed");
        }
    }

    public void CheckRule(int player, Verbtype verb, NounType noun, string adjective)
    {

    }
}
