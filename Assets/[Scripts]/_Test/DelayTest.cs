using UnityEngine;
using EKTemplate;

public class DelayTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) SetDelay(Random.Range(0.4f, 2f));
        if (Input.GetKeyDown(KeyCode.S)) SetDelay(Random.Range(1, 30));
        if (Input.GetKeyDown(KeyCode.D)) SetDelay2(Random.Range(0.4f, 2f));
    }

    private void SetDelay(float duration)
    {
        DelayManager.instance.Set(duration, () =>
        {
            Debug.Log("Timer ended. Duration: " + duration);
        });
    }

    private void SetDelay(int frameCount)
    {
        DelayManager.instance.Set(frameCount, () =>
        {
            Debug.Log("Timer ended. Frame Count: " + frameCount);
        });
    }

    private async void SetDelay2(float duration)
    {
        await DelayManager.instance.SetAsync(duration);
        Debug.Log("Timer2 ended. Duration: " + duration);
    }
}