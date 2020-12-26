using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private MoveBackground moveBackscript;
    public GameObject[] Prefab;
    private Vector2 spawnPos = new Vector2(12.0f, 0.0f);
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
            InvokeRepeating("SpawnObstacle", 0.0f, 2.0f);
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void SpawnObstacle()
    {
        if (!gameManager.isGameOver && gameManager.canStart)
        {
            int index = Random.Range(0, Prefab.Length);
            if (index == 1)
            { //치킨인 경우 offset 설정
                spawnPos.y += 0.6f;
                Instantiate(Prefab[index], spawnPos, transform.rotation);
                spawnPos.y -= 0.6f;
            }
            else if (index == 2) {
                spawnPos.y += 2.2f;
                Instantiate(Prefab[index], spawnPos, transform.rotation);
                spawnPos.y -= 2.2f;
            }
            else
            {
                Instantiate(Prefab[index], spawnPos, transform.rotation);
            }
        }
    }
    //x = 12정도 위치에 생성

    

}
