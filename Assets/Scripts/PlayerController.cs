using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public CharacterController2D controller;
    public Animator anim;
    public float movementSpeed;
    float horizontalMovementSpeed;
    bool isJumping;
    bool isHurt;

    void Start() {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        isJumping = false;
        isHurt = false;
    }

    void Update() {
        horizontalMovementSpeed = Input.GetAxisRaw("Horizontal") * movementSpeed;
        anim.SetFloat("speed", Mathf.Abs(horizontalMovementSpeed));
    }

    private void FixedUpdate() {
        controller.Move(horizontalMovementSpeed * Time.fixedDeltaTime, false, false);
        isJumping = false;
    }
}
