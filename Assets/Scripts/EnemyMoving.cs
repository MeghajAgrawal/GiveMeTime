using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{

    public float speed = 10f;
    private Transform target;
    private int waypointIndex = 1;

    private float oldSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.nodes[waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if(Vector3.Distance(transform.position,target.position) <= 0.1f)
        {
            UpdateWaypointIndex();
        }
    }
    void UpdateWaypointIndex()
    { 
        waypointIndex ++;
        target = Waypoints.nodes[waypointIndex];
    }

    // detect collision
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Blockade")
        {
            // transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z);
            oldSpeed = speed;
            speed = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Blockade")
        {
            speed = oldSpeed;
        }
    }
}
