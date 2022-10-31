using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class NextBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EKTemplate.LevelManager.instance.startEvent.AddListener(() => StartEvent());

    }

    void Update()
    {
        
    }
    void StartEvent()
    {
        EKTemplate.DelayManager.instance.Set(0.3f, () => GetComponent<Collider>().enabled = true);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "ForwardCollider")
        {
            transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.InOutCirc);
        }
        NextHole H = other.GetComponent<NextHole>();
        if(H!=null)
        {
            GameObject text = Instantiate(Resources.Load("MoneyText"), new Vector3(transform.position.x,transform.position.y+1,transform.position.z), Quaternion.identity) as GameObject;
            text.GetComponentInChildren<Text>().text = "$" + NextLevelMain.givingMoney;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        NextHole H = other.GetComponent<NextHole>();
        if(H!=null)
        {
            other.GetComponent<Collider>().enabled = false;
            EKTemplate.DelayManager.instance.Set(0.1f, () => { 
                other.GetComponent<Collider>().enabled = true;


            });
            transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).SetEase(Ease.OutElastic);
        }    
    }
}
