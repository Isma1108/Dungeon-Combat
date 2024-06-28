using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    private Vector2 _dir;
    public float speed;

    public Animator _swordAnimator;
    public GameObject sword;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_dir.normalized.x * speed, _dir.normalized.y * speed);
        if (_rb.velocity.x > 0f) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (_rb.velocity.x < 0f) transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    public void onMovementX(InputAction.CallbackContext ctx)
    {
        _dir.x = ctx.ReadValue<float>();
    }

    public void OnMovementY(InputAction.CallbackContext ctx)
    {
        _dir.y = ctx.ReadValue<float>();
    }

    public void onAttack(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed) Attack();
    }

    private void Attack()
    {
        _swordAnimator.SetTrigger("attack");
        sword.GetComponent<SwordScript>().DetectColliders();
    }




}
