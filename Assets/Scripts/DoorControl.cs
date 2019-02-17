using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DoorControl : MonoBehaviour
{
    private enum DoorState
    {
        Opening, Open, Closing, Closed
    }

    public float Speed = 0.1f;
    public float OpenTime = 1.0f;

    private float m_defaultY;
    private float m_minY;
    private DoorState m_state;
    private Vector3 m_translation;
    private float m_openedFor;
    private bool m_holdOpen;

    // Start is called before the first frame update
    void Start()
    {
        m_defaultY = transform.localPosition.y;
        m_minY = m_defaultY - transform.localScale.z;
        m_translation = new Vector3(0f, Speed, 0f);
        m_openedFor = 0f;
        m_state = DoorState.Closed;
        m_holdOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    public void TriggerOpen()
    {
        m_state = DoorState.Opening;
    }

    public void KeepOpen()
    {
        m_holdOpen = true;
        m_state = DoorState.Opening;
    }

    public void UnKeepOpen()
    {
        m_holdOpen = false;
        m_state = DoorState.Opening;
    }

    private void Move()
    {
        switch (m_state)
        {
            case DoorState.Opening:
                if (transform.localPosition.y > m_minY)
                {
                    transform.localPosition -= m_translation;
                }
                else if (!m_holdOpen)
                {
                    m_state = DoorState.Open;
                    m_openedFor = 0f;
                }
                break;
            case DoorState.Open:
                if (m_openedFor >= OpenTime)
                {
                    m_state = DoorState.Closing;
                }
                else
                {
                    m_openedFor += Time.fixedDeltaTime;
                }
                break;
            case DoorState.Closing:
                if (transform.localPosition.y < m_defaultY)
                {
                    transform.localPosition += m_translation;
                }
                else
                {
                    m_state = DoorState.Closed;
                }
                break;
            case DoorState.Closed:
                break;
            default:
                break;
        }
    }
}
