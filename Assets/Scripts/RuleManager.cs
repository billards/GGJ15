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

    public class Rule
    {
        public Rule()
        {
            verbTarget = Verbtype.Kick;
            nounTarget = NounType.Ball;
            adjective = "";
        }

        public Rule(Verbtype verb, NounType noun, string adjective)
        {
            verbTarget = verb;
            nounTarget = noun;
            this.adjective = adjective;
        }

        Verbtype verbTarget;
        NounType nounTarget;
        string adjective = "";

        public Verbtype GetVerb()
        {
            return verbTarget;
        }

        public NounType GetNoun()
        {
            return nounTarget;
        }

        public string GetAdjective()
        {
            return adjective;
        }

        static public bool operator==(Rule one, Rule two)
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

        static public bool operator !=(Rule one, Rule two)
        {
            if (one.verbTarget == two.verbTarget)
            {
                if (one.nounTarget == two.nounTarget)
                {
                    if (one.adjective == two.adjective)
                        return false;
                    else
                        return true;
                }
                else
                    return true;
            }
            else
                return true;
        }
    }

    public GameObject[] players;  // Reference to the players in scene
    ArrayList switches;           // Switches currently in the scene
    Rule currentRule;             // The current rule the game is operating off of
    GameStats gameStats;          // For updating scores
    const float MAX_TIME = 10.0f; // Maximum time a rule can be in play
    float timer = 0.0f;           // A timer counting to MAX_TIME

    void Awake()
    {
        switches = new ArrayList();
    }

	// Use this for initialization
	void Start () 
    {
        gameStats = Camera.main.GetComponent<GameStats>() as GameStats;
        currentRule = new Rule();
	}
	
	// Update is called once per frame
	void Update () 
    {
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

    public void CheckRule(int player, Rule rule)
    {
        if (currentRule == rule && gameStats != null)
        {
            gameStats.SetScore(player, 1);
        }
    }

    //Future function to support multiple nouns
    //public void CheckRule(int player, Verbtype verb, NounType noun, NounType noun2, string adjective)
    //{
    //
    //}
}
