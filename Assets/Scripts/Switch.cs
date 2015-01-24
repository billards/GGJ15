using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour 
{

    SwitchManager switchManager;
    RuleManager ruleManager;
    private bool isActivated = false;

    RuleManager.Rule switchRule;

	// Use this for initialization
	void Start () 
    {
        switchManager = Camera.main.GetComponent<SwitchManager>() as SwitchManager;
        ruleManager = Camera.main.GetComponent<RuleManager>() as RuleManager;

        if (switchManager != null)
            switchManager.AddSwitch(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isActivated)
        {
            if(switchManager != null)
                switchManager.Remove(this);
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        // Activate new Rule

        if(Input.GetButtonDown("Interact1"))
        {
            isActivated = true;
            Debug.Log("Switch Activated");
        }
    }

    void SetType(RuleManager.Rule newRule)
    {
        switchRule = newRule; 
    }

}
