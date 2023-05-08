using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGenerator : ModifiableTurret
{
    public float ironDelta;
    private float ironGenTime = 0f;

    void Start()
    {
        ironGenTime = Time.time + ironDelta;
    }

    public override void UpgradeTurret()
    {
        ironDelta = Mathf.Max(ironDelta--,3f);
        print("Iron Generator Upgraded");
    }

    public override void DestroyTurret()
    {
        Destroy(gameObject);
        print("Iron Generator Destroyed");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= ironGenTime)
        {
            GameObject.Find("GameController").GetComponent<GameHub>().addIron(1);
            ironGenTime = Time.time + ironDelta;
        }
    }
}
