using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ModifiableTurret
{
    private Transform target;
    private int level = 0;
    private SpriteRenderer spriteRenderer;
    [Header("Attributes")]
    public float range = 1.5f;
    public float fireRate = 1f;
    public float fireCountdown = 4f;

    [Header("No touchy")]
    public string enemyTag = "Enemy";
    public GameObject startpoint;
    public float turnSpeed = 5f;
    public Transform pointOfRotation;
    public GameObject Bullet;
    public Transform FirePoint;
    public GameObject renderer;
    public Sprite level1Sprite;
    public Sprite level2Sprite;


    public override void UpgradeTurret()
    {   
        if(level == 2) {
            print("Max level reached");
            int steamRes = gameObject.GetComponent<Tower>().upgradeSteamCost;
            int ironRes = gameObject.GetComponent<Tower>().upgradeIronCost;
            GameHub.instance.updateResources(-steamRes, -ironRes);
            return;
        }
        if (level == 0)
        {
            level++;
            range = range * 2;
            fireRate = fireRate * 2;
            spriteRenderer.sprite = level1Sprite;
        }
        else if (level == 1)
        {
            level++;
            range = range * 2;
            fireRate = fireRate * 2;
            spriteRenderer.sprite = level2Sprite;
        }
        print("Turret Upgraded to level " + level);
        
    }

    public override void DestroyTurret()
    {
        Destroy(gameObject);
        print("Turret Destroyed");
    }
   
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = renderer.GetComponent<SpriteRenderer>();
        startpoint = GameObject.FindGameObjectWithTag("Start");
        Vector3 dir = startpoint.transform.position - pointOfRotation.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime*turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f,0f, rotation.z);
        InvokeRepeating("UpdateTarget", 0f,0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position,enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
         if (target==null)
            return;
        Vector3 mylocation = pointOfRotation.position;
        Vector3 targetlocation = target.position;
        targetlocation.z = mylocation.z;
        Vector3 dir = targetlocation - mylocation;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0,0,45) * dir;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward,rotatedVectorToTarget);
        if(Vector3.Dot(Vector3.forward,dir) < 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, -turnSpeed * Time.deltaTime);
        }
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime*turnSpeed).eulerAngles;
        //transform.rotation = Quaternion.Euler(0f,0f, rotation.z);
        
        if (fireCountdown <= 0f && Vector3.Distance(mylocation,targetlocation) < 1)
        {
            Shoot();
            fireCountdown = 1f/fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGo = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.seek(target);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,range);
    }
    
}
