using UnityEngine;
using ShadowWeaver.Utility;

namespace ShadowWeaver.Core
{
    public class GameManager : Singleton<GameManager>
    {
        public bool IsPaused { get; private set; }
        public string CurrentLevelId { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            GameEvents.OnPlayerDied += HandlePlayerDied;
        }

        private void OnDestroy()
        {
            GameEvents.OnPlayerDied -= HandlePlayerDied;
        }

        public void SetCurrentLevel(string levelId)
        {
            CurrentLevelId = levelId;
        }

        public void TogglePause()
        {
            SetPaused(!IsPaused);
        }

        public void SetPaused(bool paused)
        {
            IsPaused = paused;
            Time.timeScale = paused ? 0f : 1f;
            GameEvents.RaisePauseStateChanged(paused);
        }

        private void HandlePlayerDied()
        {
            Invoke(nameof(RespawnAtCheckpoint), 0.6f);
        }

        private void RespawnAtCheckpoint()
        {
            CheckpointManager.Instance.RespawnPlayer();
        }

        public void RestartCurrentLevel()
        {
            Time.timeScale = 1f;
            SceneTransitionManager.Instance.ReloadCurrentScene();
        }

        public void QuitToMainMenu()
        {
            Time.timeScale = 1f;
            SceneTransitionManager.Instance.LoadScene("MainMenu");
        }
    }
}