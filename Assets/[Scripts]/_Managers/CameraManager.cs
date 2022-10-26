using UnityEngine;
using System.Collections;
using DG.Tweening;
namespace EKTemplate
{
    public class CameraManager : MonoBehaviour
    {
        public Vector3[] cameramoves;
        public static int camMovecount;
        #region Singleton
        public static CameraManager instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion
        public void CameraMove()
        {
            camMovecount++;
            transform.DOMove(cameramoves[camMovecount],1);
        }
    }

}