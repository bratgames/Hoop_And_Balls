using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;
using UnityEngine.UI;
public class NextHole : MonoBehaviour
{
    SplineFollower sf;
    bool onetime;
    void Start()
    {
        if(GetComponent<SplineFollower>()!=null)
        {
            onetime = false;
            sf = GetComponent<SplineFollower>();
            NextLevelMain.speed = sf.followSpeed;
        }
    }
    private void Update()
    {
        if(transform.GetComponent<SplineFollower>()!=null)
        {
            if (NextLevelMain.gameStarted && !onetime)
            {
                sf.follow = true;
                onetime = true;
            }
            sf.followSpeed = NextLevelMain.speed;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Ball")
        {
            int rnd = Random.Range(0, other.transform.childCount);
            GameObject ball = Instantiate(Resources.Load("NextFallBall"), other.transform.GetChild(rnd).position, Quaternion.identity)as GameObject;

            ball.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            ball.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 2f).SetEase(Ease.OutElastic);

        }
    }
}
