using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    public GameObject outpostTurret;
    public GameObject cannonTurret;
    public GameObject steamMachine;
    public GameObject ironMiner;
    public GameObject blockade;
    public GameObject missile;

    public bool powerupSelected = false;

    private GameObject selectedTurret;
    void Awake()
    {
        if(instance != null){
            Debug.LogError("Attempt to create multiple BuildManagers");
            return;
        }
        instance = this;
    }

    public GameObject GetObjectToBuild()
    {
        return selectedTurret;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        powerupSelected = false;
        selectedTurret = turret;
    }

    public void SetPowerupToBuild(GameObject powerup)
    {
        powerupSelected = true;
        selectedTurret = powerup;
    }
}
