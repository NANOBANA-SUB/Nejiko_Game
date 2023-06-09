using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;

    public float gravity;
    public float speedZ;
    public float speedJump;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //地上にいる場合のみ操作を行う
        if (controller.isGrounded)
        {
            //Inputを検知して前に進める
            if (Input.GetAxis("Vertical") > 0.0f)
                moveDirection.z = Input.GetAxis("Vertical") * speedZ;
            else
                moveDirection.z = 0;

            //方向転換
            transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

            //ジャンプ
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = speedJump;
                animator.SetTrigger("jump");
            }
        }
            //毎フレームに重力を追加
            moveDirection.y -= gravity * Time.deltaTime;
            //移動実行
            Vector3 globalDirection = transform.TransformDirection(moveDirection);
            controller.Move(globalDirection * Time.deltaTime);

            //移動後設置したらy方向の速度をゼロにする
            if (controller.isGrounded) moveDirection.y = 0;

        //速度が零以上なら走っているフラグをtrueにする
        animator.SetBool("run", moveDirection.z > 0.0f);
    }
}
