<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditorLogin.aspx.cs" Inherits="_Login" %>

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
        <div id="loginPage" >
            <div>שם משתמש:</div>
            <asp:TextBox ID="editorName" runat="server" placeholder="הזן שם משתמש" MaxLength="10"></asp:TextBox>
            <div>סיסמה:</div>
            <asp:TextBox ID="editorPassword" runat="server" placeholder="הזן סיסמה" TextMode="Password" MaxLength="10"></asp:TextBox>
            <br />
            <asp:Label ID="tryAgain" runat="server"></asp:Label>
               <a class="tooltip" href="#"><asp:Button ID="loginButton" runat="server" OnClick="Login_click" Text="כניסה" /><asp:Label ID="loginButtontooltip" runat="server" cssClass="tooltip" Text="יש להזין שם משתמש וסיסמה"></asp:Label></a>
        </div>
        </form>
</body>
</html>
