using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteAll : MonoBehaviour
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
        if (transform.position.x > 13) {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("chicken"))
        {
            if (gameManager.chickenNum % 5 == 0)
            {
                Debug.Log("levelUp!");
                gameManager.speed *= 1.2f;
            }
            gameManager.totalChicken++;
            Destroy(coll.gameObject);
            gameManager.chickenNum++;
            gameManager.isChickenChanged = true;
        }
    }
}
