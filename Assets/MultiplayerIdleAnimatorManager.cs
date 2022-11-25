using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerIdleAnimatorManager : StateMachineBehaviour {
    private static readonly int IsBored = Animator.StringToHash("isBored");
    private static readonly int RandomBored = Animator.StringToHash("randomBored");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bool bored = Random.Range(0f, 1f) < 0.06f;
        animator.SetBool(IsBored, bored);
        if (bored) {
            animator.SetInteger(RandomBored, Random.Range(0, 2));
        }
    }
}
