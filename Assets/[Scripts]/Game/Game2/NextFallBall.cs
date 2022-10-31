using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class NextFallBall : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(delay());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            StartCoroutine(delay());
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        EKTemplate.GameManager.instance.AddMoney(NextLevelMain.givingMoney);

        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutQuint).OnComplete(()=> Destroy(gameObject));
    }

}
