using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawning : MonoBehaviour
{
    // Start is called before the first frame update
    public static int EnemiesAlive = 0;
    public static bool increaseWave = true;
    public Wave[] waves;
    public Transform spawnPoint;
    private float timeBetweenWaves = 20f;
    private float countdown = 5f;
    private int waveNumber = 0;

    public TextMeshProUGUI WaveText;

    void Start()
    {
        WaveText.text = "Wave " + (waveNumber+1) + " in " + countdown + " Secs";
        EnemiesAlive = 0;
        waveNumber = 0;
        countdown = 15f;
        timeBetweenWaves = 20f;
    }
    void Update()
    {
        if(EnemiesAlive > 0)
        {
            DisableClick(false);
            WaveText.text = "Enemies Alive :" + EnemiesAlive;
            return; 
        }
        if(waveNumber == waves.Length)
        {
            UIManager.winCondition = true;
            this.enabled = false;
        }
        if(!increaseWave && waveNumber != 0)
        {
            waveNumber--;
            increaseWave = true;
            timeBetweenWaves +=5;
        }
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            SetTimeBetweenWaves();
            return;
        }
        DisableClick(true);
        countdown -= Time.deltaTime;
        WaveText.text = "Wave " + (waveNumber+1) + " in " + string.Format("{0:#}", countdown) + " Secs";
       
    }

    void DisableClick(bool flag)
    {
        GridController.Click = flag;
        Shop.instance.UpdateUIButtons();
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveNumber]; 
        
        for (int i = 0; i < wave.count; i++)
        {
            int random = Random.Range(0,11);
            if(random < 11 && random > 8)
            {
                SpawnEnemy(wave.Witchenemy);
            }
            else if(random <=8  && random > 5)
            {
                SpawnEnemy(wave.Slimeenemy);
            }
            else
            {
                SpawnEnemy(wave.Ghoulenemy);
            }
            yield return new WaitForSeconds(1/wave.rate);
        } 
        
        waveNumber++;
    }
    void SetTimeBetweenWaves()
    {
        if(timeBetweenWaves > 15)
        {
            timeBetweenWaves -= 5; 
        }
    }
    void SpawnEnemy(GameObject enemy)
    {
        GameObject enemyspawn = Instantiate(enemy, spawnPoint.position,spawnPoint.rotation);
        enemyspawn.GetComponent<Enemy>().updateHealth(waveNumber+1);
        EnemiesAlive++;
    }
}
