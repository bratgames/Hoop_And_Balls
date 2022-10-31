using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [HideInInspector] public float speed;
    void Start()
    {

    }

    void Update()
    {
        if(LevelMain.gameStarted&&transform.localScale.y<2.5f)
        {
            speed += LevelMain.balloonSpeed * Time.deltaTime / 100000;
            transform.localScale = new Vector3(1, transform.localScale.y + speed, 1);
        }
    }
}
