using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[LevelEndTrigger] Player reached the end!");
            
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.CompleteLevel();
            }
        }
    }
}
