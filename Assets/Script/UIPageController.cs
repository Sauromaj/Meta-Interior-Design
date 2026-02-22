using UnityEngine;
using System.Collections.Generic;

public class UIPageController : MonoBehaviour
{
    [Header("UI Pages")]
    public List<GameObject> pages;

    private int _currentPageIndex = 0;

    void Start()
    {
        RefreshPages();
    }

    public void NextPage()
    {
        // Loops back to 0 if at the end
        _currentPageIndex = (_currentPageIndex + 1) % pages.Count;
        RefreshPages();
        Debug.Log("Switched to Page: " + _currentPageIndex);
    }

    public void PreviousPage()
    {
        // Loops to the end if at the beginning
        _currentPageIndex--;
        if (_currentPageIndex < 0)
        {
            _currentPageIndex = pages.Count - 1;
        }
        RefreshPages();
        Debug.Log("Switched to Page: " + _currentPageIndex);
    }

    private void RefreshPages()
    {
        if (pages == null || pages.Count == 0) return;

        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i] != null)
            {
                pages[i].SetActive(i == _currentPageIndex);
            }
        }
    }
}