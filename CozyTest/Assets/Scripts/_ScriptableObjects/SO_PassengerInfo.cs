using UnityEngine;

[CreateAssetMenu(fileName = "New Passenger", menuName = "Game/Passenger")]
public class SO_PassengerInfo : ScriptableObject
{
    public string passengerName;
    public string yarnID_New;
    public string yarnID_TaskGiving;
    public string yarnID_Normal;
    public string yarnID_Off;
    public string yarnID_Gone;
}
