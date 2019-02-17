using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class SawTrapController : MonoBehaviour
{
    public bool MoveRight = true;
    public float Speed = 5f;
    public float StopAt = 0f;

    //private float m_origZ;
    private float m_translateZ;
    private bool m_active = true;

    private RigidbodyFirstPersonController.MovementSettings m_movementSettings;

    // Start is called before the first frame update
    void Start()
    {
        //m_origZ = transform.position.z;
        m_translateZ = Speed * (MoveRight? 1f : -1f);

        m_movementSettings = GameObject.FindGameObjectWithTag("Player")
                                .GetComponent<RigidbodyFirstPersonController>()
                                .movementSettings;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (m_active)
        {
            float sprintMultiplier = 1f / Mathf.Pow(1.3f, m_movementSettings.RunMultiplier - 1f);
            //Debug.Log(sprintMultiplier);
            m_translateZ = Speed * (MoveRight ? 1f : -1f);
            transform.Translate(0f, m_translateZ * Time.fixedDeltaTime * sprintMultiplier, 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SawBlade") || other.CompareTag("SideWall"))
        {
            MoveRight ^= true;
            //Debug.Log(name + " collided with " + other.name);
        }
        else if (other.CompareTag("Player") || other.CompareTag("Comrade"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Stop()
    {
        m_active = false;
    }

    public void Resume()
    {
        m_active = true;
    }
}
