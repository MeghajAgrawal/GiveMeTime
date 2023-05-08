using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHub : MonoBehaviour
{
    public static GameHub instance;

    // Start is called before the first frame update
    public int playerHealth;
    public int steamRes;
    public int ironRes;

    public static int TotalEnemies = 0;
    //UI
    public TextMeshProUGUI steamText;
    public TextMeshProUGUI ironText;
    public TextMeshProUGUI heartsText;

    void Awake()
    {
        if(instance != null){
            Debug.LogError("Attempt to create multiple GameHubs");
            return;
        }
        instance = this;
    }

    void Start()
    {
        steamText.text = steamRes.ToString();
        ironText.text = ironRes.ToString();
        heartsText.text = playerHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        steamText.text = steamRes.ToString();
        ironText.text = ironRes.ToString();
        heartsText.text = playerHealth.ToString();
        if(Input.GetKey(KeyCode.Escape))
        {
            gameObject.GetComponent<UIManager>().Pause();
        }

    }

    public void updateHealth(int damage)
    {
        playerHealth -= damage;
        Spawning.increaseWave = false;
        if(playerHealth <= 0)
        {
            print("GAME OVER");
            gameObject.GetComponent<UIManager>().GameOver();
        }
    }

    public bool checkResources(int steam, int iron)
    {
        return steamRes >= steam && ironRes >= iron;
    }

    public void updateResources(int steam, int iron)
    {
        print("Updating Resources by " + steam + " steam and " + iron + " iron");
        steamRes -= steam;
        ironRes -= iron;
        Shop.instance.UpdateUIButtons();
    }

    public void addSteam(int steam)
    {
        updateResources(-steam,0);
    }

    public void addIron(int iron)
    {
        updateResources(0,-iron);
    }

}
