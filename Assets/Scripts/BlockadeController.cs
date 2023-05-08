using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlockadeController : MonoBehaviour
{
    public int maxhealth = 10;
    public Image healthbar;

    private float health;
    private float timeTillEnd = Mathf.Infinity;
    
    void Start()
    {
        health = maxhealth;
        healthbar.fillAmount = health/maxhealth;
    }

    void Update()
    {
        if(health <= 0f || timeTillEnd <= Time.time)
        {
            Destroy(gameObject);
        }    
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            timeTillEnd = Time.time + 10f;
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Enemy")
        {
            health -= 1;
            healthbar.fillAmount = health/maxhealth;
        }
        else if(other.gameObject.tag == "Blockade")
        {
            Destroy(other.gameObject);
        }
    }
}
