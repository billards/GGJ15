using UnityEngine;
using System;
using System.Collections;

public class SwitchManager : MonoBehaviour 
{
    public GameObject switchPrefab;

    public InitSwitch initSwitch1;
    public InitSwitch initSwitch2;
    public InitSwitch initSwitch3;
    public InitSwitch initSwitch4;
    
	// Use this for initialization
    void Start()
    {
    }

    public void OnInitSwitchPress(NounType nounType)
    {
        RuleManager.instance.StartEngine(nounType);

        initSwitch1.Remove();
        initSwitch2.Remove();
        initSwitch3.Remove();
        initSwitch4.Remove();
        
        startSpawning = true;
    }

    float timeTillOnOffSwitchSpawn = 2.0f;
    bool OnOffSwitchSpawned = false;

    float timeTillNounSwitchSpawn = 3.0f;
    bool nounSwitchSpawned = false;

    float timeTillVerbSwitchSpawn = 4.0f;
    bool verbSwitchSpawned = false;

    float timeBuffer = 0f;
    bool startSpawning = false;

    void Update()
    {
        if (startSpawning)
        {
            if (timeTillOnOffSwitchSpawn <= timeBuffer && !OnOffSwitchSpawned)
            {
                SpawnSwitch();
				OnOffSwitchSpawned = true;
            }

            if (timeTillNounSwitchSpawn <= timeBuffer && !nounSwitchSpawned)
            {
                SpawnSwitch();
				nounSwitchSpawned = true;
            }

            if (timeTillVerbSwitchSpawn <= timeBuffer && !verbSwitchSpawned)
            {
                SpawnSwitch();
				verbSwitchSpawned = true;
            }

            timeBuffer += Time.deltaTime;
            if (OnOffSwitchSpawned && nounSwitchSpawned && verbSwitchSpawned) startSpawning = false;
        }
    }

    void SpawnSwitch()
    {
        Switch newSwitch = (Instantiate(switchPrefab) as GameObject).GetComponent<Switch>();
        newSwitch.Init();
    }

    private static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
}
