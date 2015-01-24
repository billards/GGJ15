using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour 
{
    SwitchManager switchManager;
    RuleManager.Rule switchRule;
    private bool isActivated = false;

	// Use this for initialization
	void Start () 
    {
        switchManager = Camera.main.GetComponent<SwitchManager>() as SwitchManager;

        if (switchManager != null)
            switchManager.AddSwitch(this);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isActivated)
        {
            if (switchManager != null)
                switchManager.Remove(this);
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

    public void SetRule(RuleManager.Rule newRule)
    {
        switchRule = newRule;
    }
}
