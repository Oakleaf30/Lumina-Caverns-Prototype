using UnityEngine;
using UnityEngine.UI;
using TMPro; // Needed for TextMeshPro

public class Tutorial : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI tutorialText;
    public Button nextButton;
    public Button lastButton;

    [Header("Content")]
    [TextArea(5, 10)] // Makes the strings easier to edit in the Inspector
    public string[] pages;

    private int currentPageIndex = 0;

    void Start()
    {
        // 1. Assign the button listeners
        nextButton.onClick.AddListener(NextPage);
        lastButton.onClick.AddListener(LastPage);

        // 2. Initialize the display
        UpdatePageDisplay();
    }

    // --------------------------------------------------------
    // Public Navigation Methods
    // --------------------------------------------------------

    public void NextPage()
    {
        // Only increase the index if we are not on the last page
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            UpdatePageDisplay();
        }
    }

    public void LastPage()
    {
        // Only decrease the index if we are not on the first page
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePageDisplay();
        }
    }

    // --------------------------------------------------------
    // Internal Update Method
    // --------------------------------------------------------

    private void UpdatePageDisplay()
    {
        // Set the text content based on the current index
        tutorialText.text = pages[currentPageIndex];

        // Control button visibility (Prevents navigating past limits)

        // Hide/disable the 'Last' button on the first page
        lastButton.gameObject.SetActive(currentPageIndex > 0);

        // Hide/disable the 'Next' button on the last page
        nextButton.gameObject.SetActive(currentPageIndex < pages.Length - 1);

        // Optional: If you want to show the page number (e.g., "Page 1 of 5"),
        // you would update another Text component here.
    }
}