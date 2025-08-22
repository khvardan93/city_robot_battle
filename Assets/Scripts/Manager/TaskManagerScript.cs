using System;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTask
{
    public GameData.Task task;
    int index;

    public ActiveTask(GameData.Task task, int index)
    {
        this.task = task;
        this.index = index;
    }

    public void claim()
    {
        PlayerPrefs.SetInt("task_claimed_" + index, 1);
    }

    public (bool isFinished, int status,bool isClaimed) getStatus()
    {
        var data = (isFinished: true, status: 0, isClaimed: true);

        int status = 0;
        bool isFinished = false;

        switch (task.taskType)
        {
            case TaskType.DealDamage:
                status = (int)(task.hasRobot ? TaskManagerScript.instance.getDamageSize(task.robot) : TaskManagerScript.instance.getDamageSize());
                break;
            case TaskType.DestroyRobots:
                status = task.hasRobot ? TaskManagerScript.instance.getDestroyedRobotCount(task.robot) : TaskManagerScript.instance.getDestroyedRobotCount();
                break;
            case TaskType.Die:
                status = TaskManagerScript.instance.dieCount;
                break;
        }

        return (isFinished, status, PlayerPrefs.GetInt("task_claimed_" + index, 0) == 1);
    }
}

public class TaskManagerScript
{
    #region SINGLETON
    private static TaskManagerScript _instance;
    public static TaskManagerScript instance
    {
        get {
            if (_instance == null) _instance = new TaskManagerScript();
            return _instance;
        }
    }

    private TaskManagerScript()  { }
    #endregion

    const int ACTIVE_TASK_COUNT = 5;

    private int currentDay {
        set
        {
            PlayerPrefs.SetInt("current_day", value);
        }
        get
        {
            return PlayerPrefs.GetInt("current_day", 0);
        }
    }

    public int dieCount
    {
        get
        {
            return PlayerPrefs.GetInt("player_die_count", 0);
        }
        set
        {
            PlayerPrefs.SetInt("player_die_count", Mathf.Clamp(value, 0, 999999999));
        }
    }

    #region ACTIVE TASKS
    private ActiveTask[] activeTasks;

    private int[] activeTaskIndexes
    {
        set
        {
            for (int i = 0; i < value.Length; i++)
            {
                PlayerPrefs.SetInt("actiev_task_index_" + i, value[i]);
            }
        }
        get
        {
            int[] indexes = new int[ACTIVE_TASK_COUNT];

            for (int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = PlayerPrefs.GetInt("actiev_task_index_" + i);
            }

            return indexes;
        }
    }

    public ActiveTask[] getActiveTasks()
    {
        if (activeTasks == null)
        {
            var activeIndexes = activeTaskIndexes;
            activeTasks = new ActiveTask[ACTIVE_TASK_COUNT];

            var tasks = GameManagerScript.Instance.GameData.tasks;
            
            for (int i = 0; i < activeTasks.Length; i++)
            {
                activeTasks[i] = new ActiveTask(tasks[activeIndexes[i]], i);
            }
        }

        return activeTasks;
    }

    public void resetActiveTasks()
    {
        if (currentDay == 0 || currentDay != DateTime.Now.Day)
        {
            activeTasks = null;
            currentDay = DateTime.Now.Day;
            resetDamageSize();
            resetDestroyedRobotCount();
            dieCount = 0;

            var idArray = new int[ACTIVE_TASK_COUNT];

            for (int i = 0; i < ACTIVE_TASK_COUNT; i++)
            {
                PlayerPrefs.SetInt("task_claimed_" + i, 0);

                if(i == ACTIVE_TASK_COUNT - 1) idArray[i] = UnityEngine.Random.Range(i * 3, 3 + i * 3);
                else idArray[i] = UnityEngine.Random.Range(i * 3, 2 + i * 3);
            }
            Debug.Log(idArray);
            activeTaskIndexes = idArray;
        }
    }
    #endregion

    #region DESTROYED ROBOT COUNT
    public void increaseDestroyedRobotCount(RobotType robotType)
    {
        var key = "destroyed_robot_count_" + robotType;
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key, 0) + 1);
    }

    public int getDestroyedRobotCount(RobotType robotType)
    {
        var key = "destroyed_robot_count_" + robotType;
        return PlayerPrefs.GetInt(key, 0);
    }

    public int getDestroyedRobotCount()
    {
        int count = 0;
        for(int i = 0; i < Enum.GetNames(typeof(RobotType)).Length; i++)
        {
            count += getDestroyedRobotCount((RobotType)i);
        }
        return count;
    }

    public void resetDestroyedRobotCount()
    {
        for (int i = 0; i < Enum.GetNames(typeof(RobotType)).Length; i++)
        {
            PlayerPrefs.SetInt("destroyed_robot_count_" + (RobotType)i, 0);
        }
    }
    #endregion

    #region DAMAGE
    public void increaseDamageSize(RobotType robotType, float damage)
    {
        var key = "damage_size_" + robotType;
        PlayerPrefs.SetFloat(key, PlayerPrefs.GetFloat(key, 0) + damage);
    }

    public float getDamageSize(RobotType robotType)
    {
        var key = "damage_size_" + robotType;
        return PlayerPrefs.GetFloat(key, 0);
    }

    public float getDamageSize()
    {
        float count = 0;
        for (int i = 0; i < Enum.GetNames(typeof(RobotType)).Length; i++)
        {
            count += getDamageSize((RobotType)i);
        }
        return count;
    }

    public void resetDamageSize()
    {
        for (int i = 0; i < Enum.GetNames(typeof(RobotType)).Length; i++)
        {
            PlayerPrefs.SetFloat("damage_size_" + (RobotType)i, 0);
        }
    }
    #endregion
}