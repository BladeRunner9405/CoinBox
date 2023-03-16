using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmitText : MonoBehaviour
{
    [SerializeField] private TMP_Text _textName;
    [SerializeField] private TMP_InputField _inputName;

    public void Submit()
    {
        _textName.SetText(_inputName.text);
    }
}
