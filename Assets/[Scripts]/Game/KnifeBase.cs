using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBase : MonoBehaviour
{
    float rotspeed;

    void Start()
    {
        rotspeed = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelMain.gameStarted)
        {
            rotspeed += Time.deltaTime * LevelMain.knifespeed;
            transform.rotation = Quaternion.Euler(0, 0, rotspeed);
        }
    }
}
