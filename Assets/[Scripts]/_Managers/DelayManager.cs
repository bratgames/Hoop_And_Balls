using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

namespace EKTemplate
{
    public class DelayManager : MonoBehaviour
    {
        private List<DelayHandler> delayQueue = new List<DelayHandler>();

        #region Singleton
        public static DelayManager instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                DestroyImmediate(this);
            }
        }
        #endregion

        public DelayHandler Set(float duration, UnityAction onComplete)
        {
            DelayHandler dh = gameObject.AddComponent<DelayHandler>();
            delayQueue.Add(dh);
            dh.SetDelay(duration, () =>
            {
                delayQueue.Remove(dh);
                onComplete?.Invoke();
            });

            return dh;
        }

        public DelayHandler Set(int frameCount, UnityAction onComplete)
        {
            DelayHandler dh = gameObject.AddComponent<DelayHandler>();
            delayQueue.Add(dh);
            dh.SetDelay(frameCount, () =>
            {
                delayQueue.Remove(dh);
                onComplete?.Invoke();
            });

            return dh;
        }

        public async Task SetAsync(float duration)
        {
            await Task.Delay((int)(duration * 1000f));
        }
    }
}