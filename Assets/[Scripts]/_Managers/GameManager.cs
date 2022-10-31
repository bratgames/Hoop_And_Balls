using UnityEngine;
using UnityEngine.SceneManagement;

namespace EKTemplate
{
    public class GameManager : MonoBehaviour
    {
        public static int scenelevelc;

        [Header("LEVEL'S"), Space(5)]
        public int level = -1;
        public int levelCount = 10;
        public int levelLoopFrom = 3;

        [Header("CURRENCY"), Space(5)]
        public int money = 0;

        [Header("TEST MODE"), Space(5)]
        public bool simulatePhone;
        #region Singleton
        public static GameManager instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
                GetDependencies();
            }
            else
            {
                DestroyImmediate(this);
            }

        }
        #endregion
        private void Start()
        {
            if (PlayerPrefs.HasKey("Levelc"))
            {
                scenelevelc = PlayerPrefs.GetInt("Levelc");
                OpenScene(scenelevelc);
            }
        }
        public void LevelPrefs()
        {
            scenelevelc++;
            PlayerPrefs.SetInt("Levelc", scenelevelc);
        }
        private void GetDependencies()
        {
            //if level variable set -1, game run as mobile
            //if we want to play specific level on editor, change -1 to value we want
            if (simulatePhone || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || level == -1) level = DataManager.instance.level;
            money = DataManager.instance.money;
        }

        #region DataOperations
        public void AddMoney(int amount)
        {
            money += amount;
            DataManager.instance.SetMoney(money);
        }

        public void LevelUp()
        {
            DataManager.instance.SetLevel(++level);
        }
        #endregion

        #region SceneOperations
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OpenScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void OpenScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        #endregion
    }
}