using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;

    [Header("Search Pages")]
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;

    [Header("Position Settings (From Screenshot)")]
    private Vector3 defaultPos = new Vector3(-0.3868313f, 0.695f, 0.009874554f);
    private Vector3 defaultRot = new Vector3(14.927f, -92.571f, 0f);
    private Vector3 defaultScale = new Vector3(0.0006952f, 0.0006952f, 0.0006952f);

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    void Start()
    {
        // 1. Opens keyboard when you click the input field in VR
        inputField.onSelect.AddListener(x => OpenKeyboard());

        // 2. DYNAMIC SEARCH: This triggers the search EVERY time a letter is typed
        inputField.onValueChanged.AddListener(delegate { SearchProducts(); });
        
        // Ensure Page 1 is the default view
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void OpenKeyboard()
    {
        if (NonNativeKeyboard.Instance != null)
        {
            // Link keyboard to this specific input field
            NonNativeKeyboard.Instance.InputField = inputField;
            NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

            // Set the exact position and rotation you requested
            GameObject kbObj = NonNativeKeyboard.Instance.gameObject;
            kbObj.transform.position = defaultPos;
            kbObj.transform.eulerAngles = defaultRot;
            kbObj.transform.localScale = defaultScale;
        }
    }

    public void SearchProducts()
    {
        string searchText = inputField.text.ToLower();

        // If search is cleared, reset to Page 1 view
        if (string.IsNullOrEmpty(searchText))
        {
            ResetToDefaultView();
            return;
        }

        // Filter both pages based on the typed letters
        FilterPage(page1, searchText);
        FilterPage(page2, searchText);
    }

    private void FilterPage(GameObject page, string query)
    {
        if (page == null) return;

        bool hasMatchOnThisPage = false;

        foreach (Transform product in page.transform)
        {
            // Ignore UI elements that aren't products
            if (product.name == "Title" || product.name.Contains("Button")) continue;

            // Check if product name contains the typed letters
            bool isMatch = product.name.ToLower().Contains(query);
            product.gameObject.SetActive(isMatch);

            if (isMatch) hasMatchOnThisPage = true;
        }

        // Wakes up the page if an item is found, otherwise hides the page
        page.SetActive(hasMatchOnThisPage);
    }

    private void ResetToDefaultView()
    {
        // Turn everything back on inside the pages
        ShowAllChildren(page1);
        ShowAllChildren(page2);

        // Set Page 1 as the visible page
        page1.SetActive(true);
        page2.SetActive(false);
    }

    private void ShowAllChildren(GameObject page)
    {
        if (page == null) return;
        foreach (Transform child in page.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}