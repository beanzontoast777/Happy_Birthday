using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
/*
 * Reference: OpenAI. (2024). ChatGPT. https://chat.openai.com
 * AI-assisted development for [specific functionality].
 * Code adapted, modified, and implemented by developer.
 */
public class IconOptimizer : MonoBehaviour
{
    [Header("Animated UI References")]
    [SerializeField] private List<Image> animatedImages;
    [SerializeField] private Animator animator;

    [Header("Optimization Settings")]
    [SerializeField] private bool useVisibilityDetection = true;
    [SerializeField] private float checkInterval = 0.1f; // Reduce check frequency

    private bool isVisible = true;
    private float lastCheckTime;
    private List<CanvasRenderer> canvasRenderers = new List<CanvasRenderer>();

    void Awake()
    {
        // Auto-get references if not set
        if (animator == null)
            animator = GetComponent<Animator>();

        if (animatedImages.Count == 0)
        {
            // Auto-populate if no images specified
            animatedImages = new List<Image>(GetComponentsInChildren<Image>());
        }

        // Get all canvas renderers from animated images
        foreach (Image image in animatedImages)
        {
            if (image != null)
            {
                CanvasRenderer renderer = image.GetComponent<CanvasRenderer>();
                if (renderer != null)
                {
                    canvasRenderers.Add(renderer);
                }
            }
        }

        // Set culling mode as backup
        if (animator != null)
        {
            animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        }

        InitializeVisibility();
    }

    void InitializeVisibility()
    {
        isVisible = CheckImagesVisibility();
        UpdateAnimatorState();
    }

    void Update()
    {
        if (!useVisibilityDetection) return;

        // Reduce check frequency for performance
        if (Time.time - lastCheckTime < checkInterval) return;

        lastCheckTime = Time.time;

        bool currentlyVisible = CheckImagesVisibility();
        if (currentlyVisible != isVisible)
        {
            isVisible = currentlyVisible;
            UpdateAnimatorState();
        }
    }

    bool CheckImagesVisibility()
    {
        // Check if any animated image is visible
        foreach (Image image in animatedImages)
        {
            if (image == null) continue;

            // Check if image is active, enabled, and has non-zero alpha/color
            if (image.gameObject.activeInHierarchy &&
                image.enabled &&
                image.color.a > 0.01f)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateAnimatorState()
    {
        if (animator == null) return;

        // Enable/disable animator based on visibility
        animator.enabled = isVisible;

        // Update animation parameter if needed
        animator.SetBool("Visible", isVisible);
    }

    // Public methods for manual control
    public void SetOptimizationEnabled(bool enable)
    {
        useVisibilityDetection = enable;
        if (!enable)
        {
            // Force enable animator if optimization is disabled
            if (animator != null)
                animator.enabled = true;
        }
        else
        {
            InitializeVisibility();
        }
    }

    public void ForceVisibleState(bool visible)
    {
        isVisible = visible;
        UpdateAnimatorState();
    }

    public void AddAnimatedImage(Image image)
    {
        if (image != null && !animatedImages.Contains(image))
        {
            animatedImages.Add(image);

            CanvasRenderer renderer = image.GetComponent<CanvasRenderer>();
            if (renderer != null && !canvasRenderers.Contains(renderer))
            {
                canvasRenderers.Add(renderer);
            }

            InitializeVisibility(); // Re-check visibility
        }
    }

    public void RemoveAnimatedImage(Image image)
    {
        if (animatedImages.Contains(image))
        {
            animatedImages.Remove(image);

            CanvasRenderer renderer = image.GetComponent<CanvasRenderer>();
            if (renderer != null && canvasRenderers.Contains(renderer))
            {
                canvasRenderers.Remove(renderer);
            }
        }
    }

    // Debug information
    void OnValidate()
    {
        if (animatedImages != null)
        {
            // Remove any null entries in inspector
            for (int i = animatedImages.Count - 1; i >= 0; i--)
            {
                if (animatedImages[i] == null)
                {
                    animatedImages.RemoveAt(i);
                }
            }
        }
    }

    // Gizmos for debugging (only in Editor)
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (animatedImages != null)
        {
            foreach (Image image in animatedImages)
            {
                if (image != null)
                {
                    Gizmos.color = isVisible ? Color.green : Color.red;
                    Gizmos.DrawWireCube(image.transform.position, Vector3.one * 10f);
                }
            }
        }
    }
#endif
}
