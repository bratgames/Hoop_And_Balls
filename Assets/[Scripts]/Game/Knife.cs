using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Knife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Balloon")
        {
            other.GetComponent<Collider>().enabled = false;
            GameObject text = Instantiate(Resources.Load("MoneyText"), new Vector3(other.transform.parent.GetChild(1).position.x, other.transform.parent.GetChild(1).position.y + 1, other.transform.parent.GetChild(1).position.z), Quaternion.identity) as GameObject;
            text.GetComponentInChildren<Text>().text = "$" + LevelMain.givingMoney;
            other.transform.DOScaleY(1,0.5f).SetEase(Ease.OutBack).OnComplete(()=> {
                other.GetComponent<Collider>().enabled = true;
                other.GetComponent<Balloon>().speed = 0;
                int rnd = Random.Range(0, 7);
                other.transform.GetComponent<MeshRenderer>().material = Resources.Load("BallColors/Ball " + rnd) as Material;
            });
            GameObject ball = Instantiate(Resources.Load("FallBall"),other.transform.parent.GetChild(1).position,Quaternion.identity) as GameObject;
            ball.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;

        }
    }
}
