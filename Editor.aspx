<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Editor.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link rel="icon" href="images/starIcon.png" />
    <title></title>
    <style type="text/css">
        .PopUp {
        }
    </style>
        <!-- הפניה לקובץ הCSS -->
    <link href="styles/StyleSheet.css" rel="stylesheet" />
      <%--הפניה לקובץ jquary--%>
    <script src="scripts/jquery-1.12.0.min.js"></script>
    <%--הפניה לקובץ JS שלנו--%>
     <script src="scripts/JavaScript.js"></script>

</head>
<body dir="rtl">
    <form id="form1" runat="server">
        <img  id="headLine" src="images/headline.png" />
        <asp:Label ID="gameStoke" runat="server" >מאגר המשחקים של</asp:Label>
        <a id="toTheGame" href="indexStarParty.html">למשחק</a>
        <div id="defaultConteiner">
<div id="addNewGame">
           <asp:Label ID="creatNewGameLbl"  runat="server" Text="הוספת משחק חדש"></asp:Label>
           <asp:TextBox ID="gameNameTextBox" placeholder="שם משחק" CharacterForLabel2="nameLblempty" CharacterForLabel="newNmaeLbl" CssClass="CharacterCount" CharacterLimit="20" runat="server"></asp:TextBox>
           <asp:Label ID="newNmaeLbl" runat="server" Text="0/20"></asp:Label>   
            <asp:Label ID="nameLblempty" Font-Bold="true" runat="server"></asp:Label>
&nbsp; <a class="tooltip" href="#"> <asp:ImageButton ID="plusSign" runat="server" ImageUrl="~/images/add.png" OnClick="ImageButton1_Click" /><asp:Label ID="plusSigntooltip" runat="server" cssClass="tooltip" Text="יצירת משחק חדש"></asp:Label></a>

</div>
            <br />
            <br />
            <div id ="gridViewDiv">
            <asp:GridView ID="GridView1" cssClass="outerGrid" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" CellPadding="3" CellSpacing="2" GridLines="None" OnRowCommand="GridView1_RowCommand" OnRowDataBound="RowDataBound1" >
                <Columns>
                    <asp:TemplateField HeaderText="קוד">
                        <ItemTemplate>
                             <a class="tooltip" href="#"><asp:Label ID="gameIDLbl" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>'></asp:Label><asp:Label ID="gameIDLbltooltip" runat="server" cssClass="tooltip" Text="קוד הכניסה למשחק"></asp:Label></a> 
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="שם משחק">
                        <ItemTemplate>
                             <a class="tooltip" href="#"><asp:ImageButton ID="editNameTB" runat="server" ImageUrl="~/images/edit.png" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>'  AutoPostBack="true" CommandName="editName" /><asp:Label ID="editNameTBtooltip" runat="server" cssClass="tooltip" Text="עריכת שם המשחק"></asp:Label></a>                       
                            <asp:TextBox ID="gameNameEditTextBox" CssClass="CharacterCount" myToolTip="EditgameBtntooltip" CharacterForLabel="nameEditLbl" CharacterLimit="20" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "gameName").ToString()%>' theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>'  Enabled="false"  ></asp:TextBox>
                            <asp:Label ID="nameEditLbl" visible="false" runat="server" Text="0/20" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>'  ></asp:Label>   
                            </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="שאלות">
                      <ItemTemplate>
                            <a class="tooltip" href="#"> <asp:ImageButton ID="EditgameBtn" cssClass="editQbutton" runat="server" ImageUrl="~/images/edit.png" theItemTotalQuest='<%#XPathBinder.Eval(Container.DataItem, "@totalQes").ToString()%>' theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>' CommandName="editRow"  /><asp:Label ID="EditgameBtntooltip" runat="server" cssClass="tooltip" Text="עריכת שאלות משחק"></asp:Label></a>
                            <asp:Label ID="totalQesIDLbl" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@totalQes").ToString()%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="זמן לכל שאלה">
                        <ItemTemplate>
                             <asp:Button ID="hiddenForTime30" CssClass="FileUpload"  runat="server" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>'  CommandName="editTime"   />   
                              <asp:Button ID="hiddenForTime60" CssClass="FileUpload" runat="server" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>' CommandName="editTime"  />   
                                 <asp:Button ID="hiddenForTime90" CssClass="FileUpload" runat="server" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>' CommandName="editTime"  />   
                                 <asp:Button ID="hiddenForTimeno" CssClass="FileUpload" runat="server" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>' CommandName="editTime"  />   
                            <a class="tooltip" href="#"><div class="custom-select" style="width:90px;"><asp:DropDownList ID="timePerQuest" cssClass="select" runat="server" width="80" AutoPostBack="true" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>' theItemValue='<%#XPathBinder.Eval(Container.DataItem, "@timePerQes").ToString()%>'/></div><asp:Label ID="timePerQuesttooltip" runat="server" cssClass="tooltip" Text="מספר שניות לשאלה"></asp:Label></a>
                            </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="מחיקה">
                        <ItemTemplate>
                           <a class="tooltip" href="#"><asp:ImageButton ID="DeleteGame" runat="server" ImageUrl="~/images/delete.png" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@code").ToString()%>' CommandName="deleteRow"  /><asp:Label ID="deleteGametooltip" runat="server" cssClass="tooltip" Text="מחיקת משחק מהמאגר"></asp:Label></a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="פורסם">
                        <ItemTemplate>
                            <a class="tooltip" id="checkB" href="#"><asp:CheckBox ID="IspassCB" cssClass="checkBoxClass" Enabled="false" runat="server" Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem,"@published"))%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@code")%>'  AutoPostBack="true"  OnCheckedChanged="IspassCB_CheckedChanged" /><asp:Label ID="IspassCBtooltip" runat="server" cssClass="tooltip" Text="פרסם משחק"></asp:Label></a>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle Font-Bold="True" ForeColor="#E7E7FF" />
                <PagerStyle ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle  ForeColor="Black" />
                <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#33276A" />
            </asp:GridView>
            <br />
                </div>
            <asp:Panel ID="grayWindows" runat="server"  Visible="false">
                <!-- פופ-אפ למחיקה - כאן אפשר להוסיף את הפקדים הרלוונטים -->
                <asp:Panel ID="DeleteConfPopUp" CssClass="PopUp" runat="server">
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <!-- תווית להצגת הודעה למשתמש -->
                    <asp:Label ID="popUpmessage" runat="server">האם אתה בטוח שברצונך למחוק את המשחק?</asp:Label>
                    <asp:Button ID="OkBtn0" runat="server" Text="כן, מחק" OnClick="OkBtn_Click"  />
                    <asp:Button ID="myExitBtn" runat="server" OnClick="ExitImageButton_Click" Text="לא, חזור" />

       </asp:Panel>
                 </asp:Panel>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/tree/games.xml" XPath="/tree/game"></asp:XmlDataSource>
            <br />
        </div>

        <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/tree/fake.xml" XPath="/tree"></asp:XmlDataSource> 
        </form>
</body>
</html>
