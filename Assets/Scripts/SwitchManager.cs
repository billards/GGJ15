using UnityEngine;
using System.Collections;

public class SwitchManager : MonoBehaviour 
{

    ArrayList switches;

	// Use this for initialization
	void Start () 
    {
        switches = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
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
