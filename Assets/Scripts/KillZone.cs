using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[KillZone] Player died!");
            
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.GameOver();
            }
        }
    }
}