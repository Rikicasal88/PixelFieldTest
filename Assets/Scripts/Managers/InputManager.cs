using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.PlayerMovementJoysStick.JoyStickinputEvent += PlayerMovementJoysStick_JoyStickinputEvent;
        UIManager.Instance.PlayerRotationJoysStick.JoyStickinputEvent += PlayerRotationJoysStick_JoyStickinputEvent;
        UIManager.Instance.AttackInputEvent += Instance_AttackInputEvent;
    }

    private void Instance_AttackInputEvent()
    {
        GameManager.Instance.PlayerAttack();
    }

    private void PlayerMovementJoysStick_JoyStickinputEvent(Vector2 input)
    {
        GameManager.Instance.MovePlayer(input);
    }

    private void PlayerRotationJoysStick_JoyStickinputEvent(Vector2 input)
    {
        GameManager.Instance.RotatePlayer(input.x);
    }
}
