using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Health health;
            if (health = collision.collider.GetComponent<Health>())
            {
                // The sender is not the arrow itself, it is player parent
                health.GetHit(1, GameManager.Instance.player.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
