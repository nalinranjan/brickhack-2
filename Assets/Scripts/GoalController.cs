using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (door != null)
        {
            door.GetComponent<DoorControl>().KeepOpen();
        }

        //if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        //{
        //    //Debug.Log("Goooaaaaaal");
        //    //GetComponent<Collider>().enabled = false;
        //}

    }

    void OnTriggerExit(Collider other)
    {
        if (door != null)
        {
            door.GetComponent<DoorControl>().UnKeepOpen();
        }
    }
}
