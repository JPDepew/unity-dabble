using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDontHitTheWallDude : MonoBehaviour {

    public float speed = 0.2f;
    float zRotation = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * 1, transform.up * 10);
        Debug.DrawRay(transform.position + transform.up * 1, transform.up * 10);

        if (hit)
        {
            if(hit.distance < 2f)
            {
                zRotation = Random.Range(0,359);
            }
        }

        transform.position += speed * transform.up * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(new Vector3(0, 0, zRotation)),0.5f);
	}
}
