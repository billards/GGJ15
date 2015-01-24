using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    private bool isActivated = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isActivated)
            Remove();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // Activate new Rule

        isActivated = true;
        Debug.Log("Switch Activated");
    }

    private void Remove()
    {
        DestroyObject(this.gameObject);

        //Visual effects for switch removal
    }
    
}
