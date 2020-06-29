using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRover : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public Vector3 targetPoint; //
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    [SerializeField] float maxDistance = 1;
    [SerializeField] float minDistance = 0.5f;
    [SerializeField] float roamSpeed = 1f;

    public LayerMask whatIsGround;

    public bool isFalling = false;
    private bool readyToRoam = false;

    private Vector3 currentDir;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, targetPoint) >= 0.2f)
        {
            var direction = (targetPoint - transform.position).normalized;

            transform.position += direction * Time.deltaTime * roamSpeed;
        }
        else
        {
            SetRandomPoint();
        }

        if (!isFalling)
        {
            if (FallCliffCheck())
            {
                SetOppositePoint();
                StartCoroutine(WaitToStayOntheGround());
            }
        }
    }

    private bool FallCliffCheck()
    {
        //right point
       Collider2D[] rightColliders = Physics2D.OverlapCircleAll(transform.position + Vector3.right * boxCollider.size.x / 2 +
            Vector3.down * boxCollider.size.y / 2 ,    0.1f, whatIsGround);

        //left point
        Collider2D[] leftColliders = Physics2D.OverlapCircleAll(transform.position + -Vector3.right * boxCollider.size.x / 2 +
            Vector3.down * boxCollider.size.y / 2,    0.1f , whatIsGround);

        if (rightColliders.Length == 0 || leftColliders.Length == 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator WaitToStayOntheGround()
    {
        isFalling = true;

        while (FallCliffCheck() != false)
        {
            FallCliffCheck();
            yield return null;
        }

        isFalling = false;
    }

    void SetOppositePoint()
    {
        currentDir = currentDir == Vector3.right ?
            -Vector3.right :
            Vector3.right;

        spriteRenderer.flipX = !spriteRenderer.flipX;

        var RandomDistance = UnityEngine.Random.Range(minDistance, maxDistance);

        targetPoint = transform.position + currentDir * RandomDistance;
    }

    void SetRandomPoint()
    {
        var randomNum = UnityEngine.Random.Range(0, 2);
        currentDir = randomNum == 0 ?
            Vector3.right :
            -Vector3.right;

        spriteRenderer.flipX = currentDir == -Vector3.right;

        var RandomDistance = UnityEngine.Random.Range(minDistance, maxDistance);

        targetPoint = transform.position + currentDir * RandomDistance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetRandomPoint();
        isFalling = false;
        readyToRoam = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, targetPoint);
    }

}
