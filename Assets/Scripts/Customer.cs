using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public int productCost = 0;
    public Transform shelfTarget;

    Vector3 target;

    int wavePointIndex;

    Rigidbody2D rb;
    Animator animator;
    Vector2 movement;

    [SerializeField] int moveSpeed;

    bool isContinue;

    float waitTime;
    [SerializeField] float countdownTime;

    [SerializeField] LayerMask humanLayerMask;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        wavePointIndex = -1;
        GetNextWaypoint();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target) < .5f && isContinue)
        {
            GetNextWaypoint();
        }
        else
        {
            if (!isContinue)
            {
                if (wavePointIndex == 1)
                {
                    target = new Vector2(shelfTarget.position.x, shelfTarget.position.y - 2f);

                    if (Vector2.Distance(transform.position, target) < .5f)
                    {
                        productCost = shelfTarget.GetComponent<Shelf>().UseProducts();

                        waitTime -= Time.fixedDeltaTime;

                        if (productCost > 0)
                        {
                            target = Waypoints.points[wavePointIndex].position;
                            isContinue = true;
                        }
                        else if (waitTime < 0)
                        {
                            wavePointIndex = Waypoints.points.Length - 2;
                            target = Waypoints.points[wavePointIndex].position;
                            isContinue = true;
                        }
                    }
                }
                else if (wavePointIndex == 3 && productCost > 0)
                {
                    if (Vector2.Distance(transform.position, target) < .5f)
                    {
                        waitTime -= Time.fixedDeltaTime;

                        if (waitTime < 0 || shelfTarget == null)
                        {
                            isContinue = true;
                            Checkout.Instance.customer = null;
                        }
                        else if (Checkout.Instance.customer == null)
                        {
                            Checkout.Instance.customer = this;
                        }
                    }
                }
                else
                {
                    isContinue = true;
                }
                animator.SetFloat("MoveSpeed", 0);
            }
            if (Vector2.Distance(transform.position, target) > .5f)
            {
                movement.x = (target.x - transform.position.x) / Vector2.Distance(target, transform.position);
                movement.y = (target.y - transform.position.y) / Vector2.Distance(target, transform.position);

                Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)(movement), new Vector2(2, 2), 0, humanLayerMask);

                if (colliders.Length < 2)
                {
                    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

                    animator.SetFloat("MovementY", movement.y);
                    animator.SetFloat("MoveSpeed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
                }
                else
                {
                    animator.SetFloat("MoveSpeed", 0);
                }
            }
        }
    }

    void GetNextWaypoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavePointIndex++;
        target = Waypoints.points[wavePointIndex].position;
        isContinue = false;
        waitTime = countdownTime * (DayManeger.instance.dayLengthInSeconds / 24);
    }
}
