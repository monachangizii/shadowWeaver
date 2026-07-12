using UnityEngine;
using ShadowWeaver.Utility;

namespace ShadowWeaver.Core
{
    public interface IRespawnable
    {
        void RespawnAt(Vector3 position, bool asMirrorWorld);
    }

    public class CheckpointManager : Singleton<CheckpointManager>
    {
        private Vector3 _lastCheckpointPosition;
        private bool _lastCheckpointWasMirrorWorld;
        private bool _hasCheckpoint;

        protected override void Awake()
        {
            base.Awake();
            GameEvents.OnCheckpointReached += HandleCheckpointReachedById;
        }

        private void OnDestroy()
        {
            GameEvents.OnCheckpointReached -= HandleCheckpointReachedById;
        }

        public void SetCheckpoint(Vector3 worldPosition, bool isMirrorWorld)
        {
            _lastCheckpointPosition = worldPosition;
            _lastCheckpointWasMirrorWorld = isMirrorWorld;
            _hasCheckpoint = true;
        }

        private void HandleCheckpointReachedById(string checkpointId)
        {
            var marker = GameObject.Find(checkpointId);
            if (marker != null)
                SetCheckpoint(marker.transform.position, _lastCheckpointWasMirrorWorld);
        }

        public void RespawnPlayer()
        {
            if (!_hasCheckpoint)
            {
                Debug.LogWarning("[CheckpointManager] هیچ چک‌پوینتی ثبت نشده.");
                return;
            }

            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj == null)
            {
                Debug.LogError("[CheckpointManager] GameObject با تگ 'Player' پیدا نشد.");
                return;
            }

            var respawnable = playerObj.GetComponent<IRespawnable>();
            if (respawnable != null)
            {
                respawnable.RespawnAt(_lastCheckpointPosition, _lastCheckpointWasMirrorWorld);
            }
            else
            {
                playerObj.transform.position = _lastCheckpointPosition;
                Debug.LogWarning("[CheckpointManager] IRespawnable پیاده‌سازی نشده؛ فقط موقعیت ست شد.");
            }
        }
    }
}