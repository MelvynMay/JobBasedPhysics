using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePaddles : MonoBehaviour
{
    public Object m_Paddle;
    public Vector2 m_Origin;
    public Vector2 m_Size = Vector2.one;
    public Vector2 m_Stride;

	void Start ()
    {
        var position = m_Origin;

        for(var x = 0; x < m_Size.x; ++x, position.x += m_Stride.x)
        {
            position.y = m_Origin.y;

            for(var y = 0; y < m_Size.y; ++y, position.y += m_Stride.y)
            {
                Instantiate(m_Paddle, new Vector3(position.x + (((y%2) * (m_Stride.x*0.5f))), position.y), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 89.0f)), transform);
            }
        }	
	}
}
