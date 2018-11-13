using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private float actualSpeed;
    private Vector2 direction;
    private Vector2 positionLastFrame;
    private Vector2 newCalculatedPosition;
    private bool inCollision = false;
    public GameObject weapon;

    public float lookSpeed = 5;
    private float rotateAmount = 0;

    //private AudioSource breakGlassSoundFX;

    #region Health
    public int startingHealth = 10;
    private int health;
    #endregion

    #region Lerp
    public float lerpConstant = 0.05f;
    #endregion

    Renderer renderer;
    bool isThisInvisible = false;

    // Use this for initialization
    void Start()
    {
        direction = Vector2.zero;
        actualSpeed = 0;//Constants.S.speed; //speed;
        //breakGlassSoundFX = GetComponent<AudioSource>();
        health = startingHealth;
        renderer = GetComponent<Renderer>();
        //StartCoroutine("EmitWeapon");
    }
    #region Lerp
    void LerpSpeed()
    {
        if (Mathf.Abs(actualSpeed) < 0.05){
            actualSpeed = 0;
            return;
        }
        actualSpeed = Mathf.Lerp(actualSpeed, 0f, Constants.S.playerLerpConstant);
        //Debug.Log("Lerp value: " + actualSpeed.ToString());
    }
    #endregion

    void getInputDirection()
    {

        rotateAmount = 0;
        if (Input.GetKey(KeyCode.W))
        {
            actualSpeed = Constants.S.speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            actualSpeed = -Constants.S.speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotateAmount += Constants.S.lookSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotateAmount -= Constants.S.lookSpeed * Time.deltaTime;
        }

        LerpSpeed();
    }

    IEnumerator EmitWeapon()
    {
        while (true)
        {
            Debug.Log("here");
            yield return new WaitForSeconds(2f);
            Instantiate(weapon,transform.position,transform.rotation);
        }
    }

    #region Health
    public int Health()
    {
        return health;
    }
    #endregion
    private void FixedUpdate()
    {

    }

    Vector2 IsThisOffScreen()
    {
        if (renderer.isVisible)
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

    void Update()
    {

        getInputDirection();
        Vector3 oldPosition = transform.position;
        Vector2 newPosition = transform.up * Time.deltaTime * actualSpeed;
        newCalculatedPosition = new Vector3(newPosition.x + transform.position.x, newPosition.y + transform.position.y, oldPosition.z);
        transform.Rotate(0, 0, rotateAmount);
    }



    private void LateUpdate()
    {
        positionLastFrame = transform.position;
        transform.position = newCalculatedPosition * IsThisOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //breakGlassSoundFX.Play();
        GameObject g = (GameObject)collision.gameObject;
        if (g.tag == "Stroid")
            Destroy(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {


    }
}
