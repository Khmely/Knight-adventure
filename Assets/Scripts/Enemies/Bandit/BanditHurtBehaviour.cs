using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditHurtBehaviour : BanditBaseFSM {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
		if (NPCScriptRef.m_health == 0 || NPCScriptRef.m_health < 0) {
            NPCScriptRef.SetIdle ();
			NPC.GetComponent <CapsuleCollider2D> ().enabled = false;
			Transform[] childLayers = new Transform[NPC.transform.childCount];

			for (int i = 0; i < childLayers.Length; i++) {
				NPC.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
			}
            
            animator.SetBool(EnemyAnimation.TransitionCoditions.Die, true);
        }
    }
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool(EnemyAnimation.TransitionCoditions.Hurt, false);
        GameManager.score += 20;
    }
}
