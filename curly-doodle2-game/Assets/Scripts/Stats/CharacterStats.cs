﻿using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth { get; private set; }

    public bool isDead = false;
    
    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(float healthToAdd)
    {
        currentHealth += healthToAdd;
        Mathf.Clamp(currentHealth, 0, currentHealth);
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
        isDead = true;
    }
}
