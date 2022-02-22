using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComp;
    private int superJumpsRemaining;
    // private bool waterTouched = false;
    // private bool isGrounded;
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    // private float initialverticalInput;
    // private float currentverticalInput;

    // Start is called before the first frame update
    void Start()
    {
       rigidbodyComp = GetComponent<Rigidbody>(); 
    //    initialverticalInput = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space key was pressed!!");
            jumpKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        // currentverticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate(){
        // if (!isGrounded){
        //     return;
        // }

        rigidbodyComp.velocity = new Vector3(horizontalInput, rigidbodyComp.velocity.y, 0);

        // if (waterTouched){
        //     Time.timeScale = 0;
        //     return;
        // }

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0){
            return;
        }
        
        if (jumpKeyPressed){
            float jumpPower = 5f;
            if (superJumpsRemaining > 0){
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComp.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }
    }

    // private void OnCollisionEnter(Collision collision){
    //     isGrounded = true;
    // }

    // private void OnCollisionExit(Collision collision){
    //     isGrounded = false;
    // }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer == 7){
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

    // private void OnTriggerWater(Collider water){
    //     if (water.gameObject.layer == 8){
    //         waterTouched = true;
    //     }
    // }
}
