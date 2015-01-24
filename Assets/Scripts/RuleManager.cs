using UnityEngine;
using System.Collections;

public class RuleManager : MonoBehaviour {

    public enum Rule
    {
        KICK,        // Kicking the ball
        RED_BALL,    // Sets target to red ball
        BLUE_BALL,   // Sets target to blue ball
        WHITE_BALL,  // Sets target to white blue
        SCORE,       // Bring the ball to current zone
        KEEP,        // Hold onto the ball
        DONOT        // Negates the requirements of the current rule
    };

    Rule rule;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
