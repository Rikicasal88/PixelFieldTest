using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter, IPunInstantiateMagicCallback
{
    public delegate void PlayerHpChanged(float hp);
    public event PlayerHpChanged PlayerHpChangedEvent;

    public Camera Camera;
    protected PhotonView photonView;
    [SerializeField] protected Animator animator;

    public override float Health
    {
        get
        {
            return base.Health;

        }
        set
        {
            base.Health = value;
            PlayerHpChangedEvent?.Invoke(maxHealth / value);
        }  
    }

    protected override void Awake()
    {
        base.Awake();
        
    }
    public override void Move(Vector3 dir)
    {
        photonView.RPC("Move_RPC", RpcTarget.All, dir);
    }

    [PunRPC]
    void Move_RPC(Vector3 dir)
    {
        rb.AddForce(transform.TransformDirection(dir) * speed * 100, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        animator.SetFloat("Z", dir.z);
        animator.SetFloat("X", dir.x);
    }

    public override void Rotate(float angles)
    {
        photonView.RPC("Rotate_RPC", RpcTarget.All, angles);
    }

    [PunRPC]
    void Rotate_RPC(float angles)
    {
        transform.Rotate(new Vector3(0, angles, 0));
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.LogError("Done");
        photonView = PhotonView.Get(this);
        if (!photonView.IsMine)
        {
            GameManager.Instance.SetOtherPlayer(this);
        }
        else
        {
            UIManager.Instance.PlayerReady(this);
        }
    }
}
