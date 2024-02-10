using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DfPI_BoneBinder : MonoBehaviour
{
   // public Animator animator; // Assign this in the inspector with the Animator component of your humanoid model
    [FormerlySerializedAs("joints")] public Vector3[] poseLandmarks;

    /*
    // [Header("Eye Transforms")]
    // [Space(5)]
    // public Transform LeftEyeInner;
    // public Transform LeftEye;
    // public Transform LeftEyeOuter;
    // public Transform RightEyeInner;
    // public Transform RightEye;
    // public Transform RightEyeOuter;
    */

    /*
    [Header("Ear Transforms")]
    [Space(5)]
    public Transform LeftEar;
    public Transform RightEar;

    [Header("Mouth Transforms")]
    [Space(5)]
    public Transform MouthLeft;
    public Transform MouthRight;
    */
    [Header("Miscellaneous Transforms")]
    [Space(5)]
    public Transform Head;

    [Header("Shoulder Transforms")]
    [Space(5)]
    public Transform LeftShoulder;
    public Transform RightShoulder;

    [Header("Elbow Transforms")]
    [Space(5)]
    public Transform LeftElbow;
    public Transform RightElbow;

    [Header("Wrist & Hand Transforms")]
    [Space(5)]
    public Transform LeftWrist;
    public Transform RightWrist;
    [Space(10)]
    public Transform LeftPinky;
    public Transform RightPinky;
    public Transform LeftIndex;
    public Transform RightIndex;
    public Transform LeftThumb;
    public Transform RightThumb;

    [Header("Lower Body Transforms")]
    [Space(5)]
    public Transform LeftHip;
    public Transform RightHip;
    public Transform LeftKnee;
    public Transform RightKnee;
    public Transform LeftAnkle;
    public Transform RightAnkle;
    public Transform LeftHeel;
    public Transform RightHeel;
    public Transform LeftFootIndex;
    public Transform RightFootIndex;
    


   private static readonly Dictionary<string, int> boneMap = new()
    {
        {"Head", 0},
        {"LeftEyeInner", 1},
        {"LeftEye", 2},
        {"LeftEyeOuter", 3},
        {"RightEyeInner", 4},
        {"RightEye", 5},
        {"RightEyeOuter", 6},
        {"LeftEar", 7},
        {"RightEar", 8},
        {"MouthLeft", 9},
        {"MouthRight", 10},
        {"LeftShoulder", 11},
        {"RightShoulder", 12},
        {"LeftElbow", 13},
        {"RightElbow", 14},
        {"LeftWrist", 15},
        {"RightWrist", 16},
        {"LeftPinky", 17},
        {"RightPinky", 18},
        {"LeftIndex", 19},
        {"RightIndex", 20},
        {"LeftThumb", 21},
        {"RightThumb", 22},
        {"LeftHip", 23},
        {"RightHip", 24},
        {"LeftKnee", 25},
        {"RightKnee", 26},
        {"LeftAnkle", 27},
        {"RightAnkle", 28},
        {"LeftHeel", 29},
        {"RightHeel", 30},
        {"LeftFootIndex", 31},
        {"RightFootIndex", 32}
    };

    // Cache for Transform components
    private Dictionary<string, Transform> transformCache = new Dictionary<string, Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // Cache Transform references using reflection
        foreach (var landmark in boneMap)
        {
            var field = GetType().GetField(landmark.Key, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                var transform = field.GetValue(this) as Transform;
                
                if (transform != null)
                {
                    transformCache.Add(landmark.Key, transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if landmarkPositions array is initialized and has the correct length
        if (poseLandmarks != null && poseLandmarks.Length == boneMap.Count)
        {
            foreach (var landmark in boneMap)
            {
                if (transformCache.TryGetValue(landmark.Key, out var transform))
                {
                    // Update the position of the Transform with the corresponding landmark position
                    transform.position = poseLandmarks[landmark.Value];
                }
            }
        }
    }

    public void ReceiveJoints(Vector3[] joints)
    {
        this.poseLandmarks = joints;
    }
    
}
