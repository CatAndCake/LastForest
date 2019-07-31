using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Animator anim;
    public Joystick joystick;
    public float horizontalSpeed = 5f;
    public float verticalSpeed = 5f;

    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        float h = joystick.Horizontal * Time.deltaTime * horizontalSpeed;
        float v = joystick.Vertical * Time.deltaTime * verticalSpeed;

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 newPosition = new Vector3(h, 0.0f, v);

        Movement(h, v);
        MovementAnim(h, v);

    }

    void Movement(float h, float v)
    {
        if (ControlManager.canvasPlayer == false && ControlManager.canvasTree == false)
        {
            Vector3 newPosition = new Vector3(h, 0.0f, v);
            transform.LookAt(newPosition + transform.position);
            transform.Translate(newPosition * 25 * Time.deltaTime, Space.World);
        }
    }

    bool MoveOrTurn(float h, float v)
    {
        if (v == 0 && h > 0 || v == 0 && h < 0)

        {
            //Turn
            return false;
        }
        else
        {
            //Move
            return true;
        }
    }

    void MovementAnim(float h, float v)
    {
        if (MoveOrTurn(h, v))
        {
            anim.SetFloat("MoveOrTurn", 0);

            if (v > 0)
            {
                anim.SetFloat("Move", 1);
            }
            if (v == 0)
            {
                anim.SetFloat("Move", 0);
            }
            if (v < 0)
            {
                anim.SetFloat("Move", 1);
            }
        }

        if (!MoveOrTurn(h, v))
        {
            anim.SetFloat("MoveOrTurn", 1);

            if (h > 0)
            {
                anim.SetFloat("Turn", 0);
            }
            if (h < 0)
            {
                anim.SetFloat("Turn", -1);
            }
        }
    }
}

    


