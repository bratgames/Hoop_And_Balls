using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "ForwardCollider")
        {
            transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.InOutCirc);
        }
        Hole H = other.GetComponent<Hole>();
        if(H!=null)
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        Hole H = other.GetComponent<Hole>();
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
