using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class TimeHackery : MonoBehaviour
{
    public float SpeedUp = 0.05f;
    public KeyCode InteractionKey = KeyCode.E;
    public Camera cam;

    public float MaxInteractDistance = 2.0f;
    public LayerMask InteractableLayer;

    public static Vector3 DefaultPos = new Vector3(6.0f, -0.2f, 0.0f);
    public static Vector3 CheckpointPos = DefaultPos;

    RigidbodyFirstPersonController m_fpsController;
    private float m_defaultRunMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        m_fpsController = GetComponent<RigidbodyFirstPersonController>();
        m_defaultRunMultiplier = m_fpsController.movementSettings.RunMultiplier;
        transform.position = CheckpointPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (m_fpsController.movementSettings.Running)
        {
            m_fpsController.movementSettings.RunMultiplier += SpeedUp;

            if (m_fpsController.GetComponent<Rigidbody>().velocity.sqrMagnitude <
                (m_fpsController.movementSettings.ForwardSpeed * m_fpsController.movementSettings.ForwardSpeed))
            {
                m_fpsController.movementSettings.RunMultiplier = m_defaultRunMultiplier;
            }
        }
        else
        {
            m_fpsController.movementSettings.RunMultiplier = m_defaultRunMultiplier;
        }

        if (transform.position.x < -35f)
        { 
            CheckpointPos.Set(-35f, CheckpointPos.y, CheckpointPos.z);
            //Debug.Log("Checkpoint: " + CheckpointPos);
        }

        if (Input.GetKeyDown(InteractionKey))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, MaxInteractDistance, InteractableLayer))
            {
                //Debug.Log(hitInfo.collider.name + " " + hitInfo.collider.tag);
                if (hitInfo.collider.CompareTag("DoorSwitch"))
                {
                    var switchController = hitInfo.collider.GetComponent<SwitchController>();
                    if (switchController != null)
                    {
                        switchController.OnPress();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckpointPos = DefaultPos;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
