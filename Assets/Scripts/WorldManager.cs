using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance; 

    [Header("World State")]
    public bool isMirrorWorld = false; 

    [Header("Visual Feedback")]
    public Camera mainCamera;
    public Color normalWorldColor = new Color(0.2f, 0.2f, 0.2f); 
    public Color mirrorWorldColor = new Color(0.05f, 0.1f, 0.2f);  

    void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        UpdateWorldVisuals();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleWorld();
        }
    }

    public void ToggleWorld()
    {
        isMirrorWorld = !isMirrorWorld; 
        UpdateWorldVisuals();
        
    }

    void UpdateWorldVisuals()
    {
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = isMirrorWorld ? mirrorWorldColor : normalWorldColor;
        }
    }
}
