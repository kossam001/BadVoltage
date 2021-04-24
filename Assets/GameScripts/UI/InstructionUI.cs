using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> pages;

    private GameObject currentPage;
    private int currentPageNumber;

    private void Awake()
    {
        currentPage = pages[0];
    }

    public void NextPage(int flipPage)
    {
        currentPage.SetActive(false);
        currentPageNumber += flipPage;

        if (currentPageNumber >= pages.Count)
            currentPageNumber = 0;
        else if (currentPageNumber < 0)
            currentPageNumber = pages.Count - 1;

        currentPage = pages[currentPageNumber];
        currentPage.SetActive(true);
    }

    public void ReturnToMain()
    {
        currentPage.SetActive(false);
        currentPageNumber = 0;
        currentPage = pages[0];
        currentPage.SetActive(true);

        gameObject.SetActive(false);
    }
}
