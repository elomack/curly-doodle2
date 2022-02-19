using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float moveSpeed = 3f;
    public float distanceToTarget;
    public float distanceToOriginalPosition;
    CharacterCombat combat;
    Vector3 originalPosition;
    Quaternion originalRotation;

    Transform target;
    NavMeshAgent navMeshAgent;
    Animator anim;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        combat = GetComponent<CharacterCombat>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        distanceToOriginalPosition = Vector3.Distance(originalPosition, transform.position);

        if (distanceToOriginalPosition <= 2.5f)
        {
            anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
            transform.rotation = originalRotation;
        }

        if (distanceToTarget > lookRadius)
        {
            ReturnToOriginalPosition();
        }

        if (distanceToTarget <= lookRadius && distanceToTarget > 3f)
        {
            FaceTarget();
            MoveAnimation();
            navMeshAgent.SetDestination(target.position);
        }

        if (distanceToTarget < 3f)
        {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if (targetStats != null)
            {
                combat.Attack(targetStats);
            }
        }

        if (distanceToOriginalPosition > lookRadius * 3)
        {
            ReturnToOriginalPosition();
        }

    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void ReturnToOriginalPosition()
    {
        navMeshAgent.SetDestination(originalPosition);
    }

    private void MoveAnimation()
    {
        anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
