using UnityEngine;

public class SimulationRunner : MonoBehaviour
{
    public bool m_PerFrameUpdate = true;
    
    void FixedUpdate()
    {
        if (!m_PerFrameUpdate)
            Physics2D.Simulate(Time.fixedDeltaTime);
    }

	void Update ()
    {
        if (m_PerFrameUpdate)
            Physics2D.Simulate(Time.deltaTime);
	}
}
