using UnityEngine;

public class AIMotion : MonoBehaviour
{
    public float speed = 5;
    public Vector2 direction;

    void Start()
    {
        direction = new Vector2(1, 1);
    }

    void Update()
    {
        direction.Normalize();
        Vector3 oldPosition = transform.position;
        Vector2 newPosition = speed * direction * Time.deltaTime + new Vector2(oldPosition.x, oldPosition.y);
        Vector3 temp = new Vector3(newPosition.x, newPosition.y, oldPosition.z);
        transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (col.tag == "AI")
        {
            transform.localScale *= 0.8f;
            if(Mathf.Abs(col.transform.position.x - transform.position.x) > Mathf.Abs(col.transform.position.y - transform.position.y))
            {
                direction.x *= -1;
            }
            else
            {
                direction.y *= -1;
            }
        }
        if (col.tag == "TopWall")
        {
            spriteRenderer.color = new Color(255,0,0,1);
            direction.y *= -1;
        }
        if(col.tag == "RightWall")
        {
            spriteRenderer.color = new Color(0, 255, 0,1);
            direction.x *= -1;
        }
        if(col.tag == "LeftWall")
        {
            spriteRenderer.color = new Color(0, 0, 255, 1);
            direction.x *= -1;
        }
        if(col.tag == "BottomWall")
        {
            spriteRenderer.color = new Color(255, 255, 255, 0.3f);
            direction.y *= -1;
        }
    }
}