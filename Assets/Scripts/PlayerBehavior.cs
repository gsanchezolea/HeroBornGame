﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    
    public float moveSpeed = 100f;
    public float rotateSpeed = 150f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public float bulletSpeed = 100f;
    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    private GameBehavior _gameManager;
     
    void Start()
    { 
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager")
            .GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {        
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            playerJump();
        }

        /*
        this.transform.Translate(Vector3.forward * vInput *
            Time.deltaTime);

        //6
        this.transform.Rotate(Vector3.up * hInput *
            Time.deltaTime);
        */
    }

    //1 
    void FixedUpdate()
    {
        //2
        Vector3 rotation = Vector3.up * hInput;

        //3
        Quaternion angleRot = Quaternion.Euler(rotation *
            Time.fixedDeltaTime);

        //4
        _rb.MovePosition(this.transform.position +
            this.transform.forward * vInput * Time.fixedDeltaTime);
        //5
        _rb.MoveRotation(_rb.rotation * angleRot);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(
                bullet, 
                this.transform.position,
                this.transform.rotation
                ) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }

        
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = 
            new Vector3(
                _col.bounds.center.x, 
                _col.bounds.min.y, 
                _col.bounds.center.z
                );
        bool grounded =
            Physics.CheckCapsule(
                _col.bounds.center,
                capsuleBottom,
                distanceToGround,
                groundLayer,
                QueryTriggerInteraction.Ignore);
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            _gameManager.Lives -= 1;
        }
    }
}
