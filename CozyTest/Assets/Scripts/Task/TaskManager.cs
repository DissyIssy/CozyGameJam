using System;
using System.Threading.Tasks;
using GameEvents.Manager;
using UnityEngine;
using UnityEngine.Serialization;

public class TaskManager : MonoBehaviour
{
    //How to build this out to be multiple tasks?
    //Clean this up a bit
    [SerializeField] private bool debugMode;
    public static TaskManager Instance { get; private set; }
    public SO_TaskInfo currentTask;

    [Header("CleanUp Attributes")]
    public int numberOfCleanUps_Total;
    public int numberOfCleanUps_Current;

    public bool taskStarted;
    
    
    //God object for all of the task, should be divided among tasks in later prototype
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

    public void StartTask(SO_PassengerInfo passenger)
    {
        if (passenger.task == null) return;
        if (taskStarted) 
        {
            DebugMode.Log(this, "Can't start a new task; a task is already active!", debugMode);
            return;
        }

        currentTask = passenger.task;
        switch (currentTask.taskType)
        {
            case Enum_TaskTypes.CleanUp:
                StartCleanUpTask();
                break;
            case Enum_TaskTypes.FetchItem:
                StartFetchItemTask();
                break;
        }
        
    }

    public void StartCleanUpTask()
    {
        //Called by NpcInteraction
        DebugMode.Log(this,"You started the CleanUp Task!",debugMode);
        
        //Debug.Log("You started the task!");
        taskStarted = true;
        numberOfCleanUps_Total = currentTask.numberOfCleanUps;
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

    public void StartFetchItemTask()
    {
        taskStarted = true;
        DebugMode.Log(this, "You started the FetchItemTask",debugMode);
    }

    public void EndTask()
    {
        DebugMode.Log(this, "You finished the task!", debugMode);
        taskStarted = false;
        
        //CleanUpTask
        numberOfCleanUps_Total = 0;
        numberOfCleanUps_Current = 0;
        
        //Fires task finished event
        TaskFinished_Event e = new TaskFinished_Event();
        GameEventManager.Raise(e);
    }

    void OnOneTrashCollected(OneTrashCollected_Event e)
    {
        RegisterClean();
    }
}
