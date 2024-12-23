using UnityEngine;
using UnityEngine.UI;


    public class MoveText : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        [SerializeField] private float movementDistance; 
        [SerializeField] private CanvasScaler canvasScaler; 

        private void OnEnable()
        {
            MoveTextTween();
        }

        private void MoveTextTween()
        {
           
        }
    }

