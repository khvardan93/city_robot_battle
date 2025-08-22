using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItemScript : MonoBehaviour
{
    [SerializeField] Image typeImage;
    [SerializeField] Sprite[] typeSprites;
    [Space]
    [SerializeField] Text rewardText;
    [SerializeField] Image currencyImage;
    [SerializeField] Sprite[] currencySprites;
    [Space]
    [SerializeField] Text descriptionText;
    [Space]
    [SerializeField] Text progressText;
    [SerializeField] Slider progressSlider;
    [Space]
    [SerializeField] GameObject claimButton;

    private ActiveTask activeTask;

    public void init(ActiveTask activeTask)
    {
        this.activeTask = activeTask;
        currencyImage.sprite = currencySprites[(int)activeTask.task.currency];
        rewardText.text = activeTask.task.reward.ToString();

        typeImage.sprite = typeSprites[(int)activeTask.task.currency];
        descriptionText.text = activeTask.task.description;

        var taskStatus = activeTask.getStatus();
        progressSlider.value = taskStatus.status / (float)activeTask.task.progress;
        progressText.text = taskStatus.status + "/" + activeTask.task.progress;
        claimButton.SetActive(taskStatus.isFinished && !taskStatus.isClaimed);

        gameObject.SetActive(true);
    }

    public void onClaim()
    {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
        activeTask.claim();
        claimButton.SetActive(false);
    }
}