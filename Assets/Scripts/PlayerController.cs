using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // Public

    public CharacterController2D controller;
    public Animator anim;
    public float movementSpeed;
    public BoxCollider2D boxCol;
    public CircleCollider2D circleCol;

    // Private

    float horizontalMovementSpeed;
    bool isJumping;
    bool isHurt;
    int score;
    public Text scoreText;

    // Methods

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        isJumping = false;
        isHurt = false;
        score = 0;
    }

    void Update()
    {
        scoreText.text = score.ToString();

        horizontalMovementSpeed = Input.GetAxisRaw("Horizontal") * movementSpeed;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMovementSpeed));

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
    }

    public void OnLanding() {
        isJumping = false;
        anim.SetBool("isJumping", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            score++;
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Enemy") 
        { 
            if (isJumping)
            {
                Destroy(collision.gameObject);
                score++;
            } else {
                anim.SetBool("isHurt", true);
                boxCol.enabled = false;
                circleCol.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMovementSpeed * Time.fixedDeltaTime, false, isJumping);
    }
}
