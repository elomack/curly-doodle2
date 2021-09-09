using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public GameObject player;
    private PlayerStats playerStats;
    private Text levelText;

    private int currentLevel;

    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        levelText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        currentLevel = playerStats.level;
        levelText.text = playerStats.level.ToString();
    }
}
