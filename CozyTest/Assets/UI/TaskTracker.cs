using System;
using GameEvents.Manager;
using TMPro;
using UnityEngine;

public class TaskTracker : MonoBehaviour
{
    [SerializeField] private GameObject trashUIContainer;
    [SerializeField] private TMP_Text trashText;
    private int trashCount;
    private int totalTrashCount;

    private void Start()
    {
        trashCount = 0;
    }

    private void OnEnable()
    {
        GameEventManager.AddListener<TaskStarted_Event>(OnTaskStarted);
        GameEventManager.AddListener<OneTrashCollected_Event>(OnTrashCollected);
        GameEventManager.AddListener<TaskFinished_Event>(OnTaskFinished);
    }

    private void OnDisable()
    {
        GameEventManager.RemoveListener<TaskStarted_Event>(OnTaskStarted);
        GameEventManager.RemoveListener<OneTrashCollected_Event>(OnTrashCollected);
        GameEventManager.RemoveListener<TaskFinished_Event>(OnTaskFinished);
    }

    //start task
    private void OnTaskStarted(TaskStarted_Event e)
    {
        totalTrashCount = e.totalTrash;
        trashUIContainer.SetActive(true);
        UpdateText();
    }

    private void OnTrashCollected(OneTrashCollected_Event e)
    {
        trashCount++;
        UpdateText();
    }

    private void OnTaskFinished(TaskFinished_Event e)
    {
        trashUIContainer.SetActive(false);
    }

    private void UpdateText()
    {
        trashText.text = "Trash collected: " + trashCount + "/" + totalTrashCount;
    }
}
