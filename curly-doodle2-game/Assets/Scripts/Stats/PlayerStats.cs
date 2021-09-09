using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{ 
    // resources
    public float energy = 100;
    public float healthRegen = 2.5f;

    // movement
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;

    // experience
    public int level = 1;
    public float currentExperience = 0;
    public float targetExperience = 1000;

    // currencies
    public float gold;

    public HealthBar healthBar;
    public ExperienceBar experienceBar;
    public GameObject experienceTextPrefab;

    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        healthBar.SetMaxHealth(maxHealth);
        InvokeRepeating("RegenerateHealth", 3f, 3f);

    }

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        healthBar.SetHealthBarText(currentHealth, maxHealth);
        experienceBar.SetExperience(currentExperience, targetExperience);
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }


    public void AddExperience(float expToAdd)
    {
        if ((currentExperience + expToAdd) % targetExperience != (currentExperience + expToAdd))
        {
            currentExperience = (currentExperience + expToAdd) % targetExperience;
            level++;
            Debug.Log("added " + expToAdd + " exp");
            Debug.Log("lvl up");
            targetExperience *= 1.4f;
        }
        else
        {
            currentExperience += expToAdd;
            Debug.Log("added " + expToAdd + " exp");
        }
        Vector3 experienceTextPosition = new Vector3(-3f, -0.25f, 0f) + transform.position;
        GameObject experienceTextObject = (GameObject)
                                Instantiate(experienceTextPrefab, experienceTextPosition, Quaternion.identity);
        experienceTextObject.transform.GetChild(0).GetComponent<TextMesh>().text =
                                                    "+ " + expToAdd.ToString() + " exp";
    }

    public void AddGold(float goldToAdd)
    {
        gold += goldToAdd;
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }

    public void RegenerateHealth()
    {
        AddHealth(healthRegen);
    }
}
