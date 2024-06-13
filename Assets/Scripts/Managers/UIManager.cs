using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void AttackInput();
    public event AttackInput AttackInputEvent;


    public static UIManager Instance;
    public Joystick PlayerMovementJoysStick;
    public Joystick PlayerRotationJoysStick;
    public Image HpBar;
    private void Awake()
    {
        Instance = this;
    }

    public void SwitchCamera()
    {
        GameManager.Instance.SwitchCamera();
    }

    public void PlayerReady(PlayerCharacter player)
    {
        player.PlayerHpChangedEvent += UpdateHp;
    }

    public void UpdateHp(float hp)
    {
        HpBar.fillAmount = hp;
    }

    public void Attack()
    {
        AttackInputEvent?.Invoke();
    }
}
