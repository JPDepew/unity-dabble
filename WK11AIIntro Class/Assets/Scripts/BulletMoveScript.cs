using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveScript : MonoBehaviour {

    public float speed = 5;
    public SceneManager sceneManager;

	// Use this for initialization
	void Start () {
        GameObject o = GameObject.FindWithTag("SceneManager");
        sceneManager = o.GetComponent<SceneManager>();
        Destroy(this.gameObject, 2.25f);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 oldPosition = transform.position;
        Vector2 newPosition = transform.up * Time.deltaTime * speed;
        transform.position = new Vector3(newPosition.x + transform.position.x, newPosition.y + transform.position.y, oldPosition.z);
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = (GameObject)collision.gameObject;
        if (g.tag == "Stroid")
        {
            Destroy(this.gameObject);
        }
    }
}
