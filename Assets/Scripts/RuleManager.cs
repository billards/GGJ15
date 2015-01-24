using UnityEngine;
using System.Collections;

public class RuleManager : MonoBehaviour 
{

    public enum Verbtype
    {
        Kick,
        Grab,
        Tag,
        Bring
    }

    public enum NounType
    {
        Ball,
        Player
    }

    public enum AdjectiveType
    {
        Blue,
        Red,
        White,
        One,
        Two,
        Three,
        Four
    }

    //Current
    public class Rule
    {
        Rule();
        Verbtype verbTarget;
        NounType nounTarget;
        string adjective = "";

        bool operator==(Rule one, Rule two)
        {
            if(one.verbTarget == two.verbTarget)
            {
                if (one.nounTarget == two.nounTarget)
                {
                    if (one.adjective == two.adjective)
                        return true;
                    else 
                        return false;
                }
                else 
                    return false;
            }
            else
                return false;
        }
    }

    public GameObject[] players;  // Reference to the players in scene
    ArrayList switches;           // Switches currently in the scene

    const float MAX_TIME = 10.0f; // Maximum time a rule can be in play
    float timer = 0.0f;           // A timer counting to MAX_TIME

    void Awake()
    {
        switches = new ArrayList();
    }

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        //if(rule != Rule.NO_RULE)
        //    timer += Time.deltaTime;

        if (timer >= MAX_TIME)
            timer = 0.0f;
	}

    public void AddSwitch(Switch gameSwitch)
    {
        Debug.Log(gameSwitch);
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
