using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetector : MonoBehaviour
{
    
    [SerializeField]
    private NinjaHombreController ninjaHombre;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ladder>())
        {
            ninjaHombre.ClimbingAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Ladder>())
        {
            ninjaHombre.ClimbingAllowed = false;
        }
    }
}
