using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AnimationManager : MonoBehaviour
{


    [SerializeField] GameObject[] _animator;
    [SerializeField] Animator _currentAnimator;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        _currentAnimator = _animator[0].GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Animator GetAnimator() { return _currentAnimator; }

    public void ChangeCurrentAnimator(int i)
    {
        _currentAnimator.enabled = false;
        index = i;
        _currentAnimator = _animator[index].GetComponent<Animator>();
        _currentAnimator.enabled = true;

    }

    public void attackAnim(bool a)
    {
        _currentAnimator.SetBool("attack", a);


    }

    public void idleAnim(bool a)
    {
        _currentAnimator.SetBool("idle", a);
    }

    public void rechargeAnim(bool a)
    {
        _currentAnimator.SetBool("recharge", a);

    }
}
