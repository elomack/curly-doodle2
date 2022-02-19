using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    public List<string> relatedQuests = new List<string>();

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }

    public override void ContributeToQuest()
    {
        base.ContributeToQuest();
        PlayerQuests playerQuestList = playerManager.player.GetComponent<PlayerQuests>();


        if (playerQuestList != null)
        {
            relatedQuests.ForEach(delegate(string questOnEnemy)
            {
                playerQuestList.quests.ForEach(delegate (Quest questOnPlayer)
                {
                    if (questOnEnemy.Equals(questOnPlayer.title))
                    {
                        //Debug.Log("wspólny quest: " + questOnEnemy);
                        questOnPlayer.goal.EnemyKilled();
                        questOnPlayer.goal.ItemCollected();
                    }
                });
            });
        }
        
    }
}
