using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour 
{
    RuleManager ruleManager;
    RuleManager.Rule switchRule;
    private bool isActivated = false;

	// Use this for initialization
	void Start () 
    {
        ruleManager = Camera.main.GetComponent<RuleManager>() as RuleManager;

        if (ruleManager != null)
            ruleManager.AddSwitch(this);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isActivated)
        {
            if (ruleManager != null)
                ruleManager.Remove(this);
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

    void SetRule(RuleManager.Rule newRule)
    {
        switchRule = newRule; 
    }

}
