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
    [SerializeField] private CanvasGroup playerInputPanel; 
    private void Awake()
    {
        Instance = this;
    }

    public void SwitchCamera()
    {
        switch (GameManager.Instance.SwitchCamera())
        {
            case ViewType.PlayerView:
                playerInputPanel.alpha = 1;
                playerInputPanel.interactable = true;
                playerInputPanel.blocksRaycasts = true;
                break;
            case ViewType.ARView:
                playerInputPanel.alpha = 0;
                playerInputPanel.interactable = false;
                playerInputPanel.blocksRaycasts = false;
                break;
        }
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
