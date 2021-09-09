using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Transform target;
    

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    private void Update()
    {

    }
}
