using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void OnWalk(bool walk)
    {
        anim.SetBool("isWalking", walk);
    }
}
