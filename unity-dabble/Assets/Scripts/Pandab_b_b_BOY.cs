using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pandab_b_b_BOY : MonoBehaviour {

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("PandaDamage");
        }
	}
}
