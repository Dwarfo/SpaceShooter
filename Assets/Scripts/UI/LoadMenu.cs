using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadMenu : MonoBehaviour, IMenu {

    public Button backButton;
    public GameObject gameSave;

    void Start()
    {
        backButton.onClick.AddListener(HandleBackButton);
        //Renew();
    }

    public void Renew()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo = info.GetFiles();
        int fileNum = fileInfo.Length;

        foreach (FileInfo file in fileInfo)
        {
            string nameString = file.ToString();
            int index = nameString.IndexOf(Application.persistentDataPath);
            string cleanPath = (index < 0) ? nameString : nameString.Remove(index, Application.persistentDataPath.Length);
            GameObject go = Instantiate(gameSave, transform);
            go.GetComponentInChildren<Text>().text = nameString;
        }
    }

    public void SetMenuActive(bool active)
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(active);
        List<Transform> listedTransforms = new List<Transform>(transforms);

        listedTransforms.Remove(gameObject.transform);

        foreach (Transform tr in listedTransforms)
            tr.gameObject.SetActive(active);
    }

    private void HandleBackButton()
    {
        UIManager.Instance.ToMainMenu(this);
    }
}
