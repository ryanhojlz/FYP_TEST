using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawningPosition;
    public List<GameObject> spawnMonster;

    float SpawnTime = 3f;
    float timer;

    void Start()
    {
        timer = SpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //GameObject newObj = Instantiate(spawnMonster[0]) as GameObject;
        //newObj.transform.position = spawningPosition[0].transform.position;
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    GameObject newObj = Instantiate(spawnMonster[0]) as GameObject;
        //    newObj.transform.position = spawningPosition[1].transform.position;
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    GameObject newObj = Instantiate(spawnMonster[0]) as GameObject;
        //    newObj.transform.position = spawningPosition[2].transform.position;
        //}

        //int IndexSpawnPos = Random.Range(0, 3);
        //int IndexUnitToSpawn = Random.Range(0, spawnMonster.Count);

        //Debug.Log("Spawn_Position = " + IndexSpawnPos + " ," + "Spawn_Unit = " + IndexUnitToSpawn);

        if (timer <= 0f)
        {
            int IndexSpawnPos = Random.Range(0, 3);
            int IndexUnitToSpawn = Random.Range(0, spawnMonster.Count);

            //Spawn
            GameObject newObj = Instantiate(spawnMonster[IndexUnitToSpawn]) as GameObject;
            newObj.transform.position = spawningPosition[IndexSpawnPos].transform.position;

            timer = SpawnTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
