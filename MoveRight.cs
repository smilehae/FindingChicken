using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{

    private GameManager gameManager;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = gameManager.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
