using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip woodSound;
    public AudioClip rockSound;
    public AudioClip chestSound;
    public AudioClip kickSound; // 나무 밑동 쳐서 사과나옴
    public AudioClip throwLignt;
    public AudioClip throwWeight;
    public AudioClip attacked;
    public AudioClip eat;

    public float speed = 10.0f;
    public float XAxis;
    public float jumpspeed = 7.0f;
    private Rigidbody2D playerRb;
    public bool isGround = true;
    private Animator anim;


    public int health = 5;
    public int hunger = 5;

    private GameManager gameManager;

    public GameObject arrowPrefab;
    public GameObject barrierPrefab;

    private bool isInfoChecked = false;

    public GameObject UIPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.canStart)
        {
            
           

            //점프
            if (Input.GetKeyDown(KeyCode.Space) && isGround && !gameManager.isGameOver)
            {
                playerRb.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                isGround = false;
                anim.SetTrigger("Jump");
            }

            //a키 누르면 라즈베리 먹음 + 생명 : 최대값 (5)이면 작동 안함 + 배고픔 최대(8)이면 작동 안함
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (gameManager.rasberryNum > 0)
                {
                    gameManager.rasberryNum--;
                    playerAudio.PlayOneShot(eat, 1.0f);
                    gameManager.totalFood++;
                    if (gameManager.life < 5)
                    {
                        gameManager.life++;
                        gameManager.isHeartChanged = true;
                    }
                    if (gameManager.hunger < 8)
                    {
                        gameManager.hunger++;
                        gameManager.isHungerChanged = true;
                    }

                }
            }

            //z키 누르면 생닭먹음 : 체력 -1 배고픔 +1
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (gameManager.chickenNum > 0)
                {
                    gameManager.chickenNum--;
                    playerAudio.PlayOneShot(eat, 1.0f);
                    gameManager.totalFood++;
                    gameManager.isChickenChanged = true;
                    gameManager.life--;
                    gameManager.isHeartChanged = true;

                    if (gameManager.life == 0)
                    {
                        gameManager.isGameOver = true;
                    }
                    if (gameManager.hunger < 8)
                    {
                        gameManager.hunger++;
                        gameManager.isHungerChanged = true;
                    }
                }
            }


            //D키 누르면 화살?! 발사 (바로 앞 1명 죽임)
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (gameManager.ArrowNum > 0)
                {
                    gameManager.ArrowNum--;
                    playerAudio.PlayOneShot(throwLignt, 1.0f);
                    Instantiate(arrowPrefab, transform.position, transform.rotation);
                }
            }

            //F키 누르면 배리어 발사 (싹 죽임)
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (gameManager.BarrierNum > 0)
                {
                    gameManager.BarrierNum--;
                    playerAudio.PlayOneShot(throwWeight, 1.0f);
                    Instantiate(barrierPrefab, transform.position, transform.rotation);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gameManager.hotChickenNum > 0)
                {
                    gameManager.hotChickenNum--;
                    playerAudio.PlayOneShot(eat, 1.0f);
                    gameManager.isHotChickenChanged = true;
                    //체력, hunger 전부 full
                    gameManager.life = 5;
                    gameManager.hunger = 8;
                    gameManager.isHeartChanged = true;
                    gameManager.isHungerChanged = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!isInfoChecked)
                {
                    UIPrefab.SetActive(true);
                    isInfoChecked = true;
                }
                else
                {
                    UIPrefab.SetActive(false);
                    isInfoChecked = false;

                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (gameManager.canStart)
        {
            //1번 점프만 가능
            if (coll.gameObject.CompareTag("ground"))
            {
                isGround = true;
            }

            else if (coll.gameObject.CompareTag("item"))
            {
                //s키를 누른채로 아이템을 만나야 
                if (Input.GetKey(KeyCode.S))
                {
                    gameManager.totalChest++;
                    playerAudio.PlayOneShot(chestSound, 1.0f);
                    int index = Random.Range(0, 2);

                    //0: 라즈베리 1 :화살촉 2 : 배리어
                    if (index == 0)
                    {
                        gameManager.rasberryNum++;
                        gameManager.isRasberryChanged = true;
                    }
                    else if (index == 1)
                    {
                        gameManager.woodNum++;
                        gameManager.isIngredChanged = true;
                    }
                    else if (index == 2)
                    {
                        gameManager.rockNum++;
                        gameManager.isIngredChanged = true;
                    }

                    //나중에 시간되면 박스 열린거로 스프라이트 바꾸기
                    coll.gameObject.SetActive(false);

                }

            }

            //동물과 부딪히면 life 감소
            else if (coll.gameObject.CompareTag("chicken"))
            {
                coll.gameObject.SetActive(false);
                gameManager.life--;
                playerAudio.PlayOneShot(attacked, 1.0f);
                gameManager.isHeartChanged = true;
                Debug.Log("health : " + gameManager.life);
                if (gameManager.life == 0)
                {
                    gameManager.isGameOver = true;
                }
            }

            else if (coll.gameObject.CompareTag("campfire"))
            {
                if (Input.GetKey(KeyCode.S))
                {
                    if (gameManager.chickenNum > 0)
                    {
                        gameManager.chickenNum--;
                        playerAudio.PlayOneShot(kickSound, 1.0f);
                        gameManager.isChickenChanged = true;
                        gameManager.hotChickenNum++;
                        gameManager.isHotChickenChanged = true;
                    }
                    coll.gameObject.SetActive(false);
                }
            }

            //나무랑 만났을때 s 눌러서 밑동을 치면 사과를 얻는다.
            //나무랑 만났을때 E를 눌러서 벌목하면 나뭇가지를 얻는다.

            else if (coll.gameObject.CompareTag("tree"))
            {
                if (Input.GetKey(KeyCode.S))
                {
                    gameManager.totalObstacle++;
                    gameManager.rasberryNum++;
                    gameManager.isRasberryChanged = true;
                    coll.gameObject.SetActive(false);
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.L))
                {
                    gameManager.totalObstacle++;
                    gameManager.woodNum++;
                    playerAudio.PlayOneShot(woodSound, 1.0f);
                    gameManager.isIngredChanged = true;
                    coll.gameObject.SetActive(false);
                }
            }

            //돌을 q누르면 채광. 부딪히면 체력 감소.

            else if (coll.gameObject.CompareTag("rock"))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.K))
                {
                    gameManager.totalObstacle++;
                    gameManager.rockNum++;
                    playerAudio.PlayOneShot(rockSound, 1.0f);
                    gameManager.isIngredChanged = true;
                    coll.gameObject.SetActive(false);
                }
                else
                {
                    //피하거나 q누르지 않고 부딪히면 체력 감소
                    gameManager.life--;
                    playerAudio.PlayOneShot(attacked, 1.0f);
                    gameManager.isHeartChanged = true;
                    coll.gameObject.SetActive(false);
                }
            }
        }
    }
}
