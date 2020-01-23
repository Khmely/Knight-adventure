using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType {
    LightSwordsman,
    Knight
};

public class Enemy : MonoBehaviour {

    public float speed;
    public EnemyType type;
    public static int damage = 15;
    public int MAX_HEALTH;

    [HideInInspector]
    public bool isCollidingWithObstacle;
    [HideInInspector]
    public int health;
    float currentSpeed;
    Vector2 direction;
    float groundRaySize;

    Rigidbody2D enemyRb;
    CapsuleCollider2D collider;
    Animator animator;
    GameManager gameManagerScript;

    void Start() {
        currentSpeed = speed;
        health = MAX_HEALTH;
        direction = new Vector2(transform.right.x, transform.right.y);
    }

    void Awake () {
        animator = GetComponent <Animator> ();
        enemyRb = GetComponent <Rigidbody2D> ();
        collider = GetComponent <CapsuleCollider2D> ();
        animator.SetBool(EnemyAnimation.TransitionCoditions.Walk, true);
        groundRaySize = collider.bounds.size.y * 0.75f;
    }

    public void Move () {
        if (health > 0) {
            enemyRb.velocity = direction * currentSpeed;
        }
    }

    public void FlipEnemy () {
		Vector2 localScale = enemyRb.transform.localScale;
		localScale.x *= -1;
        transform.localScale = localScale;
        direction = new Vector2(localScale.x , transform.right.y);
    }

    public void CheckGroundEnds () {
        LayerMask targetLayer = 1 << LayerMask.NameToLayer("Platform");
        Vector2 bounds = collider.bounds.size;
        Vector2 origin1 = new Vector2 ( transform.position.x + bounds.x,  transform.position.y);
        Vector2 origin2 = new Vector2 (transform.position.x - bounds.x, transform.position.y);

        RaycastHit2D groundCheckRayRight = Physics2D.Raycast (origin1, Vector2.down, groundRaySize, targetLayer);
        RaycastHit2D groundCheckRayLeft = Physics2D.Raycast (origin2, Vector2.down, groundRaySize, targetLayer);

        if ((groundCheckRayRight.collider == null || groundCheckRayLeft.collider == null)) {
            CheckAndFlip (groundCheckRayLeft, groundCheckRayRight);
        }
    }

    void CheckAndFlip (RaycastHit2D LeftRay, RaycastHit2D RightRay) {
        if (gameObject.tag == "Enemy")
        {
            if (LeftRay.collider == null)
            {
                Vector2 localScale = enemyRb.transform.localScale;
                if (localScale.x != 1)
                {
                    localScale.x = 1;
                    transform.localScale = localScale;
                }
                if (direction.x != 1)
                {
                    direction = new Vector2(localScale.x, transform.right.y);
                }
            }

            if (RightRay.collider == null)
            {
                Vector2 localScale = enemyRb.transform.localScale;
                if (localScale.x != -1)
                {
                    localScale.x = -1;
                    transform.localScale = localScale;
                }
                if (direction.x != -1)
                {
                    direction = new Vector2(localScale.x, transform.right.y);
                }
            }
        }
        else {
            if (LeftRay.collider == null)
            {
                Vector2 localScale = enemyRb.transform.localScale;
                if (localScale.x != 1)
                {
                    localScale.x = 2;
                    transform.localScale = localScale;
                }

                if (direction.x != 1)
                {
                    direction = new Vector2(localScale.x, transform.right.y);
                }
            }

            if (RightRay.collider == null)
            {
                Vector2 localScale = enemyRb.transform.localScale;
                if (localScale.x != -1)
                {
                    localScale.x = -2;
                    transform.localScale = localScale;
                }
                if (direction.x != -1)
                {
                    direction = new Vector2(localScale.x, transform.right.y);
                }
            }
        }
    }

    public void CheckIfEnemyHasToTurn (Vector2 playerPosition) {
        if (playerPosition.x > transform.position.x && transform.localScale.x == -1) {
            FlipEnemy ();
        }
        if (playerPosition.x < transform.position.x && transform.localScale.x == 1) {
            FlipEnemy ();
        }
    }

    void TakeDamage () {
        health -= PlayerMovement.ATTACK;
        if (health <= 0) {
            animator.SetBool(EnemyAnimation.TransitionCoditions.Die, true);
            SoundManager.PlaySound("kill");
        }
        SetIdle ();
        animator.SetBool (EnemyAnimation.TransitionCoditions.Hurt, true);
    }

    void DisableHurt () {
         animator.SetBool(EnemyAnimation.TransitionCoditions.Hurt, false);
    }

    public void SetIdle () {
        enemyRb.velocity = Vector2.zero;
    }

    public void IdleDelay () {
        StartCoroutine("IdleDelayCR");
    }

    IEnumerator IdleDelayCR () {
        yield return new WaitForSeconds(2);
        animator.SetBool (EnemyAnimation.TransitionCoditions.Walk, true);
    }

    void DisableOnDead () {
        gameObject.SetActive(false);        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (health > 0) {
            if (other.gameObject.tag == "Player Main Collider") {
                StopCoroutine("IdleDelayCR");
                animator.SetBool(EnemyAnimation.TransitionCoditions.AtkIdle, true);
            }
            if (other.gameObject.tag == "Ground&Obstacles") {
                isCollidingWithObstacle = true;
                animator.SetBool (EnemyAnimation.TransitionCoditions.Idle, true);
            }
            if (other.gameObject.tag == "PlayerSword") {
                StopCoroutine("IdleDelayCR");
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle")) {
                    animator.SetBool(EnemyAnimation.TransitionCoditions.AtkIdle, true);
                }
                TakeDamage();
            }
        }

    }
    void OnTriggerExit2D (Collider2D other) {
       if (other.gameObject.tag == "Ground&Obstacles") {
            isCollidingWithObstacle = false;
       }
    }
}