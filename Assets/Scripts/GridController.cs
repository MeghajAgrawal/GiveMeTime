using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour, IPointerEnterHandler
{
    public static bool Click = true;
    public Tilemap tilemap;
    public int xSize;
    public GameObject buildEffect;
    
    private TileBase currentTile;
    private GameObject gameController;
    private static List<Vector3> points = new List<Vector3>();
    private BuildManager buildManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Pointer entered");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentTile = null;
        gameController = GameObject.Find("GameController");
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject turret = buildManager.GetObjectToBuild();
        if (Input.GetMouseButtonDown(0) && turret!=null)
        { 
            Vector3Int spawn = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            currentTile = tilemap.GetTile(spawn);
            if (currentTile != null){
                var spawnPoint = tilemap.GetCellCenterWorld(tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                if(buildManager.powerupSelected && !Click && checkPowerupSpawn(spawnPoint, currentTile, turret)){
                    Instantiate(turret, spawnPoint, Quaternion.identity);
                }
                else if(Click && !buildManager.powerupSelected && checktilespawn(spawnPoint,currentTile,turret))
                {
                    Instantiate(turret, spawnPoint, Quaternion.identity);
                    GameObject effect = (GameObject)Instantiate(buildEffect, spawnPoint, Quaternion.identity);
                    Destroy(effect, 5f);
                    points.Add(spawnPoint);
                }
            }
        }
    }

    bool checkPowerupSpawn(Vector3 spawnPoint, TileBase currentTile, GameObject turret)
    {
        int steamRes = turret.GetComponent<Powerup>().steam;
        int ironRes = turret.GetComponent<Powerup>().iron;
        if(gameController.GetComponent<GameHub>().checkResources(steamRes,ironRes) && 
            (currentTile.name == "RoadTile1" || currentTile.name == "RoadTile3"))
        {
            gameController.GetComponent<GameHub>().updateResources(steamRes,ironRes);
            return true;
        }
        return false;
    }

    bool checktilespawn(Vector3 spawnPoint, TileBase current, GameObject turret)
    {
        int steamRes = turret.GetComponent<Tower>().steam;
        int ironRes = turret.GetComponent<Tower>().iron;
        if(points.Contains(spawnPoint) || gameController.GetComponent<GameHub>().checkResources(steamRes,ironRes) == false)
        {
            print("not enough resources");
            UIManager.fadeIn = true;
            return false;
        }
        switch(current.name)
        {
            case "grass":
            case "GrassNew":
                if(turret.name == "Outpost" || turret.name == "Cannon")
                {
                    gameController.GetComponent<GameHub>().updateResources(steamRes,ironRes);
                    return true;
                }
                else
                    return false;
            case "water":
                if(turret.name == "Steam")
                {
                    gameController.GetComponent<GameHub>().updateResources(steamRes,ironRes);
                    return true;
                }
                else
                    return false;
            case "rock":
                if(turret.name == "Miner")
                {
                    gameController.GetComponent<GameHub>().updateResources(steamRes,ironRes);
                    return true;
                }
                else
                    return false;
            default:
                return false;
        }
    }

    /* When ADDING Destroying code remember to 
    remove the spawnpoint position from the list */
    public static void removeListposition(Vector3 position)
    {
        if(points.Contains(position))
        {
            points.Remove(position);
        }
    }
}
