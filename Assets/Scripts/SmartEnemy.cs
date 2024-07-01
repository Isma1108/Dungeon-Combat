using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    // The first intelligence that the enemy will have is simply persecute the player
    // in a specific area.

    public bool canMove;
    public float speed;
    public Transform target;

    public float playerInvulnerabilityTime = 2.0f;
    private float _lastHitTime;




    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        target = GameManager.Instance.player.transform;
        _lastHitTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * speed;


            if (target.transform.position.x >= transform.position.x) transform.eulerAngles = new Vector3(0f, 0f, 0f);
            else transform.eulerAngles = new Vector3(0f, 180f, 0f);


            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);

            //Vector3 dir = target.position - transform.position;
            //transform.position += dir * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health;
        if (collision.collider.tag == "player")
        {
            if (Time.time - _lastHitTime >= playerInvulnerabilityTime)
            {
                health = collision.collider.GetComponent<Health>();
                //Debug.Log("Hice daño al player");
                health.GetHit(1, gameObject);
                _lastHitTime = Time.time;
            }
        }
    }
}
