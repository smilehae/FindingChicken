using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteCollision : MonoBehaviour
{

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 13)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("chicken"))
        {
            gameManager.chickenNum++;
            if (gameManager.chickenNum % 5 == 0) {
                Debug.Log("levelUp!");

                gameManager.speed *= 1.2f;
            }
            Destroy(coll.gameObject);
            Destroy(gameObject);
            gameManager.chickenNum++;
            gameManager.isChickenChanged = true;
        }
    }
}
