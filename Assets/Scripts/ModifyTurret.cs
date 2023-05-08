using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyTurret : MonoBehaviour
{
    Quaternion iniRot;
    Vector3 iniPos;
    public TextMeshProUGUI upgradesteam;
    public TextMeshProUGUI upgradeiron;
    public TextMeshProUGUI destroysteam;
    public TextMeshProUGUI destroyiron;
    void Awake()
    {
        GameObject go = transform.parent.gameObject;
        upgradesteam.text = "-" + go.GetComponent<Tower>().upgradeSteamCost.ToString();
        upgradeiron.text =  "-" + go.GetComponent<Tower>().upgradeIronCost.ToString();
        destroysteam.text = "+" + go.GetComponent<Tower>().destroySteamCost.ToString();
        destroyiron.text =  "+" + go.GetComponent<Tower>().destroyIronCost.ToString();
        iniRot = transform.rotation;
        iniPos = transform.position;
    }

    void LateUpdate(){
        transform.rotation = iniRot;
        transform.position = iniPos;
    }

    public void UpgradeTurret()
    {
        print("Upgrade Turret Clicked");
        GameObject go = transform.parent.gameObject;
        go.GetComponent<Tower>().UpgradeTurret();
    }

    public void DestroyTurret()
    {
        print("Destroy Turret Clicked");
        GameObject go = transform.parent.gameObject;
        go.GetComponent<Tower>().DestroyTurret();
    }
}
