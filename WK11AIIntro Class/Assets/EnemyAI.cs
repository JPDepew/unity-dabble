
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Policy;
using System.Reflection.Emit;
using System.Text;

public class EnemyAI : MonoBehaviour
{

    public float enemySpeed = 5;
    public float sightDistance = 5;

    private GameObject player;

    public enum AIMode
    {
        attack = 0,
        runAway = 1,
        trackPlayer = 2,
    }

    public AIMode aiMode = AIMode.attack;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }


    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        dir.z = 0;
        float multiplier = 1;
        float distance = dir.magnitude;

        #region AIControl
        float localSpeed = 0;
        switch (aiMode)
        {
            case AIMode.attack:
                localSpeed = enemySpeed;
                break;
            case AIMode.runAway:
                if (distance < sightDistance)
                {
                    multiplier = 12 / distance;
                    Debug.Log(multiplier);
                    localSpeed = -enemySpeed;
                }
                break;
            case AIMode.trackPlayer:
                break;
            default:
                break;
        } //switch

        #endregion

        dir.Normalize();
        transform.position = Vector2.Lerp(transform.position, transform.position + multiplier * dir * localSpeed * Time.deltaTime, 0.2f);
    }
}
