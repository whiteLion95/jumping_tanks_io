using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Deslab.Level;
//using Deslab.Scripts.Deslytics;

namespace Deslab.UI
{
    [Serializable]
    public struct UpgradePanel
    {
        public TMP_Text level;
        public TMP_Text cost;
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public LevelManager levelManager;
        public CanvasGroupWindow menuScreen;
        public FakeServerConnection fakeConnection;
        public CanvasGroupWindow gameScreen;
        public CanvasGroupWindow winScreen;
        public CanvasGroupWindow loseScreen;
        public GameObject lobbyGameObject;
        public List<UpgradePanel> upgradePanels = new List<UpgradePanel>();
        [SerializeField] private TMP_InputField playerName;

        private void Awake()
        {
            instance = this;

            StaticManager.OnWin += ShowWinScreen;
            StaticManager.OnLose += ShowLoseScreen;
            LevelManager.OnLevelLoaded += ShowMenu;

            if (PlayerPrefs.HasKey("PlayerName"))
                playerName.text = PlayerPrefs.GetString("PlayerName");
        }

        /// <summary>
        /// Start game by click UI element
        /// </summary>
        public void StartGame()
        {
            StaticManager.instance.gameStatus = GameStatus.Game;
            StaticManager.reloadLevel = false;
            fakeConnection.ShowWindow(StartGameAfterConnect);
            menuScreen.HideWindow();

            void StartGameAfterConnect()
            {
                lobbyGameObject.SetActive(false);
                levelManager?.StartLevel();
                gameScreen.ShowWindow();
                Leaderboard.instance.UpdateLeaderboardsElements();
            }
            
            //DeslyticsManager.LevelStart(StaticManager.levelID);
        }

        /// <summary>
        /// Show Menu Screen after Win|Lose
        /// </summary>
        public void ShowMenu()
        {
            StaticManager.instance.gameStatus = GameStatus.Menu;
            menuScreen.ShowWindow(ShowLobby);
            winScreen.HideWindow();
            loseScreen.HideWindow();

            void ShowLobby()
            {
                lobbyGameObject.SetActive(true);
            }
            
            //DeslyticsManager.LevelWin();
        }

        /// <summary>
        /// Show Win Screen after complete level
        /// </summary>
        public void ShowWinScreen()
        {
            Leaderboard.instance.ClearLeaderboard();
            StaticManager.instance.gameStatus = GameStatus.Menu;
            gameScreen.HideWindow();
            winScreen.ShowWindow();
            //launchScreen.HideWindow();
            //HideFlyTutorial();
        }

        /// <summary>
        /// Ident to Win Screen but lose
        /// </summary>
        public void ShowLoseScreen()
        {
            StaticManager.instance.gameStatus = GameStatus.Menu;
            gameScreen.HideWindow();
            loseScreen.ShowWindow();
            //DeslyticsManager.LevelFailed();
        }

        /// <summary>
        /// For debug screens
        /// </summary>
        private void Update()
        {
            //if (StaticManager.instance.debugMode)
            //{
                if (Input.GetKeyDown(KeyCode.W))
                {
                    //StaticManager.levelID++;
                    ShowWinScreen();
                }
                if (Input.GetKeyDown(KeyCode.L))
                    ShowLoseScreen();
            //}
        }

        //////////////////////////////////
        //
        //  That region for tutorial objects.
        //
        //////////////////////////////////

        //public GameObject flyTutorial;
        //public void ShowFlyTutorial()
        //{
        //    flyTutorial.SetActive(true);
        //}
        //public void HideFlyTutorial()
        //{
        //    flyTutorial.SetActive(false);
        //}

        public GameObject tutorial;
        public void HideTutorial()
        {
            if (tutorial != null)
                tutorial.SetActive(false);
        }
        public void ShowTutorial()
        {
            if (tutorial != null)
                tutorial.SetActive(true);
        }

        /// <summary>
        /// Upgrade some Player stats by unique upgrade ID
        /// </summary>
        /// <param name="upgradeID"></param>
        public void Upgrade(int upgradeID)
        {
            PlayerUpgradeData pUpgradeData = StaticManager.instance.playerData.playerUpgradeData[upgradeID];
            if (StaticManager.instance.playerData.money >= pUpgradeData.upgradeCost)
            {
                StaticManager.SubstractMoney(pUpgradeData.upgradeCost);
                pUpgradeData.Upgrade();
                upgradePanels[upgradeID].cost.text = pUpgradeData.upgradeCost.ToString();
                upgradePanels[upgradeID].level.text = "LVL " + pUpgradeData.upgradeLevel;

                StaticManager.instance.playerData.playerUpgradeData[upgradeID] = pUpgradeData;
                StaticManager.SavePlayerData();
            }
        }

        /// <summary>
        /// Set value from loaded Player Stats to Upgrade UI elements
        /// </summary>
        /// <param name="playerUpgradeData"></param>
        public void SetUpgradesStats(List<PlayerUpgradeData> playerUpgradeData)
        {
            List<PlayerUpgradeData> pUpgradeData = playerUpgradeData;
            for (int i = 0; i < pUpgradeData.Count; i++)
            {
                upgradePanels[i].cost.text = pUpgradeData[i].upgradeCost.ToString();
                upgradePanels[i].level.text = "LVL " + pUpgradeData[i].upgradeLevel;
            }
        }

        //For debugging
        public void OnWin()
        {
            StaticManager.OnWin?.Invoke();
        }

        public void OnLose()
        {
            StaticManager.OnLose?.Invoke();
        }
    }

    #region InDev


    //[Serializable]
    //public class LevelStars
    //{
    //    public Image starImage;
    //    Sequence starSequence;
    //    public void ShowStar()
    //    {
    //        starSequence = DOTween.Sequence();

    //        starSequence.Append(starImage.transform.transform.DOLocalMoveY(-650, 1.3f)
    //                                                         .SetEase(Ease.InBack))
    //            .Join(
    //                    starImage.transform.DOLocalRotate(new Vector3(0, 359, 0), 0.7f, RotateMode.FastBeyond360)
    //                                       .SetLoops(-1))
    //            .Join(
    //                    starImage.DOFade(0, 0.7f)
    //                             .SetDelay(0.8f));
    //    }

    //    public void ResetStar()
    //    {
    //        starSequence.Kill();
    //        starImage.color = new Color32(255, 255, 255, 255);
    //        starImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
    //        starImage.transform.localPosition = new Vector3(starImage.transform.localPosition.x, -36, 0);
    //    }
    //}
    #endregion
}
