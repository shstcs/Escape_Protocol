using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Main.Game.IsClear = true;
            Main.Game.CallStageOver();
        }
    }
}
