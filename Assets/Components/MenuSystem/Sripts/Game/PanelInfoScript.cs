using UnityEngine;
using UnityEngine.UI;

public class PanelInfoScript : MonoBehaviour
{
    public Text speedText;
    public Text ammoText;
    public Text enemyText;
    public Text moneyText;

    public ProgressBarScript healthProgress;
    public ProgressBarScript armoryProgress;

    public static float speed = 0;
    public static string ammoBar = "";
    public static string timeBar = "";
    public static string enemyBar = "";

    /// <summary>
    /// [0 - 150] after 100 the armory
    /// </summary>
    public static float healthBar = 0;
    public static float maxHealth = 100f;

    public float speedMultiplier = 3f;

    private void Start()
    {
       /* if (GameManager.instance.getCurrentLevel().gameType == Level.GameType.OnlineArena)
        {
            moneyText.text = "0$";
        }
        else
        {
            moneyText.text = "";
        }

        InvokeRepeating("frequentUpdate", 0.1f, 0.1f);
        InvokeRepeating("rareUpdate", 1f, 1f);*/
    }

    void frequentUpdate()
    {
        //this.speedText.text = Mathf.RoundToInt((GameSettings.getSpeedUnits() == GameSettings.speedUnits.KMH ? speed * 1.6f : speed) * speedMultiplier).ToString();

        this.ammoText.text = ammoBar;
        this.enemyText.text = enemyBar;

        this.armoryProgress.setProgressStatus(healthBar <= 2f * maxHealth / 3f ? 0 : Mathf.Clamp(100f * (healthBar - 2f * maxHealth / 3f) / (maxHealth / 3f), 0, 100f));
        this.healthProgress.setProgressStatus(Mathf.Clamp(healthBar >= 2f * maxHealth / 3f ? 100f : 100f * healthBar / (2f * maxHealth / 3f), 0, 100f));
    }

    void rareUpdate()
    {
        /*if(GameManager.gameTypeHandler != null)
        {
            int earnedMoney = GameManager.gameTypeHandler.currentKillsCount * GameSettings.onlineArenaRewardPerKill;
            moneyText.text = earnedMoney + "$";
        }*/
    }
}