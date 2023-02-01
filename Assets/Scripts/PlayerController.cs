using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int moveSpeed;

    Rigidbody2D rb;
    Animator animator;

    Vector2 movement;

    [SerializeField] float countDownTime;

    public GameObject InteractionObject;

    AudioSource audioSource;

    [SerializeField] AudioClip[] audioClips;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.E) && InteractionObject != null)
        {
            if (InteractionObject.CompareTag("Shelf"))
            {
                InteractionObject.GetComponent<Shelf>().AddProducts();

                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            else if (InteractionObject.CompareTag("Checkout"))
            {
                InteractionObject.GetComponent<Checkout>().GetMoney();

                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
            else if (InteractionObject.CompareTag("ClosedShelf"))
            {
                InteractionObject.GetComponent<ClosedShelf>().OpenShelf();

                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
            else if (InteractionObject.CompareTag("Garbage"))
            {
                Destroy(InteractionObject);
                CustomerSpawner.instance.extreWaitTime -= 0.2f;


                audioSource.clip = audioClips[2];
                audioSource.Play();
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        animator.SetFloat("MovementY", movement.y);
        animator.SetFloat("MoveSpeed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shelf"))
        {
            InteractionObject = other.gameObject;
        }
        else if (other.CompareTag("Checkout"))
        {
            InteractionObject = other.gameObject;
        }
        else if (other.CompareTag("ClosedShelf"))
        {
            InteractionObject = other.gameObject;
        }
        else if (other.CompareTag("Garbage"))
        {
            InteractionObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shelf") || other.CompareTag("Checkout") || other.CompareTag("ClosedShelf") || other.CompareTag("Garbage"))
        {
            InteractionObject = null;
        }
    }
}
