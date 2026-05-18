using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "CozyEndtimes/New Task")]
public class SO_TaskInfo : ScriptableObject
{
    public string taskName;
    public Enum_TaskTypes taskType;
    
    [Header("CleanUp Settings")]
    public int numberOfCleanUps;

    [Header("FetchItem Settings")
    ]public string itemIDToFetch;
}
