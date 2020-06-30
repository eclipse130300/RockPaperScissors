using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAbilities : MonoBehaviour
{
    public KeyCode abilityKey = KeyCode.E;

    public CharacterController2D characterController2D;
    public PlayerTransformator playerTransformator;
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;
    private IDamagable damagable;

    public float jumpMultiplier;
    private Vector2 jumpVelocity;

    [SerializeField] float invulnerabilityDuration;
    [SerializeField] float cutDistance;
    [SerializeField] float cutSphereRadius = 0.3f;

    private Animator animator;
    public bool canUseAbility = true;
    private DateTime timeToUnclockAbility;

    //test
    Vector3 pointToCheck;
    public Vector3 veloc;

    float JumpValue
    {
        get
        {
            float val = characterController2D.m_Grounded ? 0 : 10;
            return val;
        }
    }

    private void Awake()
    {
        characterController2D = GetComponent<CharacterController2D>();
        playerTransformator = GetComponent<PlayerTransformator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damagable = GetComponent<IDamagable>();
    }


    // Update is called once per frame
    void Update()
    {
        canUseAbility = timeToUnclockAbility <= DateTime.Now;


        if (Input.GetKeyDown(abilityKey) && canUseAbility)
        {

            switch (playerTransformator.activeForm.ABILITY_TYPE)
            {
                case ABILITY_TYPE.CUT:
                    Cut();
                    break;
                case ABILITY_TYPE.INVULNERABLILITY:
                    StartCoroutine(Invulnerability(invulnerabilityDuration));
                    break;
                case ABILITY_TYPE.SOAR:
                    Soar();
                    break;
            }

            timeToUnclockAbility = DateTime.Now.AddSeconds(playerTransformator.activeForm.AbilityCoolDown);
        }
    }

    private void FixedUpdate()
    {
        veloc = rigidbody2D.velocity;
        animator.SetFloat("Jump", JumpValue);
    }

    private void Soar()
    {
        if (!characterController2D.m_Grounded)
        {
            animator.SetTrigger("StartAbility");
            /*            animator.Play("ToAbility");*/
            StartCoroutine(SoaringDown());
        }
            /*rigidbody2D.gravityScale = soarGravity;*/

    }

    IEnumerator SoaringDown()
    {
        while (rigidbody2D.velocity.y > 0)
        {
            yield return new WaitForFixedUpdate();
        }



        /*while (rigidbody2D.velocity.y != 0)*/
        while (!characterController2D.m_Grounded)
        {
/*            Debug.Log("ДО" + rigidbody2D.velocity.y);*/

            jumpVelocity = rigidbody2D.velocity;
            jumpVelocity.y = rigidbody2D.velocity.y * 0.9f;   /*jumpVelocity.y + jumpMultiplier;*/
            rigidbody2D.velocity = jumpVelocity;

/*            Debug.Log("ПОСЛЕ" + rigidbody2D.velocity.y);*/

            yield return new WaitForFixedUpdate();
        }

        animator.SetTrigger("EndAbility");
    }

    IEnumerator Invulnerability(float duration)
    {
        damagable.isInvulnerable = true;
        animator.SetTrigger("StartAbility");


        yield return new WaitForSeconds(duration);

        animator.SetTrigger("EndAbility");
        damagable.isInvulnerable = false;
    }

    private void Cut()
    {
        animator.SetTrigger("StartAbility");

        var distance = GetComponent<CircleCollider2D>().radius + cutDistance;

        /*var*/ pointToCheck = characterController2D.m_FacingRight == true ?
            transform.position + Vector3.right * distance :
            transform.position +  -Vector3.right * distance;

        animator.SetTrigger("EndAbility");

        var colliders = Physics2D.OverlapCircleAll(pointToCheck, 0.1f);
         foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.GetComponent<ITakeDamage>() != null)
            {
                var damagable = collider.gameObject.GetComponent<ITakeDamage>();
                damagable.TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rigidbody2D.velocity);
        Gizmos.DrawLine(transform.position, pointToCheck);
        Gizmos.color = Color.green;
    }
}

public enum ABILITY_TYPE
{
    CUT,
    SOAR,
    INVULNERABLILITY
}
