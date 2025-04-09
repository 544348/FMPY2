using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;

public class tasks : MonoBehaviour
{

    public Dictionary<string, bool> taskList = new Dictionary<string, bool>();
    private int currentTask = 0;
    public string[] theTasks;
    public TextMeshProUGUI text;
    public Color notComplete;
    public Color isComplete;

    void Start()
    {
        taskList.Add(theTasks[currentTask], false);
        text.text = "currentTask: " + theTasks[currentTask];
        text.color = notComplete;
    }

    public IEnumerator UpdateTaskList()
    {
        text.color = isComplete;

        yield return new WaitForSeconds(1.5f);

        text.text = "currentTask: " + theTasks[currentTask];
        text.color = notComplete;
    }

    public void CompleteTask(string taskName)
    {
        if (taskList[taskName] == true)
        {
            Debug.Log("task has already been completed");
            return;
        }
        taskList[taskName] = true;
        Debug.Log("the task is " + taskList[taskName]);
        if (theTasks.Length - 1 != currentTask)
        {
            currentTask++;
            taskList.Add(theTasks[currentTask], false);
            StartCoroutine(UpdateTaskList());
        }
        else
        {
            text.color = isComplete;
            text.text = "all tasks are complete!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
