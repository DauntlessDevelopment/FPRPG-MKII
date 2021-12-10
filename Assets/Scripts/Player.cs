using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Agent
{

    [SerializeField] private GameObject head;
    [SerializeField] private GameObject feet;
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform ads_xform;
    [SerializeField] private Transform hip_xform;

    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    [SerializeField] private Image crosshair;
    private float normal_fov = 90;
    private float ads_fov = 45;


    private bool ADS = false;

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
            Camera.main.transform.Rotate(Vector3.right, -Input.GetAxisRaw("Mouse Y") * turn_speed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump"))
        {
            
            GetComponent<Rigidbody>().velocity += new Vector3(0, 10, 0);
            
        }

        if(Input.GetButtonDown("Fire1"))
        {
            GetComponentInChildren<Weapon>().Shoot();
            Debug.Log("Shoot in player");
        }

        if(Input.GetButtonDown("Fire2"))
        {
            ToggleADS();
        }


        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon.gameObject.SetActive(false);
            weapon = weapons[0].gameObject;
            weapon.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon.gameObject.SetActive(false);
            weapon = weapons[1].gameObject;
            weapon.gameObject.SetActive(true);
        }

    }

    private void ToggleADS()
    {
        ADS = !ADS;
        GameObject w = weapon.GetComponentInChildren<Weapon>().gameObject;
        if(ADS)
        {
            w.transform.position = ads_xform.position;
            Camera.main.fieldOfView = ads_fov;
            crosshair.enabled = false;
        }
        else
        {
            w.transform.position = hip_xform.position;
            Camera.main.fieldOfView = normal_fov;
            crosshair.enabled = true;


        }
    }
}
