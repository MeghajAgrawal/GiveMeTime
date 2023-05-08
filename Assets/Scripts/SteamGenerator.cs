using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamGenerator : ModifiableTurret
{
    public float steamDelta;
    private float steamGenTime;

    void Start()
    {
        steamGenTime = Time.time + steamDelta;
    }

    public override void UpgradeTurret()
    {
        steamDelta = Mathf.Max(steamDelta--,1f);
        print("Steam Generator Upgraded");
    }

    public override void DestroyTurret()
    {
        Destroy(gameObject);
        print("Steam Generator Destroyed");
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= steamGenTime)
        {
            GameObject.Find("GameController").GetComponent<GameHub>().addSteam(1);
            steamGenTime = Time.time + steamDelta;
        }
    }
}
