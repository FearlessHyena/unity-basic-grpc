using System;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour {

    public Material material;
    
    private readonly Color _defaultColor = Color.white;
    private ColorClient _colorClient;

    private void Start() {
        _colorClient = new ColorClient();
    }

    public void ChangeColor() {
        material.color = GetColor(material.color);
    }

    //We'll use this method to make a remote call and get a color from the server
    private UnityEngine.Color GetColor(Color currentColor) {
        var currentColorString = ColorUtility.ToHtmlStringRGBA(currentColor);

        var newColorString = _colorClient.GetRandomColor(currentColorString);

        if (ColorUtility.TryParseHtmlString(newColorString, out var newColor)) {
            return newColor;
        } else {
            Debug.LogError("Error parsing the color string: " + currentColorString);
            Debug.LogWarning("Setting to default color: " + _defaultColor);
            return _defaultColor;
        }
    }
}
