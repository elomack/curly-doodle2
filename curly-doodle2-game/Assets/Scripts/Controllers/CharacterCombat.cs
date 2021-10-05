using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;
    public Vector3 damageTextOffset = Vector3.zero;

    public event System.Action OnAttack;

    private CharacterStats myStats;
    private Animator anim;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (myStats.isDead || targetStats.isDead)
            return;

        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (anim != null)
            {
                anim.SetTrigger("Attack");
            }

            if (OnAttack != null)
            {
                OnAttack();
            }
            attackCooldown = 1 / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());


        if (anim != null)
        {
            anim.SetTrigger("GetHit");
        }
    }

}
