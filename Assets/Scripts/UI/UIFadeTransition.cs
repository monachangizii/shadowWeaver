using System.Collections;
using UnityEngine;

namespace ShadowWeaver.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIFadeTransition : MonoBehaviour
    {
        [SerializeField] private float duration = 0.25f;
        private CanvasGroup _group;
        private Coroutine _routine;

        private void Awake() => _group = GetComponent<CanvasGroup>();

        public void Show()
        {
            gameObject.SetActive(true);
            StartFade(1f, true);
        }

        public void Hide()
        {
            StartFade(0f, false, () => gameObject.SetActive(false));
        }

        private void StartFade(float target, bool interactable, System.Action onComplete = null)
        {
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(FadeRoutine(target, interactable, onComplete));
        }

        private IEnumerator FadeRoutine(float target, bool interactable, System.Action onComplete)
        {
            float start = _group.alpha;
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                _group.alpha = Mathf.Lerp(start, target, t / duration);
                yield return null;
            }
            _group.alpha = target;
            _group.interactable = interactable;
            _group.blocksRaycasts = interactable;
            onComplete?.Invoke();
        }
    }
}