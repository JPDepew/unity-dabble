using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{

    public GameObject shipPrefab;
    public GameObject AI1;
    public GameObject AI2;
    public Text timer;
    public Text hits;
    float countTimer = 0;

    private GameObject shipReference;

    void Start()
    {
        shipReference = Instantiate(shipPrefab);
        Instantiate(AI1, new Vector2(3.6f, -2.5f), transform.rotation);
        Instantiate(AI2, new Vector2(6f, 2.3f), transform.rotation);
    }

    private void Update()
    {
        if (shipReference != null)
        {
            hits.text = "Hits: " + shipReference.GetComponent<PlayerMovement>().GetHits().ToString();
        }
        timer.text = "Time: " + Time.timeSinceLevelLoad.ToString("F2");


    }
}