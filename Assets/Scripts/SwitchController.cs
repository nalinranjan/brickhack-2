using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Material ActivatedMaterial;
    public GameObject[] doors;
    public float ActiveTime = 1.5f;
    public string StopTag;

    private Material m_origMaterial;
    private float m_timeActive;
    private bool m_active;

    // Start is called before the first frame update
    void Start()
    {
        m_origMaterial = GetComponent<Renderer>().material;
        m_timeActive = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (m_active)
        {
            m_timeActive += Time.fixedDeltaTime;
            if (m_timeActive >= ActiveTime)
            {
                GetComponent<Renderer>().material = m_origMaterial;
                m_active = false;
                m_timeActive = 0f;
            }
        }
    }

    public void OnPress()
    {
        GetComponent<Renderer>().material = ActivatedMaterial;
        m_active = true;
        m_timeActive = 0f;

        foreach (var door in doors)
        {
            var doorScript = door.GetComponent<DoorControl>();
            if (doorScript != null)
            {
                doorScript.TriggerOpen();
            }
        }

        if (StopTag != null && StopTag != "")
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag(StopTag))
            {
                var controller = obj.GetComponent<SawTrapController>();
                if (controller != null)
                {
                    controller.Stop();
                }
            }
        }
    }
}
