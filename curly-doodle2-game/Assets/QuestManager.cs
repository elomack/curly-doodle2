using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton

    public static QuestManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    private PlayerQuests playerQuests;
    public Quest questToAccept;
    public GameObject questWindow;
    public GameObject layoverWindow;
    public GameObject currentQuestGiver;

    private void Start()
    {
        playerQuests = GetComponent<PlayerQuests>();
    }

    public void AcceptQuest()
    {
        currentQuestGiver.GetComponent<QuestGiver>().quest.isActive = true;
        Debug.Log("quest " + currentQuestGiver.GetComponent<QuestGiver>().quest.title + " is active");
        questToAccept = currentQuestGiver.GetComponent<QuestGiver>().quest;
        if (questToAccept != null)
        {
            playerQuests.quests.Add(questToAccept);
            CloseQuestWindow();
        }
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
        layoverWindow.SetActive(false);
        FindObjectOfType<AudioManager>().Play("QuestClose");
    }
}
