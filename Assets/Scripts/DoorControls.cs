using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour
{
    public GameObject DoorOpen;
    public GameObject DoorClose;
    public GameObject keyOpen;
    public GameObject keyClose;

    public bool _doorOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (_doorOpen)
        {
            DoorClose.SetActive(false);
            keyClose.SetActive(false);
            DoorOpen.SetActive(true);
            keyOpen.SetActive(true);
        }
        else
        {
            DoorClose.SetActive(true);
            keyClose.SetActive(true);
            DoorOpen.SetActive(false);
            keyOpen.SetActive(false);
        }
        
    }
}
