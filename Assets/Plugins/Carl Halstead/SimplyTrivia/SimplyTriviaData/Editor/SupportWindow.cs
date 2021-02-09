using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class SupportWindow : EditorWindow
{
    [SerializeField] Texture _helpImage;
    [SerializeField] Texture _ratingImage;

    [MenuItem("Window/Carl Halstead/Simply Trivia/Support")]
    private static void Init()
    {
        SupportWindow window = (SupportWindow)GetWindow(typeof(SupportWindow));
        window.titleContent = new GUIContent("Simply Trivia - Support");
        window.Show();
    }

	private bool isInitialised = false;

	GUIStyle btnStyle;

	// GUIContent supportForumContent;
	GUIContent supportEmailContent;
	GUIContent reviewContent;

	private void InitStyles()
	{
		btnStyle = new GUIStyle(EditorStyles.toolbarButton)
		{
			fixedHeight = 30f,
			fontSize = 10
		};

		// supportForumContent = new GUIContent("Support Forum", _helpImage);
		supportEmailContent = new GUIContent("Support Email", _helpImage);
		reviewContent = new GUIContent("Leave a Review/Rating", _ratingImage);

		isInitialised = true;
	}

	private Vector2 scrollPosition;

    private void OnGUI()
    {
		if (isInitialised == false)
			InitStyles();

		scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        //if (GUILayout.Button(supportForumContent, btnStyle))
        //{
        //    Application.OpenURL("https://google.co.uk");
        //}

        if (GUILayout.Button(supportEmailContent, btnStyle))
        {
            Application.OpenURL("mailto:Carl.gfinity@gmail.com");
        }

        if (GUILayout.Button(reviewContent, btnStyle))
        {
            Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/102628?aid=1101l3ozs");
        }

		GUILayout.EndScrollView();
    }
}
#endif
