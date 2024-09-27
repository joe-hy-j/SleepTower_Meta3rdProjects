using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MonsterMove : MonoBehaviour
{
    public static MonsterMove mm;
    Animator anim;
    float jumpSpeed = 10f;
    public Transform startPos;
    public Transform endPos;

    bool isJump = false;
    bool isJumpDown;

    private void Awake()
    {
        transform.position = endPos.position;
        MonsterJumpDown();
    }

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
        MiniGameManager.instance.OnVideoEnd += MonsterJumpDown;
    }

    void Update()
    {
        if (isJump)
        {
            transform.position = Vector3.Lerp(transform.position, endPos.position, jumpSpeed * Time.deltaTime);
            if (transform.position.y > endPos.position.y - 0.1f)
            {
                isJump = false;
                transform.position = endPos.position;
                anim.SetTrigger("Idle");
            }
        }
        else if (isJumpDown)
        {
            transform.position = Vector3.Lerp(transform.position, startPos.position, jumpSpeed * Time.deltaTime);
            if (transform.position.y < startPos.position.y + 0.5f)
            {
                isJumpDown = false;
                transform.position = startPos.position;
                anim.SetTrigger("JumpDown");
            }
        }
    }

    public void MonsterJump()
    {
        transform.position = startPos.position;
        anim.SetTrigger("Jump");
        isJump = true;
    }

    public void MonsterJumpDown()
    {
        transform.position = endPos.position;
        isJumpDown = true;
    }
}
