using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Policy;
using System.Reflection.Emit;
using System.Text;

public class EnemyAI : MonoBehaviour
{

    public float sightDistance = 4;
    public float runAwayDistance = 5;
    public float attackDistance = 4;
    public float enemySpeed = 5;
    public float trackDistance = 3;
    public bool alertLight = false;
    public bool facePlayer = false;

    public GameObject weapon;

    private GameObject player;
    private Light spotLight;

    private PatternsAI patternsAI;
    private WaypointsAI waypointsAI;
    private Vector2 waypointDirection;

    public enum AIMode
    {
        attack = 0,
        runAway = 1,
        trackPlayer = 2,
        deltaTrack = 3,
        patternMove = 4,
        waypointMove = 5,
        trackFromDistance = 6
    }

    public AIMode aiMode = AIMode.attack;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spotLight = GetComponentInChildren<Light>();
        spotLight.enabled = false;

        patternsAI = GetComponent<PatternsAI>();
        waypointsAI = GetComponent<WaypointsAI>();
        StartCoroutine("EmitWeapon");
    }

    void lookAtPlayer(float dist, Vector2 lookAt)
    {
        if (dist < sightDistance)
        {

            Vector3 foo = Vector3.Lerp(this.transform.forward, lookAt, Time.deltaTime * 2);
            this.transform.forward = foo;
        }
    }
    public void LightOn()
    {
        spotLight.enabled = true;
    }

    public void LightOff()
    {
        spotLight.enabled = false;
    }


    void lightUp(float dist)
    {
        if (dist <= sightDistance)
        {
            spotLight.enabled = true;
        }
        else
            spotLight.enabled = false;
    }

    public Vector2 deltaTrack()
    {
        Vector2 newDir = Vector2.zero;
        float deltaX = player.transform.position.x - transform.position.x;
        float deltaY = player.transform.position.y - transform.position.y;


        if (deltaX > 0)
            newDir.x = 1;
        else if (deltaX < 0)
            newDir.x = -1;
        if (deltaY > 0)
            newDir.y = 1;
        else if (deltaY < 0)
            newDir.y = -1;

        if (Mathf.Abs(deltaX) < 0.001)
            newDir = new Vector2(0, 1);
        if (Mathf.Abs(deltaY) < 0.001)
            newDir = new Vector2(1, 0);

        newDir.Normalize();
        return newDir;
    }

    IEnumerator EmitWeapon()
    {
        while (true)
        {
            Debug.Log("here");
            yield return new WaitForSeconds(0.2f);
            Instantiate(weapon, transform.position, transform.rotation);
        }
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;
        float distance = 0;
        dir = this.transform.position - player.transform.position;
        //dir.y = 0;
        distance = dir.magnitude;
        if (facePlayer)
            lookAtPlayer(distance, dir);
        spotLight.enabled = false;
        if (alertLight)
            lightUp(distance);
        float multiplier = 1f;



        #region AIControl
        float localSpeed = 0;


        switch (aiMode)
        {
            case AIMode.attack:
                if (distance <= attackDistance)
                {
                    dir *= -1;
                    localSpeed = enemySpeed;
                }
                break;
            case AIMode.runAway:
                if (distance <= runAwayDistance) {
                    multiplier = 12 / distance;
                    Debug.Log(multiplier);
                    localSpeed = -enemySpeed;
                }
                break;
            case AIMode.trackPlayer:
                if (distance > trackDistance)
                {
                    dir *= -1;
                    localSpeed = enemySpeed;
                }
                else
                {
                    localSpeed = enemySpeed;
                }
                break;
            case AIMode.deltaTrack:
                if (distance <= attackDistance)
                {
                    dir = deltaTrack();//(dir);
                    localSpeed = enemySpeed;
                }
                break;
            case AIMode.patternMove:
                dir = patternsAI.evaluatePattern();
                localSpeed = enemySpeed;
                break;
            case AIMode.waypointMove:
                waypointDirection = waypointsAI.evaluateWaypoint();

                #region
                //waypointDirection = waypointsAI.findClosestWaypoint(); //move to the closet way point, repeat
                #endregion
                localSpeed = enemySpeed;
                break;
            case AIMode.trackFromDistance:
                if (distance > 3.1f)
                {
                    multiplier = 20f / distance;
                    localSpeed = -enemySpeed;
                }
                else if(distance < 2.9f)
                {
                    multiplier = 20f / distance;
                    localSpeed = enemySpeed;
                }
                else
                {
                    localSpeed = 0;
                }
                Debug.Log(multiplier);
                break;
            default:
                break;
        } //switch

        if (aiMode == AIMode.waypointMove)
        {
            dir = waypointDirection;
        }
        #endregion
        transform.position = Vector2.Lerp(transform.position, transform.position + multiplier * dir * localSpeed * Time.deltaTime, 0.2f);
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Security.Policy;
//using System.Reflection.Emit;
//using System.Text;

//public class EnemyAI : MonoBehaviour
//{

//    public float sightDistance = 4;
//    public float runAwayDistance = 5;
//    public float attackDistance = 4;
//    public float enemySpeed = 5;
//    public float trackDistance = 3;
//    public bool alertLight = false;
//    public bool facePlayer = false;

//    private GameObject player;
//    private Light light;

//    public enum AIMode
//    {
//        attack = 0,
//        runAway = 1,
//        trackPlayer = 2,
//    }

//    public AIMode aiMode = AIMode.attack;


//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        light = GetComponentInChildren<Light>();
//        light.enabled = false;
//    }

//    void lookAtPlayer(float dist, Vector3 lookAt)
//    {
//        transform.rotation = player.transform.rotation;
//        //if (dist < sightDistance)
//            //this.transform.forward = tempLookAt;
//    }

//    void lightUp(float dist)
//    {
//        if (dist <= sightDistance)
//        {
//            light.enabled = true;
//            //this.transform.forward = dir;
//        }
//        else
//            light.enabled = false;
//    }

//    public void setAttackMode(){
//        aiMode = AIMode.attack;
//    }

//    public void setRunAwayMode()
//    {
//        aiMode = AIMode.runAway;
//    }

//    public void setTrackMode()
//    {
//        aiMode = AIMode.trackPlayer;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector3 dir = this.transform.position - player.transform.position;
//        dir.z = 0;
//        float distance = dir.magnitude;
//        dir.Normalize();

//        if (facePlayer)
//            lookAtPlayer(distance, dir);
//        //spotLight.enabled = false;
//        if (alertLight)
//            lightUp(distance);

//        #region AIControl
//        float localSpeed = 0;
//        Debug.Log("Dir" + dir.ToString() + " distance: " + distance.ToString());
//        switch (aiMode)
//        {
//            case AIMode.attack:
//                if (distance <= attackDistance)
//                {
//                    dir *= -1;
//                    localSpeed = enemySpeed;
//                }
//                break;
//            case AIMode.runAway:
//                if (distance <= runAwayDistance)
//                {
//                    localSpeed = enemySpeed;
//                    //Debug.Log("Here");
//                }
//                break;
//            case AIMode.trackPlayer:
//                if (distance > trackDistance)
//                {
//                    dir *= -1;
//                    localSpeed = enemySpeed;
//                }
//                else
//                {
//                    localSpeed = enemySpeed;
//                }
//                break;
//            default:
//                break;
//        } //switch

//        #endregion

//        this.transform.position += dir * localSpeed * Time.deltaTime;
//    }
//}
