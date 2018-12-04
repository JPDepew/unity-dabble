using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 2;
    public float lookSpeed = 1;
    public float maxLookSpeed = 5;
    public float fixAmount = 0.5f;

    private Vector2 direction;
    private float rotateAmount = 0;
    private float actualSpeed = 0;
    private float speed = 3f;
    private float health;

    Vector2 oldPosition;
    Vector2 positionLastFrame;
    Vector2 newCalculatedPosition;
    bool inCollision = false;

    private int hits;

    private void Start()
    {
        speed = Constants.S.speed;
        health = Constants.S.initialPlayerHealth;
    }

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, 10);
        Debug.DrawRay(transform.position, transform.up * 10);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D h in hits)
            {
                Debug.Log(h.collider.name);
            }
        }
        
        GetInput();
        newCalculatedPosition = UpdatePosition();
        transform.Rotate(0, 0, rotateAmount);
    }

    private void LateUpdate()
    {
        if (inCollision)
        {
            Vector3 fixDir = new Vector3(positionLastFrame.x - newCalculatedPosition.x, positionLastFrame.y - newCalculatedPosition.y);
            fixDir.Normalize();
            fixDir *= ModifyFixDirectionBasedOnQuadrant();

            actualSpeed = 0;
            transform.position = newCalculatedPosition - (fixDir * newCalculatedPosition * fixAmount);
            inCollision = false;
        }
        else
        {
            transform.position = newCalculatedPosition;
        }
        positionLastFrame = transform.position;
    }

    public int GetHits()
    {
        return hits;
    }

    private Vector2 ModifyFixDirectionBasedOnQuadrant()
    {
        Vector2 modifyVector = new Vector2(1, 1);
        if (newCalculatedPosition.x > 0)
        {
            modifyVector.x *= -1;
        }
        if (newCalculatedPosition.y > 0)
        {
            modifyVector.y *= -1;
        }
        return modifyVector;
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (actualSpeed < maxSpeed)
            {
                actualSpeed = Mathf.Lerp(actualSpeed,actualSpeed + speed,0.2f);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (actualSpeed > -maxSpeed)
            {
                actualSpeed = Mathf.Lerp(actualSpeed, actualSpeed - speed, 0.2f);
            }
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (Mathf.Abs(actualSpeed) > 0.00001f)
            {
                actualSpeed = Mathf.Lerp(actualSpeed, 0, 0.2f);
            }
            else
            {
                actualSpeed = 0;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (rotateAmount < maxLookSpeed)
                rotateAmount += lookSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (rotateAmount > -maxLookSpeed)
                rotateAmount += -lookSpeed * Time.deltaTime;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (Mathf.Abs(rotateAmount) > 0.00001f)
            {
                rotateAmount = Mathf.Lerp(rotateAmount, 0, 0.2f);
            }
            else
            {
                rotateAmount = 0;
            }
        }
    }

    private Vector2 UpdatePosition()
    {
        return transform.position + transform.up * Time.deltaTime * actualSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TopWall") || collision.CompareTag("BottomWall")
            || collision.CompareTag("RightWall") || collision.CompareTag("LeftWall") || collision.CompareTag("AI"))
        {
            inCollision = true;
            Debug.Log(health);
            if (collision.CompareTag("AI"))
            {
                health -= 5;
                if(health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        hits++;
    }
}