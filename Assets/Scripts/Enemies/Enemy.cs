using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType {
    BANDITS
};

public class Enemy : MonoBehaviour {

    public float m_speed;
    public EnemyType m_type;
    public int m_damage;
    public int DamageByPlayer;
    public int MAX_HEALTH;

    [HideInInspector]
    public bool isCollidingWithObstacle;
    [HideInInspector]
    public int m_health;
    float currentSpeed;
    Vector2 m_direction;
    float groundRaySize;

    Rigidbody2D m_enemyRb;
    CapsuleCollider2D m_collider;
    Animator m_animator;
    GameManager gameManagerScript;

    void Start() {
        currentSpeed = m_speed;
        m_health = MAX_HEALTH;
        m_direction = new Vector2(transform.right.x, transform.right.y);
    }

    void Awake () {
        m_animator = GetComponent <Animator> ();
        m_enemyRb = GetComponent <Rigidbody2D> ();
        m_collider = GetComponent <CapsuleCollider2D> ();
        m_animator.SetBool(EnemyAnimation.TransitionCoditions.Walk, true);
        groundRaySize = m_collider.bounds.size.y * 0.75f;
    }

    public void Move () {
        if (m_health > 0) {
            m_enemyRb.velocity = m_direction * currentSpeed;
        }
    }

    public void FlipEnemy () {
		Vector2 localScale = m_enemyRb.transform.localScale;
		localScale.x *= -1;
        transform.localScale = localScale;
        m_direction = new Vector2(localScale.x , transform.right.y);
    }

    public void CheckGroundEnds () {
        LayerMask targetLayer = 1 << LayerMask.NameToLayer("Platform");
        Vector2 bounds = m_collider.bounds.size;
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
                Vector2 localScale = m_enemyRb.transform.localScale;
                if (localScale.x != 1)
                {
                    localScale.x = 1;
                    transform.localScale = localScale;
                }
                if (m_direction.x != 1)
                {
                    m_direction = new Vector2(localScale.x, transform.right.y);
                }
            }

            if (RightRay.collider == null)
            {
                Vector2 localScale = m_enemyRb.transform.localScale;
                if (localScale.x != -1)
                {
                    localScale.x = -1;
                    transform.localScale = localScale;
                }
                if (m_direction.x != -1)
                {
                    m_direction = new Vector2(localScale.x, transform.right.y);
                }
            }
        }
        else {
            if (LeftRay.collider == null)
            {
                Vector2 localScale = m_enemyRb.transform.localScale;
                if (localScale.x != 1)
                {
                    localScale.x = 2;
                    transform.localScale = localScale;
                }

                if (m_direction.x != 1)
                {
                    m_direction = new Vector2(localScale.x, transform.right.y);
                }
            }

            if (RightRay.collider == null)
            {
                Vector2 localScale = m_enemyRb.transform.localScale;
                if (localScale.x != -1)
                {
                    localScale.x = -2;
                    transform.localScale = localScale;
                }
                if (m_direction.x != -1)
                {
                    m_direction = new Vector2(localScale.x, transform.right.y);
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
        m_health -= DamageByPlayer + PlayerMovement.ATTACK;
        if (m_health <= 0) {
            m_animator.SetBool(EnemyAnimation.TransitionCoditions.Die, true);
            SoundManager.PlaySound("kill");
        }
        SetIdle ();
        m_animator.SetBool (EnemyAnimation.TransitionCoditions.Hurt, true);
    }

    void DisableHurt () {
         m_animator.SetBool(EnemyAnimation.TransitionCoditions.Hurt, false);
    }

    public void SetIdle () {
        m_enemyRb.velocity = Vector2.zero;
    }

    public void IdleDelay () {
        StartCoroutine("IdleDelayCR");
    }

    IEnumerator IdleDelayCR () {
        yield return new WaitForSeconds(2);
        m_animator.SetBool (EnemyAnimation.TransitionCoditions.Walk, true);
    }

    void DisableOnDead () {
        gameObject.SetActive(false);        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (m_health > 0) {
            if (other.gameObject.tag == "Player Main Collider") {
                StopCoroutine("IdleDelayCR");
                m_animator.SetBool(EnemyAnimation.TransitionCoditions.AtkIdle, true);
            }
            if (other.gameObject.tag == "Ground&Obstacles") {
                isCollidingWithObstacle = true;
                m_animator.SetBool (EnemyAnimation.TransitionCoditions.Idle, true);
            }
            if (other.gameObject.tag == "PlayerSword") {
                StopCoroutine("IdleDelayCR");
                if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle")) {
                    m_animator.SetBool(EnemyAnimation.TransitionCoditions.AtkIdle, true);
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