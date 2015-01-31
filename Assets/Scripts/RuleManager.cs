using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Verbtype
{
    Kick,
    Grab,
    Tag
}

public enum NounType
{
    Ball,
    Player,
    Switch,
    Opponents
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

public class RuleManager : MonoBehaviour 
{

    public static RuleManager instance;

    public bool teamMode = false;
    public UIInterface uiInterface;
	public GameObject BGMusic;

    public class Rule
    {
        public Rule()
        {
            verbTarget = Verbtype.Kick;
            nounTarget = NounType.Ball;
            adjective = AdjectiveType.White;
        }

        public Rule(Verbtype verb, NounType noun, AdjectiveType adjective)
        {
            verbTarget = verb;
            nounTarget = noun;
            this.adjective = adjective;
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

        public bool Equals(Rule other)
        {
            if(this.verbTarget == other.verbTarget)
            {
				if (this.nounTarget == other.nounTarget)
                {
					if (this.adjective == other.adjective)
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
	private List<string> playerNames; 
    Rule currentRule;             // The current rule the game is operating off of
    GameStats gameStats;          // For updating scores
    const float MAX_TIME = 5.0f; // Maximum time a rule can be in play
    float timer = 0.0f;           // A timer counting to MAX_TIME
    System.Random rnd = new System.Random();
    bool isRuleInverted = false;

    Dictionary<Verbtype, List<NounType>> VerbToNoun = new Dictionary<Verbtype, List<NounType>>();
    Dictionary<NounType, List<Verbtype>> NounToVerb = new Dictionary<NounType, List<Verbtype>>();
    Dictionary<NounType, List<AdjectiveType>> NounToAdjective = new Dictionary<NounType, List<AdjectiveType>>();
    void Awake()
    {
        VerbToNoun[Verbtype.Kick] = new List<NounType>() { NounType.Ball, NounType.Player, NounType.Opponents };
        VerbToNoun[Verbtype.Grab] = new List<NounType>() { NounType.Ball, NounType.Player, NounType.Opponents };
        VerbToNoun[Verbtype.Tag] = new List<NounType>() { NounType.Player, NounType.Opponents };

        NounToVerb[NounType.Ball] = new List<Verbtype>() { Verbtype.Grab, Verbtype.Kick };
        NounToVerb[NounType.Player] = new List<Verbtype>() { Verbtype.Grab, Verbtype.Kick, Verbtype.Tag };
        NounToVerb[NounType.Opponents] = new List<Verbtype>() { Verbtype.Grab, Verbtype.Kick, Verbtype.Tag };


        NounToAdjective[NounType.Ball] = new List<AdjectiveType>() { AdjectiveType.Blue, AdjectiveType.Red, AdjectiveType.White };
        NounToAdjective[NounType.Player] = new List<AdjectiveType>() { AdjectiveType.One, AdjectiveType.Two, AdjectiveType.Three, AdjectiveType.Four };
        NounToAdjective[NounType.Opponents] = new List<AdjectiveType>() { AdjectiveType.None };
        NounToAdjective[NounType.Switch] = new List<AdjectiveType>() { AdjectiveType.None };
    }

	// Use this for initialization
	void Start () 
    {
        instance = this;
        gameStats = Camera.main.GetComponent<GameStats>() as GameStats;
        currentRule = null;

		GameObject teamBoolHolder = GameObject.Find("TeamBoolHolder");
		if (teamBoolHolder != null)
			teamMode = teamBoolHolder.GetComponent<TeamBoolHolder>().IsTeamGame;
		else
			teamMode = false;

        uiInterface.SetMode(teamMode);
		// start music if its not already playing
		if (GameObject.Find("BGMusic") == null)
			GameObject.Instantiate(BGMusic);

		playerNames = new List<string>();
		foreach (GameObject player in players)
		{
			playerNames.Add(player.GetComponent<Player>().Name);
		}
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (engineStarted)
        {
            if (timer < MAX_TIME)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SetRule(RandomRule());
            }
            uiInterface.UpdateInterface(UIInterface.InterfaceElement.Timer, (int)((MAX_TIME - timer) + 0.5));
        }
	}

    bool engineStarted = false;
    public void StartEngine(NounType newNoun)
    {
        Verbtype newVerb = GetRandomEnum<Verbtype>();

        while (!VerbToNoun[newVerb].Contains(newNoun))
        {
            newVerb = GetRandomEnum<Verbtype>();
        }

        AdjectiveType newAdjective = NounToAdjective[newNoun][rnd.Next(NounToAdjective[newNoun].Count)];
        SetRule(new Rule(newVerb, newNoun, newAdjective));

        engineStarted = true;
    }

    public void CheckRule(Player player, Verbtype checkVerb, NounType checkNoun, AdjectiveType checkAdjective)
    {
        CheckRule(player.Number, checkVerb, checkNoun, checkAdjective);
    }

    public void CheckRule(int player, Verbtype checkVerb, NounType checkNoun, AdjectiveType checkAdjective)
    {
        CheckRule(player, new Rule(checkVerb, checkNoun, checkAdjective));
    }

    public void CheckRule(int player, Rule rule)
    {
        if (currentRule == null) return;
        int addedScore = 1;
        if (isRuleInverted) addedScore = -1;

        if (currentRule.GetNoun() == NounType.Opponents && rule.GetNoun() == NounType.Player)
        {
            if (teamMode)
            {
                switch(player)
                {
                    case 1:
                    case 2:
                        if (rule.GetAdjective() != AdjectiveType.One && rule.GetAdjective() != AdjectiveType.Two)
                        {
                            gameStats.AddScore(player, addedScore);
                            SetRule(RandomRule());
                        }
                        break;
                        
                    case 3:
                    case 4:
                        if (rule.GetAdjective() != AdjectiveType.Three && rule.GetAdjective() != AdjectiveType.Four)
                        {
                            gameStats.AddScore(player, addedScore);
                            SetRule(RandomRule());
                        }
                        break;
                }
            }
            else
            {
                switch(player)
                {
                    case 1:
                        if (rule.GetAdjective() != AdjectiveType.One)
                        {
                            gameStats.AddScore(player, addedScore);
                            SetRule(RandomRule());
                        }
                        break;
                    case 2:
                        if (rule.GetAdjective() != AdjectiveType.Two)
                        {
                            gameStats.AddScore(player, addedScore);
                            SetRule(RandomRule());
                        }
                        break;
                    case 3:
                        if (rule.GetAdjective() != AdjectiveType.Three)
                        {
                            gameStats.AddScore(player, addedScore);
                            SetRule(RandomRule());
                        }
                        break;
                    case 4:
                        if (rule.GetAdjective() != AdjectiveType.Four)
                        {
                            gameStats.AddScore(player, addedScore);
                            SetRule(RandomRule());
                        }
                        break;
                }
            }
        }
        else
        {
            if (currentRule.Equals(rule) && gameStats != null)
            {
                gameStats.AddScore(player, addedScore);
                SetRule(RandomRule());
            }
        }
    }

    public void ChangeVerb()
    {
        Verbtype newVerb = GetRandomEnum<Verbtype>();
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
        SetRule(new Rule(newVerb, newNoun, newAdjective));
    }

    public void ChangeVerb(Verbtype newVerb)
    {
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
        SetRule(new Rule(newVerb, newNoun, newAdjective));
    }

    public void ChangeNoun(NounType nounType, AdjectiveType adjectiveType)
    {
        while (!NounToAdjective[nounType].Contains(adjectiveType))
        {
            adjectiveType = NounToAdjective[nounType][rnd.Next(rnd.Next(NounToAdjective[nounType].Count))];
        }

        Verbtype newVerb;
        do
        {
            newVerb = NounToVerb[nounType][rnd.Next(rnd.Next(NounToVerb[nounType].Count))];
        }
        while(!NounToVerb[nounType].Contains(newVerb));

        SetRule(new Rule(newVerb, nounType, adjectiveType));
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
        SetRule(new Rule(newVerb, newNoun, newAdjective));
    }

    public void InvertRule()
    {
		isRuleInverted = !isRuleInverted;
		uiInterface.UpdateRule(RuleToString(currentRule));
    }

    public Rule RandomRule()
    {
		int val = rnd.Next(3); // 1 in 3 chance to get 'not' as it's not as fun
		isRuleInverted = (val == 0);
		print (isRuleInverted + " " + val);
        Verbtype verb = GetRandomEnum<Verbtype>();
        NounType noun = VerbToNoun[verb][rnd.Next(VerbToNoun[verb].Count)];
        AdjectiveType adjective = NounToAdjective[noun][rnd.Next(NounToAdjective[noun].Count)];
        return new Rule(verb, noun, adjective);
    }

    void SetRule(Rule newRule)
    {
        timer = 0.0f;
        currentRule = newRule;
        uiInterface.UpdateRule(RuleToString(currentRule));
    }

    string RuleToString(Rule rule)
    {
        string retString = (isRuleInverted) ? ("Don't "): "";;

        switch(rule.GetVerb())
        {
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
                    retString += "ball";
                }
                break;

            case NounType.Player:
                {
                    //retString += "player ";
                    switch (adjectiveType)
                    {
                        case AdjectiveType.One:
                            //retString += "1";
							retString += playerNames[0];
                            break;

                        case AdjectiveType.Two:
                            //retString += "2";
							retString += playerNames[1];
                            break;

                        case AdjectiveType.Three:
                            //retString += "3";
							retString += playerNames[2];
                            break;

                        case AdjectiveType.Four:
                            //retString += "4";
							retString += playerNames[3];
                            break;
                    }
                }
                break;

            case NounType.Opponents:
                {
                    retString += "your opponents";
                }
                break;
        }

        return retString;
    }

    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
}
