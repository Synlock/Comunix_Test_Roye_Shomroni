using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComunixTest.View
{
    public class StartLevelDelay : MonoBehaviour
    {
        //This script allows for user preperation before level start
        [SerializeField] GameObject startingPanel;
        [SerializeField] TMP_Text levelText;
        [SerializeField] float delay = 2f;
        void Start()
        {
            StartCoroutine(DelayStart(delay));
        }
        //simple coroutine that delays the start of the level
        IEnumerator DelayStart(float delay)
        {
            levelText.text = $"Level {SceneLoader.GetCurrentLevel()}";
            startingPanel.SetActive(true);
            Model.GameManager.SetHasGameStarted(false);

            yield return new WaitForSeconds(delay);

            startingPanel.SetActive(false);
            Model.GameManager.SetHasGameStarted(true);
        }
    }
}