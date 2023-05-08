using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouch : MonoBehaviour
{
    private GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");
    }
    void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.tag == "Enemy")
        {
            int damage = Col.gameObject.GetComponent<Enemy>().damage;
            gameController.GetComponent<GameHub>().updateHealth(damage);
            Spawning.EnemiesAlive--;
            Destroy(Col.gameObject);
        }
    }
}
