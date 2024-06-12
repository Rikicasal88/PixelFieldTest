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

}
