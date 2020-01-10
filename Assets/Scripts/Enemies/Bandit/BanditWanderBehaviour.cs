using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWanderBehaviour : BanditBaseFSM {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);

		if (NPCScriptRef.isCollidingWithObstacle) {
			NPCScriptRef.FlipEnemy();
			NPCScriptRef.isCollidingWithObstacle = false;
		}
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		NPCScriptRef.Move();
		NPCScriptRef.CheckGroundEnds ();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool(EnemyAnimation.TransitionCoditions.Walk, false);
	}
}
