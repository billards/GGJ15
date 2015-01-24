using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleManager : MonoBehaviour 
{

    public static RuleManager instance;

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
        Player,
        Switch
    }

    public enum AdjectiveType
    {
        None,
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
        bool invertRule = false;

        public Rule()
        {
            verbTarget = Verbtype.Kick;
            nounTarget = NounType.Ball;
            adjective = AdjectiveType.White;
        }

        public Rule(Verbtype verb, NounType noun, AdjectiveType adjective, bool invertRule)
        {
            verbTarget = verb;
            nounTarget = noun;
            this.adjective = adjective;
            this.invertRule = invertRule;
        }

        public void InvertRule()
        {
            invertRule = !invertRule;
        }

        public bool IsInverted()
        {
            return invertRule;
        }

        Verbtype verbTarget;
        NounType nounTarget;
        AdjectiveType adjective;

        public Verbtype GetVerb()
        {
            return verbTarget;
        }

        public NounType GetNoun()
        {
            return nounTarget;
        }

        public AdjectiveType GetAdjective()
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
    Rule currentRule;             // The current rule the game is operating off of
    GameStats gameStats;          // For updating scores
    const float MAX_TIME = 10.0f; // Maximum time a rule can be in play
    float timer = 0.0f;           // A timer counting to MAX_TIME
    System.Random rnd = new System.Random();
    bool isRuleInverted = false;

    Dictionary<Verbtype, List<NounType>> VerbToNoun = new Dictionary<Verbtype, List<NounType>>();
    Dictionary<NounType, List<AdjectiveType>> NounToAdjective = new Dictionary<NounType, List<AdjectiveType>>();
    void Awake()
    {
        VerbToNoun[Verbtype.Kick] = new List<NounType>() { NounType.Ball, NounType.Player };
        VerbToNoun[Verbtype.Grab] = new List<NounType>() { NounType.Ball, NounType.Player };
        VerbToNoun[Verbtype.Tag] = new List<NounType>() { NounType.Player };
        VerbToNoun[Verbtype.Bring] = new List<NounType>() { NounType.Ball, NounType.Player };

        NounToAdjective[NounType.Ball] = new List<AdjectiveType>() { AdjectiveType.Blue, AdjectiveType.Red, AdjectiveType.White };
        NounToAdjective[NounType.Player] = new List<AdjectiveType>() { AdjectiveType.One, AdjectiveType.Two, AdjectiveType.Three, AdjectiveType.Four };
        NounToAdjective[NounType.Switch] = new List<AdjectiveType>() { AdjectiveType.None };
    }

	// Use this for initialization
	void Start () 
    {
        instance = this;
        gameStats = Camera.main.GetComponent<GameStats>() as GameStats;
        currentRule = new Rule();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (timer < MAX_TIME)
        {
            SetRule(RandomRule());
            timer += Time.deltaTime;
        }
        else
        {
            
            timer = 0.0f;
        }
	}

    public void CheckRule(int player, Rule rule)
    {
        if (currentRule == rule && gameStats != null)
        {
            if (currentRule.IsInverted())
            {
                gameStats.AddScore(player, 1);
            }
            else
            {
                gameStats.AddScore(player, -5);
            }
        }
    }

    public void ChangeVerb()
    {
        Verbtype newVerb = GetRandomEnum<RuleManager.Verbtype>();
        NounType newNoun = currentRule.GetNoun();
        AdjectiveType newAdjective = currentRule.GetAdjective();
        if (!VerbToNoun[newVerb].Contains(newNoun))
        {
           newNoun = VerbToNoun[newVerb][rnd.Next(VerbToNoun[newVerb].Count)];
           
           if (!NounToAdjective[newNoun].Contains(newAdjective))
           {
               newAdjective = NounToAdjective[newNoun][rnd.Next(NounToAdjective[newNoun].Count)];
           }
        }
        SetRule(new Rule(newVerb, newNoun, newAdjective, isRuleInverted));
    }

    public void ChangeNoun()
    {
        Verbtype newVerb = currentRule.GetVerb();

        NounType newNoun = VerbToNoun[newVerb][rnd.Next(VerbToNoun[newVerb].Count)];

        while(newNoun != currentRule.GetNoun() && VerbToNoun[newVerb].Count != 1)
        {
            newNoun = VerbToNoun[newVerb][rnd.Next(VerbToNoun[newVerb].Count)];
        }

        AdjectiveType newAdjective = NounToAdjective[newNoun][rnd.Next(NounToAdjective[newNoun].Count)];
        SetRule(new Rule(newVerb, newNoun, newAdjective, isRuleInverted));
    }

    public void InvertRule()
    {
        isRuleInverted = !isRuleInverted;
        currentRule.InvertRule();
    }

    public Rule RandomRule()
    {
        isRuleInverted = (rnd.Next(2) == 0);
        RuleManager.Verbtype verb = GetRandomEnum<RuleManager.Verbtype>();
        RuleManager.NounType noun = VerbToNoun[verb][rnd.Next(VerbToNoun[verb].Count)];
        RuleManager.AdjectiveType adjective = NounToAdjective[noun][rnd.Next(NounToAdjective[noun].Count)];
        return new Rule(verb, noun, adjective, isRuleInverted);
    }

    void SetRule(Rule newRule)
    {
        currentRule = newRule;
    }

    string RuleToString(Rule rule)
    {
        string retString = (isRuleInverted) ? ("Don't "): "";;

        switch(rule.GetVerb())
        {
            case Verbtype.Bring:
                retString += "Bring ";
                break;

            case Verbtype.Grab:
                retString += "Grab ";
                break;

            case Verbtype.Kick:
                retString += "Kick ";
                break;

            case Verbtype.Tag:
                retString += "Stun ";
                break;
        }

        AdjectiveType adjectiveType = rule.GetAdjective();
        switch(rule.GetNoun())
        {
            case NounType.Ball:
                {
                    retString += "the ";
                    switch (adjectiveType)
                    {
                        case AdjectiveType.Blue:
                            retString += "blue ";
                            break;

                        case AdjectiveType.Red:
                            retString += "red ";
                            break;

                        case AdjectiveType.White:
                            retString += "white ";
                            break;
                    }
                }
                break;

            case NounType.Player:
                {
                    retString += "player ";
                    switch (adjectiveType)
                    {
                        case AdjectiveType.One:
                            retString += "1";
                            break;

                        case AdjectiveType.Two:
                            retString += "2";
                            break;

                        case AdjectiveType.Three:
                            retString += "3";
                            break;

                        case AdjectiveType.Four:
                            retString += "4";
                            break;
                    }
                }
                break;

            case NounType.Switch:
                break;
        }

        return retString;
    }

    private static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
}
