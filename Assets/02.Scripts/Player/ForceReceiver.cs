using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    #region Fields

    [SerializeField] private CharacterController controller;
    [SerializeField] private float _drag = 0.3f;

    private Vector3 _dampingVelocity;
    private Vector3 _impact;
    private float _verticalVelocity;

    #endregion

    #region Properties

    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    #endregion

    #region MonoBehaviours

    private void Update()
    {
        if (_verticalVelocity < 0f && controller.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
    }

    #endregion

    #region Init

    public void Reset()
    {
        _impact = Vector3.zero;
        _verticalVelocity = 0f;
    }

    #endregion

    #region Methods

    public void AddForce(Vector3 force)
    {
        _impact += force;
    }

    public void Jump(float jumpForce)
    {
        _verticalVelocity += jumpForce;
    }

    #endregion
}