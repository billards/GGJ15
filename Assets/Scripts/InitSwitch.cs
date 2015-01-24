using UnityEngine;
using System.Collections.Generic;

public class InitSwitch : MonoBehaviour, Noun
{
    public SwitchManager switchManager;

    public void Remove()
    {
        gameObject.SetActive(false);
    }

    public NounType nounType;
    public AdjectiveType adjectiveType;

    public AdjectiveType Adjective
    {
        get { return adjectiveType; }
        set { }
    }

    public void Kicked(int player, Vector3 direction)
    {
        switchManager.OnInitSwitchPress(nounType);
    }

	public void Tagged(int player)
    {

    }

	public void Grabbed(int player)
    {

    }


}