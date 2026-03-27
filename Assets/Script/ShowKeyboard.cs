using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;

    [Header("Default Transform Settings")]
    // Values taken from your RectTransform screenshot
    private Vector3 defaultPos = new Vector3(-0.3868313f, 0.695f, 0.009874554f);
    private Vector3 defaultRot = new Vector3(14.927f, -92.571f, 0f);
    private Vector3 defaultScale = new Vector3(0.0006952f, 0.0006952f, 0.0006952f);

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        
        // Links the click/selection to the OpenKeyboard function
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    public void OpenKeyboard()
    {
        if (NonNativeKeyboard.Instance != null)
        {
            // 1. Link the text field to the keyboard
            NonNativeKeyboard.Instance.InputField = inputField;
            
            // 2. Open the keyboard
            NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

            // 3. Force the exact Position, Rotation, and Scale from your screenshot
            GameObject kbObj = NonNativeKeyboard.Instance.gameObject;
            
            kbObj.transform.position = defaultPos;
            kbObj.transform.eulerAngles = defaultRot;
            kbObj.transform.localScale = defaultScale;
        }
        else
        {
            Debug.LogError("NonNativeKeyboard instance not found in scene!");
        }
    }
}