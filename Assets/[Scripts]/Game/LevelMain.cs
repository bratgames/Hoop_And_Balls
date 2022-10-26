using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;
public class LevelMain : MonoBehaviour
{
    public List<Transform> BallSpawnPoses = new List<Transform>();
    public List<Transform> HookSpawnPoses = new List<Transform>();

    public GameObject[] Buttons,Circles;    
    public Sprite[] OpenSprite, CloseSprite;
    int ballCount, hookCount, circleUpgradeCount;
    public static int givingMoney = 1;

    [Header("Costs")]
    public int[] circleUpgradeCosts;
    public int[] newBallCosts;
    public int[] newHookCosts;
    public int[] incomeMoneyAmounts;

    int circleUpgradeCost;
    int newBallCost;
    int newHookCost;
    int incomeUpgradeCost;
    [Header("Max Counts")]
    public int maxIncomeCount;
    public int maxBallCount;
    public int maxHookCount;
    public int maxCircleUpgrade;
    bool speedup;
    public static float speed;
    [Header("Hooks Speed")]
    public float startSpeed;
    public float fastSpeed;
    public float fastSpeedTime;

    public static bool gameStarted;
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
        gameStarted = false;
        speedup = false;
        circleUpgradeCost = circleUpgradeCosts[circleUpgradeCount];
        Buttons[0].transform.GetComponentInChildren<Text>().text = circleUpgradeCost.ToString();

        incomeUpgradeCost = incomeMoneyAmounts[givingMoney-1];
        Buttons[1].transform.GetComponentInChildren<Text>().text = incomeUpgradeCost.ToString();

        newBallCost = newBallCosts[ballCount];
        Buttons[2].transform.GetComponentInChildren<Text>().text = newBallCost.ToString();

        newHookCost = newHookCosts[hookCount];
        Buttons[3].transform.GetComponentInChildren<Text>().text = newHookCost.ToString();

        EKTemplate.LevelManager.instance.startEvent.AddListener(() => StartEvent());
    }
    void StartEvent()
    {
        gameStarted = true;
        for (int i = 0; i < HookSpawnPoses.Count; i++)
        {
            HookSpawnPoses[i].GetComponent<SplineFollower>().follow = true;

        }
    }
    void Update()
    {
        if (speedup == true)
        {
            if (timer <= 0)
            {
                speedup = false;
                speed = startSpeed;

            }
            else
            {
                speed = fastSpeed;
                timer -= Time.deltaTime;
            }
        }
        if(maxCircleUpgrade>circleUpgradeCount)
        {
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
        if (maxIncomeCount>givingMoney)
        {
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
        if (maxBallCount > ballCount)
        {
            Buttons[2].transform.GetComponentInChildren<Text>().text = newBallCost.ToString();
            if (EKTemplate.GameManager.instance.money < newBallCost)
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

        if (maxHookCount > hookCount)
        {
            Buttons[3].transform.GetComponentInChildren<Text>().text = newHookCost.ToString();
            if (EKTemplate.GameManager.instance.money < newHookCost)
            {
                Buttons[3].GetComponent<Image>().overrideSprite = CloseSprite[3];
                Buttons[3].GetComponent<Button>().enabled = false;
            }
            else
            {
                Buttons[3].GetComponent<Image>().overrideSprite = OpenSprite[3];
                Buttons[3].GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            ButtonClose(3);
        }

    } 
    void ButtonClose(int s)
    {
        Buttons[s].GetComponent<Image>().overrideSprite = CloseSprite[s];
        Buttons[s].GetComponent<Button>().enabled = false;
        Buttons[s].transform.GetComponentInChildren<Text>().text = "MAX";
    }
    public void Clicker()
    {
        Haptic.LightTaptic();
        speedup = true;
        timer = fastSpeedTime;
    }
    public void Addmoney()
    {
        EKTemplate.GameManager.instance.AddMoney(500);
    }
    public void Circle_Upgrade()
    {
        if (EKTemplate.GameManager.instance.money > circleUpgradeCost)
        {
            Buttons[0].GetComponent<Animation>().Play();
            Buttons[0].GetComponentInChildren<ParticleSystem>().Play();

            EKTemplate.GameManager.instance.AddMoney(-circleUpgradeCost);
            Circles[circleUpgradeCount].SetActive(true);
            EKTemplate.CameraManager.instance.CameraMove();
            circleUpgradeCount++;
            circleUpgradeCost = circleUpgradeCosts[circleUpgradeCount];
            Buttons[0].transform.GetComponentInChildren<Text>().text = circleUpgradeCost.ToString();
        }


    }
    public void Income_Upgrade()
    {
        if(EKTemplate.GameManager.instance.money>incomeUpgradeCost)
        {
            Buttons[1].GetComponent<Animation>().Play();
            Buttons[1].GetComponentInChildren<ParticleSystem>().Play();
            EKTemplate.GameManager.instance.AddMoney(-incomeUpgradeCost);

            incomeUpgradeCost = incomeMoneyAmounts[givingMoney];
            Buttons[1].transform.GetComponentInChildren<Text>().text = incomeUpgradeCost.ToString();
            givingMoney++;

        }
    }
    public void New_Ball()
    {
        if(EKTemplate.GameManager.instance.money >newBallCost)
        {
            Buttons[2].GetComponent<Animation>().Play();
            Buttons[2].GetComponentInChildren<ParticleSystem>().Play();
            EKTemplate.GameManager.instance.AddMoney(-newBallCost);


            GameObject Ball = Instantiate(Resources.Load("Ball"), BallSpawnPoses[ballCount].position, Quaternion.identity) as GameObject;
            int rnd = Random.Range(0, 11);
            Ball.GetComponent<MeshRenderer>().material = Resources.Load("Colors/Color " + rnd) as Material;
            ballCount++;
            newBallCost = newBallCosts[ballCount];
        }
    }
    public void New_Hook()
    {
        if (EKTemplate.GameManager.instance.money > newHookCost)
        {
            Buttons[3].GetComponent<Animation>().Play();
            Buttons[3].GetComponentInChildren<ParticleSystem>().Play();

            EKTemplate.GameManager.instance.AddMoney(-newHookCost);
            GameObject hook = Instantiate(Resources.Load("Hook"), HookSpawnPoses[hookCount].position, new Quaternion(0,0,0,0)) as GameObject;
            hook.transform.SetParent(HookSpawnPoses[hookCount]);
            hook.transform.rotation = new Quaternion(0,0,0,0);
            int rnd = Random.Range(0, 11);
            hook.GetComponent<MeshRenderer>().material = Resources.Load("Colors/Color " + rnd) as Material;
            hookCount++;
            newHookCost = newHookCosts[hookCount]; 
        }
    }
}
