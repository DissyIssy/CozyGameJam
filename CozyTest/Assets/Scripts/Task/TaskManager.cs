using System;
using GameEvents.Manager;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    //How to build this out to be multiple tasks?
    //Clean this up a bit
    [SerializeField] private bool debugMode;
    public static TaskManager Instance { get; private set; }
    public SO_TaskInfo taskInfo;

    [Header("CleanUp Attributes")]
    public int numberOfCleanUps_Total;
    public int numberOfCleanUps_Current;

    public bool taskStarted;
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameEventManager.AddListener<OneTrashCollected_Event>(OnOneTrashCollected);
    }

    private void OnDisable()
    {
        GameEventManager.RemoveListener<OneTrashCollected_Event>(OnOneTrashCollected);
    }

    public void StartTask()
    {
        //Called by NpcInteraction
        DebugMode.Log(this,"You started the task!",debugMode);
        
        //Debug.Log("You started the task!");
        taskStarted = true;
        numberOfCleanUps_Total = taskInfo.numberOfCleanUps;
        numberOfCleanUps_Current = 0;
        
        //Fires task started event
        TaskStarted_Event e = new TaskStarted_Event();
        e.totalTrash = numberOfCleanUps_Total;
        GameEventManager.Raise(e);
    }
    
    public void RegisterClean()
    {
        numberOfCleanUps_Current++;
        DebugMode.Log(this, $"You have cleaned {numberOfCleanUps_Current} items, out ot {numberOfCleanUps_Total}", debugMode);

        if (numberOfCleanUps_Current == numberOfCleanUps_Total)
        {
            EndTask();
        }
    }

    public void EndTask()
    {
        DebugMode.Log(this, "You finished the task!", debugMode);
        taskStarted = false;
        
        //Fires task finished event
        TaskFinished_Event e = new TaskFinished_Event();
        GameEventManager.Raise(e);
    }

    void OnOneTrashCollected(OneTrashCollected_Event e)
    {
        RegisterClean();
    }
}
