using System;
using UnityEngine;

namespace Game
{
    public class SwipeController : MonoBehaviour
    {
        private static SwipeController instance;

        public static SwipeController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SwipeController>();
                }

                return instance;
            }
        }
        private Vector3 _startPosition;
        public Action<SwipeDirection> OnSwipe;


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                var diffVector = Input.mousePosition - _startPosition;

                if (Mathf.Abs(diffVector.x) > Mathf.Abs(diffVector.y))
                {
                    //horizontal movement
                    if (diffVector.x > 0)
                    {
                        OnSwipe?.Invoke(SwipeDirection.Right);
                    }
                    else
                    {
                        OnSwipe?.Invoke(SwipeDirection.Left);
                    }
                }
                else
                {
                    if (diffVector.y > 0)
                    {
                        OnSwipe?.Invoke(SwipeDirection.Up);
                    }
                    else
                    {
                        OnSwipe?.Invoke(SwipeDirection.Down);
                    }
                }
            }
        }
    }

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}