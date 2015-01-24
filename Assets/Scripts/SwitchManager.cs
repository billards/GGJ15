using UnityEngine;
using System;
using System.Collections;

public class SwitchManager : MonoBehaviour 
{

    ArrayList switches;
    public int MAX_SWITCHES = 3;    // The maximum number of switches used that can be in game at any one time
    public GameObject switchPrefab;

	// Use this for initialization
	void Start () 
    {
        UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;
        switches = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(switches.Count < MAX_SWITCHES)
        {
            // Generating random numbers within bounds of game area
            float randX = UnityEngine.Random.Range(-2.96f, 3.06f);
            float randY = UnityEngine.Random.Range(-1.13f, 3.14f);

            GameObject gameSwitch = Instantiate(switchPrefab, new Vector3(randX, randY, 0.0f), Quaternion.identity) as GameObject;
            
            if (gameSwitch != null)
                gameSwitch.GetComponent<Switch>().SetRule(new RuleManager.Rule(RuleManager.Verbtype.Bring, RuleManager.NounType.Ball, ""));

            //Keeping track of references to switches in current scene
            switches.Add(Instantiate(switchPrefab, new Vector2(randX, randY), Quaternion.identity));
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
}
