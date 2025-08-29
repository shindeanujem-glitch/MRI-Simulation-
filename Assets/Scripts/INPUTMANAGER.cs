using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INPUTMANAGER : MonoBehaviour
{
    public static INPUTMANAGER _instance;
    public static INPUTMANAGER instance
    {
        get{
            return _instance;
        }
    }

    private PlayerControl playerControls;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        playerControls = new PlayerControl();
    }

    private void OnEnable() 
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetMovementVector()
    {
       return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
       return playerControls.Player.LOOK.ReadValue<Vector2>();
    }


}

