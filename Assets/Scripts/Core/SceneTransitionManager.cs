using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShadowWeaver.Utility;

namespace ShadowWeaver.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneTransitionManager : Singleton<SceneTransitionManager>
    {
        [SerializeField] private CanvasGroup fadeCanvasGroup;
        [SerializeField] private float fadeDuration = 0.5f;

        protected override void Awake()
        {
            base.Awake();
            if (fadeCanvasGroup == null)
                fadeCanvasGroup = GetComponent<CanvasGroup>();

            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.blocksRaycasts = false;
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(TransitionRoutine(sceneName));
        }

        public void ReloadCurrentScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        private IEnumerator TransitionRoutine(string sceneName)
        {
            yield return Fade(1f);

            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
                yield return null;

            yield return Fade(0f);
        }

        private IEnumerator Fade(float targetAlpha)
        {
            fadeCanvasGroup.blocksRaycasts = true;
            float start = fadeCanvasGroup.alpha;
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                fadeCanvasGroup.alpha = Mathf.Lerp(start, targetAlpha, t / fadeDuration);
                yield return null;
            }
            fadeCanvasGroup.alpha = targetAlpha;
            fadeCanvasGroup.blocksRaycasts = targetAlpha > 0.99f;
        }
    }
}