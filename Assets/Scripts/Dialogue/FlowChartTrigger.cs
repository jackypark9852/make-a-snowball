using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FlowChartTrigger : MonoBehaviour
{
    [SerializeField] private BlockReference block;
    [SerializeField] bool destroyOnTrigger = true;
    private void Start()
    {
        BlockSignals.OnBlockStart += PauseGame; // TODO: GameManager should handle this
        BlockSignals.OnBlockEnd += ResumeGame;
    }

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

    private void PauseGame(Block block) // TODO: GameManager should handle this
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame(Block block) // TODO: GameManager should handle this
    {
        Time.timeScale = 1f;
    }
}
