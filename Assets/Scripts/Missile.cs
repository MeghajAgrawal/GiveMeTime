using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float explosionRadius = 0f;
    public GameObject impact;
    
    void Start()
    {
        GameObject effect = Instantiate(impact, transform.position, Quaternion.identity);
        Destroy(effect,3f);
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Destroy(collider.gameObject);
                Spawning.EnemiesAlive--;
            }
        }
    }
}
