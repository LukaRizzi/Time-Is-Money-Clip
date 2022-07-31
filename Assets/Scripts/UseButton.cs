using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseButton : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip useAnim;
    [SerializeField] private MonoBehaviour script;
    [SerializeField] private string funcName;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Press()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(useAnim.name))
        {
            animator.Play(useAnim.name);
            script.Invoke(funcName, 0f);
        }
    }
}
