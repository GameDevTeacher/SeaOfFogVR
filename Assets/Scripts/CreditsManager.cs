using UI_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{

    public GameObject credits;
    public float creditSpeed;
    private Vector3 _startPosition;
    private bool _creditsActive;
    
    private VRNoPeeking _noPeekingInstance;

    private bool stopUpdate;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        _noPeekingInstance = VRNoPeeking.Instance;
        _startPosition = credits.transform.position;
        await Awaitable.WaitForSecondsAsync(3);
        _creditsActive = true;
        stopUpdate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_creditsActive) return;
        if (Vector3.Distance(_startPosition, credits.transform.position) < 20)
        {
            UpdateCreditMovement();
        }
        else
        {
            UpdateCreditMovement();
            
            if (stopUpdate) return;
            stopUpdate = true;
            _noPeekingInstance.CameraFadeOut(1);
            Invoke(nameof(LoadMainMenu), 1);
        }
    }

    private void UpdateCreditMovement()
    {
        credits.transform.position += new Vector3(0, creditSpeed, 0);
    }

    private void LoadMainMenu()
    {
        print("CREDITS END");
        OutOfBounds.Instance.enabled = false;
        SceneManager.LoadScene(0);
    }
}
