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
        _currentAnimator.gameObject.SetActive(false);
        index = i;
        _currentAnimator = _animator[index].GetComponent<Animator>();
        _currentAnimator.gameObject.SetActive(true);

    }

    public void attackAnim()
    {
        _currentAnimator.SetTrigger("attack");
    }

    public void idleAnim()
    {
        _currentAnimator.SetTrigger("idle");
    }

    public void rechargeAnim()
    {
        _currentAnimator.SetTrigger("reload");
    }
    public void ResetAnim(string anim)
    {
        _currentAnimator.ResetTrigger(anim);
    }
}
