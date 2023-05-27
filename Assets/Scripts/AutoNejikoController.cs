using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoNejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //for debug
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        //���X�ɉ�����Z�����ɏ�ɑO�i������
        float accelaratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(accelaratedZ, 0, speedZ);

        //X�����͖ڕW�̃|�W�V�����܂ł̍����̊����ő��x���v�Z
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;

        //���t���[���ɏd�͂�ǉ�
        moveDirection.y -= gravity * Time.deltaTime;

        //�ړ����s
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        //�ړ���ݒu������y�����̑��x���[���ɂ���
        if (controller.isGrounded) moveDirection.y = 0;

        //���x����ȏ�Ȃ瑖���Ă���t���O��true�ɂ���
        animator.SetBool("run", moveDirection.z > 0.0f);
    }

    //���̃��[���Ɉړ����J�n
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    //�E�̃��[���Ɉړ����J�n
    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            animator.SetTrigger("jump");
        }
    }
}