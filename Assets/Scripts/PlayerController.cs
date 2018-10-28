using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public delegate void OnHitSpikeAction();
    public delegate void OnHitGoombaAction();

    public delegate void OnHitOrbAction();

    public OnHitGoombaAction OnHitGoomba;
    public OnHitSpikeAction OnHitSpike;
    public OnHitOrbAction OnHitOrb;

    float speed = 100;

    Vector3 leftBound;
    Vector3 rightBound;

    bool canJump;


	void Start () {
		if (Physics2D.gravity.y > 0)
        {
            Physics2D.gravity *= -1;
        }
	}
	

	void Update () {
        processInput();
	}

    void processInput()
    {
        if(Input.GetKey("left") || Input.GetKey("a"))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * Time.deltaTime);
        }

        if(Input.GetKey("right") || Input.GetKey("d"))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown("up") || Input.GetKeyDown("w") || Input.GetKeyDown("space"))
        {
            InvertGravity();
        }
    }

    void InvertGravity()
    {
        Physics2D.gravity *= -1;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bounds")
        {
            canJump = true;
        }

        if(collision.gameObject.GetComponent<SpikeController>() != null)
        {
            if(OnHitSpike != null)
            {
                OnHitSpike();
            }
        }

        else if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

            if(this.transform.position.y > enemy.transform.position.y + enemy.GetComponent<BoxCollider2D>().size.y / 2)
            {
                GameObject.Destroy(collision.gameObject);

                if(OnHitGoomba != null)
                {
                    OnHitGoomba();
                }
            }
            else
            {
                if (OnHitSpike != null)
                {
                    OnHitSpike();
                }
            }
        }
        else if(collision.gameObject.GetComponent<OrbController>() != null)
        {
            if(OnHitOrb != null)
            {
                OnHitOrb();
            }
        }
    }
}
