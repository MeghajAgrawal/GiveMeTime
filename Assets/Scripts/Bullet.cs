using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public float explosionRadius = 0f;
    public float speed = 7f;
    public float damage = 1f;
    public GameObject bulletImpact;
    public void seek (Transform _target)
    {
        target = _target;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;
        
        transform.Translate(dir.normalized * distance, Space.World);
        //transform.LookAt(target.transform);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            print("Collision Detected");
            HitTarget();
        }
    }
    void HitTarget()
    {
        GameObject effect = Instantiate(bulletImpact, transform.position, Quaternion.identity);
        Destroy(effect,2f);
        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    void Damage(Transform target)
    {
        if(target !=null)
        {
            target.gameObject.GetComponent<Enemy>().dealDamage(damage);
        }
    }

}
