using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameItemScript : MonoBehaviour
{
    [SerializeField] Image robotImage;
    [SerializeField] Text killedCountText;
    [SerializeField] Text damageCountText;
    [SerializeField] Text earnedMoneyCountText;

    public void init(RobotAchievementScript robotAchievementScript, int killIndex, int damageIndex, float rewardMultiplayer = 1f)
    {
        robotImage.sprite = Resources.Load<Sprite>("RobotImages/" + robotAchievementScript.robotType);
        killedCountText.text = robotAchievementScript.killedCount.ToString();
        damageCountText.text = ((int)robotAchievementScript.damage).ToString();

        float reward = 0;
        if (robotAchievementScript.killedCount > 0) reward += GameManagerScript.Instance.GameData.rewards[killIndex] * rewardMultiplayer;
        if (robotAchievementScript.damage > 0 && damageIndex != -1) reward += GameManagerScript.Instance.GameData.rewards[damageIndex] * rewardMultiplayer;

        if (robotAchievementScript.isPlayer()) PlayerModelScript.instance.silver += (int)reward;

        earnedMoneyCountText.text = ((int)reward).ToString();
    }
}
