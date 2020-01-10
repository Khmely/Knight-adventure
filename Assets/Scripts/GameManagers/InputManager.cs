using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class InputManager
{
	public static float Forward () {
		float kb = Input.GetAxis ("Horizontal");

		return kb;
	}

	public static bool Jump() {
		bool kb = Input.GetButtonDown("K_Jump");

		return kb;
	}

	public static bool Crouch() {
		bool kb = Input.GetButton("K_Crouch");

		return kb;
	}

	public static bool AttackPrimary() {
		bool kb = Input.GetButtonDown ("AttackPrimary");

		return kb;
	}

	public static bool AttackSecondary() {
		bool kb = Input.GetButtonDown ("AttackSecondary");

		return kb;
	}

    public static bool JumpPressForGrab (){
        bool kb = Input.GetButton("K_Jump");

        return kb;
    }
}
