using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MonsterMove : MonoBehaviour
{
    public static MonsterMove mm;
    Animator anim;
    float jumpSpeed = 10f;
    public Transform endPos;

    void Start()
    {
        if(mm == null)
        {
            mm = this;
        }
        else if(mm != this)
        {
            Destroy(gameObject);
        }

        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void MonsterJump()
    {
        anim.SetTrigger("Jump");
        transform.position = Vector3.Lerp(transform.position, endPos.position, jumpSpeed * Time.deltaTime);
    }
}
