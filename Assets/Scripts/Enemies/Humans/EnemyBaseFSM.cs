using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour {

	protected GameObject NPC;
	protected Enemy NPCScriptRef;
	protected GameObject PlayerObject;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		NPC = animator.gameObject;
		NPCScriptRef = NPC.GetComponent<Enemy> ();
		PlayerObject = GameObject.FindGameObjectWithTag("Player");
	}
}
