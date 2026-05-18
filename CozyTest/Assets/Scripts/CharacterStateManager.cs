using System;
using UnityEngine;
using UnityEngine.XR;
using Yarn.Unity;

public class CharacterStateManager : MonoBehaviour
{
    public static CharacterStateManager Instance;
    public GameObject caren;
    public GameObject skipper;
    private NPCInteraction carenScript;
    private NPCInteraction skipperScript;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (caren != null)
        {
            carenScript = caren.GetComponentInChildren<NPCInteraction>();
            skipperScript = skipper.GetComponentInChildren<NPCInteraction>();
        }
    }

    [YarnCommand("CharacterChangeState")]
    public void CharacterChangeState(string passengerName, string state)
    {
        // Convert the state string from Yarn into Enum_NPCState
        if (!Enum.TryParse(state, true, out Enum_NPCState parsedState))
        {
            Debug.LogError($"State '{state}' is not a valid Enum_NPCState!");
            return;
        }
        
        switch (passengerName)
        {
            case "Caren":
                carenScript.NPCState = parsedState;
                break;
            case "Skipper":
                skipperScript.NPCState = parsedState;
                break;
            default:
                Debug.Log("No character like that exists, maybe needs to be added here.");
                break;
        }
    }
}
