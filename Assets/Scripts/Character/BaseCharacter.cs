using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Windows;

public class BaseCharacter : MonoBehaviour
{
    protected Rigidbody rb;

    [SerializeField] protected float speed = 10;
    [SerializeField] protected float maxSpeed = 10;
    [SerializeField] protected float _health = 10;
    [SerializeField] protected float maxHealth = 10;
    [SerializeField] protected bool isAttacking = false;

    [SerializeField] protected Animator animator;
    public virtual float Health 
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public virtual void Move(Vector3 dir)
    {
    }

    public virtual void Rotate(float angles)
    {
    }

    public virtual void Attack()
    {
    }
    public void AttackCpmpleted()
    {
        isAttacking = false;
    }

}
