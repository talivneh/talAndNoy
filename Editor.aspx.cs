using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    XmlDocument xmlFakeDoc;
    XmlDocument xmlRealDoc;
    
    protected void Page_init(object sender, EventArgs e)
    {
        gameStoke.Text = "מאגר המשחקים של " + Session["editorName"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //מנקה את קובץ האקס-אמ-אל המיועד לעריכת שאלות
        xmlFakeDoc = XmlDataSource2.GetXmlDocument();
        xmlFakeDoc.SelectSingleNode("/tree").InnerXml = "";

        xmlRealDoc = XmlDataSource1.GetXmlDocument();
        xmlRealDoc.Load(Server.MapPath("tree/games.xml"));

        string path = (Server.MapPath("/uploadedFiles"));
        string[] files = Directory.GetFiles(path);

        foreach (string x in files)
        {
            string imageName = x.Substring((x.Length - x.LastIndexOf("'\'") + 2));

            if (xmlRealDoc.InnerText.Contains(Server.UrlEncode(imageName)) == false)
            {
                File.Delete(Server.MapPath("/uploadedFiles/" + imageName));
            }

        }
    }
    
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (gameNameTextBox.Text != "")
        {

            int GamesIds = Convert.ToInt16(xmlRealDoc.SelectSingleNode("/tree/idCounter").InnerText);
            GamesIds++;
            string newGameCode = GamesIds.ToString();
            xmlRealDoc.SelectSingleNode("/tree/idCounter").InnerText = newGameCode;
            
            XmlElement myNewGameNode = xmlRealDoc.CreateElement("game");
            myNewGameNode.SetAttribute("totalQes", "0");
            myNewGameNode.SetAttribute("timePerQes", "30");
            myNewGameNode.SetAttribute("published", "false");
            myNewGameNode.SetAttribute("code", newGameCode.ToString());

            XmlElement myNewGameName = xmlRealDoc.CreateElement("gameName");
            myNewGameName.InnerText = Server.UrlEncode(gameNameTextBox.Text);
            myNewGameNode.AppendChild(myNewGameName);

            XmlElement myNewGameQuestions = xmlRealDoc.CreateElement("questions");
            myNewGameNode.AppendChild(myNewGameQuestions);

            XmlElement myNewGameQuestion = xmlRealDoc.CreateElement("question");
            myNewGameQuestion.SetAttribute("numberQues", "0");
            myNewGameQuestions.AppendChild(myNewGameQuestion);

            XmlElement myNewGameQuestionText = xmlRealDoc.CreateElement("questionText");
            myNewGameQuestionText.InnerText = "";
            myNewGameQuestion.AppendChild(myNewGameQuestionText);

            XmlElement myNewGameQuestionAnswers = xmlRealDoc.CreateElement("Answers");
            myNewGameQuestionText.InnerXml = "";
            myNewGameQuestion.AppendChild(myNewGameQuestionAnswers);

            XmlNode FirstGame = xmlRealDoc.SelectNodes("//game").Item(0);
            xmlRealDoc.SelectSingleNode("/tree").InsertBefore(myNewGameNode, FirstGame);


            XmlDataSource1.Save();
            GridView1.DataBind();

            // ניקוי תיבת הטקסט
            gameNameTextBox.Text = "";
            nameLblempty.Text = "";
        }
        else
        {
            gameNameTextBox.Style.Add("Border", "1.5px solid gray");
            nameLblempty.Text = "*הקלד שם למשחק*";
        }
    }

    protected void IspassCB_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox myCheckBox = (CheckBox)sender;
        // מושכים את האי די של הפריט באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theId = myCheckBox.Attributes["theItemId"];
        XmlNode theGame = xmlRealDoc.SelectSingleNode("/tree/game[@code=" + theId + "]");
        //קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPass = myCheckBox.Checked;
        //עדכון של המאפיין בעץ
        theGame.Attributes["published"].InnerText = NewIsPass.ToString();

        XmlDataSource1.Save();
        GridView1.DataBind();
    }

    protected void timeChanging(object sender)
    {
        Button time = (Button)sender;
        string y = time.ID;
        string x = y.Substring(y.Length - 2);
        string gameTimeId = time.Attributes["theItemId"].ToString();
        XmlNode node = xmlRealDoc.SelectSingleNode("/tree/game[@code='" + gameTimeId + "']");
        node.Attributes["timePerQes"].InnerText = x;
        XmlDataSource1.Save();
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string totalQuest = "";
        string theId = "";
        string rowNum="";
        string buttonId = "";
        string myTextBox ="";
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        if (e.CommandSource.GetType() == typeof(ImageButton)) {
        ImageButton i = (ImageButton)e.CommandSource;
        theId = i.Attributes["theItemId"];
        buttonId = i.ID;
        myTextBox = i.Attributes["myTB"];
        rowNum = buttonId.Substring(buttonId.Length - 1);
        totalQuest = i.Attributes["theItemTotalQuest"];
        Session["gameIDSession"] = i.Attributes["theItemId"];
        }
        else { 
        if(e.CommandSource.GetType() == typeof(Button))
        {
            Button i = (Button)e.CommandSource;
            theId = i.Attributes["theItemId"];
            totalQuest = i.Attributes["theItemTotalQuest"];
            Session["gameIDSession"] = i.Attributes["theItemId"];
        }
            Session["CorrectAnswer"] = "0";
        }
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        if (totalQuest == "0")
        {
            Session["editQuestNum"] = "0";
        }
        else
        {
            Session["editQuestNum"] = "1";
        }
     
        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                deleteRow();
                break;

            case "editName":
                if (Session["edit"]!= null && Session["edit"].ToString() == "true") {
                Session["edit"] = "false";
                XmlNode GameNameNode = xmlRealDoc.SelectSingleNode("/tree/game[@code=" + theId + "]/gameName");
                Session["gameIDSession"] = "";
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                    string counter = ((Label)row.FindControl("gameIDLbl")).Text;
                    if (counter == theId)
                    {
                   xmlRealDoc.SelectSingleNode("/tree/game[@code='" + theId + "']/gameName").InnerText = Server.UrlEncode(((TextBox)row.FindControl("gameNameEditTextBox")).Text);
                    }
                }
                    XmlDataSource1.Save();
                    GridView1.DataBind();
                }
                else {
                 Session["edit"] = "true";
                 GridView1.DataBind();
                }
                break;

            case "editTime":
                Button time = (Button)e.CommandSource;
                string y = time.ID;
                string x = y.Substring(y.Length - 2);
                string gameTimeId = time.Attributes["theItemId"].ToString();
                XmlNode node = xmlRealDoc.SelectSingleNode("/tree/game[@code='" + gameTimeId + "']");
                node.Attributes["timePerQes"].Value = x;
                XmlDataSource1.Save();
                Session["gameIDSession"] = "";
                GridView1.DataBind();
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":
                Session["saved"] = false;
                XmlNode FakeGameXml = xmlFakeDoc.SelectSingleNode("tree");
                XmlElement myNewGame = xmlFakeDoc.CreateElement("game");
                //  myNewGame.SetAttribute("published", "false");
                // myNewGame.SetAttribute("timePerQes", "30");
                myNewGame.SetAttribute("totalQes", totalQuest);
                myNewGame.SetAttribute("code", theId);
                FakeGameXml.AppendChild(myNewGame);
                myNewGame.InnerXml = xmlRealDoc.SelectSingleNode("/tree/game[@code=" + theId + "]").InnerXml;
                XmlDataSource2.Save();
                Response.Redirect("EditGame.aspx");
                break;
        }

    }


    protected void deleteRow()
    {
        //הצגה של המסך האפור
        grayWindows.Visible = true;
        //הצגת הפופ-אפ של המחיקה
        DeleteConfPopUp.Visible = true;

        string gameId = Session["gameIDSession"].ToString();
        string gameName = xmlRealDoc.SelectSingleNode("/tree/game[@code='" + gameId + "']/gameName").InnerText;

        popUpmessage.Text = "האם אתה בטוח שברצונך למחוק את המשחק" + "</br>" + '"' + "<b>" + Server.UrlDecode(gameName) + "</b>" + '"'  + "?" ;
    }

    protected void OkBtn_Click(object sender, EventArgs e)
    {

        string gameIdToDelete = Session["gameIDSession"].ToString();
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("/tree/game[@code='" + gameIdToDelete + "']");
        node.ParentNode.RemoveChild(node);

        XmlDataSource1.Save();
        GridView1.DataBind();


        Button myoKBtn = (Button)sender;

        ((Panel)myoKBtn.Parent).Visible = false;

        grayWindows.Visible = false;
    }


    protected void ExitImageButton_Click(object sender, EventArgs e)
    {

        //סגירת הרקע האפור
        grayWindows.Visible = false;
    }


    protected void RowDataBound1(object sender, GridViewRowEventArgs e)
    {

        foreach (GridViewRow row in GridView1.Rows)
        {
            //חיפוש הלייבל שבו מופיע ה ID של המשחק
            Label gameCodeLbl = (Label)row.FindControl("gameIDLbl");
            //בעזרת האי-די של המשחק נוכל לבדוק האם עומד בתנאי הפרסום
            string GameCode = gameCodeLbl.Text;
            //דוגמה לבדיקה - אם קיימים לפחות 2 שאלות
            XmlNodeList quest = xmlRealDoc.SelectNodes("/tree/game[@code='" + GameCode + "']/questions/question");

            //חיפוש הצ'אק-בוקס על פי האי-די שלו
            CheckBox GameIsPublishCb = (CheckBox)row.FindControl("IspassCB");

            Label checkBoxtooltip = (Label)row.FindControl("IspassCBtooltip");
            if (quest.Count >= 12 && quest.Count % 2 == 0)
            {
                GameIsPublishCb.Enabled = true;
                if (GameIsPublishCb.Checked)
                {
                    checkBoxtooltip.Text = "המשחק פורסם לחץ לביטול";
                }
                else
                {
                    checkBoxtooltip.Text = "לחץ לפרסום משחק";
                }
            }
            else
            {
                if (quest.Count < 12)
                {
                    checkBoxtooltip.Text = "חובה מינימום 12 שאלות לפרסום";
                }

                else
                {
                    checkBoxtooltip.Text = "מספר השאלות במשחק אינו זוגי";
                }

                GameIsPublishCb.Enabled = false;
                //אם מקודם המשחק היה מפורסם, אנחנו רוצים להחזיר אותו ללא מפורסם בעץ
                XmlNode IsPublish = xmlRealDoc.SelectSingleNode("/tree/game[@code='" + GameCode + "']");
                IsPublish.Attributes["published"].InnerText = "False";
                XmlDataSource1.Save();

                //וגם לשנות את הפקד עצמו ללא לחוץ
                GameIsPublishCb.Checked = false;
            }

            Label EditgameBtntooltip = (Label)row.FindControl("EditgameBtntooltip");
            TextBox GameName = (TextBox)row.FindControl("gameNameEditTextBox");
            ImageButton EditgameBtn = (ImageButton)row.FindControl("EditgameBtn");
            ImageButton editNameTB = (ImageButton)row.FindControl("editNameTB");
            Label editNameTBtooltip = (Label)row.FindControl("timePerQuesttooltip");
            //Label timePerQuest = (Label)FindControl(row.RowIndex + "Time");

            Label editNameTimetooltip = (Label)row.FindControl("editNameTBtooltip");
            GameName.Text = Server.UrlDecode(GameName.Text);

            //if (Session["edit"]!=null && Session["edit"].ToString()=="true")
            //{
            //    EditgameBtntooltip.Text = "סיים עריכת שם המשחק";
            //    EditgameBtn.Enabled = false;
            //    editNameTBtooltip.Text = "שמור שם משחק";
            //    //timePerQuest.Enabled = false;
            //    editNameTBtooltip.Text = "סיים עריכת שם המשחק";
            //    GameIsPublishCb.Enabled = false;
            //    checkBoxtooltip.Text = "סיים עריכת שם המשחק";
            //}
        }

        string timePerQuestion = "30,60,90,בלי הגבלה";
        string[] tpqArray = timePerQuestion.Split(',');

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList tpq = (DropDownList)e.Row.FindControl("timePerQuest");
            tpq.CssClass = "timePerQuestClass";
            Session["dropDown"] = tpq.ToString();
            tpq.Attributes["TagName"] = "select";

            foreach (string timePerQ in tpqArray)
            {
                ListItem li = new ListItem();
                li.Text = timePerQ;
                if (li.Text!="בלי הגבלה") { 
                li.Value = timePerQ;}
                else
                {
                li.Value = "no";
                }
                tpq.Items.Add(li);
            }

            if (Session["gameIDSession"] != null)
            {
                ImageButton nameEditBtn= (ImageButton)e.Row.FindControl("editNameTB");
                TextBox nameEdit = (TextBox)e.Row.FindControl("gameNameEditTextBox");
                nameEdit.Text = Server.UrlDecode(nameEdit.Text);
                string rowId = nameEdit.ID;
                Label nameEditLbl = (Label)e.Row.FindControl("nameEditLbl");
                string theNameId = Session["gameIDSession"].ToString();
                nameEditBtn.Attributes["myTB"] = "GridView1_gameNameEditTextBox_" + e.Row.RowIndex;
                int caracters = nameEdit.Text.Length;
                if (nameEdit.Attributes["theItemId"].ToString() == theNameId && nameEditLbl.Attributes["theItemId"].ToString() == theNameId && Session["edit"] !=null && Session["edit"].ToString() != "false")
                {
                    nameEditLbl.ID = "nameEditLblTrue";
                    nameEditLbl.Visible = true;
                    nameEditLbl.Text = caracters.ToString() + "/20";
                    nameEdit.Attributes["CharacterForLabel"] = "GridView1_nameEditLblTrue_" + e.Row.RowIndex;
                    nameEdit.Attributes["enableThis"] = "GridView1_EditgameBtn_" + e.Row.RowIndex;
                    nameEdit.Attributes["myToolTip"] = "GridView1_EditgameBtntooltip_" + e.Row.RowIndex;
                    nameEdit.Attributes["myBtn"] = "GridView1_editNameTB_" + e.Row.RowIndex;
                    nameEdit.Attributes["myBtnToolTip"] = "GridView1_editNameTBtooltip_" + e.Row.RowIndex;
                    nameEdit.Attributes["myCheckBox"] = "GridView1_IspassCB_" + e.Row.RowIndex;
                    nameEdit.Attributes["myCheckBoxTooltip"] = "GridView1_IspassCBtooltip_" + e.Row.RowIndex;
                    nameEdit.Attributes["myDelete"] = "GridView1_DeleteGame_" + e.Row.RowIndex;
                    nameEdit.Attributes["myDeleteTooltip"] = "GridView1_DeleteGametooltip_" + e.Row.RowIndex;
                    nameEdit.Attributes["myTime"] = e.Row.RowIndex + "Time";
                    nameEdit.Attributes["myTimeTooltip"] = "GridView1_timePerQuesttooltip_" + e.Row.RowIndex;
                    nameEdit.Enabled = true;
                    if (caracters == 0)
                    {
                        nameEdit.Attributes["placeHolder"] = "הזן שם למשחק";
                        nameEdit.Style.Add("border", "1 px solid red");
                    }

                }
                else
                {
                    if(caracters != 0) {
                    nameEditLbl.ID = "nameEditLbl";
                    nameEditLbl.Visible = false;
                    nameEdit.Attributes["CharacterForLabel"] = "nameEditLbl";
                    nameEdit.Enabled = false;
                    }
                    else
                    {

                    }
                }
            }


            tpq.SelectedValue = tpq.Attributes["theItemValue"].ToString();

        }
    }


}

