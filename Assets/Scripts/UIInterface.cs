using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIInterface : MonoBehaviour 
{
    public enum InterfaceElement
    {
        Timer,
        P1Score,
        P2Score,
        P3Score,
        P4Score,
        T1Score,
        T2Score
    }

    // Canvas elements
    public Text interfaceTimer;
    public Text team1Text;
    public Text team2Text;
    public Text player1Text;
    public Text player2Text;
    public Text player3Text;
    public Text player4Text;
    public Text RuleChangeText;
    // States
    bool teamMode = true;

	// Use this for initialization
	void Start () 
    {
        UpdateInterface(InterfaceElement.Timer, 0);

        UpdateInterface(InterfaceElement.P1Score, 0);
        UpdateInterface(InterfaceElement.P2Score, 0);
        UpdateInterface(InterfaceElement.P3Score, 0);
        UpdateInterface(InterfaceElement.P4Score, 0);

        UpdateInterface(InterfaceElement.T1Score, 0);
        UpdateInterface(InterfaceElement.T2Score, 0);
	}

    public void UpdateRule(string ruleString)
    {
        RuleChangeText.text = ruleString;
    }

    public void UpdateInterface(InterfaceElement toUpdate, int value)
    {
        switch(toUpdate)
        {
            case InterfaceElement.Timer:
                {
                    interfaceTimer.text = value.ToString();
                    break;
                }
            case InterfaceElement.P1Score:
                {
                    player1Text.text = value.ToString();
                    break;
                }
            case InterfaceElement.P2Score:
                {
                    player2Text.text = value.ToString();
                    break;
                }
            case InterfaceElement.P3Score:
                {
                    player3Text.text = value.ToString();
                    break;
                }
            case InterfaceElement.P4Score:
                {
                    player4Text.text = value.ToString();
                    break;
                }
            case InterfaceElement.T1Score:
                {
                    team1Text.text = value.ToString();
                    break;
                }
            case InterfaceElement.T2Score:
                {
                    team2Text.text = value.ToString();
                    break;
                }
            default:
                {
                    Debug.Log("ERROR - Invalid interface element!");
                    break;
                }
        }
    }

    //Using bool for now maybe enum in the future
    public void SetMode(bool teamMode)
    {
        this.teamMode = teamMode;
        if(teamMode)
            GameObject.Find("TeamUI").SetActive(true);
        else
            GameObject.Find("SoloUI").SetActive(true);
    }
}
