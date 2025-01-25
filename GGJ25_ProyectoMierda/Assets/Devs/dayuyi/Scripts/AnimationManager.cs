using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void attackAnim(bool a)
    {
        _animator.SetBool("attack", a);
    }

    public void idleAnim(bool a)
    {
        _animator.SetBool("idle", a);
    }

    public void rechargeAnim(bool a)
    {
        _animator.SetBool("recharge", a);

    }
}
