using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Joystick PlayerMovementJoysStick;
    public Joystick PlayerRotationJoysStick;

    private void Awake()
    {
        Instance = this;    
    }

    public void SwitchCamera()
    {
        GameManager.Instance.SwitchCamera();
    }
}
