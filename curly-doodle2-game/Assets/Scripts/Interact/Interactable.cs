using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
    // how close to be able to interact
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    bool hasInteracted = false;
    bool hasContributedToQuest;

    Transform playerTransform;

    public bool isQuestObjective = false;


    public virtual void Interact() { }

    public virtual void ContributeToQuest() { }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(playerTransform.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        this.playerTransform = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        playerTransform = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
