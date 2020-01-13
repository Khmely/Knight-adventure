using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditAtkIdleBehaviour : BanditBaseFSM {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
		NPCScriptRef.SetIdle();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!NPCScriptRef.isCollidingWithObstacle) {
			NPCScriptRef.CheckIfEnemyHasToTurn (PlayerObject.transform.position);
		}
		if (Vector2.Distance(animator.transform.position,PlayerObject.transform.position) < 2) {
			animator.SetBool (EnemyAnimation.TransitionCoditions.Attack, true);
		} else if (Vector2.Distance(animator.transform.position,PlayerObject.transform.position) > 5) {
			animator.SetBool (EnemyAnimation.TransitionCoditions.Walk, true);
		} 
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool (EnemyAnimation.TransitionCoditions.AtkIdle, false);
	}
}
