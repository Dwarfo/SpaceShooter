using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadMenu : MonoBehaviour {

    public Button backButton;
	

	void Start ()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo = info.GetFiles();
        int fileNum = fileInfo.Length;

        foreach (FileInfo file in fileInfo)
        {
            string nameString = file.ToString();
            int index = nameString.IndexOf(Application.persistentDataPath);
            string cleanPath = (index < 0) ? nameString : nameString.Remove(index, Application.persistentDataPath.Length);

        }

	
	// Update is called once per frame
	void Update () {
		
	}
}
