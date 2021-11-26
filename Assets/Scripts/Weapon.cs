using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public List<Sprite> muzzle_flashes = new List<Sprite>();
    public GameObject muzzle_flash_object;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        //fire ray
        Debug.Log("Shoot in weapon");

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            if(hit.transform.GetComponent<Agent>() != null)
            {
                //swap crosshair temporarily
            }


        }
        muzzle_flash_object.GetComponent<SpriteRenderer>().sprite = muzzle_flashes[Random.Range(0, muzzle_flashes.Count)];
        //play animation
        GetComponentInParent<Animator>().SetTrigger("shoot");
    }
}
