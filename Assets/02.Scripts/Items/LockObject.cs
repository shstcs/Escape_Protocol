using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockObject : MonoBehaviour, IInteractable
{
    public LockedDoorData lockedDoor;

    [SerializeField] private GameObject KeyDoor;
    [SerializeField] private AudioSource OpenDoorSound;

    private BoxCollider _doorCollider;
    private Animator _doorAnimator;

    enum Color {Blue, Green, Red };
    [SerializeField] private Color _keycolor;
    

    //case blue, red, green 일때 따로따로 배치하는 방법이 필요하다.


    private void Start()
    {
        _doorCollider = KeyDoor.GetComponentInChildren<BoxCollider>();
        _doorAnimator = KeyDoor.GetComponentInChildren<Animator>();
    }


    public string GetInteractPrompt()
    {
        if (Main.Player.KeyCheck.Blue == true && _keycolor == Color.Blue)
            return string.Format("Open {0} Door", _keycolor);

        else if (Main.Player.KeyCheck.Red == true && _keycolor == Color.Red)
            return string.Format("Open {0} Door", _keycolor);

        else if (Main.Player.KeyCheck.Green == true && _keycolor == Color.Green)
            return string.Format("Open {0} Door", _keycolor);

        else
            return string.Format("Need {0} Key", _keycolor);
    }

    public void OnInteract()
    {
        if (Main.Player.KeyCheck.Blue == true && _keycolor == Color.Blue)
            DoorInteraction();          
        if (Main.Player.KeyCheck.Red == true && _keycolor == Color.Red)
            DoorInteraction();
        if(Main.Player.KeyCheck.Green == true && _keycolor == Color.Green)
            DoorInteraction();
    }

    public void DoorInteraction()
    {
        _doorAnimator.SetTrigger("OpenDoor");        
        Main.Game.CallDoorOpen();
        Destroy(_doorCollider);

        if(!OpenDoorSound.isPlaying)             
            OpenDoorSound.Play();
    }
}
