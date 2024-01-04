using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.DisplayName);
    }

    public void OnInteract()
    {
        switch (item.DisplayName)
        {
            case "BlueKey":
                Main.Player.KeyCheck.Blue = true;
                break;

            case "GreenKey":
                Main.Player.KeyCheck.Green = true;
                break;

            case "RedKey":
                Main.Player.KeyCheck.Red = true;
                break;
        }
        Debug.Log(Main.Player.KeyCheck.Blue + " " + Main.Player.KeyCheck.Green + " " + Main.Player.KeyCheck.Red);
        Main.Game.CallkeyGet();
        Destroy(gameObject);
    }
}