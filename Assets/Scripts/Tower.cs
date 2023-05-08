using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerDownHandler
{
    public int steam;
    public int iron;

    public int upgradeSteamCost;
    public int upgradeIronCost;

    public int destroySteamCost;
    public int destroyIronCost;

    public GameObject destroyAnimation;

    private void Start()
    {
        AddPhysics2DRaycaster();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var buttons = transform.Find("UpgradePop/Buttons").gameObject;
        buttons.SetActive(!buttons.activeSelf);
        if(buttons.activeSelf)
        {
            BuildManager.instance.SetTurretToBuild(null);
        }
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void UpgradeTurret(){
        // Check if player has enough resources
        if (GameHub.instance.checkResources(upgradeSteamCost, upgradeIronCost))
        {
            // Remove resources
            GameHub.instance.updateResources(upgradeSteamCost, upgradeIronCost);
            // Upgrade turret
            gameObject.GetComponent<ModifiableTurret>().UpgradeTurret();
        }
        else {
            UIManager.fadeIn = true;
            Debug.Log("Not enough resources");
        }
    }

    public void DestroyTurret(){
        GridController.removeListposition(transform.position);
        GameObject anim = Instantiate(destroyAnimation, transform.position, Quaternion.identity);
        Destroy(anim, 2f);
        GameHub.instance.updateResources(-destroySteamCost, -destroyIronCost);
        gameObject.GetComponent<ModifiableTurret>().DestroyTurret();
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

}
