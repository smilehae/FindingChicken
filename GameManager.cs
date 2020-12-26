using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int hunger = 8;
    public int life = 5;
    public float count = 0;
    public bool isGameOver = false;
    public bool isHungerChanged = false;
    public TextMeshProUGUI chickenText;
    public TextMeshProUGUI rasberryText;
    public TextMeshProUGUI hotchickenText;

    public TextMeshProUGUI totalFoodText;
    public TextMeshProUGUI totalObsText;
    public TextMeshProUGUI totalChickenText;
    public TextMeshProUGUI totalChestText;

    public GameObject story1;
    public GameObject story2;
    public GameObject story3;


    public TextMeshProUGUI woodText;
    public TextMeshProUGUI rockText;
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI barrierText;


    public bool isHeartChanged = false;
    public bool isRasberryChanged = false;

    public GameObject[] lifeImage;
    public GameObject[] hungerGraph;
    public GameObject gameOverBg;

    public float speed = 5.0f;

    public int chickenNum = 0;
    public int rasberryNum = 0;
    public int hotChickenNum = 0;

    public bool isChickenChanged = false;
    public bool isHotChickenChanged = false;

    public int woodNum;
    public int rockNum;
    public int ArrowNum; //wood 1 + rock1
    public int BarrierNum; // wood 2

    public bool isIngredChanged = false; //가진 재료수가 변경 

    public int totalFood = 0;
    public int totalObstacle = 0;
    public int totalChicken = 0;
    public int totalChest = 0;

    public bool canStart = false;

    public GameObject cover;

 

    public int story = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.O)) {
            cover.SetActive(false);

            canStart = true;

        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (story == 0)
            {
                story1.SetActive(true);
                story2.SetActive(true);
                story3.SetActive(true);

                story++;
            }
            else if (story == 1)
            {
                story1.SetActive(false);
                story++;
            }
            else if (story == 2)
            {
                story2.SetActive(false);
                story++;
            }
            else if (story == 3)
            {
                story3.SetActive(false);
                story++;
            }
            
        }
        if (canStart)
        {
            if (isChickenChanged)
            {

                chickenText.text = chickenNum.ToString();
                isChickenChanged = false;
            }

            if (isHotChickenChanged)
            {

                hotchickenText.text = hotChickenNum.ToString();
                isChickenChanged = false;
            }
            if (isIngredChanged)
            {

                //나무 2개로 배리어 1개
                if (woodNum >= 2)
                {
                    woodNum -= 2;
                    BarrierNum++;
                }

                // 돌 2개로 배리어 1개
                if (rockNum >= 2)
                {
                    rockNum -= 2;
                    BarrierNum++;
                }
                else if (woodNum > 0 && rockNum > 0)
                {
                    woodNum--;
                    rockNum--;
                    ArrowNum++;
                }

                woodText.text = woodNum.ToString();
                rockText.text = rockNum.ToString();
                arrowText.text = ArrowNum.ToString();
                barrierText.text = BarrierNum.ToString();

                isIngredChanged = false;


            }
            if (isGameOver)
            {
                totalChestText.text = totalChest.ToString();
                totalChickenText.text = totalChicken.ToString();
                totalFoodText.text = totalFood.ToString();
                totalObsText.text = totalObstacle.ToString();

                gameOverBg.SetActive(true);
            }


            if (isHeartChanged)
            {
                for (int i = 0; i < lifeImage.Length; i++)
                {
                    lifeImage[i].SetActive(false);
                }
                for (int i = 0; i < life; i++)
                {
                    lifeImage[i].SetActive(true);
                }
            }

            if (isHungerChanged)
            {
                for (int i = 0; i < hungerGraph.Length; i++)
                {
                    hungerGraph[i].SetActive(false);
                }
                for (int i = 0; i < hunger; i++)
                {
                    hungerGraph[i].SetActive(true);
                }
            }

            if (isRasberryChanged)
            {
                rasberryText.text = "" + rasberryNum;
            }




            count += Time.deltaTime;
            if (count > 5)
            {
                hunger--;
                if (hunger == 0)
                {
                    isGameOver = true;
                }
                isHungerChanged = true;
                count = 0;
            }

        }



    }

}
