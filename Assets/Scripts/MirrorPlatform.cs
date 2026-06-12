using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MirrorPlatform : MonoBehaviour
{
    private BoxCollider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        
        UpdatePlatformState();
    }

    void Update()
    {
        UpdatePlatformState();
    }

    void UpdatePlatformState()
    {
        if (WorldManager.Instance == null) return;

        bool shouldBeSolid = WorldManager.Instance.isMirrorWorld;
        
        col.enabled = shouldBeSolid;

        if (sr != null)
        {
            Color currentColor = sr.color;
            currentColor.a = shouldBeSolid ? 1.0f : 0.2f; 
            sr.color = currentColor;
        }
    }
}