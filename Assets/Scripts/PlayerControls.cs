using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    private Vector2 _dir;
    public float speed;

    public Animator _swordAnimator;
    public GameObject sword;

    public Animator _bowAnimator;
    public GameObject bow;

    private bool _swordInUse = true;

    public GameObject arrow;
    public GameObject firepoint;
    public float arrow_speed;

    private float _bowTimer = 0f;
    public float delayBow;
    private bool _canUseBow = true;


    public Image iconSword;
    public Image iconBow;

    private bool _mouseRight;

    private bool _isPause;
    public GameObject menu;

    public AudioManager audioManager;

    public TMP_Text textKills;
    public string initial_text;
    public float _killCounter = 0;




    private void Awake()
    {
       //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
       GameManager.Instance.player = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mouseRight = true;
        _isPause = false;

        iconSword.color = new Color(1f, 1f, 0f, 0.1f);
        iconBow.color = new Color(1f, 1f, 1f, 0.1f);
    }

    private void Update()
    {
        textKills.text = initial_text + _killCounter;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_dir.normalized.x * speed, _dir.normalized.y * speed);
        //if (_rb.velocity.x > 0f) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        //else if (_rb.velocity.x < 0f) transform.eulerAngles = new Vector3(0f, 180f, 0f);

        if (_mouseRight) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else transform.eulerAngles = new Vector3(0f, 180f, 0f);

        if (_bowTimer > 0)
        {
            _bowTimer -= Time.fixedDeltaTime;
            if (_bowTimer < 0) _canUseBow = true; 
        }
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
        if (ctx.performed)
        {
            if (_swordInUse) Attack();
            else
            {
                BowAttack();
            }
        }
    }

    public void onMouse(InputAction.CallbackContext ctx)
    {
        if (!_isPause)
        {
            Vector3 mousePos = ctx.ReadValue<Vector2>();
            //mousePos.z = Camera.main.nearClipPlane;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            _mouseRight = mousePos.x >= transform.position.x ? true : false;
        }
    }

    public void onPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) Pause();
    }

    public void onSwitchWeapon(InputAction.CallbackContext ctx)
    {
        _swordInUse = !_swordInUse;
        if (_swordInUse)
        {
            sword.SetActive(true);
            bow.SetActive(false);
            iconSword.color = new Color(1f, 1f, 0f, 0.1f);
            iconBow.color = new Color(1f, 1f, 1f, 0.1f);
        }
        else
        {
            sword.SetActive(false);
            bow.SetActive(true);
            iconBow.color = new Color(1f, 1f, 0f, 0.1f);
            iconSword.color = new Color(1f, 1f, 1f, 0.1f);
        }
    }

    private void Attack()
    {
        if (!_isPause)
        {
            _swordAnimator.SetTrigger("attack");
            sword.GetComponent<SwordScript>().DetectColliders();
            audioManager.PlaySFX(audioManager.swordAttack);
        }
    }

    private void BowAttack()
    {
        if (!_isPause && _canUseBow)
        {
            var quaternion = Quaternion.identity;
            if (_mouseRight) quaternion = Quaternion.Euler(0, 0, -90);
            else quaternion = Quaternion.Euler(0, 0, 90);

            GameObject newArrow = Instantiate(arrow, firepoint.transform.position, quaternion);
            newArrow.GetComponent<Rigidbody2D>().velocity = new Vector2((firepoint.transform.position.x - transform.position.x), 0f) * arrow_speed;
            Destroy(newArrow, 1.5f);

            _bowAnimator.SetTrigger("attack");
            audioManager.PlaySFX(audioManager.bowAttack);

            _canUseBow = false;
            _bowTimer = delayBow;
        }
    }

    public void Pause()
    {
        _isPause = !_isPause;
        if (_isPause)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
            audioManager.musicSource.Pause(); // background music

        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
            audioManager.musicSource.UnPause();
        }
    }


}
