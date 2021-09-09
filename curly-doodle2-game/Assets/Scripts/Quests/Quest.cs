using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isCompleted;
    public string title;
    public string description;
    public int goldReward;
    public int experienceReward;
    public QuestGoal goal;

    public void Complete()
    {
        if (goal.IsReached() && isActive)
        {
            isActive = false;
            isCompleted = true;
            Debug.Log(title + " was completed!");
            PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            playerStats.AddExperience(experienceReward);
            playerStats.AddGold(goldReward);
        }
    }
}
