using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleBehaviour : EnemyBaseFSM {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
		NPCScriptRef.FlipEnemy ();
		NPCScriptRef.SetIdle ();
		if (NPCScriptRef.health > 0) {
			NPCScriptRef.IdleDelay();
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool (EnemyAnimation.TransitionCoditions.Idle, false);
	}
}
