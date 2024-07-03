using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Transform circleOrigin;
    public float radius;
    public float force = 10000;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }            
        }
    }

}

