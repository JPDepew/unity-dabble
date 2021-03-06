﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneManager : MonoBehaviour {

    public Text numberOfHits;
    private GameObject player;
    private PlayerMoveScript playerMoveScript;
    public GameObject[] spawnPoints;
    private GameObject playerObject;
    public float timeBetweenSpawns = 1.2f;


    public GameObject asteroidObject;

    AudioSource audioSource;

    public bool enemySpawnEnabled = false;

    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerMoveScript = playerObject.GetComponent<PlayerMoveScript>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
        numberOfHits.text = "";

      
    }
}
