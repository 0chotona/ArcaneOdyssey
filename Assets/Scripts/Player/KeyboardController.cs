using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : InputController
{
    public void Update()
    {
        _inputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }
}
