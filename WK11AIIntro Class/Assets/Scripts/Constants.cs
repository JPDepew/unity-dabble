//using System.Collections;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Constants : MonoBehaviour
{

    public int initialPlayerHealth = 5;
    public int pickupHealthRestore = 1;
    public int enemyDamage = 2;
    public int maxNumberOfEnemiesAllowed = 2;
    public float speed = 20.0f;
    public float lookSpeed = 25;
    public int score = 0;
    public float playerLerpConstant = 0.2f;

#region PlayerPrefs
    public string playerPrefHighScoreKey = "playerHighScore";
#endregion  

    static public Constants S;

    void Awake()
    {
        S = this;


    }

    private void Start()
    {
        #region PlayerPrefs
        if (!PlayerPrefs.HasKey(playerPrefHighScoreKey))
        {
            score = PlayerPrefs.GetInt(playerPrefHighScoreKey);
            Debug.Log("Setting Player Prefs for the first time");
        }
        else
        {
            Debug.Log("Reading Player Prefs");
            PlayerPrefs.SetInt(playerPrefHighScoreKey, score);
        }
        #endregion PlayerPrefs
    }

    // Update is called once per frame
    void Update()
    {


    }
}