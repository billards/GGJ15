using UnityEngine;
using System;
using System.Collections;

public class SwitchManager : MonoBehaviour 
{

    ArrayList switches;
    public int MAX_SWITCHES = 0;    // The maximum number of switches used that can be in game at any one time
    public GameObject switchPrefab;

	// Use this for initialization
	void Start () 
    {
        UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;
        switches = new ArrayList();

        Switch OnOffSwitch = (Instantiate(switchPrefab) as GameObject).GetComponent<Switch>();
        OnOffSwitch.gameObject.name = "OnOffSwitch";
        OnOffSwitch.Init(Switch.SwitchType.Invert);

        Switch NounSwitch = (Instantiate(switchPrefab) as GameObject).GetComponent<Switch>();
        NounSwitch.gameObject.name = "NounSwitch";
        NounSwitch.Init(Switch.SwitchType.Noun);

        Switch VerbSwitch = (Instantiate(switchPrefab) as GameObject).GetComponent<Switch>();
        VerbSwitch.gameObject.name = "VerbSwitch";
        VerbSwitch.Init(Switch.SwitchType.Verb);

        AddSwitch(OnOffSwitch);
        AddSwitch(NounSwitch);
        AddSwitch(VerbSwitch);
	}
	
	// Update is called once per frame
	void Update () 
    {


        //if(switches.Count < MAX_SWITCHES)
        //{
        //    // Generating random numbers within bounds of game area
        //    float randX = UnityEngine.Random.Range(-2.96f, 3.06f);
        //    float randY = UnityEngine.Random.Range(-1.13f, 3.14f);

        //    GameObject gameSwitch = Instantiate(switchPrefab, new Vector3(randX, randY, 0.0f), Quaternion.identity) as GameObject;
        //    AddSwitch(gameSwitch.GetComponent<Switch>() as Switch);

        //    RuleManager.Verbtype verb = GetRandomEnum<RuleManager.Verbtype>();
        //    RuleManager.NounType noun = GetRandomEnum<RuleManager.NounType>();
        //    RuleManager.AdjectiveType adjective = GetRandomEnum<RuleManager.AdjectiveType>();

        //    Debug.Log(verb + " " + noun + " " + adjective);

        //    if (gameSwitch != null)
        //        gameSwitch.GetComponent<Switch>().SetRule(new RuleManager.Rule(verb, noun, adjective.ToString()));
        //}
	}

    private static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public void AddSwitch(Switch gameSwitch)
    {
        if(gameSwitch == null)
            return;

        switches.Add(gameSwitch);
        Debug.Log(gameSwitch + " Added");
        Debug.Log("ArrayCount: " + switches.Count);
    }
    
    public void Remove(Switch gameSwitch)
    {
        Switch toDestroy;
        toDestroy = switches[switches.IndexOf(gameSwitch)] as Switch;
        switches.Remove(gameSwitch);

        for (int i = 0; i < switches.Count; ++i)
        {
            if (i.Equals(gameSwitch))
                switches.Remove(i);

            Destroy(gameSwitch.gameObject);
            Debug.Log(i + " Removed");
        }
    }
}
