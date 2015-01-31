using UnityEngine;
using System.Collections;
using System;

public class Switch : MonoBehaviour, Noun 
{

    public enum SwitchType
    {
        None,
        Grab,
        Kick,
        Tag,
        Invert,
        BlueBall,
        RedBall,
        WhiteBall,
        Opponents
    }

    public Verbtype verbType;
    public NounType nounType;
    public AdjectiveType adjectiveType = AdjectiveType.None;
    public Animator switchAnimator;

    public SwitchType switchType = SwitchType.None;

    RuleManager.Rule switchRule;
    private bool isActivated = false;

    bool needsToSpawn = false;
    float spawnTime = 2f;
    float spawnTimeCounter = 0f;

	void Awake()
    {
        switchAnimator = gameObject.GetComponent<Animator>();
    }

    void Update() 
    {
        if (needsToSpawn)
        {
            if (spawnTimeCounter >= spawnTime)
            {
                Spawn();
            }
            spawnTimeCounter += Time.deltaTime;
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("Grab1"))
        {
            if(isActivated == false && other.tag == "CanInteract")
            {
                isActivated = true;
                Debug.Log("Switch Activated");
            }
        }
    }

    public void Init()
    {
        Spawn();
    }

    public AdjectiveType Adjective
    {
        get { return AdjectiveType.None; }
        set { }
    }

    public void Spawn()
    {
        do
        {
            switchType = RuleManager.GetRandomEnum<SwitchType>();
        }
        while(switchType == SwitchType.None);

        gameObject.name = switchType.ToString() + "Switch";

        float randX = UnityEngine.Random.Range(-2.6f, 2.6f);
        float randY = UnityEngine.Random.Range(-1.0f, 2.14f);
        this.gameObject.transform.position = new Vector3(randX, randY, 0.0f);

        gameObject.SetActive(true);
        switchAnimator.SetInteger("state", (int)switchType);
        needsToSpawn = false;
    }

    public void Reset()
    {
        needsToSpawn = true;
        spawnTimeCounter = 0f;
        gameObject.transform.position = new Vector3(10000,10000,1);
    }

	private void Activate()
	{
		switch (switchType)
		{
			
		case SwitchType.Grab:
			RuleManager.instance.ChangeVerb(Verbtype.Grab);
			break;
			
		case SwitchType.Kick:
			RuleManager.instance.ChangeVerb(Verbtype.Kick);
			break;
			
		case SwitchType.Tag:
			RuleManager.instance.ChangeVerb(Verbtype.Tag);
			break;
			
		case SwitchType.Invert:
			RuleManager.instance.InvertRule();
			break;
			
		case SwitchType.BlueBall:
			RuleManager.instance.ChangeNoun(NounType.Ball, AdjectiveType.Blue);
			break;
			
		case SwitchType.RedBall:
			RuleManager.instance.ChangeNoun(NounType.Ball, AdjectiveType.Red);
			break;
			
		case SwitchType.WhiteBall:
			RuleManager.instance.ChangeNoun(NounType.Ball, AdjectiveType.White);
			break;
			
		case SwitchType.Opponents:
			RuleManager.instance.ChangeNoun(NounType.Opponents, AdjectiveType.None);
			break;
			
		}
		Reset();
	}

    public void Kicked(int player, Vector3 direction)
    {
		Activate ();
    }

    public void Tagged(int player) 
	{
		Activate ();
	}
    public void Grabbed(int player) 
	{
		Activate ();
	}
}
