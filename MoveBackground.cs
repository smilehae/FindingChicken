using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{

    Vector3 startPos;
    private float groundSpeed;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //처음 위치 담김
        startPos = transform.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        groundSpeed = gameManager.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver && gameManager.canStart)
        {
            transform.Translate(Vector2.left * Time.deltaTime * groundSpeed);
            if (transform.position.x < -12)
            {
                transform.position = startPos;
            }
        }
       
     }

    
}
