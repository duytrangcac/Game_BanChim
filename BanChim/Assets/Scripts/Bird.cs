using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float xSpeed;
    public float minYSpeed;
    public float maxYSpeed;

    public GameObject deathVfx;
    Rigidbody2D m_rb;
    bool moveLeftOnStart;
    bool isDead;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
   private void Start()
    {
        RandomMovingDirection();
      
    }

    // Update is called once per frame
    private void Update()
    {
        m_rb.velocity = moveLeftOnStart ?
            new Vector2(-xSpeed, Random.Range(minYSpeed, maxYSpeed))
            
            : new Vector2(xSpeed, Random.Range(minYSpeed, maxYSpeed));
        Flip();
    }
    public void RandomMovingDirection()
    {
        moveLeftOnStart = transform.position.x > 0 ? true : false;
    }
    void Flip()
    {
        if (moveLeftOnStart)
        {
            if (transform.localScale.x < 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (transform.localScale.x > 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
        if (deathVfx)
        {
            Instantiate(deathVfx, transform.position, Quaternion.identity);
        }
        GameManager.Ins.BirdKilled++;
        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);
    }
}
