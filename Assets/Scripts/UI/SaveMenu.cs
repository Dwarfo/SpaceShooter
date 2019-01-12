using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour, IMenu {

    public Button saveButton;
    public InputField saveText;
    public Button backButton;


    void Start ()
    {
        saveButton.onClick.AddListener(HandleSaveButton);
        backButton.onClick.AddListener(HandleBackButton);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void HandleSaveButton()
    {
        GameManager.Instance.SaveGame(saveText.text);
    }

    private void HandleBackButton()
    {
        if (GameManager.Instance.CurrentGameState == GameState.PAUSED)
            UIManager.Instance.ToPauseMenu(this);
        if (GameManager.Instance.CurrentGameState == GameState.LEVELSTATS)
            UIManager.Instance.ToStatsMenu(this);
    }



    public void SetMenuActive(bool active)
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(active);
        List<Transform> listedTransforms = new List<Transform>(transforms);

        listedTransforms.Remove(gameObject.transform);

        foreach (Transform tr in listedTransforms)
            tr.gameObject.SetActive(active);
    }
}
