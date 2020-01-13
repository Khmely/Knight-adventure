using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	Animator m_animator;
	InputController m_input;

	static class TransitionCoditions {
		public static string moving 	 	= "moving";
		public static string isJumpPressed  = "isJumpPressed";
		public static string isFalling      = "isFalling";
		public static string isGrounded  	= "isGrounded";
		public static string isCrouching 	= "isCrouching";
		public static string isInFlight  	= "isInFlight";
		public static string Attack         = "Attack";
		public static string AttackType     = "AttackType";
        public static string grabCorner     = "grabCorner";
        public static string isHurt         = "isHurt";
        public static string isDead         = "isDead";
	};

	bool prevJumpState;
	int currentAttackT;
	void Awake () {
		m_animator = GetComponent <Animator> ();
		m_input = GetComponent <InputController>();

		currentAttackT = 0;
	}
	void Update () {
		m_animator.SetBool (TransitionCoditions.isGrounded, m_input.isOnGround);
		SetWalk ();
		SetJump ();
		SetCrouch ();
		SetAttack ();
        SetGrabCorner();
        SetClimb();
        SetHurt();
        SetDead();
    }

    void SetDead() {
        m_animator.SetBool(TransitionCoditions.isDead, m_input.isDead);
        //m_input.isDead = false;
    }

	void SetWalk () {
		if (m_input.isOnGround && !m_input.m_crouchPressed) {
			m_animator.SetFloat (TransitionCoditions.moving, Mathf.Abs (m_input.m_horizontal));
		}
	}

	void SetJump () {
		if (m_input.isOnGround && m_input.m_jumpPressed) {
            m_animator.SetBool (TransitionCoditions.isJumpPressed, true);
		} else if (!m_input.isOnGround && m_input.isFalling) {
			m_animator.SetBool (TransitionCoditions.isFalling, true);
		} else {
			m_animator.SetBool (TransitionCoditions.isJumpPressed, false);
			m_animator.SetBool (TransitionCoditions.isFalling, false);
		}

		m_animator.SetBool (TransitionCoditions.isInFlight, m_input.isInFlight);
	}

	void SetCrouch () {
		m_animator.SetBool (TransitionCoditions.isCrouching, m_input.m_crouchPressed);
        if (m_input.m_crouchPressed)
        {
            m_input.grabCorner = false;
        }
    }

	void SetAttack () {

		bool AtkSec = m_animator.GetCurrentAnimatorStateInfo(0).IsName ("Attack State.Attack3");

        if (m_input.isOnGround)
        {
            if (m_input.m_attack1 && !isAttackPrimaryPlaying() && !isAttackSecondaryPlaying())
            {
                currentAttackT = currentAttackT == 0 ? 1 : 0;
                SoundManager.PlaySound("attack");
                InitiateAttack(m_input.m_attack1);
            }
            else if (m_input.m_attack2 && !isAttackSecondaryPlaying() && !isAttackPrimaryPlaying())
            {
                currentAttackT = 2;
                SoundManager.PlaySound("attack");
                InitiateAttack(m_input.m_attack2);
            }
            else
            {
                InitiateAttack(false);
            }
        }
	}

	void InitiateAttack (bool isAtk) {
		m_animator.SetBool (TransitionCoditions.Attack, isAtk);
		if (isAtk) {
			m_animator.SetInteger (TransitionCoditions.AttackType, currentAttackT);
		}
	}

	public bool isAttackPrimaryPlaying () {
		bool AtkPrim1 = m_animator.GetCurrentAnimatorStateInfo(0).IsName ("Attack State.Attack1");
		bool AtkPrim2 = m_animator.GetCurrentAnimatorStateInfo(0).IsName ("Attack State.Attack2");

		return AtkPrim1 || AtkPrim2;
	}

    public bool isAttackSecondaryPlaying()
    {
        bool AtkSec1 = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack State.Attack3");

        return AtkSec1;
    }

    private void SetGrabCorner ()
    {
        m_animator.SetBool(TransitionCoditions.grabCorner, m_input.grabCorner);
    }

    private void SetClimb ()
    {
        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.CornerGrab") && m_input.m_jumpPressed)
        {
            m_input.grabCorner = false;
        }
            
    }

    private void SetHurt ()
    {
        m_animator.SetBool(TransitionCoditions.isHurt, m_input.isHurt);
        m_input.isHurt = false;
    }
}
