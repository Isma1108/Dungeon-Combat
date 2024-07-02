using System;
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

    private bool _mouseRight;

    private bool _isPause;
    public GameObject menu;

    private AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mouseRight = true;
        _isPause = false;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_dir.normalized.x * speed, _dir.normalized.y * speed);
        //if (_rb.velocity.x > 0f) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        //else if (_rb.velocity.x < 0f) transform.eulerAngles = new Vector3(0f, 180f, 0f);

        if (_mouseRight) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    public void onMovementX(InputAction.CallbackContext ctx)
    {
        _dir.x = ctx.ReadValue<float>();
    }

    public void onMovementY(InputAction.CallbackContext ctx)
    {
        _dir.y = ctx.ReadValue<float>();
    }

    public void onAttack(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed) Attack();
    }

    public void onMouse(InputAction.CallbackContext ctx)
    {
        Vector3 mousePos = ctx.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        _mouseRight = mousePos.x >= transform.position.x ? true : false;
    }

    public void onPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) Pause();
    }

    private void Attack()
    {
        _swordAnimator.SetTrigger("attack");
        sword.GetComponent<SwordScript>().DetectColliders();
        audioManager.PlaySFX(audioManager.swordAttack);
    }

    public void Pause()
    {
        _isPause = !_isPause;
        if (_isPause)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
        }
    }


}
