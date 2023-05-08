using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 10;
    public float maxhealth = 10f;
    private float health;
    public Image healthbar;
    void Start()
    {
        setHealth();
    }
    public void updateHealth(int waveNumber)
    {
        maxhealth += maxhealth*waveNumber/5;
        setHealth();
    }
    void setHealth()
    {
        health = maxhealth;
        healthbar.fillAmount = health/maxhealth;
    }
    public void dealDamage(float dealtdamage)
    {
        health -= dealtdamage;
        healthbar.fillAmount = health/maxhealth;
        if(health <= 0f)
        {
            GameHub.TotalEnemies++;
            Destroy(gameObject);
            Spawning.EnemiesAlive --;
        }
    }
}
