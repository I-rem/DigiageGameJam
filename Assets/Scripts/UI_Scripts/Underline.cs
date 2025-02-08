using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Underline : MonoBehaviour
{
    public TMP_Text TextComponent;
    private void Awake()
    {
        TextComponent.fontStyle = FontStyles.Underline;
    }
}
