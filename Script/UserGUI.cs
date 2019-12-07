using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
public class UserGUI : MonoBehaviour
{
    public int status = 0;
    private IUserAction userAction;
    GUIStyle style;
    GUIStyle buttonStyle;
    GameState start, end;

    public int leftPriests = 3;
    public int leftDevils = 3;
    public int rightPriests = 0;
    public int rightDevils = 0;
    public bool boat_pos = true;
    private string tips = "";
    void Start()
    {
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;
        end = new GameState(0, 0, 3, 3, false, null);

    }

    void OnGUI()
    {
        GUIStyle fontstyle = new GUIStyle();
        fontstyle.normal.background = null;
        fontstyle.normal.textColor = new Color(255, 192, 203);
        fontstyle.fontSize = 50;

        style = new GUIStyle()
        {
            fontSize = 50
        };
        style.normal.textColor = new Color(0, 0, 0);
        buttonStyle = new GUIStyle("button")
        {
            fontSize = 10
        };
        GUI.Label(new Rect(250, 15, 100, 100), "Priest & Devil", fontstyle); //title
        if (status == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Gameover!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                userAction.Restart();
            }
        }
        else if (status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You win!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                userAction.Restart();
            }

        }

        GUI.Button(new Rect(200, Screen.height - 60, 400, 50), tips, buttonStyle);
        if (GUI.Button(new Rect(100, Screen.height - 60, 100, 50), "Tips", buttonStyle))
        {
            int[] arr = userAction.getStateInfo();
            leftPriests = arr[0];
            leftDevils = arr[1];
            rightPriests = arr[2];
            rightDevils = arr[3];

            if (arr[4] == 0) boat_pos = true; 
            else boat_pos = false; 
            start = new GameState(leftPriests, leftDevils, rightPriests, rightDevils, boat_pos, null);
          
            GameState temp = GameState.BFS(start, end);

            leftPriests = temp.lp;
            leftDevils = temp.ld;
            rightPriests = temp.rp;
            rightDevils = temp.rd;

            tips = "Goal:    right priests : " + leftPriests + "        right devils : " + leftDevils
                + "\n    left priests : " + rightPriests + "        left devils : " + rightDevils;
        }
    }
}