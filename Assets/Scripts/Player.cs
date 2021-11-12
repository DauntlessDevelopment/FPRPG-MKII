using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{

    [SerializeField] private GameObject head;
    [SerializeField] private GameObject feet;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();
        if (Physics.Raycast(feet.transform.position, -transform.up, 0.5f))
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);

        }
    }

    private void HandleInput()
    {
        bool still = true;
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(transform.forward * Input.GetAxisRaw("Vertical") * move_speed * Time.deltaTime, Space.World);
            animator.SetFloat("speed", move_speed * Time.deltaTime);
            still = false;
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * move_speed * Time.deltaTime, Space.World);
            animator.SetFloat("speed", move_speed * Time.deltaTime);
            still = false;
        }
        if(still)
        {
            animator.SetFloat("speed", 0);

        }
        if (Input.GetAxisRaw("Mouse X")!=0)
        {
            transform.Rotate(transform.up, Input.GetAxisRaw("Mouse X") * turn_speed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Mouse Y") != 0)
        {
            head.transform.Rotate(Vector3.right, -Input.GetAxisRaw("Mouse Y") * turn_speed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump"))
        {
            
            GetComponent<Rigidbody>().velocity += new Vector3(0, 10, 0);
            
        }
    }
}
