using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter, IPunInstantiateMagicCallback
{
    public delegate void PlayerHpChanged(float hp);
    public event PlayerHpChanged PlayerHpChangedEvent;

    public Camera Camera;
    public CinemachineVirtualCamera VCamera;
    protected PhotonView photonView;


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
        transform.Rotate(new Vector3(0, angles * rotationSpeed, 0));
    }

    public override void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            photonView.RPC("Attack_RPC", RpcTarget.All);
        }
    }

    [PunRPC]
    void Attack_RPC()
    {
        animator.SetTrigger("Attack");
        if (PhotonNetwork.IsMasterClient)
        {
            RaycastHit hit;
            int layerMask = 1 << 8;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, layerMask))
            {
                if (hit.collider.tag == "Enemy")
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                }
                else if (hit.collider.tag == "Object")
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                }
            }
        }
    }

    public void SetCamera(bool value)
    {
        Camera.gameObject.SetActive(value);
        VCamera.gameObject.SetActive(value);
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //Debug.LogError("Done");
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
