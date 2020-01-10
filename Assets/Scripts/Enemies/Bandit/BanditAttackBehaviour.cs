using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditAttackBehaviour : BanditBaseFSM {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
		animator.SetBool (EnemyAnimation.TransitionCoditions.AtkIdle, false);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (Vector3.Distance(animator.transform.position, PlayerObject.transform.position) > 3) {
			animator.SetBool (EnemyAnimation.TransitionCoditions.Attack, false);
		}
	}
}
