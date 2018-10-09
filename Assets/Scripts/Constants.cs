using UnityEngine;

public class Constants : MonoBehaviour {

    public long initialPlayerHealth = 50000000000;
    public float speed = 20.0f;

    public string playerPrefHighScoreKey = "playerHighScore";
    int score = 0;
    static public Constants S;

    private void Awake()
    {
        S = this;

        if (!PlayerPrefs.HasKey(playerPrefHighScoreKey))
        {
            score = PlayerPrefs.GetInt(playerPrefHighScoreKey);
        }
        else
        {
            PlayerPrefs.SetInt(playerPrefHighScoreKey, score);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
