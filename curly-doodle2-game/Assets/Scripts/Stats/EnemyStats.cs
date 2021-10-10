using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStats
{
    public int level = 1;
    [SerializeField]private float expYield;
    private float deathDelay = 3f;

    public EnemyType enemyType;

    Animator anim;
    Enemy enemy;
    EnemyController controller;
    NavMeshAgent agent;
    Canvas characterCanvas;

    private PlayerStats playerStats;
    public HealthBar healthBar;

    private void Start()
    {
        expYield = 40 * level * 1.4f;
        anim = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<EnemyController>();
        characterCanvas = GetComponentInChildren<Canvas>();
        healthBar.SetMaxHealth(maxHealth);
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        healthBar.SetHealthBarText(currentHealth, maxHealth);
    }

    public override void Die()
    {
        base.Die();
        agent.enabled = false;
        controller.enabled = false;
        characterCanvas.enabled = false;
        anim.SetTrigger("Die");
        playerStats.AddExperience(expYield);
        if (enemy.isQuestObjective)
        {
            enemy.ContributeToQuest();
            Debug.Log(transform.name + " contributed to quest.");
        }
        StartCoroutine(DestroyObject(deathDelay));
    }

    IEnumerator DestroyObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (enemyType == EnemyType.Orc)
        {
            FindObjectOfType<AudioManager>().Play("OrcDamaged");
        }
        else if (enemyType == EnemyType.Golem)
        {
            FindObjectOfType<AudioManager>().Play("GolemDamaged");
        }
        
    }

    public enum EnemyType { Orc, Golem };

}
