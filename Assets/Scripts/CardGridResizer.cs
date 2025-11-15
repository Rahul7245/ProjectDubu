using UnityEngine;
using UnityEngine.UI;

public class CardGridResizer : MonoBehaviour
{
    [SerializeField] private int numberOfRows = 2;
    [SerializeField] private int numberOfCols = 3;
    [SerializeField] private RectTransform containerRect; // The panel/area where cards should fit
    [SerializeField] private GridLayoutGroup gridLayout;
    
    // Card aspect ratio is 2:3 (width:height)
    private const float CARD_ASPECT_RATIO = 2f / 3f; // width / height
    
    void Start()
    {
       Init(numberOfRows,numberOfCols);
    }

    public void Init(int rows, int columns)
    {
        if (containerRect != null && gridLayout != null)
        {
            ApplyToGridLayout(rows, columns);
        }
    }
    // Apply calculated size to GridLayoutGroup (only changes cellSize)
    public void ApplyToGridLayout(int rows, int cols)
    {
        if (gridLayout == null)
        {
            Debug.LogError("GridLayoutGroup is not assigned!");
            return;
        }
        
        Vector2 cardSize = CalculateCardSize(rows, cols);
        
        // Only modify cellSize, preserve all other GridLayoutGroup settings
        gridLayout.cellSize = cardSize;
        gridLayout.constraintCount = cols;
    }

    public Vector2 CalculateCardSize(int rows, int cols)
    {
        if (containerRect == null)
        {
            Debug.LogError("Container RectTransform is not assigned!");
            return Vector2.zero;
        }
        
        if (gridLayout == null)
        {
            Debug.LogError("GridLayoutGroup is not assigned!");
            return Vector2.zero;
        }
        
        // Get existing padding and spacing from GridLayoutGroup
        RectOffset padding = gridLayout.padding;
        Vector2 spacing = gridLayout.spacing;
        
        // Calculate available space after accounting for padding and spacing
        float availableWidth = containerRect.rect.width 
            - padding.left - padding.right 
            - (spacing.x * (cols - 1));
        
        float availableHeight = containerRect.rect.height 
            - padding.top - padding.bottom 
            - (spacing.y * (rows - 1));
        
        // Calculate card size based on width constraint
        float cardWidth = availableWidth / cols;
        float cardHeight = cardWidth / CARD_ASPECT_RATIO; // height = width / (2/3) = width * 1.5
        
        // Check if the calculated height fits in the available space
        float totalHeightNeeded = cardHeight * rows;
        
        if (totalHeightNeeded > availableHeight)
        {
            // Height doesn't fit, so calculate based on height constraint instead
            cardHeight = availableHeight / rows;
            cardWidth = cardHeight * CARD_ASPECT_RATIO; // width = height * (2/3)
        }
        
        return new Vector2(cardWidth, cardHeight);
    }
}