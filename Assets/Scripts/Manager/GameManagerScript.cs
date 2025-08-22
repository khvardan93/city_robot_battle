using System.Collections.Generic;
using RobotBattle.Robot;
using UnityEngine;
using RobotBattle.UI.GameMenu;

public class GameManagerScript 
{
    public bool testMode = true;
    public RobotType testRobotType = RobotType.Weaver;
    public List<RobotAchievementScript> robotAchievmentList;

    private GameData CachedGameData;
    public GameData GameData 
    {
        get
        {
            if (!CachedGameData)
            {
                CachedGameData = Resources.Load<GameData>("GameData");
            }

            return CachedGameData;
        }
    } 

    
    public RobotType currentRobotType
    {
        get
        {
            return testMode ? testRobotType : (RobotType)PlayerPrefs.GetInt("current_robot_type", (int)RobotType.BlackPheonix);
        }
        set
        {
            PlayerPrefs.SetInt("current_robot_type", (int)value);
        }
    }

    #region SETTINGS
    public bool isMuted
    {
        get
        {
            return PlayerPrefs.GetInt("is_muted", 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("is_muted", value ? 1 : 0);
        }
    }

    public bool isNoMusic
    {
        get
        {
            return PlayerPrefs.GetInt("is_music", 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("is_music", value ? 1 : 0);
        }
    }

    public float _sensitivity = -1;
    public float sensitivity
    {
        get
        {
            if(_sensitivity == -1)
            {
                _sensitivity = PlayerPrefs.GetFloat("sensitivity", 0.5f);
            }
            return _sensitivity;
        }
        set
        {
            _sensitivity = Mathf.Clamp(value, 0, 1);
            PlayerPrefs.SetFloat("sensitivity", _sensitivity);
        }
    }

    public string playerName
    {
        get
        {
            return PlayerPrefs.GetString("player_name", "Player");
        }
        set
        {
            PlayerPrefs.SetString("player_name", value);
        }
    }
    #endregion

    public Vector3 cameraTarget = Vector3.zero;
    public PlayerBehaviour currentPlayer;

    public static Transform robotInFireArea;

    private static GameManagerScript _instance;
    public static GameManagerScript Instance
    {
        get
        {
            if (_instance == null) new GameManagerScript();
            return _instance;
        }
    }

    private GameManagerScript()
    {
        _instance = this;
        robotAchievmentList = new List<RobotAchievementScript>();
    }

    private void doWinAction()
    {
        GameMenuScript.Instance.openWinPage();
    }

    public void doLoseAction()
    {
        GameMenuScript.Instance.openLosePage();
    }

    public void checkGameStatus()
    {
        int team1 = 0, team2 = 0;

        foreach (var item in robotAchievmentList)
        {
            if (item.isDead)
            {
                /*if (item.behaviourScript.team == Team.Team1)
                {
                    team1++;
                }
                else
                {
                    team2++;
                }*/
            }
        }

        if(team1 == 4)
        {
            doLoseAction();
        }

        if (team2 == 4)
        {
            doWinAction();
        }
    }

    public List<RobotAchievementScript> getResults(Team team)
    {
        List<RobotAchievementScript> itemList = new List<RobotAchievementScript>(robotAchievmentList);
        List<RobotAchievementScript> orderedList = new List<RobotAchievementScript>();

        int maxKillCount = 0;
        int currentIndex = 0;

        for (int i = 0; i < 4; i++)
        {
            maxKillCount = 0;

            for (int j = 0; j < itemList.Count; j++)
            {
                /*if (itemList[j].behaviourScript.team == team && maxKillCount <= itemList[j].killedCount)
                {
                    maxKillCount = itemList[j].killedCount;
                    currentIndex = j;
                }*/
            }

            orderedList.Add(itemList[currentIndex]);
            itemList.RemoveAt(currentIndex);
        }
        return orderedList;
    }

    public List<int> orderDamage(List<RobotAchievementScript> list)
    {
        List<RobotAchievementScript> itemList = new List<RobotAchievementScript>(list);
        List<int> orderedList = new List<int>();

        float maxDamage = 0;
        int currentIndex = 0;

        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < itemList.Count; j++)
            {
                if (!orderedList.Contains(j) && maxDamage <= itemList[j].damage)
                {
                    maxDamage = itemList[j].damage;
                    currentIndex = j;
                }
            }
            orderedList.Add(currentIndex);
            itemList.RemoveAt(currentIndex);
        }

        return orderedList;
    }

    public Transform getClosestEnemyTransform(Vector3 position)
    {
        Transform enemyTransform = null;
        float distance = float.MaxValue;

        foreach (var item in robotAchievmentList)
        {
            /*if (!item.isDead && item.behaviourScript.team != team && Vector3.Distance(position, item.behaviourScript.transform.position) <= distance)
            {
                distance = Vector3.Distance(position, item.behaviourScript.transform.position);
                enemyTransform = item.behaviourScript.GetPosition();
            }*/
        }

        return enemyTransform;
    }
}