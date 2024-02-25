using UnityEngine;
using Lean.Touch;
using Deslab.UI;
using System.Collections;
using System;
using DG.Tweening;
using TMPro;

namespace Deslab.Level
{
    public class LevelManager : MonoBehaviour
    {
        public LevelProgression levelProgression;
        public GameObject currentLevel;
        public GameObject currentPlayer;
        public ColorSchemeObject[] colorSchemeObjects;

        public static Action OnLevelLoaded;
        public static Action OnLevelStarted = delegate { };

        //private int _currentColorScheme = 0;

        private void Start()
        {
            //LoadLevelFromResources(); //For first time debug
            
        }

        /// <summary>
        /// Loading level from Resources/Levels/ by current Level ID.
        /// Level will be instanced in zero position world coordinates.
        /// 
        /// Loading player from Resources/PlayerController/.
        /// Player will be instanced in zero position world coordinates.
        /// </summary>
        public void LoadLevelFromResources()
        {
            if (currentLevel != null) Destroy(currentLevel);
            if (currentPlayer != null) Destroy(currentPlayer);

            GameObject _instance;
            _instance = Instantiate(Resources.Load<GameObject>("Levels/Level_" + StaticManager.GetLevelID()));
            ResetTransform(_instance.transform);
            currentLevel = _instance;

            LevelSettings levelSettings = _instance.GetComponent<LevelSettings>();

            GameObject playerInstance;
            playerInstance = Instantiate(Resources.Load("Player/PlayerController", typeof(GameObject))) as GameObject;
            SetTransform(playerInstance.transform, levelSettings.startLevelPoint.position);
            currentPlayer = playerInstance;

            if (levelProgression != null)
            {
                levelProgression.startPoint = levelSettings.startLevelPoint;
                levelProgression.endPoint = levelSettings.endLevelPoint;
                levelProgression.playerTransform = playerInstance.transform;
                levelProgression.SetDistance();
            }
            
            //if (StaticManager.instance.playerData.changeColorCounter == 2 || StaticManager.levelID == 0)
            //{
            //    SetColorSchemeObject(levelSettings);
            //}

            OnLevelLoaded?.Invoke();
            StaticManager.SavePlayerData();
        }

        /// <summary>
        /// Reset transform position, scale and rotation to zero.
        /// </summary>
        /// <param name="_transform">Transform for reset</param>
        private void ResetTransform(Transform _transform)
        {
            _transform.position = Vector3.zero;
            _transform.localScale = Vector3.one;
            _transform.rotation = Quaternion.identity;
        }

        /// <summary>
        /// Sets transform's position to starting position and reset scale and rotation
        /// </summary>
        /// <param name="_transform">Transform to set</param>
        /// <param name="startPos">Starting position</param>
        private void SetTransform(Transform _transform, Vector3 startPos)
        {
            _transform.position = startPos;
        }

        [SerializeField] private int startGameDelay = 3;

        /// <summary>
        /// Called after started play level.
        /// </summary>
        internal void StartLevel()
        {
            //Make what you need when game/level started
            StartCoroutine(CountDown());
            
            Player.Instance.playerName = playerName.text;
            PlayerPrefs.SetString("PlayerName", playerName.text);
            PlayerPrefs.Save();
        }

        [SerializeField] private TextMeshProUGUI countDownText;
        [SerializeField] private TextMeshProUGUI playerName;
        private IEnumerator CountDown()
        {
            int count = startGameDelay;

            for (int i = count; i >= 0; i--)
            {
                if (count > 0)
                    countDownText.text = count.ToString();
                else
                    countDownText.text = "START!";

                countDownText.transform.localScale = Vector3.zero;
                countDownText.DOScale(1f, 0.2f).onComplete += () => countDownText.transform.DOShakePosition(0.8f, 20f, 15, 120);
                count--;
                yield return new WaitForSeconds(1f);
            }
            countDownText.transform.localScale = Vector3.zero;

            StartGame();
        }

        private void StartGame()
        {
            OnLevelStarted?.Invoke();
            UIManager.instance.ShowTutorial();
            LeanTouch.OnFingerDown += HideTutorial;
        }

        private void HideTutorial(LeanFinger finger)
        {
            UIManager.instance.HideTutorial();
            LeanTouch.OnFingerDown -= HideTutorial;
        }

        private void SetColorSchemeObject(LevelSettings levelSettings)
        {
            levelSettings.colorScheme = colorSchemeObjects[StaticManager.instance.playerData.currentColorScheme];
            levelSettings.colorScheme.ApplyScheme();

            if (StaticManager.instance.playerData.currentColorScheme < colorSchemeObjects.Length - 1)
            {
                StaticManager.instance.playerData.currentColorScheme++;
            }
            else
            {
                StaticManager.instance.playerData.currentColorScheme = 0;
                levelSettings.colorScheme = colorSchemeObjects[StaticManager.instance.playerData.currentColorScheme];
                levelSettings.colorScheme.ApplyScheme();
            }

            if (StaticManager.levelID != 0)
            {
                StaticManager.instance.playerData.changeColorCounter = 0;
            }
        }
    }
}