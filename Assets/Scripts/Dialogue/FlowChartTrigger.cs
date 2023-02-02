using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FlowChartTrigger : MonoBehaviour
{
    [SerializeField] private BlockReference block;
    [SerializeField] bool destroyOnTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<PlayerController>() != null)
        {
            block.Execute();
            if (destroyOnTrigger)
            {
                Destroy(gameObject);
            }
        }

    }

}
