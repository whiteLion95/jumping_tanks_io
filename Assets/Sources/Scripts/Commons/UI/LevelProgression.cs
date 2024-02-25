using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display level progression by distance.
/// </summary>
public class LevelProgression : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform playerTransform;
    public float levelDistance;
    public Vector3 currentPosition;
    public Vector3 endPosition;
    public Vector3 startPosition;
    public Slider progressionSlider;

    [SerializeField] private TMP_Text currentLevelLabel;

    //private bool updateDistance = false;

    /// <summary>
    /// Setting distance from start point to end and setting distance value to progressionSlider
    /// </summary>
    public void SetDistance()
    {
        levelDistance = FastDistance(startPoint.position, endPoint.position);
        //progressionSlider.maxValue = levelDistance;
        //progressionSlider.value = progressionSlider.maxValue;
        currentLevelLabel.text = "LEVEL " + StaticManager.levelID.ToString();
        //updateDistance = true;
    }

    /// <summary>
    /// Calc distance by X axis every frame
    /// </summary>
    void Update()
    {
        //if (updateDistance && playerTransform != null)
        //{
        //    levelDistance = FastDistance(playerTransform.position, endPoint.position);
        //    if (levelDistance < 1)
        //        levelDistance = 0;
        //    progressionSlider.value = levelDistance;
        //}

        //if (progressionSlider.value == 0)
        //{
        //    updateDistance = false;
        //}
    }

    /// <summary>
    /// Fast distance calc
    /// </summary>
    /// <param name="start">Start point in world coordinates</param>
    /// <param name="end">End point in world coordinates</param>
    /// <returns></returns>
    private float FastDistance(Vector3 start, Vector3 end)
    {
        Vector3 cache = new Vector3();
        cache.x = start.x - end.x;
        cache.y = start.y - end.y;
        cache.z = start.z - end.z;

        return Mathf.Abs(cache.z); //currently returned X axis distance. Change if you need.
    }
}
