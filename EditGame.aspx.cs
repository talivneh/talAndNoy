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

public partial class Edit : System.Web.UI.Page
{
    XmlDocument FakexmlDoc;
    XmlDocument RealxmlDoc;
    string questId;
    string gameID;
    bool save;

    protected void Page_Load(object sender, EventArgs e)
    {
        questId = Session["editQuestNum"].ToString();
        gameID = Session["gameIDSession"].ToString();
        
        XmlNode realQuestNum = RealxmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]");
        if (Convert.ToInt16(realQuestNum.Attributes["totalQes"].Value) >= 12)
        {
            min12.Style.Add("background-image", "/images/v.png");
        }
        else
        {
            min12.Style.Add("background-image", "/images/x.png");
        }
        if (Convert.ToInt16(realQuestNum.Attributes["totalQes"].Value) % 2 == 0)
        {
            couple.Style.Add("background-image", "/images/v.png");
        }
        else
        {
            couple.Style.Add("background-image", "/images/x.png");
        }


        XmlNode fakeQuest = FakexmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions/question[@numberQues = " + questId + "]");
        XmlNode realQuest = RealxmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions/question[@numberQues = " + questId + "]");
        
        if (Page.IsPostBack)
        {
            //FakexmlDoc.Load(Server.MapPath("tree/fake.xml"));
            //RealxmlDoc.Load(Server.MapPath("tree/games.xml"));
            //saveButton.Enabled = true;
            if (((RadioButtonList)FindControl("answersList")).SelectedItem != null)
            {
                string correctAns = ((RadioButtonList)FindControl("answersList")).SelectedValue;
                Session["CorrectAnswer"] = ((RadioButtonList)FindControl("answersList")).SelectedValue;
                XmlNode fakeQuestAnsewrs = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/Answers");
                ListItemCollection answers = ((RadioButtonList)FindControl("answersList")).Items;
                foreach (ListItem answer in answers)
                {
                    answer.Text = "";
                }
                foreach (XmlNode node in fakeQuestAnsewrs)
                {
                    node.Attributes["result"].Value = "wrong";
                }
            ((RadioButtonList)FindControl("answersList")).SelectedItem.Text = ".";
                fakeQuestAnsewrs.ChildNodes.Item(Convert.ToInt16(Session["CorrectAnswer"]) - 1).Attributes["result"].Value = "correct";
                XmlDataSourceFakeGame.Save();
            }
            XmlNode answersList = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/Answers");
            //כל התנאים לשמירת תשובה
            if (questionText.Text != "")
            {
                save = true;

                if (answersList.ChildNodes.Count < 2)
                {
                    save = false;
                    saveButtontooltip.Text = "שאלה חייבת להכיל שתי תשובות לפחות";
                }
                else
                {
                    foreach (XmlNode answer in answersList)
                    {
                        if (answer.Attributes["result"].Value == "correct")
                        {
                            save = true;
                        }
                    }
                    if (((RadioButtonList)FindControl("answersList")).SelectedValue == "")
                      {
                            save = false;
                            saveButtontooltip.Text = "חובה לסמן תשובה נכונה";
                    }
                    if (save == true && answersList.ChildNodes.Count <= 1)
                    {
                        foreach (XmlNode answer in answersList)
                        {
                            if (answer.InnerText == "")
                            {
                                save = false;
                                saveButtontooltip.Text = "שאלה חייבת להכיל שתי תשובות לפחות";
                            }
                        }
                    }
                }
            }
            else
            {
                saveButtontooltip.Text = "לא ניתן לשמור שאלה ללא טקסט";
            }
            if (questId != "0")
            {
                if (realQuest.InnerXml == fakeQuest.InnerXml)
                {
                    save = false;
                    saveButton.Enabled = false;
                    saveButtontooltip.Style.Add("visibility", "visible");
                }
            }
            if (save == false)
            {
                saveButton.Enabled = false;
                saveButtontooltip.Style.Add("visibility", "visible");
            }
            else
            {
                saveButton.Enabled = true;
                saveButtontooltip.Style.Add("visibility", "hidden");
            }
        }
        
    }

    protected void Page_init(object sender, EventArgs e)
    {
        Panel3.BackImageUrl = "/images/spaceship.png";
        gameID = Session["gameIDSession"].ToString();
        questId = Session["editQuestNum"].ToString();
        
        XmlDataSourceRealGame.XPath = "/tree/game[@code = " + gameID + "]/questions/question";
        XmlDataSourceFakeGame.XPath = "/tree/game[@code = " + gameID + "]/questions/question";

        RealxmlDoc = XmlDataSourceRealGame.GetXmlDocument();
        RealxmlDoc.Load(Server.MapPath("tree/games.xml"));

        FakexmlDoc = XmlDataSourceFakeGame.GetXmlDocument();
        FakexmlDoc.Load(Server.MapPath("tree/fake.xml"));

        XmlNode fakeQuestNode = FakexmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions/question[@numberQues = " + questId + "]");
        XmlNode realQuestNode = RealxmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions/question[@numberQues = " + questId + "]");


            if (questId != "0")
            {
            saveButton.Text = "שמור שינויים";
            if (realQuestNode.InnerXml == fakeQuestNode.InnerXml)
                {
                    save = false;
                    saveButton.Enabled = false;
                    saveButtontooltip.Style.Add("visibility", "visible");
                    saveButtontooltip.Text = "טרם נערכו שינויים בשאלה";
                }
        }
        else
        {
            saveButtontooltip.Text = "טרם נערכו שינויים בשאלה";
            saveButton.Text = "שמור שאלה";
        }
        
        gameName.Text = Server.UrlDecode(FakexmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/gameName").InnerText);

        XmlNode fakeQuest = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]");

        XmlNode fakeQuestText = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/questionText");
        questionText.Text = Server.UrlDecode(fakeQuestText.InnerText);
        QuestCuontLbl.Text = questionText.Text.Length.ToString() + "/100";
        XmlNode fakeQuestImage = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/pic");
        
        if (fakeQuestImage != null)
        {
            questionText.CssClass = "questImageTrue";
            ImageButtonquestImg.ImageUrl = Server.UrlDecode(fakeQuestImage.InnerText);
            ImageButtonquestImg.CssClass = "questImageClassTrue";
            ImageButton deleteImg0 = new ImageButton();
            deleteImg0.ID = "deleteImg0";
            deleteImg0.ImageUrl = "images/Exit.png";
            deleteImg0.Click += questImageDelete;
            Panel1.Controls.Add(deleteImg0);
            ImageButtonquestImgtooltip.Text="החלפת תמונה";
            ((Label)FindControl("QuestCuontLbl")).CssClass = "IfImageTrue";
            ((Label)FindControl("QuestCuontLbl")).Style.Add("visibility", "hidden");
        }


        XmlNode fakeQuestAnsewrs = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/Answers");

        int ansNum = 1;

        RadioButtonList answersList = new RadioButtonList();
        answersList.ID = "answersList";
        answersList.AutoPostBack = true;
        Panel1.Controls.Add(answersList);
        
        if (questionText.Text.Length != 0)
        {
            ((Label)FindControl("answer1tooltip")).Style.Add("visibility", "hidden");
            ((ImageButton)FindControl("ImageButton1")).Enabled = true;
            ((ImageButton)FindControl("answerImage1")).Enabled = true;
            ((ImageButton)FindControl("ImageButton1")).Style.Add("visibility", "visible");
            ((ImageButton)FindControl("answerImage1")).Style.Add("visibility", "visible");
            ((Label)FindControl("Label1")).Enabled = true;
            ((Label)FindControl("Label1")).Style.Add("visibility", "visible");

            if (fakeQuestAnsewrs.ChildNodes.Count <= 1)
            {
                saveButtontooltip.Text = "שאלה חייבת להכיל שתי תשובות לפחות";
            }
        }

        for (int i = 3; i <= 6; i++)
        {
            ((Label)FindControl("answer" + i + "tooltip")).Style.Add("visibility", "hidden");
            ((ImageButton)FindControl("ImageButton" + i)).Style.Add("visibility", "hidden");
            ((ImageButton)FindControl("answerImage" + i)).Style.Add("visibility", "hidden");
            ((Label)FindControl("Label" + i)).Style.Add("visibility", "hidden");
        }


        foreach (XmlNode node in fakeQuestAnsewrs)
        {
            ((Label)FindControl("answer" + ansNum + "tooltip")).Style.Add("visibility", "hidden");
            ((ImageButton)FindControl("ImageButton" + ansNum)).Enabled = false;
            ((ImageButton)FindControl("answerImage" + ansNum)).Enabled = false;
            ((ImageButton)FindControl("ImageButton" + ansNum)).Style.Add("visibility", "hidden");
            ((ImageButton)FindControl("answerImage" + ansNum)).Style.Add("visibility", "hidden");
            ((Label)FindControl("Label" + ansNum)).Enabled = false;
            ((Label)FindControl("Label" + ansNum)).Style.Add("visibility", "hidden");

            ImageButton delete = new ImageButton();
            delete.ID = "answerDelete" + ansNum;
            delete.CssClass = "answer" + ansNum + "deleteClass";
            delete.ImageUrl = "images/Exit.png";

            ListItem check = new ListItem();
            check.Text = "";
            check.Value = ansNum.ToString();
            answersList.Items.Add(check);
            
            if (node.Attributes["result"].Value == "correct")
            {
               Session["CorrectAnswer"] = ansNum;
               check.Text = ".";
               check.Selected = true;

            }

            if (node.Attributes["pic"].Value == "no")
            {
                TextBox ansewrText = new TextBox();
                ansewrText.Attributes["placeholder"] = "הזן תשובה";
                ansewrText.Attributes["enableThis"] = (ansNum + 1).ToString();
                ansewrText.Text = Server.UrlDecode(node.InnerText);
                ansewrText.TextMode = TextBoxMode.MultiLine;
                ansewrText.ID = "answer" + ansNum + "Textbox";
                ansewrText.CssClass = "ansText" + ansNum + "Class";
                ansewrText.Attributes["CharacterLimit"] = "50";
                ansewrText.Attributes["CharacterForLabel"] = "AnslableCount" + ansNum;

                Label lableCount = new Label();
                lableCount.ID = "AnslableCount" + ansNum;
                lableCount.CssClass = "ansText" + ansNum + "Class";
                lableCount.Text = ansewrText.Text.Length.ToString() + "/50";
                lableCount.Style.Add("visibility", "hidden");
                Panel1.Controls.Add(ansewrText);
                Panel1.Controls.Add(lableCount);   
            }
            else
            {
                ((ImageButton)FindControl("answerImage" + ansNum)).Enabled = true;
                ((ImageButton)FindControl("answerImage" + ansNum)).Style.Add("visibility", "visible");
                ImageButton ansewrImage = (ImageButton)FindControl("answerImage" + ansNum);
                ansewrImage.ImageUrl = Server.UrlDecode(node.InnerText);
                ansewrImage.CssClass = "answerImageClassTrue";
            }

            delete.Click += Delete_Answer;
            Panel1.Controls.Add(delete);
            
            if (ansNum < 6)
            {
                ansNum++;

            }

            ((Label)FindControl("answer" + ansNum + "tooltip")).Style.Add("visibility", "hidden");
            ((ImageButton)FindControl("ImageButton" + ansNum)).Enabled = true;
            ((ImageButton)FindControl("answerImage" + ansNum)).Enabled = true;
            ((ImageButton)FindControl("ImageButton" + ansNum)).Style.Add("visibility", "visible");
            ((ImageButton)FindControl("answerImage" + ansNum)).Style.Add("visibility", "visible");
            ((Label)FindControl("Label" + ansNum)).Enabled = true;
            ((Label)FindControl("Label" + ansNum)).Style.Add("visibility", "visible");
        }
         if (answersList.SelectedValue == "") {
            saveButton.Enabled = false;
             if (answersList.Items.Count>=2) { 
                     saveButtontooltip.Text = "חובה לסמן תשובה נכונה";
             }
             }
        else
        {
            if (answersList.Items.Count <= 1)
            {
                saveButtontooltip.Text = "שאלה חייבל להכיל שתי תשובות לפחות";
                saveButton.Enabled = false;
            }
            else { 
            if (saveButton.Enabled == true) { 
            saveButtontooltip.Style.Add("visibility", "hidden");
            }
            }
        }
 
        Panel1.Controls.Add(new LiteralControl("<br/>"));
        Panel1.DataBind();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];
        Session["CorrectAnswer"] = 0;
        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור 
        XmlNode fakeQuestNode = FakexmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions");
        XmlNode realQuestNode = RealxmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions");

        switch (e.CommandName)
        {

            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                Session["DeleteQuestNum"] = theId;

                saveChanges_inPage();

                if (fakeQuestNode.InnerText == realQuestNode.InnerText)
                {
                    deleterow();
                }
                else { 
                //הצגה של המסך האפור
                grayWindows.Visible = true;
                //הצגת הפופ-אפ של המחיקה
                DeleteConfPopUp.Visible = true;

                Session["commend"] = "deleteRow";

                string questIdToDel = Session["DeleteQuestNum"].ToString();
                string QuestName = FakexmlDoc.SelectSingleNode("/tree/game[@code='" + gameID + "']/questions/question[@numberQues='" + questIdToDel + "']/questionText").InnerText;
                popUpmessage.Text = "האם אתה בטוח שברצונך למחוק את השאלה:" + "</br>" + '"' + "<b>" + Server.UrlDecode(QuestName) + "</b>" + '"' + "?";
                }

                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":

                Session["editQuestion"] = i.Attributes["theItemId"];

                saveChanges_inPage();
                
                if (fakeQuestNode.InnerText == realQuestNode.InnerText)
                {
                    Session["editQuestNum"] = Session["editQuestion"];

                    fakeQuestNode.InnerXml = realQuestNode.InnerXml;

                    XmlDataSourceFakeGame.Save();

                    Response.Redirect("EditGame.aspx");
                }
                else { 
                grayWindows.Visible = true;
                //הצגת הפופ-אפ של המחיקה
                DeleteConfPopUp.Visible = true;
                popUpmessage.Text = "אתה עומד לעזוב את הדף לפני ששמרת את השינויים" + "</br>" + "האם ברצונך להמשיך?";
                OkBtn0.Text = "כן, המשך";
                myExitBtn.Text = "לא, השאר";
                Session["commend"] = "editRow";
                }
                break;
        }
    }
    
    protected void Delete_Answer(object sender, ImageClickEventArgs e)
    {
        Session["CorrectAnswer"] = 0;
        ImageButton btn = (ImageButton)sender; 
        string AnsId = btn.ID;
        int x = Convert.ToInt16(AnsId.Substring(AnsId.Length - 1));
        Label countLbl = (Label)FindControl("AnslableCount" + x);
        string questId = Session["editQuestNum"].ToString();
        int z = x - 1;
        XmlNode questioninnerText = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/questionText");
        XmlNode answersList = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/Answers");
        questioninnerText.InnerText = Server.UrlEncode(questionText.Text);

        for (int i=0;i<=5; i++)
        {
            if ((TextBox)FindControl("answer" + (i+1) + "Textbox")!=null)
            {
                answersList.ChildNodes[i].InnerText = Server.UrlEncode(((TextBox)FindControl("answer" + (i + 1) + "Textbox")).Text);
            }
        }

        answersList.ChildNodes[z].InnerText = answersList.LastChild.InnerText;
        answersList.ChildNodes[z].Attributes["pic"].Value = answersList.LastChild.Attributes["pic"].Value;
        answersList.ChildNodes[z].Attributes["result"].Value = answersList.LastChild.Attributes["result"].Value;

        XmlNode answerToRemove = answersList.LastChild;
        //if (answerToRemove.Attributes["result"].Value == "correct")
        //     {
        //         ((CheckBox)FindControl("answersList_" + x)).Checked = false;
        //     }

        if (answerToRemove.Attributes["pic"].Value == "yes")
        {
            string filePath = answerToRemove.InnerText;
            File.Delete(Server.MapPath(filePath));
            ImageButton AnswerImageToRemove = (ImageButton)FindControl("answerImage" + x);
            AnswerImageToRemove.ImageUrl = "/images/image_icon.png";
            AnswerImageToRemove.CssClass = "answerImageClass";
        }
        else
        {
            TextBox AnswerTxtToRemove = (TextBox)FindControl("answer" + x + "Textbox");
            Panel1.Controls.Remove(AnswerTxtToRemove);
        }

        answersList.RemoveChild(answerToRemove);
        XmlDataSourceFakeGame.Save();


        ((ImageButton)FindControl("ImageButton" + x)).Enabled = true;
        ((ImageButton)FindControl("answerImage" + x)).Enabled = true;
        ((ImageButton)FindControl("ImageButton" + x)).Style.Add("visibility", "visible");
        ((ImageButton)FindControl("answerImage" + x)).Style.Add("visibility", "visible");
        ((Label)FindControl("Label" + x)).Enabled = true;
        ((Label)FindControl("Label" + x)).Style.Add("visibility", "visible");

        Panel1.Controls.Remove(btn);
        Panel1.Controls.Remove(countLbl);
        Panel1.DataBind();
        Response.Redirect("EditGame.aspx");
    }

    protected void OkBtn_Click(object sender, EventArgs e)
    {

        XmlNode fakeQuestNode = FakexmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions");
        XmlNode realQuestNode = RealxmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions");

        if (Session["commend"].ToString() == "deleteRow")
        {
            deleterow();
            
            fakeQuestNode.InnerXml = realQuestNode.InnerXml;

            XmlDataSourceFakeGame.Save();
        }

        if (Session["commend"].ToString() == "newQuest")
        {
            newQuest();

            fakeQuestNode.InnerXml = realQuestNode.InnerXml;

            XmlDataSourceFakeGame.Save();
        }

        if (Session["commend"].ToString() == "goBack")
        {
            Session["gameIdSession"] = "";

            fakeQuestNode.InnerXml = realQuestNode.InnerXml;

            XmlDataSourceFakeGame.Save();

            Response.Redirect("Editor.aspx");
        }

        if (Session["commend"].ToString() == "editRow")
        {
            Session["editQuestNum"] = Session["editQuestion"];

            fakeQuestNode.InnerXml = realQuestNode.InnerXml;

            XmlDataSourceFakeGame.Save();

            Response.Redirect("EditGame.aspx");
        }

        
        grayWindows.Visible = false;
        DeleteConfPopUp.Visible = false;

    }

    protected void deleterow()
    {
        string gameID = Session["gameIDSession"].ToString();
        string questId = Session["DeleteQuestNum"].ToString();

        XmlNode questionToDelete = FakexmlDoc.SelectSingleNode(" / tree /game / questions / question[@numberQues = " + questId + "]");
        XmlNode realquestionToDelete = RealxmlDoc.SelectSingleNode(" / tree /game / questions / question[@numberQues = " + questId + "]");
        int DeletedQuestNum = Convert.ToInt16(questionToDelete.Attributes["numberQues"].Value);
        XmlNodeList myQuestNum = FakexmlDoc.SelectNodes("/tree/game/questions/question");
        XmlNodeList realmyQuestNum = RealxmlDoc.SelectNodes("/tree/game/questions/question");
        if (Convert.ToInt16(realquestionToDelete.ParentNode.ParentNode.Attributes["totalQes"].Value) <= 1){
            questionToDelete.SelectSingleNode("//questionText").InnerText = "";
            questionToDelete.SelectSingleNode("//Answers").InnerXml = "";
            questionToDelete.Attributes["numberQues"].Value = "0";

            questionToDelete.ParentNode.ParentNode.Attributes["totalQes"].Value = "0";

            realquestionToDelete.SelectSingleNode("//questionText").InnerText = "";
            realquestionToDelete.SelectSingleNode("//Answers").InnerXml = "";
            realquestionToDelete.Attributes["numberQues"].Value = "0";

            realquestionToDelete.ParentNode.ParentNode.Attributes["totalQes"].Value = "0";

            Session["editQuestNum"] = "0";
        }
        else
        {
            questionToDelete.ParentNode.RemoveChild(questionToDelete);
            realquestionToDelete.ParentNode.RemoveChild(realquestionToDelete);
        }

        foreach (XmlElement question in myQuestNum)
            {
                if (Convert.ToInt16(question.Attributes["numberQues"].Value) > DeletedQuestNum)
                {
                    int questNum = Convert.ToInt16(question.Attributes["numberQues"].Value);
                    question.Attributes["numberQues"].Value = (questNum - 1).ToString();
                }
            }

            foreach (XmlElement question in realmyQuestNum)
            {
                if (Convert.ToInt16(question.Attributes["numberQues"].Value) > DeletedQuestNum)
                {
                    int questNum = Convert.ToInt16(question.Attributes["numberQues"].Value);
                    question.Attributes["numberQues"].Value = (questNum - 1).ToString();
                }
            }



        XmlNode gameQuestNum = FakexmlDoc.SelectSingleNode("/tree/game");
        XmlNode realgameQuestNum = FakexmlDoc.SelectSingleNode("/tree/game");
        int numberOfQuest = Convert.ToInt16(realgameQuestNum.Attributes["totalQes"].Value);
        gameQuestNum.Attributes["totalQes"].Value = (numberOfQuest - 1).ToString();
        realgameQuestNum.Attributes["totalQes"].Value = (numberOfQuest - 1).ToString();

        if (Session["editQuestNum"].ToString() == Session["DeleteQuestNum"].ToString() && Session["editQuestNum"].ToString() != "1" && Session["editQuestNum"].ToString() != "0")
        {
            Session["editQuestNum"] = Convert.ToInt16(Session["editQuestNum"]) - 1;
        }


        XmlDataSourceFakeGame.Save();
        XmlDataSourceRealGame.Save();
        innerGridView.DataBind();
        Response.Redirect("EditGame.aspx");
    }

    protected void newQuest()
    {
        XmlNode myQuestions = FakexmlDoc.SelectSingleNode("//game/questions");

        XmlElement myNewQuestion = FakexmlDoc.CreateElement("question");
        myNewQuestion.SetAttribute("numberQues", "0");

        XmlNode FirstQuest = myQuestions.FirstChild;
        FakexmlDoc.SelectSingleNode("//questions").InsertBefore(myNewQuestion, FirstQuest);

        XmlElement myNewQuestionText = FakexmlDoc.CreateElement("questionText");
        myNewQuestionText.InnerText = " ";
        myNewQuestion.AppendChild(myNewQuestionText);

        XmlElement myNewQuestionAnswers = FakexmlDoc.CreateElement("Answers");
        myNewQuestionText.InnerXml = "";
        myNewQuestion.AppendChild(myNewQuestionAnswers);

        XmlDataSourceFakeGame.Save();

        XmlNodeList myQuestNum = FakexmlDoc.SelectNodes("/tree/game/questions/question");

        Session["editQuestNum"] = 0;
        Response.Redirect("EditGame.aspx");
    }

    protected void ExitImageButton_Click(object sender, EventArgs e)
    {
        grayWindows.Visible = false;
    }

    protected void saveChanges_inPage()
    {
       gameID = Session["gameIDSession"].ToString();
       questId = Session["editQuestNum"].ToString();

        XmlNode gameQuestNum = FakexmlDoc.SelectSingleNode("/tree/game");
        gameQuestNum.SelectSingleNode("//question[@numberQues = " + questId + "]/questionText").InnerText = Server.UrlEncode(questionText.Text);
        XmlNodeList myQuestNum = FakexmlDoc.SelectNodes("/tree/game/questions/question");
        XmlNode answersList = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/Answers");
        for (int i = 0; i < answersList.ChildNodes.Count; i++)
        {
            if (!answersList.ChildNodes[i].InnerText.Contains("uploadedFiles"))
            {
                if (((TextBox)FindControl("answer" + (i + 1) + "TextBox")).Text.Length >= 1)
                {
                    answersList.ChildNodes[i].InnerText = Server.UrlEncode(((TextBox)FindControl("answer" + (i + 1) + "TextBox")).Text);
                }
                else
                {
                    answersList.RemoveChild(answersList.ChildNodes[i]);
                }
            }

        }

        if (questId == "0")
        {
            foreach (XmlElement question in myQuestNum)
            {
                int questNum = Convert.ToInt16(question.Attributes["numberQues"].Value);
                question.Attributes["numberQues"].Value = (questNum + 1).ToString();
            }

            int numberOfQuest = Convert.ToInt16(gameQuestNum.Attributes["totalQes"].Value);
            gameQuestNum.Attributes["totalQes"].Value = (numberOfQuest + 1).ToString();
            questId = "1";
            Session["editQuestNum"] = questId;
        }

        XmlDataSourceFakeGame.Save();
    }
  
    protected void saveButton_Click(object sender, EventArgs e)
    {
        saveChanges_inPage();

        gameID = Session["gameIDSession"].ToString();
        
        Session["CorrectAnswer"] = 0;

        XmlNode questionForEdit = RealxmlDoc.SelectSingleNode("/tree/game[@code=" + gameID + "]/questions");
        XmlNode FakequestionForEdit = FakexmlDoc.SelectSingleNode("/tree/game/questions");
        questionForEdit.InnerXml = FakequestionForEdit.InnerXml;
        XmlNode gameQuestNum = FakexmlDoc.SelectSingleNode("/tree/game");
        questionForEdit.ParentNode.Attributes["totalQes"].Value = gameQuestNum.Attributes["totalQes"].Value;
        XmlDataSourceFakeGame.Save();
        XmlDataSourceRealGame.Save();
        Panel1.DataBind();
        innerGridView.DataBind();
        Response.Redirect("EditGame.aspx");
    }

    protected void questImageDelete(object sender, ImageClickEventArgs e)
    {
        string questId = Session["editQuestNum"].ToString();
        XmlNode ImageToDelete = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/pic");
        ImageToDelete.ParentNode.RemoveChild(ImageToDelete);
        ImageButtonquestImgtooltip.Style.Add("margin-top", "4%");
        ImageButtonquestImgtooltip.Text = "הוסף תמונה מסוג JPEG '\' PNG";
        ImageButton deleteImg0 = (ImageButton)FindControl("deleteImg0");
        ((ImageButton)FindControl("ImageButtonquestImg")).CssClass = "questImageClass";
        ((ImageButton)FindControl("ImageButtonquestImg")).ImageUrl = "~/Images/image_icon.png";
        ((Label)FindControl("QuestCuontLbl")).CssClass = "IfImageFalse";
        questionText.CssClass = "questImageFalse";
        if (questionText.Text.Length != 0)
        {
            FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/questionText").InnerText = Server.UrlEncode(questionText.Text);
        }
        XmlDataSourceFakeGame.Save();
        Panel1.Controls.Remove(deleteImg0);
        Panel1.DataBind();
        Response.Redirect("EditGame.aspx");
    }

    protected void questionImageButton_Click(object sender, ImageClickEventArgs e)
    {
        string questId = Session["editQuestNum"].ToString();
        //FakexmlDoc = XmlDataSourceFakeGame.GetXmlDocument();
        XmlNode question = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]");
        string imagesLibPath = "uploadedFiles/";

        string fileType = ((FileUpload)FindControl("FileUpload0")).PostedFile.ContentType;

        if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
        {
            // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
            string fileName = ((FileUpload)FindControl("FileUpload0")).PostedFile.FileName;
            // הסיומת של הקובץ
            string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
            //לקיחת הזמן האמיתי למניעת כפילות בתמונות
            string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");
            // חיבור השם החדש עם הסיומת של הקובץ
            string imageNewName = myTime + "0" + endOfFileName;
            // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(((FileUpload)FindControl("FileUpload0")).PostedFile.InputStream);
            //קריאה לפונקציה המקטינה את התמונה
            //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
            System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);
            //שמירה של הקובץ לספרייה בשם החדש שלו
            objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);
            //הצגה של הקובץ החדש מהספרייה
            ((ImageButton)FindControl("ImageButtonquestImg")).ImageUrl = imagesLibPath + imageNewName;

            XmlElement pic = FakexmlDoc.CreateElement("pic");

            pic.InnerText = Server.UrlEncode(imagesLibPath + imageNewName);

            ((ImageButton)FindControl("ImageButtonquestImg")).CssClass = "questImageClassTrue";
            ((TextBox)FindControl("questionText")).CssClass = "questImageTrue";
            ((Label)FindControl("QuestCuontLbl")).CssClass = "IfImageTrue";
            ImageButton deleteImg0 = new ImageButton();
            deleteImg0.Style.Add("visibility", "visible");
            deleteImg0.ID = "deleteImg0";
            deleteImg0.ImageUrl = "images/Exit.png";
            deleteImg0.Click += questImageDelete;
            FakexmlDoc.SelectSingleNode("//question[@numberQues = " + questId + "]").InsertBefore(pic, question.ChildNodes[1]);

            ImageButtonquestImgtooltip.Style.Add("margin-top", "1.5%");
            ImageButtonquestImgtooltip.Text = "ערוך תמונה";

            if (questionText.Text.Length!=0) { 
            FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/questionText").InnerText = Server.UrlEncode(questionText.Text);
             }
            XmlDataSourceFakeGame.Save();
            Panel1.Controls.Add(deleteImg0);
            Panel1.DataBind();
            Response.Redirect("EditGame.aspx");
        }
        else
        {
            //הצגה של המסך האפור
            grayWindows.Visible = true;
            //הצגת הפופ-אפ של המחיקה
            DeleteConfPopUp.Visible = true;

            popUpmessage.Text = "הקובץ שבחרת איננו מסוג PNG  /  JPEG";
            OkBtn0.Text = "בחר קובץ אחר";
            OkBtn0.OnClientClick += "openFileUploader(0); return false;";
            myExitBtn.Text = "המשך עריכה";
        }
    }

    protected void answerImageButton_Click(object sender, ImageClickEventArgs e)
     {
        string questId = Session["editQuestNum"].ToString();
        ImageButton btn = (ImageButton)sender;
        string AnsId = btn.ID;
        int ansNum = Convert.ToInt16(AnsId.Substring(AnsId.Length - 1));
        Session["answer"] = ansNum;
        
        //FakexmlDoc = XmlDataSourceFakeGame.GetXmlDocument();

        XmlElement Answer = FakexmlDoc.CreateElement("Answer");
        Answer.SetAttribute("pic", "no");
        Answer.SetAttribute("result", "wrong");
        XmlNode answers = FakexmlDoc.SelectSingleNode("/tree/game/questions/question[@numberQues = " + questId + "]/Answers");
        answers.AppendChild(Answer);

        ImageButton delete = new ImageButton();
        delete.ID = "answerDelete" + ansNum;
        delete.CssClass = "answer" + ansNum + "deleteClass";
        delete.ImageUrl = "images/Exit.png";
       delete.Click += Delete_Answer;

        RadioButtonList answersList=(RadioButtonList)FindControl("answersList");
        ListItem check = new ListItem();
        check.Text = "";
        check.Value = ansNum.ToString();
        answersList.Items.Add(check);

        ((Label)FindControl("answer" + ansNum + "tooltip")).Style.Add("visibility", "hidden");
        ((ImageButton)FindControl("ImageButton" + ansNum)).Enabled = false;
        ((Label)FindControl("Label" + ansNum)).Enabled = false;
        ((ImageButton)FindControl("ImageButton" + ansNum)).Style.Add("visibility", "hidden");
        ((Label)FindControl("Label" + ansNum)).Style.Add("visibility", "hidden");

        if (ansNum != 6)
        {
            ((ImageButton)FindControl("ImageButton" + (ansNum + 1))).Style.Add("visibility", "visible");
            ((ImageButton)FindControl("answerImage" + (ansNum + 1))).Style.Add("visibility", "visible");
            ((Label)FindControl("Label" + (ansNum + 1))).Style.Add("visibility", "visible");
            ((Label)FindControl("answer" + (ansNum + 1) + "tooltip")).Style.Add("visibility", "visible");
            ((Label)FindControl("answer" + (ansNum + 1) + "tooltip")).Text = "ניתן להוסיף תשובה רק לאחר מילוי תוכן התשובות הקיימות";
        }

        if (AnsId.Contains("Button")) {
        TextBox ansewrText = new TextBox();
        ansewrText.TextMode = TextBoxMode.MultiLine;
        ansewrText.Attributes["placeholder"] = "הזן תשובה";
        ansewrText.Attributes["enableThis"] = (ansNum+1).ToString();
        ansewrText.ID = "answer" + ansNum + "Textbox";
        ansewrText.CssClass = "ansText" + ansNum + "Class";
        ansewrText.Attributes["CharacterLimit"] = "50";
        ansewrText.Attributes["CharacterForLabel"] = "AnslableCount" + ansNum;
        ansewrText.Attributes["ifEmptyDisable"] = "ImageButton" + (ansNum + 1);
        Label lableCount = new Label();
        lableCount.ID = "AnslableCount" + ansNum;
        lableCount.CssClass = "ansText" + ansNum + "Class";
        lableCount.Text = ansewrText.Text.Length.ToString()+ "/50";
        lableCount.Style.Add("visibility", "hidden");
        Panel1.Controls.Add(ansewrText);
        Panel1.Controls.Add(lableCount);
        }
        else
        {
         Answer.Attributes["pic"].Value = "yes";
            //נתיב לשמירה התמונות
          string imagesLibPath = "uploadedFiles/";

                string fileType = ((FileUpload)FindControl("FileUpload" + ansNum)).PostedFile.ContentType;

                if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                {
                    // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                    string fileName = ((FileUpload)FindControl("FileUpload" + ansNum)).PostedFile.FileName;
                    // הסיומת של הקובץ
                    string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                    string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");
                    // חיבור השם החדש עם הסיומת של הקובץ
                    string imageNewName = myTime + ansNum + endOfFileName;
                // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(((FileUpload)FindControl("FileUpload" + ansNum)).PostedFile.InputStream);
                //קריאה לפונקציה המקטינה את התמונה
                //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);
                //שמירה של הקובץ לספרייה בשם החדש שלו
                objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);
                //הצגה של הקובץ החדש מהספרייה
                ((ImageButton)FindControl("answerImage" + ansNum)).ImageUrl = imagesLibPath + imageNewName;
                Answer.InnerText = Server.UrlEncode(imagesLibPath + imageNewName);

                if (ansNum != 6)
                {
                    ((Label)FindControl("answer" + (ansNum + 1) + "tooltip")).Style.Add("visibility", "hidden");
                    ((ImageButton)FindControl("ImageButton" + (ansNum + 1))).Enabled = true;
                    ((ImageButton)FindControl("answerImage" + (ansNum + 1))).Enabled = true;
                    ((Label)FindControl("Label" + (ansNum + 1))).Enabled = true;
                    ((Label)FindControl("Label" + (ansNum + 1))).CssClass = "answerLabelClass";
                }

                }
                else
                {
                //הצגה של המסך האפור
                grayWindows.Visible = true;
                //הצגת הפופ-אפ של המחיקה
                DeleteConfPopUp.Visible = true;

                popUpmessage.Text = "הקובץ שבחרת איננו מסוג PNG  /  JPEG";
                OkBtn0.Text = "בחר קובץ אחר";
                OkBtn0.OnClientClick += "openFileUploader("+ansNum+"); return false;";
                myExitBtn.Text = "המשך עריכה";
            }
            ImageButton NewImageAns = (ImageButton)FindControl("answerImage" + ansNum);
            NewImageAns.CssClass = "answerImageClassTrue";
        }
        saveButton.Enabled = true;
        saveButtontooltip.Style.Add("visibility", "hidden");
        XmlDataSourceFakeGame.Save();
        Panel1.Controls.Add(delete);
        Panel1.DataBind();
    }
    
    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }
  
    protected void AddNewQuest(object sender, ImageClickEventArgs e)
    {
        saveChanges_inPage();

        XmlNode fakeQuestNode = FakexmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions/question[@numberQues = " + questId + "]");
        XmlNode realQuestNode = RealxmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions/question[@numberQues = " + questId + "]");
        
        if (fakeQuestNode.InnerText== realQuestNode.InnerText)
        {
            newQuest();
        }

        else
        {

            Session["commend"] = "newQuest";
            //הצגה של המסך האפור
            grayWindows.Visible = true;
            //הצגת הפופ-אפ של המחיקה
            DeleteConfPopUp.Visible = true;

            popUpmessage.Text = "אתה עומד לעזוב את הדף לפני ששמרת את השינויים" + "</br>" + "האם ברצונך להמשיך?";
            OkBtn0.Text = "כן, המשך";
            myExitBtn.Text = "לא, השאר";
        }
    }

    protected void goBackButtonClick(object sender, EventArgs e)
    {
        saveChanges_inPage();

        XmlNode fakeQuestNode = FakexmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions/question[@numberQues = " + questId + "]");
        XmlNode realQuestNode = RealxmlDoc.SelectSingleNode("/tree/game[@code = " + gameID + "]/questions/question[@numberQues = " + questId + "]");

        if (fakeQuestNode.InnerText == realQuestNode.InnerText)
        {
            Session["gameIdSession"] = "";
            Response.Redirect("Editor.aspx");
        }
        
        else
        {
            Session["commend"] = "goBack";
            //הצגה של המסך האפור
            grayWindows.Visible = true;
            //הצגת הפופ-אפ של המחיקה
            DeleteConfPopUp.Visible = true;

            popUpmessage.Text = "אתה עומד לעזוב את הדף לפני ששמרת את השינויים" + "</br>" + "האם ברצונך להמשיך?";
            OkBtn0.Text = "כן, המשך";
            myExitBtn.Text = "לא, השאר";
        }
    }
    
    protected void RowDataBound_innerGrid(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow row in innerGridView.Rows)
        {
            Label questName = (Label)row.FindControl("questNameLbl");
            questName.Text = Server.UrlDecode(questName.Text);

            if (((Label)row.FindControl("questIDLbl")).Text=="0")
            {
                ((ImageButton)row.FindControl("DeleteQuestbtn")).Enabled = false;
                ((Label)row.FindControl("DeleteQuestbtntooltip")).Visible = false;
                ((ImageButton)row.FindControl("EditquestBtn")).Enabled = false;
                ((Label)row.FindControl("EditquestBtntooltip")).Visible = false;
            }
        }

    }

    }
