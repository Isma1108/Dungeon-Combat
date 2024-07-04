using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyControls : MonoBehaviour
{
    [SerializeField]
    private GameObject DoorControl;

    private DoorControls _door;

    private float _openTimer;
    private bool _canOpen = true;
    public float delay;

    [SerializeField]
    private TMP_Text textDoor;

    public void Start()
    {
       _door = DoorControl.GetComponent<DoorControls>();
       _openTimer = 0f;
      
       textDoor.enabled = false;
       textDoor.text = "You need to kill 20 enemies ...";
    }

    private void FixedUpdate()
    {

        if (_openTimer > 0)
        {
            _openTimer -= Time.fixedDeltaTime;
            if (_openTimer < 0)
            {
                _canOpen = true;
                textDoor.enabled = false;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "player" && _canOpen)
        {

            if (GameManager.Instance.player._killCounter >= 20)
            {
                _door._doorOpen = !_door._doorOpen;
                _canOpen = false;
                _openTimer = delay;
            }
            else
            {
                textDoor.enabled = true;
                _openTimer = delay;
            }
        }
    }
}
