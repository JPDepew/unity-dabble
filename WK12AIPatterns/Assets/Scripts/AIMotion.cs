// This script has been modified to wrap the NPC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMotion : MonoBehaviour {


    public float speed = 5;
    public Vector2 direction;

    Renderer rendererObject;
    bool isThisInvisible = false;

    SceneManager sceneManager;

    // Use this for initialization
    void Start () {
        int x = Random.Range(1, 10);
        int y = Random.Range(1, 10);
        int decide = Random.Range(1, 5);

        if (decide == 3)
            x *= -1;
        else if (decide == 1)
            y *= -1;

        speed += Random.Range(0, 2);
        Debug.Log("Speed " + speed.ToString());
        if (speed == 0)
            speed = 2;
        direction = new Vector2(x,y);
        direction.Normalize();
        Vector3 newLocation = new Vector3(0, 0, 0);//Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        rendererObject = GetComponent<Renderer>();
        GameObject sceneManagerObject = GameObject.FindWithTag("SceneManager");
        sceneManager = sceneManagerObject.GetComponent<SceneManager>();
    }

    Vector2 IsThisOffScreen()
    {
        if (rendererObject.isVisible)
        {
            isThisInvisible = false;
            return new Vector2(1, 1);

        }
        if (isThisInvisible)
            return new Vector2(1, 1);

        float xFix = 1;
        float yFix = 1;
        Vector3 viewPortPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPortPosition.x > 1 || viewPortPosition.x < 0)
            xFix = -1;
        if (viewPortPosition.y > 1 || viewPortPosition.y < 0)
            yFix = -1;
        isThisInvisible = true;
        return new Vector2(xFix, yFix);
    }

        // Update is called once per frame
        void Update () {
        Vector3 oldPosition = transform.position;
        Vector2 newPosition = direction*Time.deltaTime*speed + new Vector2(oldPosition.x, oldPosition.y);
        Vector3 temp = new Vector3(newPosition.x, newPosition.y, oldPosition.z);
        transform.position = temp * IsThisOffScreen();
    }

   void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = (GameObject)collision.gameObject;

        if (g.tag == "Player" || g.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
