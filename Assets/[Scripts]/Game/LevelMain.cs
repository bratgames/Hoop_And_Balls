using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LevelMain : MonoBehaviour
{
    public GameObject[] Knifes, Circles, Buttons,BalloonSpawners;  
    public Sprite[] OpenSprite, CloseSprite;
    int knifeCount=1, circleUpgradeCount;
    public static int givingMoney = 1;
    [Header("Costs")]
    public int[] circleUpgradeCosts;
    public int[] newKnifeCosts;
    public int[] incomeMoneyAmounts;

    int circleUpgradeCost;
    int newKnifeCost;
    int incomeUpgradeCost;
    [Header("Max Counts")]
    public int maxIncomeCount;
    public int maxKnifeCount;
    public int maxCircleUpgrade;

    bool speedup;
    public static float knifespeed;
    public static float balloonSpeed;
    [Header("Knife Speed")]
    public float startKnifeSpeed;
    public float fastKnifeSpeed;
    public float fastSpeedTime;
    [Header("Balloon Speed")]
    public int startBalloonGrowSpeed;
    public int fastBalloonGrowSpeed;

    public Vector3[] knifeLengths;
    public GameObject canvas;
    public static bool gameStarted;
    bool nextlevel;
    float timer;
    #region Singleton
    public static LevelMain instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    void Start()
    {
        nextlevel = false;
        Application.targetFrameRate = 60;
        gameStarted = false;
        speedup = false;
        circleUpgradeCost = circleUpgradeCosts[circleUpgradeCount];
        Buttons[0].transform.GetComponentInChildren<Text>().text = circleUpgradeCost.ToString();

        incomeUpgradeCost = incomeMoneyAmounts[givingMoney - 1];
        Buttons[1].transform.GetComponentInChildren<Text>().text = incomeUpgradeCost.ToString();

        newKnifeCost = newKnifeCosts[newKnifeCost];
        Buttons[2].transform.GetComponentInChildren<Text>().text = newKnifeCost.ToString();


        EKTemplate.LevelManager.instance.startEvent.AddListener(() => StartEvent());
        knifespeed = startKnifeSpeed;
        balloonSpeed = startBalloonGrowSpeed;
        #region Prefabs
        if (PlayerPrefs.HasKey("level0CircleCount"))
        {
            circleUpgradeCount = PlayerPrefs.GetInt("level0CircleCount");
            if (circleUpgradeCount >= 6&&circleUpgradeCount<9)
            {
                Circles[0].SetActive(false);
                Circles[1].SetActive(true);
                Circles[2].SetActive(false);
                maxCircleUpgrade += 3;
                maxKnifeCount += 4;
                maxIncomeCount += 2;
                for (int i = 0; i < Knifes.Length; i++)
                {
                    Knifes[i].transform.DOScaleX(knifeLengths[0].x, 0f);
                }
            }
            else if (circleUpgradeCount >= 9)
            {
                fastBalloonGrowSpeed *= 10;
                Circles[0].SetActive(false);
                Circles[1].SetActive(false);
                Circles[2].SetActive(true);
                maxCircleUpgrade += 7;
                maxKnifeCount += 8;
                maxIncomeCount += 4;
                for (int i = 0; i < Knifes.Length; i++)
                {
                    Knifes[i].transform.DOScaleX(knifeLengths[1].x, 0f);
                }
            }
            for (int i = 0; i < circleUpgradeCount; i++)
            {
                BalloonSpawners[i].SetActive(true);

            }
            circleUpgradeCost = circleUpgradeCosts[circleUpgradeCount];
            Buttons[0].transform.GetComponentInChildren<Text>().text = circleUpgradeCost.ToString();
        }

        if (PlayerPrefs.HasKey("level0GivingMoney"))
        {
            givingMoney = PlayerPrefs.GetInt("level0GivingMoney");
            for (int i = 0; i < givingMoney; i++)
            {
                incomeUpgradeCost = incomeMoneyAmounts[i];
                Buttons[1].transform.GetComponentInChildren<Text>().text = incomeUpgradeCost.ToString();
            }
        }
        if (PlayerPrefs.HasKey("level0KnifeCount"))
        {
            knifeCount = PlayerPrefs.GetInt("level0KnifeCount");
            for (int i = 0; i < knifeCount; i++)
            {
                Knifes[i].SetActive(true);
                newKnifeCost = newKnifeCosts[i];
                Buttons[2].transform.GetComponentInChildren<Text>().text = newKnifeCost.ToString();
            }
        }
        #endregion
    }
    void StartEvent()
    {
        gameStarted = true;
    }
    void Update()
    {
        if (speedup == true)
        {
            if (timer <= 0)
            {
                speedup = false;
                knifespeed = startKnifeSpeed;
                balloonSpeed = startBalloonGrowSpeed;
                for (int i = 0; i < knifeCount; i++)
                {
                    Knifes[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                knifespeed = fastKnifeSpeed;
                balloonSpeed = fastBalloonGrowSpeed;
                for (int i = 0; i < knifeCount; i++)
                {
                    Knifes[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                timer -= Time.deltaTime;
            }
        }
        if (maxCircleUpgrade > circleUpgradeCount)
        {
            Buttons[0].transform.GetComponentInChildren<Text>().text = circleUpgradeCost.ToString();
            if (EKTemplate.GameManager.instance.money < circleUpgradeCost)
            {
                Buttons[0].GetComponent<Image>().overrideSprite = CloseSprite[0];
                Buttons[0].GetComponent<Button>().enabled = false;
            }
            else
            {
                Buttons[0].GetComponent<Image>().overrideSprite = OpenSprite[0];
                Buttons[0].GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            ButtonClose(0);
        }
        if (maxIncomeCount > givingMoney)
        {
            Buttons[1].transform.GetComponentInChildren<Text>().text = incomeUpgradeCost.ToString();
            if (EKTemplate.GameManager.instance.money < incomeUpgradeCost)
            {
                Buttons[1].GetComponent<Image>().overrideSprite = CloseSprite[1];
                Buttons[1].GetComponent<Button>().enabled = false;
            }
            else
            {
                Buttons[1].GetComponent<Image>().overrideSprite = OpenSprite[1];
                Buttons[1].GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            ButtonClose(1);
        }
        if (maxKnifeCount > knifeCount)
        {
            Buttons[2].transform.GetComponentInChildren<Text>().text = newKnifeCost.ToString();
            if (EKTemplate.GameManager.instance.money < newKnifeCost)
            {
                Buttons[2].GetComponent<Image>().overrideSprite = CloseSprite[2];
                Buttons[2].GetComponent<Button>().enabled = false;
            }
            else
            {
                Buttons[2].GetComponent<Image>().overrideSprite = OpenSprite[2];
                Buttons[2].GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            ButtonClose(2);
        }
        if (circleUpgradeCount == 13&&knifeCount == 16&&givingMoney == 8&&!nextlevel)
        {
            nextlevel = true;
            StartCoroutine(delay());
        }
    } 
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        Buttons[3].SetActive(true);

    }
    public void Clicker()
    {
        Haptic.LightTaptic();
        speedup = true;
        timer = fastSpeedTime;
    }
    public void Addmoney()
    {
        EKTemplate.GameManager.instance.AddMoney(50000);
    }
    public void Next_Scene()
    {
        if(EKTemplate.GameManager.instance.money>50000)
        {
            PlayerPrefs.DeleteAll();
            EKTemplate.GameManager.instance.money = 0;
            SceneManager.LoadScene(1);
            EKTemplate.GameManager.instance.LevelPrefs();
        }
    }
    public void Circle_Upgrade()
    {
        if (EKTemplate.GameManager.instance.money > circleUpgradeCost)
        {
            ButtonActionSame(0);
            EKTemplate.GameManager.instance.AddMoney(-circleUpgradeCost);
            if (circleUpgradeCount==5||circleUpgradeCount == 8)
            {
                EKTemplate.CameraManager.instance.CameraMove();

                if (circleUpgradeCount == 5)
                {
                    for (int i = 0; i < Knifes.Length; i++)
                    {
                        Knifes[i].transform.DOScaleX(knifeLengths[0].x, 0.5f).SetEase(Ease.OutBack);
                    }
                    Circles[0].SetActive(false);
                    Circles[1].SetActive(true);
                    Circles[2].SetActive(false);
                    maxCircleUpgrade += 3;
                }
                else if (circleUpgradeCount >= 8)
                {
                    for (int i = 0; i < Knifes.Length; i++)
                    {
                        Knifes[i].transform.DOScaleX(knifeLengths[1].x, 0.5f).SetEase(Ease.OutBack);
                    }
                    fastBalloonGrowSpeed *= 10;
                    Circles[0].SetActive(false);
                    Circles[1].SetActive(false);
                    Circles[2].SetActive(true);
                    maxCircleUpgrade += 4;

                }

                maxKnifeCount += 4;
                maxIncomeCount += 2;
            }
            else
            {
                BalloonSpawners[circleUpgradeCount].SetActive(true);
            }
            circleUpgradeCount++;
            circleUpgradeCost = circleUpgradeCosts[circleUpgradeCount];
            Buttons[0].transform.GetComponentInChildren<Text>().text = circleUpgradeCost.ToString();
            PlayerPrefs.SetInt("level0CircleCount", circleUpgradeCount);

        }
    }
    public void Income_Upgrade()
    {
        if (EKTemplate.GameManager.instance.money > incomeUpgradeCost)
        {
            ButtonActionSame(1);
            EKTemplate.GameManager.instance.AddMoney(-incomeUpgradeCost);
            incomeUpgradeCost = incomeMoneyAmounts[givingMoney];
            Buttons[1].transform.GetComponentInChildren<Text>().text = incomeUpgradeCost.ToString();
            givingMoney++;
            PlayerPrefs.SetInt("level0GivingMoney", givingMoney);
        }
    }
    public void New_Knife()
    {
        if(EKTemplate.GameManager.instance.money>newKnifeCost)
        {
            ButtonActionSame(2);
            Knifes[knifeCount].SetActive(true);
            EKTemplate.GameManager.instance.AddMoney(-newKnifeCost);

            newKnifeCost = newKnifeCosts[knifeCount];
            Buttons[2].transform.GetComponentInChildren<Text>().text = newKnifeCost.ToString();
            knifeCount++;
            PlayerPrefs.SetInt("level0KnifeCount", knifeCount);
            
        }
    }
    void ButtonClose(int s)
    {
        Buttons[s].GetComponent<Image>().overrideSprite = CloseSprite[s];
        Buttons[s].GetComponent<Button>().enabled = false;
        Buttons[s].transform.GetComponentInChildren<Text>().text = "MAX";
    }
    void ButtonActionSame(int c)
    {
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        EKTemplate.DelayManager.instance.Set(1f, () => canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay);
        Buttons[c].GetComponent<Animation>().Play();
        Buttons[c].GetComponentInChildren<ParticleSystem>().Play();
    }
}