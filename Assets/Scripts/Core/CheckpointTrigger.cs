using UnityEngine;

namespace ShadowWeaver.Core
{
    [RequireComponent(typeof(Collider2D))]
    public class CheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private string checkpointId = "Checkpoint_01";
        [SerializeField] private bool isMirrorWorldCheckpoint = false;
        [SerializeField] private ParticleSystem activationVFX;
        private bool _activated;

        private void Reset()
        {
            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_activated || !other.CompareTag("Player")) return;

            _activated = true;
            CheckpointManager.Instance.SetCheckpoint(transform.position, isMirrorWorldCheckpoint);
            GameEvents.RaiseCheckpointReached(checkpointId);

            if (activationVFX != null)
                activationVFX.Play();
        }
    }
}