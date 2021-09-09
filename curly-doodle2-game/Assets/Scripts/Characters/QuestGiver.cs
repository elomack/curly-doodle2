using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : Interactable
{
    public Quest quest;
    public GameObject player;
    public GameObject questWindow;
    public Text titleText;
    public GameObject layoverText;
    public Text descriptionText;
    public Text goldText;
    public Text experienceText;
    private PlayerQuests playerQuests;
    public GameObject questMark;

    private void Start()
    {
        playerQuests = player.GetComponent<PlayerQuests>();
    }

    private void Update()
    {
        if (quest.isCompleted)
        {
            playerQuests.quests.Remove(quest);
        }

        if (quest.isCompleted || quest.isActive)
        {
            questMark.SetActive(false);
        }
    }

    public override void Interact()
    {
        base.Interact();
        FaceTarget();
        OpenQuestWindow();
    }
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        if (quest.isCompleted)
        {
            layoverText.SetActive(true);
            titleText.text = null;
            descriptionText.text = "Howdy!";
            experienceText.text = null;
            goldText.text = null;
        }
        else
        {
            titleText.text = quest.title;
            descriptionText.text = quest.description;
            experienceText.text = quest.experienceReward.ToString();
            goldText.text = quest.goldReward.ToString();
        }
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

    public void AcceptQuest()
    {
        if (playerQuests.quests.Exists(x => x.title == quest.title))
        {
            Debug.Log("you are already on quest: " + quest.title);
        }
        else if (quest.isCompleted)
        {
            Debug.Log("you have already completed the quest: " + quest.title);
        }
        else
        {
            questWindow.SetActive(false);
            quest.isActive = true;
            playerQuests.quests.Add(quest);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 50f);
    }
}
