<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditGame.aspx.cs" Inherits="Edit" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link rel="icon" href="images/starIcon.png" />
    <title></title>
    <%-- css --%>
    <link href="styles/StyleSheet.css" rel="stylesheet" />
    <%--הפניה לקובץ jquary--%>
    <script src="scripts/jquery-1.12.0.min.js"></script>
    <%--הפניה לקובץ JS שלנו--%>
    <script src="scripts/JavaScript.js"></script>

    </head>
<body>
    <form id="form1" runat="server">
              <asp:Panel ID="Panel1" runat="server" Height="490px" meta:resourcekey="Panel1Resource1" Width="1264px">
                  <a id="backTo" href="#">רשימת המשחקים ></a>
                  &nbsp;<asp:Label ID="nameLbl" runat="server" Text="עמוד עריכת משחק: " style="font-weight: 500" meta:resourcekey="nameLblResource1"></asp:Label>
                  <asp:Label ID="gameName" runat="server"></asp:Label>
                  &nbsp;<asp:Panel ID="Panel3" runat="server" Height="500px" Width="800px" meta:resourcekey="Panel3Resource1">
       
                  <asp:XmlDataSource ID="XmlDataSourceRealGame" runat="server" DataFile="~/tree/games.xml" XPath=""></asp:XmlDataSource>
                   <asp:XmlDataSource ID="XmlDataSourceFakeGame" runat="server" DataFile="~/tree/fake.xml" XPath=""></asp:XmlDataSource>
                      <div id="qeustion" style="height: 233px; width: 350px; top: 77px; right: 216px">
                          <asp:Label ID="LabelQuest" runat="server" Text="תוכן השאלה:" CssClass="answers"></asp:Label>
                          <asp:TextBox ID="questionText" enableThis="1" placeholder="הזן שאלה" ifEmptyDisable="questImg" CssClass="questImageFalse"  CharacterForLabel="QuestCuontLbl" CharacterLimit="100" runat="server" TextMode="MultiLine" meta:resourcekey="questinText1Resource1" ></asp:TextBox>
                           <asp:Label ID="QuestCuontLbl" CssClass="IfImageFalse" runat="server" Text="0/100"></asp:Label>   
                        <a class="tooltip" href="#"> <asp:ImageButton ID="ImageButtonquestImg" CssClass="questImageClass" runat="server" ImageUrl="~/Images/image_icon.png" OnClientClick="openFileUploader(0); return false;" /><asp:Label ID="ImageButtonquestImgtooltip" runat="server" cssClass="tooltip" Text="הוסף תמונה מסוג JPG\PNG"></asp:Label></a>

                          &nbsp;</div>

                      <div id="imgDiv">
                        <asp:FileUpload ID="FileUpload0" runat="server" CssClass="FileUpload" Width="104px" />
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FileUpload" Width="104px" />
                        <asp:FileUpload ID="FileUpload2" runat="server" CssClass="FileUpload" Width="104px" />
                        <asp:FileUpload ID="FileUpload3" runat="server" CssClass="FileUpload" Width="104px" />
                        <asp:FileUpload ID="FileUpload4" runat="server" CssClass="FileUpload" Width="104px" />
                        <asp:FileUpload ID="FileUpload5" runat="server" CssClass="FileUpload" Width="104px" />
                        <asp:FileUpload ID="FileUpload6" runat="server" CssClass="FileUpload" Width="104px" />
                    
                      </div>

                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <a class="tooltip" href="#"><div id="answer5" class="answers">
                          <asp:Label ID="Label5" runat="server" Text="סוג תשובה:" meta:resourcekey="Label5Resource1" CssClass="answerLabelClass"></asp:Label>
                          <br />
                          <asp:ImageButton ID="ImageButton5" runat="server" Height="16px" ImageUrl="~/images/Text_icon.png" meta:resourcekey="ImageButton5Resource1" CssClass="textTypeImage" OnClick="answerImageButton_Click" />
                         <asp:ImageButton ID="answerImage5" runat="server" CssClass="answerImageClass" ImageUrl="~/Images/image_icon.png" OnClientClick="openFileUploader(5); return false;" tooltip="הוסף תמונה" />
                    </div><asp:Label ID="answer5tooltip" runat="server" cssClass="tooltip" Text="ניתן להוסיף תשובה רק לאחר מילוי התשובות הקיימות" ></asp:Label></a>


                       <a class="tooltip" href="#"><div id="answer3" class="answers">
                          <asp:Label ID="Label3" runat="server" Text="סוג תשובה:" meta:resourcekey="Label3Resource1" CssClass="answerLabelClass"></asp:Label>
                          <br />
                          <asp:ImageButton ID="ImageButton3" runat="server" Height="16px" ImageUrl="~/images/Text_icon.png" meta:resourcekey="ImageButton3Resource1" CssClass="textTypeImage" OnClick="answerImageButton_Click"/>
                          <asp:ImageButton ID="answerImage3" runat="server" CssClass="answerImageClass" ImageUrl="~/Images/image_icon.png" OnClientClick="openFileUploader(3); return false;" tooltip="הוסף תמונה"  />
                       </div><asp:Label ID="answer3tooltip" runat="server" cssClass="tooltip" Text="ניתן להוסיף תשובה רק לאחר מילוי התשובות הקיימות" ></asp:Label></a>

                    
                      <a class="tooltip" href="#"> <div id="answer1" class="answers">
                          <asp:Label ID="Label1" runat="server" Text="סוג תשובה:" meta:resourcekey="Label1Resource1" CssClass="answerLabelClass"></asp:Label>
                          <br />
                          <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Text_icon.png"  meta:resourcekey="ImageButton1Resource1" CssClass="textTypeImage" OnClick="answerImageButton_Click" />
                          <asp:ImageButton ID="answerImage1" runat="server" CssClass="answerImageClass" ImageUrl="~/Images/image_icon.png" OnClientClick="openFileUploader(1); return false;" tooltip="הוסף תמונה"/> 
                </div><asp:Label ID="answer1tooltip" runat="server" cssClass="tooltip" Text="ניתן להוסיף תשובה רק לאחר הוספת תוכן לגוף השאלה"></asp:Label></a>

                      <a class="tooltip" href="#"> <div id="answer2" class="answers">
                          <asp:Label ID="Label2" runat="server" Text="סוג תשובה:" meta:resourcekey="Label2Resource1" CssClass="answerLabelClass"></asp:Label>
                          <br />
                          <asp:ImageButton ID="ImageButton2" runat="server" Height="15px" ImageUrl="~/images/Text_icon.png" meta:resourcekey="ImageButton2Resource1" CssClass="textTypeImage" OnClick="answerImageButton_Click"  />
                         <asp:ImageButton ID="answerImage2" runat="server" CssClass="answerImageClass" ImageUrl="~/Images/image_icon.png" OnClientClick="openFileUploader(2); return false;" tooltip="הוסף תמונה"  />
                      </div><asp:Label ID="answer2tooltip" runat="server" cssClass="tooltip" Text="ניתן להוסיף תשובה רק לאחר מילוי התשובות הקיימות"></asp:Label></a>
                   
                      <a class="tooltip" href="#"><div id="answer4" class="answers">
                          <asp:Label ID="Label4" runat="server" Text="סוג תשובה:" meta:resourcekey="Label4Resource1" CssClass="answerLabelClass"></asp:Label>
                          <br />
                          <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/Text_icon.png" meta:resourcekey="ImageButton4Resource1" CssClass="textTypeImage" OnClick="answerImageButton_Click" />
                           <asp:ImageButton ID="answerImage4" runat="server" CssClass="answerImageClass" ImageUrl="~/Images/image_icon.png" OnClientClick="openFileUploader(4); return false;" tooltip="הוסף תמונה" />

                      </div><asp:Label ID="answer4tooltip" runat="server" cssClass="tooltip" Text="ניתן להוסיף תשובה רק לאחר מילוי התשובות הקיימות"></asp:Label></a>

                       <a class="tooltip" href="#"><div id="answer6" class="answers" >
                          <asp:Label ID="Label6" runat="server" Text="סוג תשובה:" meta:resourcekey="Label6Resource1" CssClass="answerLabelClass"></asp:Label>
                          <asp:ImageButton ID="ImageButton6" runat="server" Height="15px" ImageUrl="~/images/Text_icon.png" meta:resourcekey="ImageButton6Resource1" CssClass="textTypeImage" OnClick="answerImageButton_Click" />                     
                  <asp:ImageButton ID="answerImage6" runat="server" CssClass="answerImageClass" ImageUrl="~/Images/image_icon.png" OnClientClick="openFileUploader(6); return false;" tooltip="הוסף תמונה" />

                      </div><asp:Label ID="answer6tooltip" runat="server" cssClass="tooltip" Text="ניתן להוסיף תשובה רק לאחר מילוי התשובות הקיימות" ></asp:Label></a>
                  </asp:Panel>
                                                          <div id="condition">
                          <span>תנאים לפרסום משחק:</span>
                          <span id="min12" runat="server" >מינימום 12 שאלות</span>
                          <span id="couple" runat="server">מספר שאלות זוגי</span>
                          <asp:Button ID="goBackButton" OnClick="goBackButtonClick" AutoPostBack="false"  text="לרשימת המשחקים" runat="server"/>

                                            </div>
     
                  <a class="tooltip" id="saveBa" href="#" ><asp:Button ID="saveButton" runat="server" meta:resourcekey="saveButtonResource1" text="שמור שינויים" OnClick="saveButton_Click"/><asp:Label ID="saveButtontooltip" runat="server" cssClass="tooltip"></asp:Label></a>
                  <a class="tooltip" id="addNewQuestionS" href="#" ><asp:ImageButton ID="addNewQuestion" runat="server" OnClick="AddNewQuest" AutoPostBack="false" ImageUrl="~/images/add.png"/><asp:Label ID="addNewQuestiontooltip" runat="server" cssClass="tooltip" Text="יצירת שאלה חדשה"></asp:Label></a>
                  <asp:Label ID="addNewQuestionText" runat="server" OnClick="AddNewQuest" AutoPostBack="false" >שאלה חדשה</asp:Label>
             
                  <div id="innerGridDiv">
                  <asp:GridView ID="innerGridView" runat="server" AutoGenerateColumns="False"  CellPadding="3" CellSpacing="2" DataSourceID="XmlDataSourceRealGame" GridLines="None" OnRowDataBound="RowDataBound_innerGrid" OnRowCommand="GridView2_RowCommand" >
                      <Columns>
                          <asp:TemplateField HeaderText="מספר">
                              <ItemTemplate>
                                  <asp:Label ID="questIDLbl" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@numberQues").ToString()%>' Width="10%"></asp:Label>
                              </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="שאלה">
                              <ItemTemplate>
                                  <asp:Label ID="questNameLbl" CssClass="questNameLblclass" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "questionText").ToString()%>'  Visible="True" ToolTip='<%#XPathBinder.Eval(Container.DataItem, "questionText").ToString()%>' Font-Overline="False" Font-Strikeout="False" Width="250px"></asp:Label>
                              </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="עריכה">
                              <ItemTemplate>
                                    <a class="tooltip" href="#" ><asp:ImageButton ID="EditquestBtn" runat="server" CommandName="editRow" ImageUrl="~/images/edit.png" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@numberQues").ToString()%>'/><asp:Label ID="EditquestBtntooltip" runat="server" cssClass="tooltip" Text="עריכה"></asp:Label></a>
                              </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="מחיקה">
                              <ItemTemplate>
                                        <a class="tooltip" href="#" ><asp:ImageButton ID="DeleteQuestbtn" runat="server" CommandName="deleteRow" ImageUrl="~/images/delete.png" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@numberQues").ToString()%>' /><asp:Label ID="DeleteQuestbtntooltip" runat="server" cssClass="tooltip" Text="מחיקה"></asp:Label></a>
                              </ItemTemplate>
                          </asp:TemplateField>
                      </Columns>
                      <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                      <HeaderStyle Font-Bold="True" ForeColor="#E7E7FF" />
                      <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                      <RowStyle BackColor="#DEDFDE" ForeColor="Black"  />
                      <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                      <SortedAscendingHeaderStyle BackColor="#594B9C" />
                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                      <SortedDescendingHeaderStyle BackColor="#33276A" />
                  </asp:GridView>
                             </div>
                              <asp:Panel ID="grayWindows" runat="server"  Visible="false">
                <!-- פופ-אפ למחיקה - כאן אפשר להוסיף את הפקדים הרלוונטים -->
                <asp:Panel ID="DeleteConfPopUp" CssClass="PopUp" runat="server">
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <!-- תווית להצגת הודעה למשתמש -->
                    <asp:Label ID="popUpmessage" runat="server">נסיון</asp:Label>
                    <asp:Button ID="OkBtn0" runat="server" Text="כן, מחק" OnClick="OkBtn_Click"  />
                    <asp:Button ID="myExitBtn" runat="server" OnClick="ExitImageButton_Click" Text="לא, חזור" />
       </asp:Panel>
                 </asp:Panel>
                  <asp:ImageButton ID="ansbutImage0" runat="server" Text="Button" CssClass="FileUpload" OnClick="questionImageButton_Click" />
                  <asp:ImageButton ID="ansbutImage1" runat="server" Text="Button" CssClass="FileUpload" OnClick="answerImageButton_Click" />
                  <asp:ImageButton ID="ansbutImage2" runat="server" Text="Button" CssClass="FileUpload" OnClick="answerImageButton_Click" />
                 <asp:ImageButton ID="ansbutImage3" runat="server" Text="Button" CssClass="FileUpload" OnClick="answerImageButton_Click" />
                  <asp:ImageButton ID="ansbutImage4" runat="server" Text="Button" CssClass="FileUpload" OnClick="answerImageButton_Click" />
                 <asp:ImageButton ID="ansbutImage5" runat="server" Text="Button" CssClass="FileUpload" OnClick="answerImageButton_Click" />
                 <asp:ImageButton ID="ansbutImage6" runat="server" Text="Button" CssClass="FileUpload" OnClick="answerImageButton_Click" />
              </asp:Panel>
    </form>
    </body>
</html>
