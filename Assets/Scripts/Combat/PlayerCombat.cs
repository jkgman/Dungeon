using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CombatBody {
    // Use this for initialization
    void Start () {
        InputController.instance.onButtonInput += CombatProcess;
    }

    void CombatProcess(string axis, InputState state) {
        Melee();
    }
    void Melee() {

    }
}
