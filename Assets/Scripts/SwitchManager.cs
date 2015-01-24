using UnityEngine;
using System.Collections;

public class SwitchManager : MonoBehaviour {

    ArrayList switches;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void AddSwitch(GameObject gameSwitch)
    {
        switches.Add(gameSwitch);
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
