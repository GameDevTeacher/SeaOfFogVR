using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public bool saveToggle;
    public bool loadToggle;

    public PlayerSaving player;
    public BoatSaving boat;
    public List<SceneData> scene;
    //public EventManager? events;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSaving>();
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<BoatSaving>();
    }
    
    public void Update()
    {
        if (saveToggle)
        {
            print("saving");
            SaveSystem.Save();
            saveToggle = false;
        }

        if (loadToggle)
        {
            print("loading");
            SaveSystem.Load();
            loadToggle = false;
        }
    }
    




}
