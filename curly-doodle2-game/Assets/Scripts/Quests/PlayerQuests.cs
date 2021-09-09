using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerQuests : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    
    private void Start()
    {
        quests.Clear();
    }

    private void Update()
    {
        quests.ForEach(x => x.Complete());
        /*quests.ForEach(delegate (Quest x)
        {
            if (x.isCompleted == true)
            {
                quests.Remove(x);
            }
        });*/
    }
}
