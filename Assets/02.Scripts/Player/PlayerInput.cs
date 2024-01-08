using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Properties

    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

    #endregion

    #region MonoBehaviours

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        PlayerActions = InputActions.Player;
    }

    #endregion

    #region Methods

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    #endregion

}