using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    void Awake()
    {
        if(instance != null){
            //Debug.LogError("Attempt to create multiple BuildManagers");
            return;
        }
        instance = this;
    }

    void Start()
    {
        UpdateUIButtons(); // Update the UI buttons to reflect the current state of the game
    }

    public void UpdateUIButtons()
    {
        //print("Shop.UpdateUIButtons()");
        foreach(Transform child in this.transform)
        {
            //print(child.tag);
            var button = (Button) child.GetComponent<Button>();
            //print(button.interactable);
            if(child.tag == "Tower")
            {
                int steam = child.Find("RefObject").gameObject.GetComponent<Tower>().steam;
                int iron = child.Find("RefObject").gameObject.GetComponent<Tower>().iron;
                button.interactable = GridController.Click && GameHub.instance.checkResources(steam, iron);
            }
            else if(child.tag == "Powerup")
            {
                int steam = child.Find("RefObject").gameObject.GetComponent<Powerup>().steam;
                int iron = child.Find("RefObject").gameObject.GetComponent<Powerup>().iron;
                button.interactable = !GridController.Click && GameHub.instance.checkResources(steam, iron);
            }
        }
    }

    public void SelectOutpost()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.outpostTurret);
    }

    public void SelectCannon()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.cannonTurret);
    }

    public void SelectSteamMachine()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.steamMachine);
    }

    public void SelectIronMiner()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.ironMiner);
    } 

    public void SelectBlockade()
    {
        BuildManager.instance.SetPowerupToBuild(BuildManager.instance.blockade);
    }

    public void SelectMissile()
    {
        BuildManager.instance.SetPowerupToBuild(BuildManager.instance.missile);
    }
}
